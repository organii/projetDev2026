using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using AgileAi.Data.Context;
using AgileAi.Domain.Interfaces;

namespace AgileAi.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext context;
        private DbSet<TEntity> table = null;

        #region Constructor
        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            table = this.context.Set<TEntity>();
        }
        #endregion

        #region Create Funtion
        public TEntity Add(TEntity entity)
        {
            try
            {
                table.Add(entity);
                context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        #endregion

        #region Read Function
        public TEntity Get(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            try
            {
                IQueryable<TEntity> query = table;

                if (includes != null)
                {
                    query = includes(query);
                }
                if (condition != null)
                    return query.FirstOrDefault(condition);
                else
                    return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            try
            {
                
                IQueryable<TEntity> query = table;

                if (includes != null)
                {
                    query = includes(query);
                }
                if (condition != null)
                    return query.Where(condition).ToList();

                else
                    return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
        #endregion

        #region Update Function
        public TEntity Put(TEntity entity)
        {
            try
            {
                table.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
                return entity;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
        #endregion

        #region Remove Function
        public TEntity Remove(Guid id)
        {
            try
            {
                TEntity entity = table.Find(id);
                if (entity == null)
                    return null;
                else
                {
                    table.Remove(entity);
                    context.SaveChanges();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        #endregion
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
    }
}