using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain;
using EcommerceCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public class CategoryRepository: GenericRepository<EcommerceDbContext,Category>, ICategoryRepository
    {
        private readonly EcommerceDbContext _context;
        public CategoryRepository(EcommerceDbContext dbContext) : base(dbContext)
        {

        }

        public int GetMaxOrderDisplay()
        {
            return DbContext.Categories.Max(o => o.OrderDisplay) + 1;
        }


        //public async Task<List<Category>> GetSubCategoryById(Guid id)
        //{
        //    return await DbContext.Categories.Where(o => o.ParentId == id).ToListAsync();
        //}

        
    }
}
