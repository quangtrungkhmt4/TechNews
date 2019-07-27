using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EcommerceCore.Services.Infrastructure.Services;
using EcommerceCore.Websites.Areas.Admin.Models.PagingDto;
using SupplierViewModel = EcommerceCore.Websites.Areas.Admin.Models.ViewModels.SupplierViewModel;

namespace EcommerceCore.Websites.Areas.Admin.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HandleError]
        public async Task<ActionResult> Index(DataTableRequest request)
        {
            var suppliers = await _supplierService.GetAll();

            var supplierViewModels = Mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);

            ViewBag.Suppliers = supplierViewModels;
            return View();
        }
    }
}