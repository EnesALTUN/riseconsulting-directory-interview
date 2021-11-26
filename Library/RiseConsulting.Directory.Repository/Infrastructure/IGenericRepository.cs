using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.Repository.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        TEntity GetWithCriteria(Expression<Func<TEntity, bool>> filterExpression);
        Task<TEntity> GetWithCriteriaAsync(Expression<Func<TEntity, bool>> filterExpression);
        List<TEntity> GetAllWithCriteria(Expression<Func<TEntity, bool>> filterExpression);
        Task<List<TEntity>> GetAllWithCriteriaAsync(Expression<Func<TEntity, bool>> filterExpression);
        void Insert(TEntity obj);
        Task InsertAsync(TEntity obj);
        void Update(TEntity obj);
        void Delete(object id);
        Task DeleteAsync(object id);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}