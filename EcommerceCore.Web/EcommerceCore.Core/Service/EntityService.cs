using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Common.Service
{
    public abstract class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : BaseEntity
    {
        private const string ParamNull = "Entity input can't null";
        private readonly IGenericRepository<TEntity> _repository;
        public EntityService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repository.GetAllAsyn();
        }


        public async Task<TEntity> Find(Guid id)
        {
            return await _repository.FindAsync(s => s.Id == id);
        }

        public async Task<ICollection<TEntity>> GetAllAsyn()
        {
            return await _repository.GetAllAsyn();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.GetAllIncluding(includeProperties);
        }

        public async Task<bool> Exist(Expression<Func<TEntity, bool>> spec = null)
        {
            return await _repository.Exist(spec);
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> spec = null)
        {
            return await _repository.Count(spec);
        }

        public virtual async Task CreateAsync(TEntity entity, bool isSaved = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(ParamNull);
            }
            await _repository.CreateAsync(entity, isSaved);
        }

        public virtual async Task DeleteAsync(TEntity entity, bool isSaved = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(ParamNull);
            }
            await _repository.DeleteAsyn(entity, isSaved);
        }

        public virtual async Task UpdateAsync(TEntity entity, object key, bool isSaved = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(ParamNull);
            }
            await _repository.UpdateAsyn(entity, key, isSaved);
        }
    }
}
