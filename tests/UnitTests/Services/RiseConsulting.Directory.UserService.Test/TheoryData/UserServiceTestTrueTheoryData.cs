using RiseConsulting.Directory.Entities.Models;
using System;
using Xunit;

namespace RiseConsulting.Directory.UserService.Test.TheoryData
{
    public class UserServiceTestTrueTheoryData : TheoryData<Users>
    {
        public UserServiceTestTrueTheoryData()
        {
            Add(new Users
            {
                Name = "Test Name",
                Surname = "Test Surname",
                Username = "Test Username",
                Password = "Test Password",
                CreatedDate = DateTime.Now
            });
        }
    }
}