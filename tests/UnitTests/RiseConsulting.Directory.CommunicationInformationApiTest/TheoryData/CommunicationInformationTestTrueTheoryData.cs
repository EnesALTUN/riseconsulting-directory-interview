using RiseConsulting.Directory.Entities.Models;
using System;
using Xunit;

namespace RiseConsulting.Directory.CommunicationInformationApiTest.TheoryData
{
    public class CommunicationInformationTestTrueTheoryData : TheoryData<CommunicationInformation>
    {
        public CommunicationInformationTestTrueTheoryData()
        {
            Add(new CommunicationInformation
            {
                PhoneNumber = "050012345678",
                Email = "testemail@hotmail.com",
                Location = "İstanbul",
                Detail = "DetayTest",
                DirectoryUsersId = new Guid("a9a08fd1-3a27-411e-0f77-08d9b2697616"),
                CreatedDate = DateTime.Now
            });
        }
    }
}