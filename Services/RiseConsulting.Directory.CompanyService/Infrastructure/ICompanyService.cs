using RiseConsulting.Directory.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.CompanyService.Infrastructure
{
    public interface ICompanyService
    {
        List<Company> GetAllCompany();
        Task<List<Company>> GetAllCompanyAsync();
        Company GetCompanyById(object id);
        Task<Company> GetCompanyByIdAsync(object id);
        Company GetCompanyWithCriteria(Expression<Func<Company, bool>> filterExpression);
        Task<Company> GetCompanyWithCriteriaAsync(Expression<Func<Company, bool>> filterExpression);
        List<Company> GetAllCompanyWithCriteria(Expression<Func<Company, bool>> filterExpression);
        Task<List<Company>> GetAllCompanyWithCriteriaAsync(Expression<Func<Company, bool>> filterExpression);
        Company AddCompany(Company obj);
        Task<Company> AddCompanyAsync(Company obj);
        void UpdateCompany(Company obj);
        void DeleteCompany(object id);
        Task DeleteCompanyAsync(object id);
    }
}