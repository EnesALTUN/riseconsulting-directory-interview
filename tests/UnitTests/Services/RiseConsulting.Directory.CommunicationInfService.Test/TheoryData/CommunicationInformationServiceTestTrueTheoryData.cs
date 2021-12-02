using RiseConsulting.Directory.Entities.Models;
using System;
using Xunit;

namespace RiseConsulting.Directory.CommunicationInfService.Test.TheoryData
{
    public class CommunicationInformationServiceTestTrueTheoryData : TheoryData<CommunicationInformation>
    {
        public CommunicationInformationServiceTestTrueTheoryData()
        {
            Add(new CommunicationInformation
            {
                PhoneNumber = "05001234567",
                Email = "testemail@hotmail.com",
                Location = "Test Location",
                Detail = "Test Detay",
                DirectoryUsersId = new Guid("a9a08fd1-3a27-411e-0f77-08d9b2697616"),
                CreatedDate = DateTime.Now
            });
        }
    }
}