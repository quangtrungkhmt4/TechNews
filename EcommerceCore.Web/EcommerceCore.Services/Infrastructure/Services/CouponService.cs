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
    public class CouponService: EntityService<Coupon>, ICouponService
    {
        private readonly ICouponRepository _CouponRepository;

        public CouponService(ICouponRepository CouponRepository) : base(CouponRepository)
        {
            _CouponRepository = CouponRepository;
        }

        
    }
}
