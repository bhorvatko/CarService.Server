using CarService.Server.Core.Projections;
using CarService.Server.Domain.Model;
using CarService.Server.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Persistence.MsSql.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly AppDbContext _dbContext;

        protected Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(TEntity entity)
            =>  await _dbContext.Set<TEntity>().AddAsync(entity);

        public async Task<TEntity> Get(int id)
        {
            TEntity? entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                throw new EntityNotFoundException<TEntity>(id);
            }

            _dbContext.Set<TEntity>().Attach(entity);

            return entity;
        }

        protected async Task<TEntity> GetWithQueryable(int id, Func<DbSet<TEntity>, IQueryable<TEntity>> queryableFactory)
        {
            TEntity? entity = await queryableFactory(_dbContext.Set<TEntity>()).FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                throw new EntityNotFoundException<TEntity>(id);
            }

            _dbContext.Set<TEntity>().Attach(entity);

            return entity;
        }

        public async Task<TResult> Get<TResult>(int id, Expression<Func<TEntity, TResult>> projection)
        {
            TResult? result = await _dbContext.Set<TEntity>().Where(e => e.Id == id).Select(projection).FirstOrDefaultAsync();

            if (result == null)
            {
                throw new EntityNotFoundException<TEntity>(id);
            }

            return result;
        }

        public async Task<TResult?> GetNullable<TResult>(int id, Expression<Func<TEntity, TResult>> projection)
        {
            TResult? result = await _dbContext.Set<TEntity>().Where(e => e.Id == id).Select(projection).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<TEntity>> Get(IEnumerable<int> ids)
        {
            IEnumerable<TEntity> result = await _dbContext.Set<TEntity>().Where(e => ids.Contains(e.Id)).ToListAsync();

            if (result.Count() != ids.Count())
            {
                throw new EntityNotFoundException<TEntity>(ids.First(id => !result.Select(r => r.Id).Contains(id)));
            }

            return result;
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
            => await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();

        public async Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> projection)
            => await _dbContext.Set<TEntity>().Where(predicate).Select(projection).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAll()
            => await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> projection)
            => await _dbContext.Set<TEntity>().Select(projection).ToListAsync();

        public async Task Delete(int id)
            => _dbContext.Set<TEntity>().Remove(await Get(id));

        public async Task Delete(IEnumerable<int> ids)
            => _dbContext.Set<TEntity>().RemoveRange(await Get(ids));

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);
    }
}
