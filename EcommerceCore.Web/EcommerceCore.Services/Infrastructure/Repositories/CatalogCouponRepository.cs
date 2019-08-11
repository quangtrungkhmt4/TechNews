using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public class CatalogCouponRepository: GenericRepository<EcommerceDbContext,CatalogCoupon>, ICatalogCouponRepository
    {
        public CatalogCouponRepository(EcommerceDbContext dbContext) : base(dbContext)
        {

        }
    }
}
