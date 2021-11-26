using RiseConsulting.Directory.CompanyService.Infrastructure;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly IGenericRepository<Company> _companyRepository;

        public CompanyService(IGenericRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Company AddCompany(Company obj)
        {
            _companyRepository.Insert(obj);

            _companyRepository.SaveChanges();

            return obj;
        }

        public async Task<Company> AddCompanyAsync(Company obj)
        {
            await _companyRepository.InsertAsync(obj);

            await _companyRepository.SaveChangesAsync();

            return obj;
        }

        public void DeleteCompany(object id)
        {
            _companyRepository.Delete(id);

            _companyRepository.SaveChanges();
        }

        public async Task DeleteCompanyAsync(object id)
        {
            await _companyRepository.DeleteAsync(id);

            await _companyRepository.SaveChangesAsync();
        }

        public List<Company> GetAllCompany()
        {
            return _companyRepository.GetAll();
        }

        public Task<List<Company>> GetAllCompanyAsync()
        {
            return _companyRepository.GetAllAsync();
        }

        public List<Company> GetAllCompanyWithCriteria(Expression<Func<Company, bool>> filterExpression)
        {
            return _companyRepository.GetAllWithCriteria(filterExpression);
        }

        public async Task<List<Company>> GetAllCompanyWithCriteriaAsync(Expression<Func<Company, bool>> filterExpression)
        {
            return await _companyRepository.GetAllWithCriteriaAsync(filterExpression);
        }

        public Company GetCompanyById(object id)
        {
            return _companyRepository.GetById(id);
        }

        public Task<Company> GetCompanyByIdAsync(object id)
        {
            return _companyRepository.GetByIdAsync(id);
        }

        public Company GetCompanyWithCriteria(Expression<Func<Company, bool>> filterExpression)
        {
            return _companyRepository.GetWithCriteria(filterExpression);
        }

        public async Task<Company> GetCompanyWithCriteriaAsync(Expression<Func<Company, bool>> filterExpression)
        {
            return await _companyRepository.GetWithCriteriaAsync(filterExpression);
        }

        public void UpdateCompany(Company obj)
        {
            _companyRepository.Update(obj);

            _companyRepository.SaveChanges();
        }
    }
}