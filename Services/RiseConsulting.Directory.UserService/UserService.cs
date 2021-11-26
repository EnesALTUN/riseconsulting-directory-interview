using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Repository.Infrastructure;
using RiseConsulting.Directory.UserService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.UserService
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<Users> _usersRepository;

        public UserService(IGenericRepository<Users> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Users AddUser(Users obj)
        {
            _usersRepository.Insert(obj);

            _usersRepository.SaveChanges();

            return obj;
        }

        public async Task<Users> AddUserAsync(Users obj)
        {
            await _usersRepository.InsertAsync(obj);

            await _usersRepository.SaveChangesAsync();

            return obj;
        }

        public void DeleteUser(object id)
        {
            _usersRepository.Delete(id);

            _usersRepository.SaveChanges();
        }

        public async Task DeleteUserAsync(object id)
        {
            await _usersRepository.DeleteAsync(id);

            await _usersRepository.SaveChangesAsync();
        }

        public List<Users> GetAllUsers()
        {
            return _usersRepository.GetAll();
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _usersRepository.GetAllAsync();
        }

        public List<Users> GetAllUsersWithCriteria(Expression<Func<Users, bool>> filterExpression)
        {
            return _usersRepository.GetAllWithCriteria(filterExpression);
        }

        public async Task<List<Users>> GetAllUsersWithCriteriaAsync(Expression<Func<Users, bool>> filterExpression)
        {
            return await _usersRepository.GetAllWithCriteriaAsync(filterExpression);
        }

        public Users GetUserById(object id)
        {
            return _usersRepository.GetById(id);
        }

        public async Task<Users> GetUserByIdAsync(object id)
        {
            return await _usersRepository.GetByIdAsync(id);
        }

        public Users GetUserWithCriteria(Expression<Func<Users, bool>> filterExpression)
        {
            return _usersRepository.GetWithCriteria(filterExpression);
        }

        public Task<Users> GetUserWithCriteriaAsync(Expression<Func<Users, bool>> filterExpression)
        {
            return _usersRepository.GetWithCriteriaAsync(filterExpression);
        }

        public void UpdateUser(Users obj)
        {
            _usersRepository.Update(obj);

            _usersRepository.SaveChanges();
        }
    }
}