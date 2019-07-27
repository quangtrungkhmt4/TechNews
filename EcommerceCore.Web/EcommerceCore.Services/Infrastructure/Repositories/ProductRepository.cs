using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Services.Infrastructure.Dto;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public class ProductRepository: GenericRepository<EcommerceDbContext,Product>, IProductRepository
    {
        public ProductRepository(EcommerceDbContext dbContext) : base(dbContext)
        {

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
    }
}
