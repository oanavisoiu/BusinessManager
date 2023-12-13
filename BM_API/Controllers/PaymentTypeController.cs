using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BM_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        public PaymentTypeController(IPaymentTypeRepository paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }
        [HttpGet("get-payment-type-by-name/{name}")]
        public async Task<IActionResult> GetPaymentTypeByName([FromRoute]string name)
        {
            try
            {
                if(name== null)
                {
                    return BadRequest("Name is empty");
                }
                PaymentType paymentType=await _paymentTypeRepository.GetPaymentTypeByNameAsync(name);
                if(paymentType==null)
                {
                    return NotFound("Payment type not found");
                }
                return Ok(paymentType);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("get-payment-types")]
        public async Task<IActionResult> GetPaymentTypes()
        {
            try
            {
                ICollection<PaymentType> paymentTypes = await _paymentTypeRepository.GetPaymentTypesAsync();
                if (paymentTypes.Count <= 0)
                {
                    return NotFound("Payment types not found.");
                }
                return Ok(paymentTypes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("get-payment-type-names")]
        public async Task<IActionResult> GetPaymentTypeNames()
        {
            try
            {
                ICollection<string> paymentTypes = await _paymentTypeRepository.GetPaymentTypeNamesAsync();
                if (paymentTypes.Count <= 0)
                {
                    return NotFound("Payment types not found.");
                }
                return Ok(paymentTypes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("add-payment-type")]
        public async Task<IActionResult> AddPaymentType([FromBody] PaymentType paymentType)
        {
            try
            {
                if (paymentType == null)
                {
                    return BadRequest("Payment type is empty");
                }
                PaymentType foundPaymentType = await _paymentTypeRepository.GetPaymentTypeByNameAsync(paymentType.Name);
                if (foundPaymentType != null)
                {
                    return BadRequest("Payment type already exists.");
                }
                PaymentType paymentTypeAdd = new PaymentType
                {
                    Id = Guid.NewGuid(),
                    Name = paymentType.Name,
                };
                _paymentTypeRepository.Add(paymentTypeAdd);
                if(await _paymentTypeRepository.SaveChangesAsync())
                {
                    return Ok(paymentTypeAdd);
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
