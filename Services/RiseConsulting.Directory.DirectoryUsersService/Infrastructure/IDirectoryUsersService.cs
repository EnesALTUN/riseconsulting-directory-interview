using RiseConsulting.Directory.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.DirectoryUsersService.Infrastructure
{
    public interface IDirectoryUsersService
    {
        List<DirectoryUsers> GetAllDirectoryUsers();

        Task<List<DirectoryUsers>> GetAllDirectoryUsersAsync();

        DirectoryUsers GetDirectoryUserById(object id);

        Task<DirectoryUsers> GetDirectoryUserByIdAsync(object id);

        DirectoryUsers GetDirectoryUserWithCriteria(Expression<Func<DirectoryUsers, bool>> filterExpression);

        Task<DirectoryUsers> GetDirectoryUserWithCriteriaAsync(Expression<Func<DirectoryUsers, bool>> filterExpression);

        List<DirectoryUsers> GetAllDirectoryUsersWithCriteria(Expression<Func<DirectoryUsers, bool>> filterExpression);

        Task<List<DirectoryUsers>> GetAllDirectoryUsersWithCriteriaAsync(Expression<Func<DirectoryUsers, bool>> filterExpression);

        DirectoryUsers AddDirectoryUser(DirectoryUsers obj);

        Task<DirectoryUsers> AddDirectoryUserAsync(DirectoryUsers obj);

        void UpdateDirectoryUser(DirectoryUsers obj);

        void DeleteDirectoryUser(object id);

        Task DeleteDirectoryUserAsync(object id);
    }
}