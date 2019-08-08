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
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HandleError]
        public async Task<ViewResult> Index()
        {
            var coupons = await _couponService.GetAll();

            var couponViewModels = Mapper.Map<IEnumerable<CouponViewModel>>(coupons);

            return View(couponViewModels);
        }

        // GET: Coupon/Create
        [Authorize(Roles = "Admin")]
        public ViewResult Create()
        {
            return View();
        }

        [ValidateModelStateFilter]
        [HttpPost]
        public async Task<ActionResult> Create(CouponViewModel couponViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var coupon = Mapper.Map<Coupon>(couponViewModel);

                    coupon.Status = CommonStatus.Active;
                    await _couponService.CreateAsync(coupon, true);
                    return RedirectToAction("Index");
                }

            }
            catch
            {

            }
            return View();
        }

        // GET: Coupon/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var coupon = await _couponService.Find(id.Value);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            var couponViewModel = Mapper.Map<CouponViewModel>(coupon);
            return View(couponViewModel);
        }

        // POST: Coupon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CouponViewModel couponViewModel)
        {
            if (ModelState.IsValid)
            {
                Coupon coupon = await _couponService.Find(couponViewModel.Id);
                if (coupon == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(couponViewModel, coupon);
                coupon.UpdatedDate = DateTime.Now;
                await _couponService.UpdateAsync(coupon, couponViewModel.Id, true);
                return RedirectToAction("Index");
            }
            return View(couponViewModel);
        }
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var coupon = await _couponService.Find(id.Value);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            var couponViewModel = Mapper.Map<CouponViewModel>(coupon);
            return View(couponViewModel);
        }

        // GET: Coupon/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            var coupon = await _couponService.Find(id.Value);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            var couponViewModel = Mapper.Map<CouponViewModel>(coupon);
            return View(couponViewModel);
        }

        // POST: Coupon/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var coupon = await _couponService.Find(id);
                if (coupon == null)
                {
                    return HttpNotFound();
                }
                await _couponService.DeleteAsync(coupon, true);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


    }
}
