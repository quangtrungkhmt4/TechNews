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
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HandleError]
        public async Task<ActionResult> Index(DataTableRequest request)
        {
            var manufacturer = await _manufacturerService.GetAll();

            var manufacturerViewModels = Mapper.Map<IEnumerable<ManufacturerViewModel>>(manufacturer);

            ViewBag.manufacturer = manufacturerViewModels;
            return View();
        }
    }
}