using BM_API.DTOs.CompanySupplier;
using BM_API.Models;
using BM_API.Repositories;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace BM_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanySupplierController : ControllerBase
    {
        private readonly ICompanySupplierRepository? _companySupplierRepository;
        private readonly ICompanyRepository? _companyRepository;
        private readonly ISupplierRepository? _supplierRepository;
        public CompanySupplierController(ICompanySupplierRepository? companySupplierRepository, ICompanyRepository companyRepository, ISupplierRepository? supplierRepository)
        {
            _companySupplierRepository = companySupplierRepository;
            _companyRepository = companyRepository;
            _supplierRepository = supplierRepository;
        }
        [HttpGet("get-company-suppliers-by-company/{companyId}")]
        public async Task<IActionResult> GetCompanySuppliersByCompanyId([FromRoute] Guid companyId)
        {
            try
            {
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("Company id is null");
                }
                ICollection<CompanySupplier> companySupplier = await _companySupplierRepository.GetCompanySuppliersByCompanyIdAsync(companyId);
                List<Supplier> suppliers = new List<Supplier>();

                foreach (CompanySupplier cs in companySupplier)
                {
                    cs.Supplier = await _supplierRepository.GetSupplierByIdAsync(cs.SupplierId);
                    suppliers.Add(cs.Supplier);
                }

                if (suppliers == null || suppliers.Count == 0)
                {
                    return NotFound("Suppliers not found");
                }

                return Ok(suppliers);
            }
            catch (Exception)
            {
                return BadRequest("Db failure.");
            }
        }

        [HttpPost("add-company-supplier")]
        public async Task<IActionResult> AddCompanySupplier(CompanySupplier companySupplier)
        {
            try
            {
                if (companySupplier == null)
                {
                    return BadRequest("Company employee is null.");
                }
                Company company  = await _companyRepository.GetCompanyByIdAsync(companySupplier.CompanyId);
                if (company == null)
                {
                    return NotFound("Company not found");
                }
                Supplier supplier = await _supplierRepository.GetSupplierByIdAsync(companySupplier.SupplierId);
                if (supplier == null)
                {
                    return NotFound("Supplier not found.");
                }
                var foundCompanySupplier = await _companySupplierRepository.GetCompanySupplierBySupplierIdAsync(companySupplier.SupplierId);
                if (foundCompanySupplier != null)
                {
                    return BadRequest("Employee already exists in this company.");
                }
                companySupplier.Supplier = supplier;
                companySupplier.Company = company;
                _companySupplierRepository.Add(companySupplier);
                if(await _companySupplierRepository.SaveChangesAsync())
                {
                    return Ok(companySupplier);
                }
                return BadRequest("Db failure.");

            }
            catch (Exception)
            {
                return BadRequest("Db failure.");
            }
        }
        [HttpDelete("delete-company-supplier/{supplierId}")]
        public async Task<IActionResult> DeleteCompanyEmployeeBySupplierId([FromRoute] Guid supplierId)
        {
            try
            {
                if (supplierId.Equals(Guid.Empty))
                {
                    return BadRequest("Supplier id is null.");
                }
                CompanySupplier foundSupplier = await _companySupplierRepository.GetCompanySupplierBySupplierIdAsync(supplierId);
                if (foundSupplier == null)
                {
                    return NotFound("Supplier not found.");
                }
                _companySupplierRepository.Delete(foundSupplier);
                if(await _companySupplierRepository.SaveChangesAsync())
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
