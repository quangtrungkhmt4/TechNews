using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using EcommerceCore.Common.Service;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Services.Infrastructure.Services
{
    public interface ICategoryService : IEntityService<Category>
    {
        Category GetByProduct(Guid productId);
        Stream CreateExcelFile(Stream stream = null);
        DataTable ReadFromExcelfile(string path, string sheetName);
        int GetMaxOrderDisplay();

        Task<ICollection<Category>> GetSubCategoryById(Guid id);
    }



}
