using RiseConsulting.Directory.DirectoryUsersService.Infrastructure;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.DirectoryUsersService
{
    public class DirectoryUsersService : IDirectoryUsersService
    {
        private readonly IGenericRepository<DirectoryUsers> _directoryUsersRepository;

        public DirectoryUsersService(IGenericRepository<DirectoryUsers> directoryUsersRepository)
        {
            _directoryUsersRepository = directoryUsersRepository;
        }

        public DirectoryUsers AddDirectoryUser(DirectoryUsers obj)
        {
            _directoryUsersRepository.Insert(obj);

            _directoryUsersRepository.SaveChanges();

            return obj;
        }

        public async Task<DirectoryUsers> AddDirectoryUserAsync(DirectoryUsers obj)
        {
            await _directoryUsersRepository.InsertAsync(obj);

            await _directoryUsersRepository.SaveChangesAsync();

            return obj;
        }

        public void DeleteDirectoryUser(object id)
        {
            _directoryUsersRepository.Delete(id);

            _directoryUsersRepository.SaveChanges();
        }

        public async Task DeleteDirectoryUserAsync(object id)
        {
            await _directoryUsersRepository.DeleteAsync(id);

            await _directoryUsersRepository.SaveChangesAsync();
        }

        public List<DirectoryUsers> GetAllDirectoryUsers()
        {
            return _directoryUsersRepository.GetAll();
        }

        public Task<List<DirectoryUsers>> GetAllDirectoryUsersAsync()
        {
            return _directoryUsersRepository.GetAllAsync();
        }

        public List<DirectoryUsers> GetAllDirectoryUsersWithCriteria(Expression<Func<DirectoryUsers, bool>> filterExpression)
        {
            return _directoryUsersRepository.GetAllWithCriteria(filterExpression);
        }

        public async Task<List<DirectoryUsers>> GetAllDirectoryUsersWithCriteriaAsync(Expression<Func<DirectoryUsers, bool>> filterExpression)
        {
            return await _directoryUsersRepository.GetAllWithCriteriaAsync(filterExpression);
        }

        public DirectoryUsers GetDirectoryUserById(object id)
        {
            return _directoryUsersRepository.GetById(id);
        }

        public async Task<DirectoryUsers> GetDirectoryUserByIdAsync(object id)
        {
            return await _directoryUsersRepository.GetByIdAsync(id);
        }

        public DirectoryUsers GetDirectoryUserWithCriteria(Expression<Func<DirectoryUsers, bool>> filterExpression)
        {
            return _directoryUsersRepository.GetWithCriteria(filterExpression);
        }

        public async Task<DirectoryUsers> GetDirectoryUserWithCriteriaAsync(Expression<Func<DirectoryUsers, bool>> filterExpression)
        {
            return await _directoryUsersRepository.GetWithCriteriaAsync(filterExpression);
        }

        public void UpdateDirectoryUser(DirectoryUsers obj)
        {
            _directoryUsersRepository.Update(obj);

            _directoryUsersRepository.SaveChanges();
        }
    }
}