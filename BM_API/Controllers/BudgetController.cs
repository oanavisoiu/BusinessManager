using BM_API.Models;
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
        public BudgetController(IBudgetRepository budgetRepository, IPaymentTypeRepository paymentTypeRepository)
        {
            _budgetRepository = budgetRepository;
            _paymentTypeRepository = paymentTypeRepository;
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
        [HttpGet("get-budgets-by-company-id/{id}")]
        public async Task<IActionResult> GetBudgetByCompanyId([FromRoute] Guid id)
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
    }
}
