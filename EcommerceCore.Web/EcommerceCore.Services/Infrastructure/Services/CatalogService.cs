using EcommerceCore.Common.Service;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Services.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Services.Infrastructure.Services
{
   public class CatalogService : EntityService<CatalogCoupon>, ICatalogService
    {
        public readonly ICatalogCouponReponsitory _icatalogcouponeeponsitory;
        public CatalogService(ICatalogCouponReponsitory catalogCouponReponsitory) : base(catalogCouponReponsitory)
        {
            catalogCouponReponsitory = _icatalogcouponeeponsitory;
        }

        public CatalogCoupon GetByCatalogCoupon(Guid CatalogCouponId)
        {
            throw new NotImplementedException();
        }
    }
}
