using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Common.Repository
{
    public abstract class GenericRepository<TContext, TEntity> : IGenericRepository<TEntity>
         where TEntity : BaseEntity
         where TContext : DbContext
    {
        private const string ParamNull = "Entity input can't null";
        protected readonly TContext DbContext;
        public GenericRepository(TContext dbContext)
        {
            DbContext = dbContext;
        }      
        public IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>();
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsyn()
        {

            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public virtual TEntity Get(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }              

        public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return DbContext.Set<TEntity>().SingleOrDefault(match);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await DbContext.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return DbContext.Set<TEntity>().Where(match).ToList();
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await DbContext.Set<TEntity>().Where(match).ToListAsync();
        }

        public Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            try
            {
                IQueryable<TEntity> query = DbContext.Set<TEntity>().AsNoTracking();
                query = query.Where(predicate);
                if (orderBy != null) query = orderBy(query);
                return query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Exist(Expression<Func<TEntity, bool>> spec = null)
        {
            return await (spec == null ? DbContext.Set<TEntity>().AnyAsync() : DbContext.Set<TEntity>().AnyAsync(spec));
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> spec = null)
        {
            return await (spec == null ? DbContext.Set<TEntity>().CountAsync() : DbContext.Set<TEntity>().CountAsync(spec));
        }
            
        #region CRUD
        public virtual void Create(TEntity t)
        {
            if (t == null)
            {
                throw new ArgumentNullException(ParamNull);
            }
            DbContext.Set<TEntity>().Add(t);
            DbContext.SaveChanges();
        }

        public virtual void Update(TEntity t, object key)
        {
            if (t == null)
            {
                throw new ArgumentNullException(ParamNull);
            }
            TEntity exist = DbContext.Set<TEntity>().Find(key);
            if (exist != null)
            {
                DbContext.Entry(exist).CurrentValues.SetValues(t);
                DbContext.SaveChanges();
            }
        }

        public virtual void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            DbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region CRUD async

        public virtual async Task DeleteAsyn(TEntity entity, bool isSaved = false)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(ParamNull);
                }
                DbContext.Set<TEntity>().Remove(entity);
                if (isSaved) await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task CreateAsync(TEntity t, bool isSaved = false)
        {
            if (t == null)
            {
                throw new ArgumentNullException(ParamNull);
            }
            DbContext.Set<TEntity>().Add(t);
            if (isSaved) await DbContext.SaveChangesAsync();
        }
        public virtual async Task UpdateAsyn(TEntity t, object key, bool isSaved = false)
        {
            if (t == null)
            {
                throw new ArgumentNullException(ParamNull);
            }
            TEntity exist = await DbContext.Set<TEntity>().FindAsync(key);
            if (exist != null)
            {
                DbContext.Entry(exist).CurrentValues.SetValues(t);
                if (isSaved) await DbContext.SaveChangesAsync();
            }            
        }
        public virtual async Task SaveChangesAsync()
        {
            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {

            IQueryable<TEntity> queryable = GetAll();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<TEntity, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
