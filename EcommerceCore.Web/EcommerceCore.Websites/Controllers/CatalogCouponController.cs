using AutoMapper;
using EcommerceCore.Common.Filter;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Domain.Enums;
using EcommerceCore.Services.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using EcommerceCore.Websites.Models.ViewModels;

namespace EcommerceCore.Web.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class CatalogCouponController : Controller
    {
        private readonly ICatalogCouponService _catalogCouponService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CatalogCouponController
        (
            ICatalogCouponService catalogCouponService,
            ICategoryService categoryService,
            IProductService productService
        )
        {
            _catalogCouponService = catalogCouponService;
            _categoryService = categoryService;
            _productService = productService;
        }

        [HandleError]
        public async Task<ViewResult> Index()
        {
            var catalogCoupons = await _catalogCouponService.GetAll();

            var catalogCouponViewModels = Mapper.Map<IEnumerable<CatalogCouponViewModel>>(catalogCoupons);

            return View(catalogCouponViewModels);
        }

        // GET: CatalogCoupon/Create
        [Authorize(Roles = "Admin")]
        public async Task<ViewResult> Create()
        {
            var categories = await _categoryService.GetAll();
            var products = await _productService.GetAll();

            ViewBag.ApplyForCategory = new SelectList(categories, "Id", "Name");
            ViewBag.ApplyForProduct = new SelectList(products, "Id", "Title");
            return View();
        }

        [ValidateModelStateFilter]
        [HttpPost]
        public async Task<ActionResult> Create(CatalogCouponViewModel catalogCouponViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var catalogCoupon = Mapper.Map<CatalogCoupon>(catalogCouponViewModel);

                    catalogCoupon.Status = CommonStatus.Active;
                    await _catalogCouponService.CreateAsync(catalogCoupon, true);
                    return RedirectToAction("Index");
                }

            }
            catch
            {

            }
            var categories = await _categoryService.GetAll();
            var products = await _productService.GetAll();

            ViewBag.ApplyForCategory = new SelectList(categories, "Id", "Name", catalogCouponViewModel.ApplyForCategory);
            ViewBag.ApplyForProduct = new SelectList(products, "Id", "Title", catalogCouponViewModel.ApplyForProduct);
            return View(catalogCouponViewModel);
        }

        // GET: CatalogCoupon/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catalogCoupon = await _catalogCouponService.FindAsyn(id.Value);
            if (catalogCoupon == null)
            {
                return HttpNotFound();
            }
            var categories = await _categoryService.GetAll();
            var products = await _productService.GetAll();

            ViewBag.ApplyForCategory = new SelectList(categories, "Id", "Name", catalogCoupon.ApplyForCategory);
            ViewBag.ApplyForProduct = new SelectList(products, "Id", "Title", catalogCoupon.ApplyForProduct);

            var catalogCouponViewModel = Mapper.Map<CatalogCouponViewModel>(catalogCoupon);
            return View(catalogCouponViewModel);
        }

        // POST: CatalogCoupon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CatalogCouponViewModel catalogCouponViewModel)
        {
            if (ModelState.IsValid)
            {
                CatalogCoupon catalogCoupon = await _catalogCouponService.FindAsyn(catalogCouponViewModel.Id);
                if (catalogCoupon == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(catalogCouponViewModel, catalogCoupon);
                catalogCoupon.UpdatedDate = DateTime.Now;
                await _catalogCouponService.UpdateAsync(catalogCoupon, catalogCouponViewModel.Id, true);
                return RedirectToAction("Index");
            }
            var categories = await _categoryService.GetAll();
            var products = await _productService.GetAll();

            ViewBag.ApplyForCategory = new SelectList(categories, "Id", "Name", catalogCouponViewModel.ApplyForCategory);
            ViewBag.ApplyForProduct = new SelectList(products, "Id", "Title", catalogCouponViewModel.ApplyForProduct);
            return View(catalogCouponViewModel);
        }
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catalogCoupon = await _catalogCouponService.FindAsyn(id.Value);
            if (catalogCoupon == null)
            {
                return HttpNotFound();
            }
            var catalogCouponViewModel = Mapper.Map<CatalogCouponViewModel>(catalogCoupon);
            return View(catalogCouponViewModel);
        }

        // GET: CatalogCoupon/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            var catalogCoupon = await _catalogCouponService.FindAsyn(id.Value);
            if (catalogCoupon == null)
            {
                return HttpNotFound();
            }
            var catalogCouponViewModel = Mapper.Map<CatalogCouponViewModel>(catalogCoupon);
            return View(catalogCouponViewModel);
        }

        // POST: CatalogCoupon/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var catalogCoupon = await _catalogCouponService.FindAsyn(id);
                if (catalogCoupon == null)
                {
                    return HttpNotFound();
                }
                await _catalogCouponService.DeleteAsync(catalogCoupon, true);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


    }
}
