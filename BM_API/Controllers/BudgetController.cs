using AutoMapper;
using BM_API.DTOs.Budget;
using BM_API.DTOs.EmployeeUpdateDto;
using BM_API.DTOs.ToDo;
using BM_API.Models;
using BM_API.Repositories;
using BM_API.Repositories.RepositoryInterfaces;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BM_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IBudgetTypeRepository _budgetTypeRepository;
        private readonly ICompanyEmployeeRepository _companyEmployeeRepository;
        private readonly IMapper _mapper;
        public BudgetController(IBudgetRepository budgetRepository, IPaymentTypeRepository paymentTypeRepository, IBudgetTypeRepository budgetTypeRepository, ICompanyEmployeeRepository companyEmployeeRepository, IMapper mapper)
        {
            _budgetRepository = budgetRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _budgetTypeRepository = budgetTypeRepository;
            _companyEmployeeRepository = companyEmployeeRepository;
            _mapper = mapper;
        }
        [HttpGet("get-budget/{id}")]
        public async Task<IActionResult> GetBudgetById(Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty))
                {
                    return BadRequest("Id is null");
                }
                Budget budget = await _budgetRepository.GetBudgetByIdAsync(id);
                if (budget == null)
                {
                    return NotFound("Budget not found");
                }
                return Ok(budget);
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }
        [HttpPost("add-budget")]
        public async Task<IActionResult> AddBudget([FromBody] Budget budget)
        {
            try
            {
                if (budget == null)
                {
                    return BadRequest();
                }
                Budget budgetAdd = new Budget
                {
                    Id = Guid.NewGuid(),
                    Name = budget.Name,
                    Date = budget.Date,
                    CreatedDate = DateTime.Now,
                    CompanyId = budget.CompanyId,
                };
                BudgetType budgetType = await _budgetTypeRepository.GetBudgetTypeByNameAsync(budget.BudgetTypeName);
                if (budgetType == null)
                {
                    return BadRequest();
                }
                budgetAdd.BudgetTypeName = budgetType.Name;
                if (budgetAdd.BudgetTypeName == "Salaries")
                {
                    budgetAdd.PaymentTypeName = "Expense";
                    budgetAdd.Value = -budget.Value;
                }
                else if (budgetAdd.BudgetTypeName == "Other")
                {
                    PaymentType paymentType = await _paymentTypeRepository.GetPaymentTypeByNameAsync(budget.PaymentTypeName);
                    if (paymentType == null)
                    {
                        return BadRequest();
                    }
                    budgetAdd.PaymentTypeName = paymentType.Name;
                    if (budget.PaymentTypeName == "Expense")
                    {
                        budgetAdd.Value = -budget.Value;
                    }
                    else
                    {
                        budgetAdd.Value = budget.Value;
                    }
                }

                _budgetRepository.Add(budgetAdd);
                if (await _budgetRepository.SaveChangesAsync())
                {
                    return Ok(budgetAdd);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("update-budget/{budgetId}")]
        public async Task<IActionResult> UpdateToDo([FromRoute] Guid budgetId, [FromBody] BudgetDTO budget)
        {
            try
            {
                if (budgetId.Equals(Guid.Empty))
                {
                    return BadRequest("Budget id is null");
                }
                if (budget == null)
                {
                    return BadRequest("Budget is null");
                }
                Budget foundBudget = await _budgetRepository.GetBudgetByIdAsync(budgetId);
                if (foundBudget == null)
                {
                    return NotFound("Budget not found");
                }
                foundBudget.BudgetTypeName = budget.BudgetTypeName;
                foundBudget.Name = budget.Name;
                foundBudget.Date=budget.Date;
                foundBudget.CreatedDate = budget.CreatedDate;
                foundBudget.Value = budget.Value;
                foundBudget.PaymentTypeName = budget.PaymentTypeName;
                _budgetRepository.Update(foundBudget);
                if (await _budgetRepository.SaveChangesAsync())
                {
                    return Ok(foundBudget);
                }
                return BadRequest("Db fail");
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }
        [HttpGet("get-budgets-by-company-id/{id}")]
        public async Task<IActionResult> GetBudgetsByCompanyId([FromRoute] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest();
                }
                ICollection<Budget> budgets = await _budgetRepository.GetBudgetsByCompanyIdAsync(id);
                if (budgets.Count <= 0)
                {
                    return NotFound("Budgets not found in this company");
                }
                return Ok(budgets);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete("delete-budget/{id}")]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty))
                {
                    return BadRequest();
                }
                Budget budget = await _budgetRepository.GetBudgetByIdAsync(id);
                if (budget == null)
                {
                    return NotFound("Budget not found");
                }
                _budgetRepository.Delete(budget);
                if (await _budgetRepository.SaveChangesAsync())
                {
                    return Ok(budget);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("get-upcoming-budgets/{companyId}")]
        public async Task<IActionResult> GetUpcomingBudgets(Guid companyId)
        {
            try
            {
                ICollection<UpcomingBudgetDTO> upcomingBudgets = new List<UpcomingBudgetDTO>();
                ICollection<Budget> budgets = await _budgetRepository.GetUpcomingBudgetsAsync(companyId);
                if(budgets.Count <= 0)
                {
                    return Ok("Upcoming budgets not found");
                }
                foreach (var budget in budgets)
                {
                    UpcomingBudgetDTO upcomingBudget = _mapper.Map<UpcomingBudgetDTO>(budget);
                    upcomingBudgets.Add(upcomingBudget);
                }
                return Ok(upcomingBudgets);
            }
            catch(Exception)
            {
                return BadRequest("Db fail");
            }
        }
        [HttpGet("get-day-budgets/{companyId}")]
        public async Task<IActionResult> GetDayBudgets(Guid companyId)
        {
            try
            {
                ICollection<DayBudgetDTO> dayBudgets = await _budgetRepository.Get30DayBudget(companyId);
                if(dayBudgets.Count <= 0)
                {
                    return NotFound("No budgets for the last 30 days found");
                }
                return Ok(dayBudgets);
                
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }
    }
}
