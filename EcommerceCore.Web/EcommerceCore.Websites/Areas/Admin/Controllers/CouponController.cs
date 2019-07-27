using AutoMapper;
using EcommerceCore.Services.Infrastructure.Services;
using EcommerceCore.Websites.Areas.Admin.Models.PagingDto;
using EcommerceCore.Websites.Areas.Admin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EcommerceCore.Websites.Areas.Admin.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HandleError]
        public async Task<ActionResult> Index(DataTableRequest request)
        {
            var coupon = await _couponService.GetAll();

            var couponViewModels = Mapper.Map<IEnumerable<CouponViewModel>>(coupon);

            ViewBag.Coupon = couponViewModels;
            return View();
        }
    }
}