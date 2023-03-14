using CarService.Server.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> Get(int id);
        Task<TResult> Get<TResult>(int id, Expression<Func<TEntity, TResult>> projection);
        Task<TResult?> GetNullable<TResult>(int id, Expression<Func<TEntity, TResult>> projection);
        Task<IEnumerable<TEntity>> Get(IEnumerable<int> ids);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> projection);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> projection);
        Task Add(TEntity entity);
        Task Delete(int id);
        Task Delete(IEnumerable<int> ids);
        void Delete(TEntity entity);
    }
}
