using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        int GetMaxOrderDisplay();
        //List<Category> GetSubCategoryById(Guid id);
    }
}
