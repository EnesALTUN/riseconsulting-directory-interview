using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiseConsulting.Directory.Data;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Repository;
using RiseConsulting.Directory.Repository.Infrastructure;
using RiseConsulting.Directory.UserService.Infrastructure;
using RiseConsulting.Directory.UserService.Test.TheoryData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RiseConsulting.Directory.UserService.Test
{
    public class UserServiceTest
    {
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RiseConsultingDirectoryDbContext>(options =>
                options.UseSqlServer("Server=.;Database=RiseConsulting;Trusted_Connection=True"));

            services.AddScoped(typeof(IGenericRepository<Users>), typeof(GenericRepository<Users>));
            services.AddTransient<IUserService, UserService>();

            var serviceProvider = services.BuildServiceProvider();

            _userService = serviceProvider.GetService<IUserService>();
        }

        [Theory]
        [ClassData(typeof(UserServiceTestTrueTheoryData))]
        public void ToAddUser_ReturnUser(Users parameter)
        {
            Users actionResult = _userService.AddUser(parameter);

            Assert.IsType<Users>(actionResult);
        }

        [Theory]
        [ClassData(typeof(UserServiceTestTrueTheoryData))]
        public async Task ToAddUserAsync_ReturnUser(Users parameter)
        {
            Users actionResult = await _userService.AddUserAsync(parameter);

            Assert.IsType<Users>(actionResult);
        }

        [Theory]
        [ClassData(typeof(UserServiceTestTrueTheoryData))]
        public void ToDeleteUser(Users parameter)
        {
            Users addedUser = _userService.AddUser(parameter);
            _userService.DeleteUser(addedUser.UserId);
        }

        [Theory]
        [ClassData(typeof(UserServiceTestTrueTheoryData))]
        public async Task ToDeleteUserAsync(Users parameter)
        {
            Users addedUser = await _userService.AddUserAsync(parameter);
            _userService.DeleteUser(addedUser.UserId);
        }

        [Fact]
        public void ToGetAllUsers_ReturnUsers()
        {
            var result = _userService.GetAllUsers();

            Assert.IsType<List<Users>>(result);
        }

        [Fact]
        public async Task ToGetAllUsersAsync_ReturnUsers()
        {
            var result = await _userService.GetAllUsersAsync();

            Assert.IsType<List<Users>>(result);
        }

        [Theory]
        [InlineData("Enes")]
        public void ToGetAllUsersWithCriteria_ReturnUsers_NameEqualParameter(string name)
        {
            var results = _userService.GetAllUsersWithCriteria(x => x.Name == name);

            Assert.IsType<List<Users>>(results);
            foreach (var item in results)
            {
                Assert.Equal(name, item.Name);
            }
        }

        [Theory]
        [InlineData("Oðuzhan")]
        public void ToGetAllUsersWithCriteria_ReturnEmptyUsers_NameEqualParameter(string name)
        {
            var results = _userService.GetAllUsersWithCriteria(x => x.Name == name);

            Assert.Empty(results);
        }

        [Theory]
        [InlineData("Enes")]
        public async Task ToGetAllUsersWithCriteriaAsync_ReturnUsers_NameEqualParameter(string name)
        {
            var results = await _userService.GetAllUsersWithCriteriaAsync(user => user.Name == name);

            Assert.IsType<List<Users>>(results);
            foreach (var item in results)
            {
                Assert.Equal(name, item.Name);
            }
        }

        [Theory]
        [InlineData("Oðuzhan")]
        public async Task ToGetAllUsersWithCriteriaAsync_ReturnEmptyUsers_NameEqualParameter(string name)
        {
            var results = await _userService.GetAllUsersWithCriteriaAsync(user => user.Name == name);

            Assert.Empty(results);
        }

        [Fact]
        public void ToGetUserById_ReturnUser()
        {
            var result = _userService.GetUserById(new Guid("e6f2f64e-6556-47ad-8ede-372c45e0807b"));

            Assert.IsType<Users>(result);
        }

        [Fact]
        public void ToGetUserById_ReturnNullUser()
        {
            var result = _userService.GetUserById(new Guid());

            Assert.Null(result);
        }

        [Fact]
        public async Task ToGetUserByIdAsync_ReturnUser()
        {
            var results = await _userService.GetUserByIdAsync(new Guid("e6f2f64e-6556-47ad-8ede-372c45e0807b"));

            Assert.IsType<Users>(results);
        }
        
        [Fact]
        public async Task ToGetUserByIdAsync_ReturnNullUser()
        {
            var result = await _userService.GetUserByIdAsync(new Guid());

            Assert.Null(result);
        }

        [Theory]
        [InlineData("Enes")]
        public void ToGetUserWithCriteria_ReturnUser_NameEqualParameter(string name)
        {
            var result = _userService.GetUserWithCriteria(x => x.Name == name);

            Assert.IsType<Users>(result);
            Assert.Equal(name, result.Name);
        }

        [Theory]
        [InlineData("Oðuzhan")]
        public void ToGetUserWithCriteria_ReturnNullUser_NameEqualParameter(string name)
        {
            var result = _userService.GetUserWithCriteria(x => x.Name == name);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("Enes")]
        public async Task ToGetUserWithCriteriaAsync_ReturnUser_NameEqualParameter(string name)
        {
            var result = await _userService.GetUserWithCriteriaAsync(user => user.Name == name);

            Assert.IsType<Users>(result);
            Assert.Equal(name, result.Name);
        }

        [Theory]
        [InlineData("Oðuzhan")]
        public async Task ToGetUserWithCriteriaAsync_ReturnNullUser_NameEqualParameter(string name)
        {
            var result = await _userService.GetUserWithCriteriaAsync(user => user.Name == name);

            Assert.Null(result);
        }

        [Theory]
        [ClassData(typeof(UserServiceTestTrueTheoryData))]
        public void ToUpdateUser(Users parameter)
        {
            Users addedUser = _userService.AddUser(parameter);
            addedUser.Name = "Test Update User Service";

            _userService.UpdateUser(addedUser);
        }
    }
}