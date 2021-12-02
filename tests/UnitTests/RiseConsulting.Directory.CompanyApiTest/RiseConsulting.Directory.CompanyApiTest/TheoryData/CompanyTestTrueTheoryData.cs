using RiseConsulting.Directory.Entities.Models;
using System;
using Xunit;

namespace RiseConsulting.Directory.CompanyApiTest.TheoryData
{
    public class CompanyTestTrueTheoryData : TheoryData<Company>
    {
        public CompanyTestTrueTheoryData()
        {
            Add(new Company
            {
                Name = "Test Company Name 1",
                CreatedDate = DateTime.Now
            });
        }
    }
}