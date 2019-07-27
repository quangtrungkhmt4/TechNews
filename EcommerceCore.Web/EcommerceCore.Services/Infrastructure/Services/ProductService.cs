using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceCore.Common.Service;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Services.Infrastructure.Dto;
using EcommerceCore.Services.Infrastructure.Repositories;

namespace EcommerceCore.Services.Infrastructure.Services
{
    public class ProductService: EntityService<Product>, IProductService
    {
        private readonly IProductRepository _ProductRepository;

        public ProductService(IProductRepository ProductRepository) : base(ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }


        public async Task<List<ProductDto>> GetProductForDashboard()
        {
            var products = await _ProductRepository.GetProductForDashboard();
            return products;
        }
    }
}
