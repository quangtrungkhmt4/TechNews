using EcommerceCore.Common.Repository;
using EcommerceCore.Common.Service;
using EcommerceCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Services.Infrastructure.Services
{
    public interface ICatalogService : IEntityService<CatalogCoupon>
    {
        CatalogCoupon GetByCatalogCoupon(Guid CatalogCouponId);
    }
}
