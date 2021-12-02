using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiseConsulting.Directory.CommunicationInformationApi.Controllers.V1;
using RiseConsulting.Directory.CommunicationInformationApiTest.TheoryData;
using RiseConsulting.Directory.CommunicationInformationService.Infrastructure;
using RiseConsulting.Directory.Core.Models;
using RiseConsulting.Directory.Data;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Repository;
using RiseConsulting.Directory.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RiseConsulting.Directory.CommunicationInformationApiTest
{
    public class CommunicationInformationApiTest
    {
        private readonly ICommunicationInformationService _communicationInformationService;
        private readonly CommunicationInformationController _controller;

        public CommunicationInformationApiTest()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RiseConsultingDirectoryDbContext>(options =>
                options.UseSqlServer("Server=.;Database=RiseConsulting;Trusted_Connection=True"));

            services.AddScoped(typeof(IGenericRepository<CommunicationInformation>), typeof(GenericRepository<CommunicationInformation>));
            services.AddTransient<ICommunicationInformationService, CommunicationInformationService.CommunicationInformationService>();

            var serviceProvider = services.BuildServiceProvider();

            _communicationInformationService = serviceProvider.GetService<ICommunicationInformationService>();
            _controller = new CommunicationInformationController(_communicationInformationService);
        }

        #region GetAllCommunicationInformation
        [Fact]
        public async Task ToGetAllCommunicationInformation_ReturnOkResult()
        {
            // Act
            var actionResult = await _controller.GetAllCommunicationInformations();
            var result = actionResult as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /*
        [Fact]
        public async Task ToGetAllCommunicationInformation_ReturnNoContentResult()
        {
            // Act
            var actionResult = await _controller.GetAllCommunicationInformations();

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }
        */


        [Fact]
        public async Task ToGetAllCommunicationInformation_ReturnAllItems()
        {
            // Arr
            List<CommunicationInformation> expected = await _communicationInformationService.GetAllCommunicationInformationsAsync();
            ApiReturn<List<CommunicationInformation>> exceptedApiReturn = new ApiReturn<List<CommunicationInformation>> { Data = expected };

            // Act
            var actionResult = await _controller.GetAllCommunicationInformations();
            var result = actionResult as OkObjectResult;

            // Assert
            var actual = Assert.IsType<ApiReturn<List<CommunicationInformation>>>(result.Value);
            actual.Should().BeEquivalentTo(exceptedApiReturn);
        }
        #endregion

        #region GetCommunicationInformation
        [Fact]
        public async Task ToCommunicationInformation_ReturnOkResult()
        {
            // Arr
            Guid id = new Guid("8ac56401-1d5d-4d8b-a27d-8f685399ef23");

            // Act
            var actionResult = await _controller.GetCommunicationInformation(id);
            var result = actionResult as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ToCommunicationInformation_ReturnNoContent()
        {
            // Arr
            Guid id = new Guid();

            // Act
            var actionResult = await _controller.GetCommunicationInformation(id);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task ToCommunicationInformation_ReturnCommunicationInformation()
        {
            // Arr
            Guid id = new Guid("8ac56401-1d5d-4d8b-a27d-8f685399ef23");
            CommunicationInformation expected = await _communicationInformationService.GetCommunicationInformationByIdAsync(id);
            ApiReturn<CommunicationInformation> expectedApiReturn = new ApiReturn<CommunicationInformation> { Data = expected };

            // Act
            var actionResult = await _controller.GetCommunicationInformation(id);
            var result = actionResult as OkObjectResult;

            // Assert
            var actual = Assert.IsType<ApiReturn<CommunicationInformation>>(result.Value);
            actual.Should().BeEquivalentTo(expectedApiReturn);
        }
        #endregion

        #region AddCommunicationInformation
        [Theory]
        [ClassData(typeof(CommunicationInformationTestFalseTheoryData))]
        public async Task ToAddCommunicationInformation_ReturnBadRequest(CommunicationInformation parameter)
        {
            // Arr
            _controller.ModelState.AddModelError("PhoneNumber", "Required");

            // Act
            var actionResult = await _controller.AddCommunicationInformation(parameter);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Theory]
        [ClassData(typeof(CommunicationInformationTestTrueTheoryData))]
        public async Task ToAddCommunicationInformation_ReturnAsExpected(CommunicationInformation parameter)
        {
            // Act
            var actionResult = await _controller.AddCommunicationInformation(parameter);
            var result = actionResult as CreatedAtActionResult;
            var actual = result.Value as CommunicationInformation;

            // Assert
            Assert.IsType<CommunicationInformation>(actual);
            Assert.IsType<CreatedAtActionResult>(result);
            actual.Should().BeEquivalentTo(parameter);
        }
        #endregion

        #region UpdateCommunicationInformation
        [Theory]
        [ClassData(typeof(CommunicationInformationTestTrueTheoryData))]
        public async Task ToUpdateCommunicationInformation_ReturnOkResult(CommunicationInformation parameter)
        {
            // Arr
            CommunicationInformation communicationInformation = await _communicationInformationService.AddCommunicationInformationAsync(parameter);
            communicationInformation.Location = "Test Location";

            // Act
            var actionResult = _controller.UpdateCommunicationInformation(communicationInformation);

            await _communicationInformationService.DeleteCommunicationInformationAsync(communicationInformation.CommunicationInformationId);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }

        [Theory]
        [ClassData(typeof(CommunicationInformationTestFalseTheoryData))]
        public void ToUpdateCommunicationInformation_ReturnBadRequest(CommunicationInformation parameter)
        {
            // Arr
            _controller.ModelState.AddModelError("PhoneNumber", "Required");

            // Act
            var actionResult = _controller.UpdateCommunicationInformation(parameter);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }
        #endregion

        #region DeleteCommunicationInformation
        [Theory]
        [ClassData(typeof(CommunicationInformationTestTrueTheoryData))]
        public async Task ToDeleteCommunicationInformation_ReturnOkRequest(CommunicationInformation parameter)
        {
            // Arr
            CommunicationInformation communicationInformation = await _communicationInformationService.AddCommunicationInformationAsync(parameter);

            // Act
            var actionResult = await _controller.DeleteCommunicationInformation(communicationInformation.CommunicationInformationId);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }
        #endregion
    }
}