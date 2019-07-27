using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        int GetMaxOrderDisplay();
        Task<List<Category>> GetSubCategoryById(Guid id);
        Task<List<Category>> GetCategories();
    }
}
