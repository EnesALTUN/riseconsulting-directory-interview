using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiseConsulting.Directory.CompanyApi.Controllers.V1;
using RiseConsulting.Directory.CompanyApiTest.TheoryData;
using RiseConsulting.Directory.CompanyService.Infrastructure;
using RiseConsulting.Directory.Core.Models;
using RiseConsulting.Directory.Data;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Repository;
using RiseConsulting.Directory.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RiseConsulting.Directory.CompanyApiTest
{
    public class CompanyApiTest
    {
        private readonly ICompanyService _companyService;
        private readonly CompanyController _companyController;

        public CompanyApiTest()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RiseConsultingDirectoryDbContext>(options =>
                options.UseSqlServer("Server=.;Database=RiseConsulting;Trusted_Connection=True"));

            services.AddScoped(typeof(IGenericRepository<Company>), typeof(GenericRepository<Company>));
            services.AddTransient<ICompanyService, CompanyService.CompanyService>();

            var serviceProvider = services.BuildServiceProvider();

            _companyService = serviceProvider.GetService<ICompanyService>();
            _companyController = new CompanyController(_companyService);
        }

        #region GetAllCompany
        [Fact]
        public async Task ToGetAllCompany_ReturnOkResult()
        {
            // Act
            var actionResult = await _companyController.GetAllCompany();
            var result = actionResult as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /*
        [Fact]
        public async Task ToGetAllCompany_ReturnNoContentResult()
        {
            // Act
            var actionResult = await _companyController.GetAllCompany();

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }
        */

        [Fact]
        public async Task ToGetAllCompany_ReturnAllItems()
        {
            // Arr
            List<Company> expected = await _companyService.GetAllCompanyAsync();
            ApiReturn<List<Company>> exceptedApiReturn = new ApiReturn<List<Company>> { Data = expected };

            // Act
            var actionResult = await _companyController.GetAllCompany();
            var result = actionResult as OkObjectResult;

            // Assert
            var actual = Assert.IsType<ApiReturn<List<Company>>>(result.Value);
            actual.Should().BeEquivalentTo(exceptedApiReturn);
        }
        #endregion

        #region GetCompany
        [Fact]
        public async Task ToGetCompany_ReturnOkResult()
        {
            // Arr
            Guid id = new Guid("6f62317b-739b-4320-870c-014e43166167");

            // Act
            var actionResult = await _companyController.GetCompany(id);
            var result = actionResult as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ToGetCompany_ReturnNoContent()
        {
            // Arr
            Guid id = new Guid();

            // Act
            var actionResult = await _companyController.GetCompany(id);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task ToGetCompany_ReturnCompany()
        {
            // Arr
            Guid id = new Guid("6f62317b-739b-4320-870c-014e43166167");
            Company expected = await _companyService.GetCompanyByIdAsync(id);
            ApiReturn<Company> expectedApiReturn = new ApiReturn<Company> { Data = expected };

            // Act
            var actionResult = await _companyController.GetCompany(id);
            var result = actionResult as OkObjectResult;

            // Assert
            var actual = Assert.IsType<ApiReturn<Company>>(result.Value);
            actual.Should().BeEquivalentTo(expectedApiReturn);
        }
        #endregion

        #region AddCompany
        [Theory]
        [ClassData(typeof(CompanyTestFalseTheoryData))]
        public async Task ToAddCompany_ReturnBadRequest(Company parameter)
        {
            // Arr
            _companyController.ModelState.AddModelError("Name", "Required");

            // Act
            var actionResult = await _companyController.AddCompany(parameter);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Theory]
        [ClassData(typeof(CompanyTestTrueTheoryData))]
        public async Task ToAddCompany_ReturnAsExpected(Company parameter)
        {
            // Act
            var actionResult = await _companyController.AddCompany(parameter);
            var result = actionResult as CreatedAtActionResult;
            var actual = result.Value as Company;

            // Assert
            Assert.IsType<Company>(actual);
            Assert.IsType<CreatedAtActionResult>(result);
            actual.Should().BeEquivalentTo(parameter);
        }
        #endregion

        #region UpdateCompany
        [Theory]
        [ClassData(typeof(CompanyTestTrueTheoryData))]
        public async Task ToUpdateCompany_ReturnOkResult(Company parameter)
        {
            // Arr
            Company company = await _companyService.AddCompanyAsync(parameter);
            company.Name = $"{company.Name} Test";

            // Act
            var actionResult = _companyController.UpdateCompany(company);

            await _companyService.DeleteCompanyAsync(company.CompanyId);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }

        [Theory]
        [ClassData(typeof(CompanyTestFalseTheoryData))]
        public void ToUpdateCompany_ReturnBadRequest(Company parameter)
        {
            // Arr
            _companyController.ModelState.AddModelError("Name", "Required");

            // Act
            var actionResult = _companyController.UpdateCompany(parameter);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }
        #endregion

        #region DeleteCompany
        [Theory]
        [ClassData(typeof(CompanyTestTrueTheoryData))]
        public async Task ToDeleteCompany_ReturnOkRequest(Company parameter)
        {
            // Arr
            Company company = await _companyService.AddCompanyAsync(parameter);

            // Act
            var actionResult = await _companyController.Delete(company.CompanyId);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }
        #endregion
    }
}