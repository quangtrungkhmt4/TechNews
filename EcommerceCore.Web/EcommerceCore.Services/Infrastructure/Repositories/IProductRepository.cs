using EcommerceCore.Common.Repository;
using EcommerceCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceCore.Services.Infrastructure.Dto;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<ProductDto>> GetProductForDashboard();
    }
}
