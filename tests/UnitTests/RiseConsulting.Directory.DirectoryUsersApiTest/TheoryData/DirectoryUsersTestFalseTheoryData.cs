using RiseConsulting.Directory.Entities.Models;
using System;
using Xunit;

namespace RiseConsulting.Directory.DirectoryUsersApiTest.TheoryData
{
    public class DirectoryUsersTestFalseTheoryData : TheoryData<DirectoryUsers>
    {
        public DirectoryUsersTestFalseTheoryData()
        {
            Add(new DirectoryUsers
            {
                Surname = "Test Surname",
                CompanyId = new Guid("30492dae-8499-404e-a870-a72edbc79b3c"),
                UserId = new Guid("e6f2f64e-6556-47ad-8ede-372c45e0807b"),
                CreatedDate = DateTime.Now
            });
        }
    }
}
