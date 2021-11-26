using RiseConsulting.Directory.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.UserService.Infrastructure
{
    public interface IUserService
    {
        List<Users> GetAllUsers();
        Task<List<Users>> GetAllUsersAsync();
        Users GetUserById(object id);
        Task<Users> GetUserByIdAsync(object id);
        Users GetUserWithCriteria(Expression<Func<Users, bool>> filterExpression);
        Task<Users> GetUserWithCriteriaAsync(Expression<Func<Users, bool>> filterExpression);
        List<Users> GetAllUsersWithCriteria(Expression<Func<Users, bool>> filterExpression);
        Task<List<Users>> GetAllUsersWithCriteriaAsync(Expression<Func<Users, bool>> filterExpression);
        Users AddUser(Users obj);
        Task<Users> AddUserAsync(Users obj);
        void UpdateUser(Users obj);
        void DeleteUser(object id);
        Task DeleteUserAsync(object id);
    }
}