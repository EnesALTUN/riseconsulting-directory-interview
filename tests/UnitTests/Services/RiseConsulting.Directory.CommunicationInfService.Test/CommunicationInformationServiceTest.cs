using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiseConsulting.Directory.CommunicationInformationService.Infrastructure;
using RiseConsulting.Directory.CommunicationInfService.Test.TheoryData;
using RiseConsulting.Directory.Data;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Repository;
using RiseConsulting.Directory.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RiseConsulting.Directory.CommunicationInfService.Test
{
    public class CommunicationInformationServiceTest
    {
        private readonly ICommunicationInformationService _communicationInformationService;

        public CommunicationInformationServiceTest()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RiseConsultingDirectoryDbContext>(options =>
                options.UseSqlServer("Server=.;Database=RiseConsulting;Trusted_Connection=True"));

            services.AddScoped(typeof(IGenericRepository<CommunicationInformation>), typeof(GenericRepository<CommunicationInformation>));
            services.AddTransient<ICommunicationInformationService, CommunicationInformationService.CommunicationInformationService>();

            var serviceProvider = services.BuildServiceProvider();

            _communicationInformationService = serviceProvider.GetService<ICommunicationInformationService>();
        }

        [Theory]
        [ClassData(typeof(CommunicationInformationServiceTestTrueTheoryData))]
        public void ToAddCommunicationInformation_ReturnCommunicationInformation(CommunicationInformation parameter)
        {
            var actionResult = _communicationInformationService.AddCommunicationInformation(parameter);

            Assert.IsType<CommunicationInformation>(actionResult);
        }

        [Theory]
        [ClassData(typeof(CommunicationInformationServiceTestTrueTheoryData))]
        public async Task ToAddCommunicationInformationAsync_ReturnCommunicationInformation(CommunicationInformation parameter)
        {
            var actionResult = await _communicationInformationService.AddCommunicationInformationAsync(parameter);

            Assert.IsType<CommunicationInformation>(actionResult);
        }

        [Theory]
        [ClassData(typeof(CommunicationInformationServiceTestTrueTheoryData))]
        public void ToDeleteCommunicationInformation(CommunicationInformation parameter)
        {
            CommunicationInformation addedCommunicationInformation = _communicationInformationService.AddCommunicationInformation(parameter);
            _communicationInformationService.DeleteCommunicationInformation(addedCommunicationInformation.CommunicationInformationId);
        }

        [Theory]
        [ClassData(typeof(CommunicationInformationServiceTestTrueTheoryData))]
        public async Task ToDeleteCommunicationInformationAsync(CommunicationInformation parameter)
        {
            CommunicationInformation addedCommunicationInformation = await _communicationInformationService.AddCommunicationInformationAsync(parameter);
            await _communicationInformationService.DeleteCommunicationInformationAsync(addedCommunicationInformation.CommunicationInformationId);
        }

        [Fact]
        public void ToGetAllCommunicationInformation_ReturnCommunicationInformation()
        {
            var results = _communicationInformationService.GetAllCommunicationInformations();

            Assert.IsType<List<CommunicationInformation>>(results);
        }

        [Fact]
        public async Task ToGetAllCommunicationInformationAsync_ReturnCommunicationInformation()
        {
            var result = await _communicationInformationService.GetAllCommunicationInformationsAsync();

            Assert.IsType<List<CommunicationInformation>>(result);
        }

        [Theory]
        [InlineData("Ýstanbul")]
        public void ToGetAllCommunicationInformationWithCriteria_ReturnCommunicationInformation_LocationEqualParameter(string location)
        {
            var results = _communicationInformationService.GetAllCommunicationInformationsWithCriteria(x => x.Location == location);

            Assert.IsType<List<CommunicationInformation>>(results);
            foreach (var item in results)
            {
                Assert.Equal(location, item.Location);
            }
        }

        [Theory]
        [InlineData("Ankara")]
        public void ToGetAllCommunicationInformationWithCriteria_ReturnEmptyCommunicationInformation_LocationEqualParameter(string location)
        {
            var results = _communicationInformationService.GetAllCommunicationInformationsWithCriteria(x => x.Location == location);

            Assert.Empty(results);
        }

        [Theory]
        [InlineData("Ýstanbul")]
        public async Task ToGetAllCommunicationInformationWithCriteriaAsync_ReturnCommunicationInformation_LocationEqualParameter(string location)
        {
            var results = await _communicationInformationService.GetAllCommunicationInformationsWithCriteriaAsync(x => x.Location == location);

            Assert.IsType<List<CommunicationInformation>>(results);
            foreach (var item in results)
            {
                Assert.Equal(location, item.Location);
            }
        }

        [Theory]
        [InlineData("Ankara")]
        public async Task ToGetAllCommunicationInformationWithCriteriaAsync_ReturnEmptyCommunicationInformation_LocationEqualParameter(string location)
        {
            var results = await _communicationInformationService.GetAllCommunicationInformationsWithCriteriaAsync(x => x.Location == location);

            Assert.Empty(results);
        }

        [Fact]
        public void ToGetCommunicationInformationById_ReturnCommunicationInformation()
        {
            var result = _communicationInformationService.GetCommunicationInformationById(new Guid("8ac56401-1d5d-4d8b-a27d-8f685399ef23"));

            Assert.IsType<CommunicationInformation>(result);
        }

        [Fact]
        public void ToGetCommunicationInformationById_ReturnNullCommunicationInformation()
        {
            var result = _communicationInformationService.GetCommunicationInformationById(new Guid());

            Assert.Null(result);
        }
        
        [Fact]
        public async Task ToGetCommunicationInformationByIdAsync_ReturnCommunicationInformation()
        {
            var result = await _communicationInformationService.GetCommunicationInformationByIdAsync(new Guid("8ac56401-1d5d-4d8b-a27d-8f685399ef23"));

            Assert.IsType<CommunicationInformation>(result);
        }

        [Fact]
        public async Task ToGetCommunicationInformationByIdAsync_ReturnNullCommunicationInformation()
        {
            var result = await _communicationInformationService.GetCommunicationInformationByIdAsync(new Guid());

            Assert.Null(result);
        }

        [Theory]
        [InlineData("Ýstanbul")]
        public void ToGetCommunicationInformationWithCriteria_ReturnCommunicationInformation_LocationEqualParameter(string location)
        {
            var result = _communicationInformationService.GetCommunicationInformationWithCriteria(x => x.Location == location);

            Assert.IsType<CommunicationInformation>(result);
            Assert.Equal(location, result.Location);
        }

        [Theory]
        [InlineData("Ankara")]
        public void ToGetCommunicationInformationWithCriteria_ReturnNullCommunicationInformation_LocationEqualParameter(string location)
        {
            var result = _communicationInformationService.GetCommunicationInformationWithCriteria(x => x.Location == location);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("Ýstanbul")]
        public async Task ToGetCommunicationInformationWithCriteriaAsync_ReturnCommunicationInformation_LocationEqualParameter(string location)
        {
            var result = await _communicationInformationService.GetCommunicationInformationWithCriteriaAsync(x => x.Location == location);

            Assert.IsType<CommunicationInformation>(result);
            Assert.Equal(location, result.Location);
        }

        [Theory]
        [InlineData("Ankara")]
        public async Task ToGetCommunicationInformationWithCriteriaAsync_ReturnNullCommunicationInformation_LocationEqualParameter(string location)
        {
            var result = await _communicationInformationService.GetCommunicationInformationWithCriteriaAsync(x => x.Location == location);

            Assert.Null(result);
        }
        
        [Theory]
        [ClassData(typeof(CommunicationInformationServiceTestTrueTheoryData))]
        public void ToUpdateCommunicationInformation(CommunicationInformation parameter)
        {
            CommunicationInformation addedCommunicationInformation = _communicationInformationService.AddCommunicationInformation(parameter);
            addedCommunicationInformation.Location = "Test Update Location";

            _communicationInformationService.UpdateCommunicationInformation(addedCommunicationInformation);
        }
    }
}
