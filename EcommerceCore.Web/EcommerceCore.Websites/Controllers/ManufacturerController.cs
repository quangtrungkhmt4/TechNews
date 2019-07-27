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
    [Authorize(Roles = "Admin")]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HandleError]
        public async Task<ViewResult> Index()
        {
            var manufacturers = await _manufacturerService.GetAll();

            var manufacturerViewModels = Mapper.Map<IEnumerable<ManufacturerViewModel>>(manufacturers);

            return View(manufacturerViewModels);
        }

        // GET: Manufacturer/Create
        public ViewResult Create()
        {
            return View();
        }

        [ValidateModelStateFilter]
        [HttpPost]
        public async Task<ActionResult> Create(ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var manufacturer = Mapper.Map<Manufacturer>(manufacturerViewModel);

                    manufacturer.Status = CommonStatus.Active;
                    await _manufacturerService.CreateAsync(manufacturer, true);
                    return RedirectToAction("Index");
                }

            }
            catch
            {

            }
            return View();
        }

        // GET: Manufacturer/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var manufacturer = await _manufacturerService.Find(id.Value);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            var manufacturerViewModel = Mapper.Map<ManufacturerViewModel>(manufacturer);
            return View(manufacturerViewModel);
        }

        // POST: Manufacturer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ManufacturerViewModel manufacturerViewModel)
        {
            if (ModelState.IsValid)
            {
                Manufacturer manufacturer = await _manufacturerService.Find(manufacturerViewModel.Id);
                if (manufacturer == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(manufacturerViewModel, manufacturer);
                manufacturer.UpdatedDate = DateTime.Now;
                await _manufacturerService.UpdateAsync(manufacturer, manufacturerViewModel.Id, true);
                return RedirectToAction("Index");
            }
            return View(manufacturerViewModel);
        }
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var manufacturer = await _manufacturerService.Find(id.Value);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            var manufacturerViewModel = Mapper.Map<ManufacturerViewModel>(manufacturer);
            return View(manufacturerViewModel);
        }

        // GET: Manufacturer/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            var manufacturer = await _manufacturerService.Find(id.Value);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            var manufacturerViewModel = Mapper.Map<ManufacturerViewModel>(manufacturer);
            return View(manufacturerViewModel);
        }

        // POST: Manufacturer/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var manufacturer = await _manufacturerService.Find(id);
                if (manufacturer == null)
                {
                    return HttpNotFound();
                }
                await _manufacturerService.DeleteAsync(manufacturer, true);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


    }
}
