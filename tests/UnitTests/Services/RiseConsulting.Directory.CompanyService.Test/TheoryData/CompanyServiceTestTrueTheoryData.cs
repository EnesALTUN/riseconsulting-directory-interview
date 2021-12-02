using RiseConsulting.Directory.Entities.Models;
using System;
using Xunit;

namespace RiseConsulting.Directory.CompanyService.Test.TheoryData
{
    public class CompanyServiceTestTrueTheoryData : TheoryData<Company>
    {
        public CompanyServiceTestTrueTheoryData()
        {
            Add(new Company
            {
                Name = "Test Company Name",
                CreatedDate = DateTime.Now
            });
        }
    }
}