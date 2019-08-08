using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceCore.Common.Service;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Services.Infrastructure.Dto;

namespace EcommerceCore.Services.Infrastructure.Services
{
    public interface IProductService : IEntityService<Product>
    {
        Task<List<ProductDto>> GetProductForDashboard();
    }
}
