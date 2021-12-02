using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiseConsulting.Directory.CompanyService.Infrastructure;
using RiseConsulting.Directory.CompanyService.Test.TheoryData;
using RiseConsulting.Directory.Data;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Repository;
using RiseConsulting.Directory.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RiseConsulting.Directory.CompanyService.Test
{
    public class CompanyServiceTest
    {
        private readonly ICompanyService _companyService;

        public CompanyServiceTest()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RiseConsultingDirectoryDbContext>(options =>
                options.UseSqlServer("Server=.;Database=RiseConsulting;Trusted_Connection=True"));

            services.AddScoped(typeof(IGenericRepository<Company>), typeof(GenericRepository<Company>));
            services.AddTransient<ICompanyService, CompanyService>();

            var serviceProvider = services.BuildServiceProvider();

            _companyService = serviceProvider.GetService<ICompanyService>();
        }


        [Theory]
        [ClassData(typeof(CompanyServiceTestTrueTheoryData))]
        public void ToAddCompany_ReturnCompany(Company parameter)
        {
            var actionResult = _companyService.AddCompany(parameter);

            Assert.IsType<Company>(actionResult);
        }

        [Theory]
        [ClassData(typeof(CompanyServiceTestTrueTheoryData))]
        public async Task ToAddUserAsync_ReturnUser(Company parameter)
        {
            var actionResult = await _companyService.AddCompanyAsync(parameter);

            Assert.IsType<Company>(actionResult);
        }

        [Theory]
        [ClassData(typeof(CompanyServiceTestTrueTheoryData))]
        public void ToDeleteCompany(Company parameter)
        {
            Company addedCompany = _companyService.AddCompany(parameter);
            _companyService.DeleteCompany(addedCompany.CompanyId);
        }

        [Theory]
        [ClassData(typeof(CompanyServiceTestTrueTheoryData))]
        public async Task ToDeleteCompanyAsync(Company parameter)
        {
            Company addedCompany = await _companyService.AddCompanyAsync(parameter);
            await _companyService.DeleteCompanyAsync(addedCompany.CompanyId);
        }

        [Fact]
        public void ToGetAllCompany_ReturnCompany()
        {
            var results = _companyService.GetAllCompany();

            Assert.IsType<List<Company>>(results);
        }

        [Fact]
        public async Task ToGetAllCompanyAsync_ReturnCompany()
        {
            var result = await _companyService.GetAllCompanyAsync();

            Assert.IsType<List<Company>>(result);
        }

        [Theory]
        [InlineData("Rise Consulting")]
        public void ToGetAllCompanyWithCriteria_ReturnCompany_NameEqualParameter(string name)
        {
            var results = _companyService.GetAllCompanyWithCriteria(x => x.Name == name);

            Assert.IsType<List<Company>>(results);
            foreach (var item in results)
            {
                Assert.Equal(name, item.Name);
            }
        }

        [Theory]
        [InlineData("Deneme")]
        public void ToGetAllCompanyWithCriteria_ReturnEmptyCompany_NameEqualParameter(string name)
        {
            var results = _companyService.GetAllCompanyWithCriteria(x => x.Name == name);

            Assert.Empty(results);
        }

        [Theory]
        [InlineData("Rise Consulting")]
        public async Task ToGetAllCompanyWithCriteriaAsync_ReturnCompany_NameEqualParameter(string name)
        {
            var results = await _companyService.GetAllCompanyWithCriteriaAsync(x => x.Name == name);

            Assert.IsType<List<Company>>(results);
            foreach (var item in results)
            {
                Assert.Equal(name, item.Name);
            }
        }

        [Theory]
        [InlineData("Deneme")]
        public async Task ToGetAllCompanyWithCriteriaAsync_ReturnEmptyCompany_NameEqualParameter(string name)
        {
            var results = await _companyService.GetAllCompanyWithCriteriaAsync(x => x.Name == name);

            Assert.Empty(results);
        }

        [Fact]
        public void ToGetCompanyById_ReturnCompany()
        {
            var result = _companyService.GetCompanyById(new Guid("6f62317b-739b-4320-870c-014e43166167"));

            Assert.IsType<Company>(result);
        }

        [Fact]
        public void ToGetCompanyById_ReturnNullCompany()
        {
            var result = _companyService.GetCompanyById(new Guid());

            Assert.Null(result);
        }

        [Fact]
        public async Task ToGetCompanyByIdAsync_ReturnCompany()
        {
            var result = await _companyService.GetCompanyByIdAsync(new Guid("6f62317b-739b-4320-870c-014e43166167"));

            Assert.IsType<Company>(result);
        }

        [Fact]
        public async Task ToGetCompanyByIdAsync_ReturnNullCompany()
        {
            var result = await _companyService.GetCompanyByIdAsync(new Guid());

            Assert.Null(result);
        }

        [Theory]
        [InlineData("Rise Consulting")]
        public void ToGetCompanyWithCriteria_ReturnCompany_NameEqualParameter(string name)
        {
            var result = _companyService.GetCompanyWithCriteria(x => x.Name == name);

            Assert.IsType<Company>(result);
            Assert.Equal(name, result.Name);
        }

        [Theory]
        [InlineData("Deneme")]
        public void ToGetCompanyWithCriteria_ReturnNullCompany_NameEqualParameter(string name)
        {
            var result = _companyService.GetCompanyWithCriteria(x => x.Name == name);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("Rise Consulting")]
        public async Task ToGetCompanyWithCriteriaAsync_ReturnCompany_NameEqualParameter(string name)
        {
            var result = await _companyService.GetCompanyWithCriteriaAsync(x => x.Name == name);

            Assert.IsType<Company>(result);
            Assert.Equal(name, result.Name);
        }

        [Theory]
        [InlineData("Deneme")]
        public async Task ToGetCompanyWithCriteriaAsync_ReturnNullCompany_NameEqualParameter(string name)
        {
            var result = await _companyService.GetCompanyWithCriteriaAsync(x => x.Name == name);

            Assert.Null(result);
        }
        
        [Theory]
        [ClassData(typeof(CompanyServiceTestTrueTheoryData))]
        public void ToUpdateCompany(Company parameter)
        {
            Company addedCompany = _companyService.AddCompany(parameter);
            addedCompany.Name = "Test Update Company Service";

            _companyService.UpdateCompany(addedCompany);
        }
    }
}