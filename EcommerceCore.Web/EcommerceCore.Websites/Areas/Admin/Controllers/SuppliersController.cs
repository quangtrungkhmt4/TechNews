using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EcommerceCore.Services.Infrastructure.Services;
using EcommerceCore.Websites.Areas.Admin.Models.PagingDto;
using CategoryViewModel = EcommerceCore.Websites.Areas.Admin.Models.ViewModels.CategoryViewModel;

namespace EcommerceCore.Websites.Areas.Admin.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISuppliersController _categoryService;

        public SuppliersController(ISuppliersController categoryService)
        {
            _categoryService = categoryService;
        }

        [HandleError]
        public async Task<ActionResult> Index(DataTableRequest request)
        {
            var categories = await _categoryService.GetAll();

            var categoryViewModels = Mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            ViewBag.Categories = categoryViewModels;
            return View();
        }

        [HttpGet]
        public ActionResult Export()
        {
            var stream = _categoryService.CreateExcelFile();

            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // hiện Save As dialog
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=ExcelDemo.xlsx");
            // Lưu file excel 
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ReadFromExcel()
        {
            var data = _categoryService.ReadFromExcelfile(@"D:\ExcelDemo.xlsx", "First Sheet");
            return RedirectToAction("Index");
        }


    }
}