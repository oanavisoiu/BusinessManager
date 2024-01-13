using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BM_API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class BudgetTypeController : Controller
    {
        private readonly IBudgetTypeRepository _budgetTypeRepository;
        public BudgetTypeController(IBudgetTypeRepository budgetTypeRepository)
        {
            _budgetTypeRepository = budgetTypeRepository;
        }
        [HttpGet("get-budget-type-by-name/{name}")]
        public async Task<IActionResult> GetBudgetTypeByName([FromRoute] string name)
        {
            try
            {
                if (name == null)
                {
                    return BadRequest("Name is null");
                }
                BudgetType budgetType = await _budgetTypeRepository.GetBudgetTypeByNameAsync(name);
                if (budgetType == null)
                {
                    return NotFound("Budget type doesn't exist.");
                }
                return Ok(budgetType);
            }
            catch (Exception)
            {
                return BadRequest("Db fail.");
            }
        }
        [HttpPost("add-budget-type")]
        public async Task<IActionResult> AddBudgetType([FromBody] BudgetType budgetType)
        {
            try
            {
                if (budgetType == null)
                {
                    return BadRequest("Budget type is null.");
                }
                BudgetType budgetTypeFound = await _budgetTypeRepository.GetBudgetTypeByNameAsync(budgetType.Name);
                if (budgetTypeFound != null)
                {
                    return BadRequest("Budget type already exists.");
                }
                BudgetType budgetTypeAdd = new BudgetType
                {
                    Id = Guid.NewGuid(),
                    Name = budgetType.Name,
                };
                _budgetTypeRepository.Add(budgetTypeAdd);
                if (await _budgetTypeRepository.SaveChangesAsync())
                {
                    return Ok(budgetTypeAdd);
                }
                return BadRequest("Db fail.");
            }
            catch (Exception)
            {
                return BadRequest("Db fail.");
            }
        }
        [HttpGet("get-budget-types")]
        public async Task<IActionResult> GetBudgetTypes()
        {
            try
            {
                ICollection<BudgetType> budgetTypes = await _budgetTypeRepository.GetBudgetTypesAsync();
                if (budgetTypes.Count <= 0)
                {
                    return NotFound("No budget type found.");
                }
                return Ok(budgetTypes);
            }
            catch (Exception)
            {
                return BadRequest("Db fail.");
            }
        }
        [HttpGet("get-budget-type-names")]
        public async Task<IActionResult> GetBudgetTypeNames()
        {
            try
            {
                ICollection<string> budgetTypeNames = await _budgetTypeRepository.GetBudgetTypeNamesAsync();
                if (budgetTypeNames.Count <= 0)
                {
                    return NotFound("No budget type found.");
                }
                return Ok(budgetTypeNames);
            }
            catch (Exception)
            {
                return BadRequest("Db fail.");
            }
        }
    }
}
