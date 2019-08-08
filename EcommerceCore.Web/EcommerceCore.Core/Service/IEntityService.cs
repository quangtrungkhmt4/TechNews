using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Common.Service
{
    public interface IEntityService<TEntity> :  IService 
        where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Find(Guid id);
        Task<ICollection<TEntity>> GetAllAsyn();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<bool> Exist(Expression<Func<TEntity, bool>> spec = null);
        Task<int> Count(Expression<Func<TEntity, bool>> spec = null);
        Task CreateAsync(TEntity entity, bool isSaved = false);
        Task DeleteAsync(TEntity entity, bool isSaved = false);        
        Task UpdateAsync(TEntity entity, object key,bool isSaved = false);
    }
}
