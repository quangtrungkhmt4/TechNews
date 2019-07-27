using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EcommerceCore.Common.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region CRUD

        void SaveChanges();
        void Create(TEntity t);
        void Update(TEntity t, object key);
        void Delete(TEntity entity);
        Task UpdateAsyn(TEntity t, object key, bool isSaved = true);
        Task SaveChangesAsync();
        Task DeleteAsyn(TEntity entity, bool isSaved = true);
        Task CreateAsync(TEntity t, bool isSaved = true);

        #endregion

        #region Gets
        TEntity Find(Expression<Func<TEntity, bool>> match);
        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(Guid id);
        Task<TEntity> GetAsync(Guid id);
        IQueryable<TEntity> GetAll();
        Task<ICollection<TEntity>> GetAllAsyn();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
       
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null);

        #endregion

        Task<bool> Exist(Expression<Func<TEntity, bool>> spec = null);
        Task<int> Count(Expression<Func<TEntity, bool>> spec = null);
        void Dispose();


    }
}
