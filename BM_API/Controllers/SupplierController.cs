using AutoMapper;
using BM_API.DTOs.EmployeeUpdateDto;
using BM_API.DTOs.Supplier;
using BM_API.Migrations;
using BM_API.Models;
using BM_API.Repositories;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BM_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository? _supplierRepository;
        private readonly ICompanySupplierRepository? _companyRepository;
        private readonly IMapper _mapper;

        public SupplierController(ISupplierRepository? supplierRepository, ICompanySupplierRepository? companyRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        [HttpGet("get-supplier/{supplierId}")]
        public async Task<IActionResult> GetSupplier([FromRoute]Guid supplierId)
        {
            try
            {
                if(supplierId.Equals(Guid.Empty))
                {
                    return BadRequest("Supplier id is null");
                }
                Supplier supplier = await _supplierRepository.GetSupplierByIdAsync(supplierId);
                if(supplier == null)
                {
                    return NotFound("Supplier not found");
                }
                return Ok(supplier);
            }
            catch (Exception)
            {
                return BadRequest("Db failure.");
            }
        }
        [HttpPost("add-supplier")]
        public async Task<IActionResult> AddSupplier([FromBody] SupplierDTO supplier)
        {
            try
            {
                Supplier supplierAdd = _mapper.Map<Supplier>(supplier);
                supplierAdd.Id = Guid.NewGuid();
                _supplierRepository.Add(supplierAdd);
                if (await _supplierRepository.SaveChangesAsync())
                {
                    return Ok(supplierAdd);
                }
                return BadRequest("Db failure");
            }
            catch (Exception)
            {
                return BadRequest("Db failure");
            }
        }
        [HttpPut("updatesupplier/{supplierId}")]
        public async Task<IActionResult> UpdateSupplier([FromRoute] Guid supplierId, [FromBody] SupplierDTO supplier)
        {
            try
            {
                if (supplierId == Guid.Empty)
                { 
                    return BadRequest("Id is empty."); 
                }
                Supplier updatedSupplier = _mapper.Map<Supplier>(supplier);
                updatedSupplier.Id = supplierId;
                _supplierRepository.Update(updatedSupplier);
                if (await _supplierRepository.SaveChangesAsync())
                    return Ok(updatedSupplier);
                return BadRequest("Db failure");
            }
            catch (Exception)
            {
                return BadRequest("Db failure");
            }
        }
        [HttpDelete("delete-supplier/{supplierId}")]
        public async Task<IActionResult> DeleteSupplier([FromRoute] Guid supplierId)
        {
            try
            {
                if (supplierId.Equals(Guid.Empty))
                {
                    return BadRequest("Supplier id is null.");
                }
                Supplier foundSupplier = await _supplierRepository.GetSupplierByIdAsync(supplierId);
                if (foundSupplier == null)
                {
                    return NotFound("Supplier not found.");
                }
                _supplierRepository.Delete(foundSupplier);
                if (await _supplierRepository.SaveChangesAsync())
                {
                    return Ok(foundSupplier);
                }
                return BadRequest("Db failure.");

            }
            catch (Exception)
            {
                return BadRequest("Db failure.");
            }
        }
    }
}
