using RiseConsulting.Directory.Entities.Models;
using System;
using Xunit;

namespace RiseConsulting.Directory.CompanyApiTest.TheoryData
{
    public class CompanyTestFalseTheoryData : TheoryData<Company>
    {
        public CompanyTestFalseTheoryData()
        {
            Add(new Company
            {
                CreatedDate = DateTime.UtcNow
            });
        }
    }
}
