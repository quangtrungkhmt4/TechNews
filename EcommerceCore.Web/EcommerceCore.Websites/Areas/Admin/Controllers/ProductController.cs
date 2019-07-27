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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HandleError]
        public async Task<ActionResult> Index(DataTableRequest request)
        {
            var products = await _productService.GetAll();

            var productViewModels = Mapper.Map<IEnumerable<ProductViewModel>>(products);

            ViewBag.products = productViewModels;
            return View();
        }
    }
}