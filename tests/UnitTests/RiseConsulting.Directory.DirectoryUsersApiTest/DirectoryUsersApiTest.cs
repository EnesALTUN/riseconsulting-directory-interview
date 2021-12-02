using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiseConsulting.Directory.CommunicationInformationService.Infrastructure;
using RiseConsulting.Directory.Core.Models;
using RiseConsulting.Directory.Data;
using RiseConsulting.Directory.DirectoryUsersApi.Controllers.V1;
using RiseConsulting.Directory.DirectoryUsersApiTest.TheoryData;
using RiseConsulting.Directory.DirectoryUsersService.Infrastructure;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Entities.ViewModels;
using RiseConsulting.Directory.Repository;
using RiseConsulting.Directory.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RiseConsulting.Directory.DirectoryUsersApiTest
{
    public class DirectoryUsersApiTest
    {
        private readonly IDirectoryUsersService _directoryUsersService;
        private readonly ICommunicationInformationService _communicationInformationService;
        private readonly DirectoryUsersController _controller;

        public DirectoryUsersApiTest()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RiseConsultingDirectoryDbContext>(options =>
                options.UseSqlServer("Server=.;Database=RiseConsulting;Trusted_Connection=True"));

            services.AddScoped(typeof(IGenericRepository<DirectoryUsers>), typeof(GenericRepository<DirectoryUsers>));
            services.AddScoped(typeof(IGenericRepository<CommunicationInformation>), typeof(GenericRepository<CommunicationInformation>));
            services.AddTransient<IDirectoryUsersService, DirectoryUsersService.DirectoryUsersService>();
            services.AddTransient<ICommunicationInformationService, CommunicationInformationService.CommunicationInformationService>();

            var serviceProvider = services.BuildServiceProvider();

            _directoryUsersService = serviceProvider.GetService<IDirectoryUsersService>();
            _communicationInformationService = serviceProvider.GetService<ICommunicationInformationService>();
            _controller = new DirectoryUsersController(_directoryUsersService, _communicationInformationService);
        }

        #region GetAllDirectoryUsers
        [Fact]
        public async Task ToGetAllDirectoryUsers_ReturnOkResult()
        {
            // Act
            var actionResult = await _controller.GetAllDirectoryUsers();
            var result = actionResult as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /*
        [Fact]
        public async Task ToGetAllDirectoryUsers_ReturnNoContentResult()
        {
            // Act
            var actionResult = await _controller.GetAllDirectoryUsers();

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }
        */

        [Fact]
        public async Task ToGetAllDirectoryUsers_ReturnAllItems()
        {
            // Arr
            List<DirectoryUsers> expected = await _directoryUsersService.GetAllDirectoryUsersAsync();
            ApiReturn<List<DirectoryUsers>> exceptedApiReturn = new ApiReturn<List<DirectoryUsers>> { Data = expected };

            // Act
            var actionResult = await _controller.GetAllDirectoryUsers();
            var result = actionResult as OkObjectResult;

            // Assert
            var actual = Assert.IsType<ApiReturn<List<DirectoryUsers>>>(result.Value);
            actual.Should().BeEquivalentTo(exceptedApiReturn);
        }
        #endregion

        #region GetDirectoryUser
        [Fact]
        public async Task ToGetDirectoryUser_ReturnOkResult()
        {
            // Arr
            Guid id = new Guid("a9a08fd1-3a27-411e-0f77-08d9b2697616");

            // Act
            var actionResult = await _controller.GetDirectoryUser(id);
            var result = actionResult as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ToGetDirectoryUser_ReturnNoContent()
        {
            // Arr
            Guid id = new Guid();

            // Act
            var actionResult = await _controller.GetDirectoryUser(id);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task ToGetDirectoryUser_ReturnDirectoryUser()
        {
            // Arr
            Guid id = new Guid("a9a08fd1-3a27-411e-0f77-08d9b2697616");
            DirectoryUsers expected = await _directoryUsersService.GetDirectoryUserByIdAsync(id);
            ApiReturn<DirectoryUsers> expectedApiReturn = new ApiReturn<DirectoryUsers> { Data = expected };

            // Act
            var actionResult = await _controller.GetDirectoryUser(id);
            var result = actionResult as OkObjectResult;

            // Assert
            var actual = Assert.IsType<ApiReturn<DirectoryUsers>>(result.Value);
            actual.Should().BeEquivalentTo(expectedApiReturn);
        }
        #endregion

        #region AddDirectoryUser
        [Theory]
        [ClassData(typeof(DirectoryUsersTestFalseTheoryData))]
        public async Task ToAddDirectoryUser_ReturnBadRequest(DirectoryUsers parameter)
        {
            // Arr
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var actionResult = await _controller.AddDirectoryUser(parameter);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Theory]
        [ClassData(typeof(DirectoryUsersTestTrueTheoryData))]
        public async Task ToAddDirectoryUser_ReturnAsExpected(DirectoryUsers parameter)
        {
            // Act
            var actionResult = await _controller.AddDirectoryUser(parameter);
            var result = actionResult as CreatedAtActionResult;
            var actual = result.Value as DirectoryUsers;

            // Assert
            Assert.IsType<DirectoryUsers>(actual);
            Assert.IsType<CreatedAtActionResult>(result);
            actual.Should().BeEquivalentTo(parameter);
        }
        #endregion

        #region UpdateDirectoryUser
        [Theory]
        [ClassData(typeof(DirectoryUsersTestTrueTheoryData))]
        public async Task ToUpdateDirectoryUsers_ReturnOkResult(DirectoryUsers parameter)
        {
            // Arr
            DirectoryUsers directoryUser = await _directoryUsersService.AddDirectoryUserAsync(parameter);
            directoryUser.Name = $"{directoryUser.Name} Test";

            // Act
            var actionResult = _controller.UpdateDirectoryUser(directoryUser);

            await _directoryUsersService.DeleteDirectoryUserAsync(directoryUser.DirectoryUsersId);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }

        [Theory]
        [ClassData(typeof(DirectoryUsersTestFalseTheoryData))]
        public void ToUpdateDirectoryUsers_ReturnBadRequest(DirectoryUsers parameter)
        {
            // Arr
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var actionResult = _controller.UpdateDirectoryUser(parameter);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }
        #endregion

        #region DeleteDirectoryUser
        [Theory]
        [ClassData(typeof(DirectoryUsersTestTrueTheoryData))]
        public async Task ToDeleteDirectoryUser_ReturnOkRequest(DirectoryUsers parameter)
        {
            // Arr
            DirectoryUsers directoryUsers = await _directoryUsersService.AddDirectoryUserAsync(parameter);

            // Act
            var actionResult = await _controller.DeleteDirectoryUser(directoryUsers.DirectoryUsersId);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }
        #endregion

        #region GetDirectoryUserInformation
        [Fact]
        public void ToGetDirectoryUserInformation_ReturnOkResult()
        {
            // Arr
            Guid userId = new Guid("e6f2f64e-6556-47ad-8ede-372c45e0807b");
            Guid directoryUserId = new Guid("86ea3032-be77-4fa3-9db1-d0de8a4fafcc");

            // Act
            var actionResult = _controller.GetDirectoryUserInformation(userId, directoryUserId);
            var result = actionResult as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ToGetDirectoryUserInformation_ReturnNoContent()
        {
            // Arr
            Guid userId = new Guid();
            Guid directoryUserId = new Guid();

            // Act
            var actionResult = _controller.GetDirectoryUserInformation(userId, directoryUserId);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public void ToGetDirectoryUserInformation_ReturnDirectoryUserInformation()
        {
            // Arr
            Guid userId = new Guid("e6f2f64e-6556-47ad-8ede-372c45e0807b");
            Guid directoryUserId = new Guid("86ea3032-be77-4fa3-9db1-d0de8a4fafcc");
            DirectoryUsersInformationVM expected = _directoryUsersService.GetDirectoryUsersDetail(userId, directoryUserId);
            ApiReturn<DirectoryUsersInformationVM> expectedApiReturn = new ApiReturn<DirectoryUsersInformationVM> { Data = expected };

            // Act
            var actionResult = _controller.GetDirectoryUserInformation(userId, directoryUserId);
            var result = actionResult as OkObjectResult;

            // Assert
            var actual = Assert.IsType<ApiReturn<DirectoryUsersInformationVM>>(result.Value);
            actual.Should().BeEquivalentTo(expectedApiReturn);
        }
        #endregion

        #region AddDirectoryUserInformation
        [Theory]
        [ClassData(typeof(DirectoryUserInformationTestFalseTheoryData))]
        public async Task ToAddDirectoryUserInformation_ReturnBadRequest(Guid userId, Guid directoryUserId, CommunicationInformation parameter)
        {
            // Arr
            _controller.ModelState.AddModelError("UserId", "Required");
            _controller.ModelState.AddModelError("DirectoryUserId", "Required");
            _controller.ModelState.AddModelError("PhoneNumber", "Required");

            // Act
            var actionResult = await _controller.AddDirectoryInformation(userId, directoryUserId, parameter);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Theory]
        [ClassData(typeof(DirectoryUserInformationTestTrueTheoryData))]
        public async Task ToAddDirectoryUserInformation_ReturnAsExpected(Guid userId, Guid directoryUserId, CommunicationInformation parameter)
        {
            // Act
            var actionResult = await _controller.AddDirectoryInformation(userId, directoryUserId, parameter);
            var result = actionResult as OkObjectResult;
            var actual = result.Value as ApiReturn<CommunicationInformation>;

            // Assert
            Assert.IsType<ApiReturn<CommunicationInformation>>(actual);
            Assert.IsType<OkObjectResult>(result);
            actual.Data.Should().BeEquivalentTo(parameter);
        }
        #endregion

        #region DeleteDirectoryUserInformation
        [Theory]
        [ClassData(typeof(DirectoryUserInformationTestTrueTheoryData))]
        public async Task ToDeleteDirectoryUserInformation_ReturnOkRequest(Guid userId, Guid directoryUserId, CommunicationInformation parameter)
        {
            // Arr
            CommunicationInformation communicationInformation = await _communicationInformationService.AddCommunicationInformationAsync(parameter);

            // Act
            var actionResult = await _controller.DeleteDirectoryInformation(userId, directoryUserId, communicationInformation.CommunicationInformationId);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }
        #endregion
    }
}