using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EcommerceCore.Common.Filter;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Domain.Enums;
using EcommerceCore.Services.Infrastructure.Services;
using EcommerceCore.Websites.Areas.Admin.Models.ViewModels;


namespace EcommerceCore.Websites.Controllers
{    
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HandleError]
        public async Task<ViewResult> Index()
        {
            var categories = await _categoryService.GetAll();

            var categoryViewModels = Mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            return View(categoryViewModels);
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin")]
        public async Task<ViewResult> Create()
        {
            var categories = await _categoryService.GetAll();
            ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            return View();
        }

        [ValidateModelStateFilter]
        [HttpPost]
        public async Task<ActionResult> Create(CategoryViewModel categoryViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = Mapper.Map<Category>(categoryViewModel);

                    category.Status = CommonStatus.Active;
                    await _categoryService.CreateAsync(category, true);
                    return RedirectToAction("Index");
                }

            }
            catch
            {

            }
            var categories = await _categoryService.GetAll();
            ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            return View();
        }

        // GET: Category/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await _categoryService.FindAsyn(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            var categories = await _categoryService.GetAll();
            ViewBag.ParentId = new SelectList(categories, "Id", "Name", category.ParentId);
            var categoryViewModel = Mapper.Map<CategoryViewModel>(category);
            categoryViewModel.CreatedDate = categoryViewModel.CreatedDate;

            return View(categoryViewModel);
        }

        //// POST: Category/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(CategoryViewModel categoryViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Category category = await _categoryService.Find(categoryViewModel.Id);
        //        if (category == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        Mapper.Map(categoryViewModel, category);
        //        category.UpdatedDate = DateTime.Now;
        //        await _categoryService.UpdateAsync(category, categoryViewModel.Id, true);
        //        return RedirectToAction("Index");
        //    }
        //    var categories = await _categoryService.GetAll();
        //  //  ViewBag.ParentId = new SelectList(categories, "Id", "Name", categoryViewModel.ParentId);
        //    return View(categoryViewModel);
        //}
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await _categoryService.FindAsyn(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            var categoryViewModel = Mapper.Map<CategoryViewModel>(category);
            return View(categoryViewModel);
        }

        // GET: Category/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            var category = await _categoryService.FindAsyn(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            var categoryViewModel = Mapper.Map<CategoryViewModel>(category);
            return View(categoryViewModel);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var category = await _categoryService.FindAsyn(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                await _categoryService.DeleteAsync(category, true);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


    }
}
