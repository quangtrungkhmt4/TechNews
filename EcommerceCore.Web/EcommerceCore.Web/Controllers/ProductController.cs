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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IManufacturerService _manufacturerService;

        public ProductController
        (   
            IProductService productService, ICategoryService categoryService,
            ISupplierService supplierService, IManufacturerService manufacturerService
        )
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _manufacturerService = manufacturerService;

        }

        [HandleError]
        public async Task<ViewResult> Index()
        {
            var products = await _productService.GetAll();

            var productViewModels = Mapper.Map<IEnumerable<ProductViewModel>>(products);

            return View(productViewModels);
        }

        // GET: Product/Create
        public async Task<ViewResult> Create()
        {
            var categories    = await  _categoryService.GetAll();
            var suppliers      = await  _supplierService.GetAll();
            var manufacturers  = await  _manufacturerService.GetAll();

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(manufacturers, "Id", "Name");
            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name");
            return View();
        }

        [ValidateModelStateFilter]
        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel productViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = Mapper.Map<Product>(productViewModel);

                    product.Status = CommonStatus.Active;
                    await _productService.CreateAsync(product, true);
                    return RedirectToAction("Index");
                }

            }
            catch
            {

            }

            var categories = await _categoryService.GetAll();
            var suppliers = await _supplierService.GetAll();
            var manufacturers = await _manufacturerService.GetAll();

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productViewModel.CategoryId);
            ViewBag.ManufacturerId = new SelectList(manufacturers, "Id", "Name", productViewModel.ManufacturerId);
            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name", productViewModel.SupplierId);
            return View(productViewModel);
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = await _productService.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            var categories = await _categoryService.GetAll();
            var suppliers = await _supplierService.GetAll();
            var manufacturers = await _manufacturerService.GetAll();

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);
            ViewBag.ManufacturerId = new SelectList(manufacturers, "Id", "Name", product.ManufacturerId);
            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name", product.SupplierId);

            var productViewModel = Mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                Product product = await _productService.Find(productViewModel.Id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(productViewModel, product);
                product.UpdatedDate = DateTime.Now;
                await _productService.UpdateAsync(product, productViewModel.Id, true);
                return RedirectToAction("Index");
            }
            var categories = await _categoryService.GetAll();
            var suppliers = await _supplierService.GetAll();
            var manufacturers = await _manufacturerService.GetAll();

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productViewModel.CategoryId);
            ViewBag.ManufacturerId = new SelectList(manufacturers, "Id", "Name", productViewModel.ManufacturerId);
            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name", productViewModel.SupplierId);
            return View(productViewModel);
        }
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = await _productService.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            var productViewModel = Mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            var product = await _productService.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            var productViewModel = Mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var product = await _productService.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                await _productService.DeleteAsync(product, true);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


    }
}
