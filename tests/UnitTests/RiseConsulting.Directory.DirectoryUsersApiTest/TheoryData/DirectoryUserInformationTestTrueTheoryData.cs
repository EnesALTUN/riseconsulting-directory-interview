using RiseConsulting.Directory.Entities.Models;
using System;
using Xunit;

namespace RiseConsulting.Directory.DirectoryUsersApiTest.TheoryData
{
    public class DirectoryUserInformationTestTrueTheoryData : TheoryData<Guid, Guid, CommunicationInformation>
    {
        public DirectoryUserInformationTestTrueTheoryData()
        {
            // userId, directoryUserId, CommunicationInformation
            Add(
                new Guid("e6f2f64e-6556-47ad-8ede-372c45e0807b"),
                new Guid("86ea3032-be77-4fa3-9db1-d0de8a4fafcc"),
                new CommunicationInformation
                {
                    PhoneNumber = "05001234567",
                    Email = "testemail@hotmail.com",
                    Location = "Test Location",
                    Detail = "Test Detay",
                    DirectoryUsersId = new Guid("86ea3032-be77-4fa3-9db1-d0de8a4fafcc"),
                    CreatedDate = DateTime.Now
                });
        }
    }
}