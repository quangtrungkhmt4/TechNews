using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Domain.Enums;
using EcommerceCore.Services.Infrastructure.Services;
using EcommerceCore.Websites.Areas.Admin.Models.PagingDto;
using EcommerceCore.Websites.Areas.Admin.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace EcommerceCore.Websites.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HandleError]
        public async Task<ActionResult> Index(DataTableRequest request)
        {

            var categories = await _categoryService.GetCategories();

            var categoryViewModels = Mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            ViewBag.Categories = categoryViewModels;
            return View();
        }


        [HttpGet]
        public async Task<PartialViewResult> CreateOrEdit(Guid? id)
        {
            // Tạo ra 1 object category mặc định cho insert
            var model = new CategoryViewModel()
            {
                Id = null,
                Description = string.Empty,
                Name = string.Empty,
                OrderDisplay = 0,
                ParentId = Guid.NewGuid(),
                Status = CommonStatus.Active,
                IsShowHomePage = true
            };

            // Case Update

            if (id != null)
            {
                var entity = await _categoryService.Find(id.Value);
                if (entity != null)
                {
                    model = Mapper.Map<CategoryViewModel>(entity);
                }
            }

            ViewBag.categoryParent = await _categoryService.GetAllAsyn();

            return PartialView(model);
        }


        //[Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> CreateOrEdit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    result = false,
                    Errors =
                    ModelState.Keys.Where(
                        p => ModelState[p].Errors.Any(e => string.IsNullOrEmpty(e.ErrorMessage) == false)).
                        Select(p => new
                        {
                            field = p,
                            msgs = ModelState[p].Errors.Select(e => e.ErrorMessage)
                        })
                });
            }

            model.CreatedDate = DateTime.Now;
            
            if (model.OrderDisplay <= 0)
            {
                model.OrderDisplay = _categoryService.GetMaxOrderDisplay() + 1;
            }           
                //var cate = await _categoryService.Find(model.Id.Value);

           
            try
            {
                if (model.Id.HasValue)
                {
                    var cate = Mapper.Map<CategoryViewModel, Category>(model);
                    await _categoryService.UpdateAsync(cate, model.Id, true);
                }
                // Case insert
                else
                {
                    model.Id = Guid.NewGuid();
                    var cate = Mapper.Map<CategoryViewModel, Category>(model);
                    await _categoryService.CreateAsync(cate, true);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    result = false,
                    Errors = ex.Data
                });
            }
            return Json(new { result = true });
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