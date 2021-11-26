using Microsoft.AspNetCore.Mvc;
using RiseConsulting.Directory.CompanyService.Infrastructure;
using RiseConsulting.Directory.Entities.Models;
using System;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.CompanyApi.V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok(await _companyService.GetAllCompanyAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            return Ok(await _companyService.GetCompanyByIdAsync(id));
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