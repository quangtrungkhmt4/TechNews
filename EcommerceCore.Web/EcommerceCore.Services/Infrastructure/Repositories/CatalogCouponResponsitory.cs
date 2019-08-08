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
   public class CatalogCouponResponsitory : GenericRepository<EcommerceDbContext,CatalogCoupon>,ICatalogCouponReponsitory
    {
        public CatalogCouponResponsitory(EcommerceDbContext dbContext) : base(dbContext)
        {

        }
    }
}
