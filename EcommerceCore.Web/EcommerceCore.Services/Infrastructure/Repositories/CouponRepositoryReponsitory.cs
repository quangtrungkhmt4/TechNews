using EcommerceCore.Common.Repository;
using EcommerceCore.Domain;
using EcommerceCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
   public class CouponRepository : GenericRepository<EcommerceDbContext,Coupon>,ICouponRespository
    {
        public CouponRepository(EcommerceDbContext dbContext): base(dbContext)
        {

        }
    }
}
