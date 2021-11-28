using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseConsulting.Directory.CompanyService.Infrastructure;
using RiseConsulting.Directory.Core.Models;
using RiseConsulting.Directory.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.CompanyApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompany()
        {
            List<Company> companies = await _companyService.GetAllCompanyAsync();

            if (companies is null)
                return NoContent();
                
            return Ok(new ApiReturn<List<Company>> { Success = true, Code = StatusCodes.Status200OK, Data = companies });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            Company company = await _companyService.GetCompanyByIdAsync(id);

            if (company is null)
                return NoContent();

            return Ok(new ApiReturn<Company> { Success = true, Code = StatusCodes.Status200OK, Data = company });
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] Company company)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Company addedCompany = await _companyService.AddCompanyAsync(company);

            return CreatedAtAction("GetCompany","Company", new { id = addedCompany.CompanyId }, addedCompany);
        }

        [HttpPut]
        public IActionResult UpdateCompany([FromBody] Company company)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _companyService.UpdateCompany(company);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _companyService.DeleteCompanyAsync(id);

            return Ok();
        }
    }
}