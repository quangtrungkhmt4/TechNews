using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceCore.Common.Service;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Services.Infrastructure.Repositories;

namespace EcommerceCore.Services.Infrastructure.Services
{
    public class CatalogCouponService: EntityService<CatalogCoupon>, ICatalogCouponService
    {
        private readonly ICatalogCouponRepository _catalogCouponRepository;

        public CatalogCouponService(ICatalogCouponRepository catalogCouponRepository) : base(catalogCouponRepository)
        {
            _catalogCouponRepository = catalogCouponRepository;
        }

       
    }
}
