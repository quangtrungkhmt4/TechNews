using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public class CouponRepository: GenericRepository<EcommerceDbContext,Coupon>, ICouponRepository
    {
        public CouponRepository(EcommerceDbContext dbContext) : base(dbContext)
        {

        }
    }
}
