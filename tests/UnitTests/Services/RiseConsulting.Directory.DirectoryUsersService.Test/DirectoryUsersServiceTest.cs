using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiseConsulting.Directory.Data;
using RiseConsulting.Directory.DirectoryUsersService.Infrastructure;
using RiseConsulting.Directory.DirectoryUsersService.Test.TheoryData;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Entities.ViewModels;
using RiseConsulting.Directory.Repository;
using RiseConsulting.Directory.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RiseConsulting.Directory.DirectoryUsersService.Test
{


    public class DirectoryUsersServiceTest
    {
        private readonly IDirectoryUsersService _directoryUsersService;

        public DirectoryUsersServiceTest()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RiseConsultingDirectoryDbContext>(options =>
                options.UseSqlServer("Server=.;Database=RiseConsulting;Trusted_Connection=True"));

            services.AddScoped(typeof(IGenericRepository<DirectoryUsers>), typeof(GenericRepository<DirectoryUsers>));
            services.AddScoped(typeof(IGenericRepository<CommunicationInformation>), typeof(GenericRepository<CommunicationInformation>));
            services.AddTransient<IDirectoryUsersService, DirectoryUsersService>();

            var serviceProvider = services.BuildServiceProvider();

            _directoryUsersService = serviceProvider.GetService<IDirectoryUsersService>();
        }

        [Theory]
        [ClassData(typeof(DirectoryUsersServiceTestTrueTheoryData))]
        public void ToAddDirectoryUsers_ReturnDirectoryUsers(DirectoryUsers parameter)
        {
            DirectoryUsers actionResult = _directoryUsersService.AddDirectoryUser(parameter);

            Assert.IsType<DirectoryUsers>(actionResult);
        }

        [Theory]
        [ClassData(typeof(DirectoryUsersServiceTestTrueTheoryData))]
        public async Task ToAddDirectoryUsers_ReturnDirectoryUsersAsync(DirectoryUsers parameter)
        {
            DirectoryUsers actionResult = await _directoryUsersService.AddDirectoryUserAsync(parameter);

            Assert.IsType<DirectoryUsers>(actionResult);
        }

        [Theory]
        [ClassData(typeof(DirectoryUsersServiceTestTrueTheoryData))]
        public void ToDeleteDirectoryUsers(DirectoryUsers parameter)
        {
            DirectoryUsers addedDirectoryUser = _directoryUsersService.AddDirectoryUser(parameter);

            _directoryUsersService.DeleteDirectoryUser(addedDirectoryUser.DirectoryUsersId);
        }

        [Theory]
        [ClassData(typeof(DirectoryUsersServiceTestTrueTheoryData))]
        public async Task ToDeleteDirectoryUsersAsync(DirectoryUsers parameter)
        {
            DirectoryUsers addedDirectoryUser = await _directoryUsersService.AddDirectoryUserAsync(parameter);

            await _directoryUsersService.DeleteDirectoryUserAsync(addedDirectoryUser.DirectoryUsersId);
        }

        [Fact]
        public void ToGetAllDirectoryUsers_ReturnDirectoryUsers()
        {
            var results = _directoryUsersService.GetAllDirectoryUsers();

            Assert.IsType<List<DirectoryUsers>>(results);
        }

        [Fact]
        public async Task ToGetAllDirectoryUsersAsync_ReturnDirectoryUsers()
        {
            var results = await _directoryUsersService.GetAllDirectoryUsersAsync();

            Assert.IsType<List<DirectoryUsers>>(results);
        }

        [Theory]
        [InlineData("Oðuz")]
        public void ToGetAllDirectoryUsersWithCriteria_ReturnDirectoryUsers_NameEqualParameter(string name)
        {
            var results = _directoryUsersService.GetAllDirectoryUsersWithCriteria(x => x.Name == name);

            Assert.IsType<List<DirectoryUsers>>(results);
            foreach (var item in results)
            {
                Assert.Equal(name, item.Name);
            }
        }

        [Theory]
        [InlineData("Enes")]
        public void ToGetAllDirectoryUsersWithCriteria_ReturnEmptyDirectoryUsers_NameEqualParameter(string name)
        {
            var results = _directoryUsersService.GetAllDirectoryUsersWithCriteria(x => x.Name == name);

            Assert.Empty(results);
        }

        [Theory]
        [InlineData("Oðuz")]
        public async Task ToGetAllDirectoryUsersWithCriteriaAsync_ReturnDirectoryUsers_NameEqualParameter(string name)
        {
            var results = await _directoryUsersService.GetAllDirectoryUsersWithCriteriaAsync(user => user.Name == name);

            Assert.IsType<List<DirectoryUsers>>(results);
            foreach (var item in results)
            {
                Assert.Equal(name, item.Name);
            }
        }

        [Theory]
        [InlineData("Enes")]
        public async Task ToGetAllDirectoryUsersWithCriteriaAsync_ReturnEmptyDirectoryUsers_NameEqualParameter(string name)
        {
            var results = await _directoryUsersService.GetAllDirectoryUsersWithCriteriaAsync(user => user.Name == name);

            Assert.Empty(results);
        }

        [Fact]
        public void ToGetDirectoryUserById_ReturnDirectoryUser()
        {
            var result = _directoryUsersService.GetDirectoryUserById(new Guid("a9a08fd1-3a27-411e-0f77-08d9b2697616"));

            Assert.IsType<DirectoryUsers>(result);
        }

        [Fact]
        public void ToGetDirectoryUserById_ReturnNullDirectoryUser()
        {
            var result = _directoryUsersService.GetDirectoryUserById(new Guid());

            Assert.Null(result);
        }

        [Fact]
        public async Task ToGetDirectoryUserByIdAsync_ReturnDirectoryUser()
        {
            var result = await _directoryUsersService.GetDirectoryUserByIdAsync(new Guid("a9a08fd1-3a27-411e-0f77-08d9b2697616"));

            Assert.IsType<DirectoryUsers>(result);
        }

        [Fact]
        public void ToGetDirectoryUserByIdAsync_ReturnNullDirectoryUser()
        {
            var result = _directoryUsersService.GetDirectoryUserById(new Guid());

            Assert.Null(result);
        }

        [Theory]
        [InlineData("Oðuz")]
        public void ToGetDirectoryUserWithCriteria_ReturnDirectoryUser_NameEqualParameter(string name)
        {
            var result = _directoryUsersService.GetDirectoryUserWithCriteria(x => x.Name == name);

            Assert.IsType<DirectoryUsers>(result);
            Assert.Equal(name, result.Name);
        }

        [Theory]
        [InlineData("Enes")]
        public void ToGetDirectoryUserWithCriteria_ReturnNullDirectoryUser_NameEqualParameter(string name)
        {
            var result = _directoryUsersService.GetDirectoryUserWithCriteria(x => x.Name == name);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("Oðuz")]
        public async Task ToGetDirectoryUserWithCriteriaAsync_ReturnDirectoryUser_NameEqualParameter(string name)
        {
            var result = await _directoryUsersService.GetDirectoryUserWithCriteriaAsync(x => x.Name == name);

            Assert.IsType<DirectoryUsers>(result);
            Assert.Equal(name, result.Name);
        }

        [Theory]
        [InlineData("Enes")]
        public async Task ToGetDirectoryUserWithCriteriaAsync_ReturnNullDirectoryUser_NameEqualParameter(string name)
        {
            var result = await _directoryUsersService.GetDirectoryUserWithCriteriaAsync(x => x.Name == name);

            Assert.Null(result);
        }

        [Theory]
        [ClassData(typeof(DirectoryUsersServiceTestTrueTheoryData))]
        public void ToUpdateDirectoryUser(DirectoryUsers parameter)
        {
            DirectoryUsers addedDirectoryUser = _directoryUsersService.AddDirectoryUser(parameter);
            addedDirectoryUser.Name = "Test Update User Service";

            _directoryUsersService.UpdateDirectoryUser(addedDirectoryUser);
        }

        [Fact]
        public void ToGetDirectoryUsersDetail_Return_DirectoryUserInformation()
        {
            Guid userId = new Guid("e6f2f64e-6556-47ad-8ede-372c45e0807b");
            Guid directoryUserId = new Guid("a9a08fd1-3a27-411e-0f77-08d9b2697616");

            var result = _directoryUsersService.GetDirectoryUsersDetail(userId, directoryUserId);

            Assert.IsType<DirectoryUsersInformationVM>(result);
        }

        [Fact]
        public void ToGetDirectoryUsersDetail_ReturnNull_DirectoryUserInformation()
        {
            Guid userId = new Guid();
            Guid directoryUserId = new Guid();

            var result = _directoryUsersService.GetDirectoryUsersDetail(userId, directoryUserId);

            Assert.Null(result);
        }
    }
}
