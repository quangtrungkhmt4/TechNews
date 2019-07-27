using AutoMapper;
using EcommerceCore.Common.Filter;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Domain.Enums;
using EcommerceCore.Services.Infrastructure.ViewModels;
using EcommerceCore.Services.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EcommerceCore.Web.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HandleError]
        public async Task<ViewResult> Index()
        {
            var suppliers = await _supplierService.GetAll();

            var supplierViewModels = Mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);

            return View(supplierViewModels);
        }

        // GET: Supplier/Create
        public ViewResult Create()
        {
            return View();
        }

        [ValidateModelStateFilter]
        [HttpPost]
        public async Task<ActionResult> Create(SupplierViewModel supplierViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var supplier = Mapper.Map<Supplier>(supplierViewModel);

                    supplier.Status = CommonStatus.Active;
                    await _supplierService.CreateAsync(supplier, true);
                    return RedirectToAction("Index");
                }

            }
            catch
            {

            }
            return View();
        }

        // GET: Supplier/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = await _supplierService.Find(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            var supplierViewModel = Mapper.Map<SupplierViewModel>(supplier);
            return View(supplierViewModel);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SupplierViewModel supplierViewModel)
        {
            if (ModelState.IsValid)
            {
                Supplier supplier = await _supplierService.Find(supplierViewModel.Id);
                if (supplier == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(supplierViewModel, supplier);
                supplier.UpdatedDate = DateTime.Now;
                await _supplierService.UpdateAsync(supplier, supplierViewModel.Id, true);
                return RedirectToAction("Index");
            }
            return View(supplierViewModel);
        }
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = await _supplierService.Find(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            var supplierViewModel = Mapper.Map<SupplierViewModel>(supplier);
            return View(supplierViewModel);
        }

        // GET: Supplier/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            var supplier = await _supplierService.Find(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            var supplierViewModel = Mapper.Map<SupplierViewModel>(supplier);
            return View(supplierViewModel);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var supplier = await _supplierService.Find(id);
                if (supplier == null)
                {
                    return HttpNotFound();
                }
                await _supplierService.DeleteAsync(supplier, true);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


    }
}
