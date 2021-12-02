using RiseConsulting.Directory.Entities.Models;
using System;
using Xunit;

namespace RiseConsulting.Directory.CommunicationInformationApiTest.TheoryData
{
    public class CommunicationInformationTestFalseTheoryData : TheoryData<CommunicationInformation>
    {
        public CommunicationInformationTestFalseTheoryData()
        {
            Add(new CommunicationInformation
            {
                Email = "testemail@hotmail.com",
                Location = "İstanbul",
                Detail = "DetayTest",
                DirectoryUsersId = new Guid("a9a08fd1-3a27-411e-0f77-08d9b2697616"),
                CreatedDate = DateTime.Now
            });
        }
    }
}
