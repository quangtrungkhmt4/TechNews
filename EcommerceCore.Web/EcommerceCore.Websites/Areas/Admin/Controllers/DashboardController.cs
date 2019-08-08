using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EcommerceCore.Services.Infrastructure.Services;
using Microsoft.AspNet.Identity;

namespace EcommerceCore.Websites.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICatalogCouponService _catalogCouponService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public DashboardController(ICatalogCouponService catalogCouponService, ICategoryService categoryService,
            IProductService productService)        
        {
            _catalogCouponService = catalogCouponService;
            _categoryService = categoryService;
            _productService = productService;
        }


        // GET: Admin/Dashboard
        public async Task<ActionResult> Index()
        {
            // Get user id login 
            ViewBag.UserId = User.Identity.GetUserId();
            // Get user name
            ViewBag.UserName = User.Identity.GetUserName();
            //var name = User.Identity.Name;
            //// Check authen of user login
            //var IsAuthenticated = User.Identity.IsAuthenticated;
            //// Check role
            //var isAdmin = User.IsInRole("Admin");
            ViewBag.totalProduct = await _productService.Count();
            ViewBag.totalCategory = await _categoryService.Count();
            ViewBag.totalCoupon = await _catalogCouponService.Count();
            ViewBag.totalOrder = 100;

            // Get product
            var products = await _productService.GetProductForDashboard();
            ViewBag.Products = products;
            return View();
        }
    }
}