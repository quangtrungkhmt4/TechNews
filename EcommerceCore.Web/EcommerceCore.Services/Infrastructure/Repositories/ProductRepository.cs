using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Domain.Enums;
using EcommerceCore.Services.Infrastructure.Dto;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public class ProductRepository: GenericRepository<EcommerceDbContext,Product>, IProductRepository
    {
        private readonly ICategoryRepository _categoryRepository;        
        public ProductRepository(EcommerceDbContext dbContext, ICategoryRepository categoryRepository) 
            : base(dbContext)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ProductDto>> GetProductForDashboard()
        {
            var products = await (from p in DbContext.Products
                join s in DbContext.Suppliers on p.SupplierId equals s.Id into pst
                from ps in pst.DefaultIfEmpty()
                orderby p.CreatedBy descending
                select new ProductDto()
                {
                    Name = p.Title,
                    Price = p.Price,
                    View = p.View,
                    PublicationDate = p.PublicationDate ?? null,
                    SupplierName = ps != null ? ps.Name : "",
                    Status = p.Status
                }).Take(10).ToListAsync();

            return products;
        }

        public async Task CreateProductWithCategoryNotExists(Guid categoryId)
        {
            // Step 1: Check category exitsts
            var result = _categoryRepository.Exist(c => c.Id == categoryId);
            // Step2: Insert category if not exitst
            if (result == null)
            {
                var categoryNew = new Category
                {
                    Name = "Khanh nhac hoc",
                    Description = "Khanh sai roi",
                    IsShowHomePage = true,
                    OrderDisplay = _categoryRepository.GetMaxOrderDisplay(),
                    Status = CommonStatus.Active,
                    IsDeleted = false
                };
                await _categoryRepository.CreateAsync(categoryNew, false);

                // Step 3: Then insert product

                var productNew = new Product
                {
                   Title = "Vay cham bi",
                    Description = "...",
                    CategoryId = categoryNew.Id,
                    ManufacturerId = null,
                    SupplierId = null,
                    ProductPrices = null,
                    Price = 300
                };

                await CreateAsync(productNew, true);
                
            }


        }
    }
}
