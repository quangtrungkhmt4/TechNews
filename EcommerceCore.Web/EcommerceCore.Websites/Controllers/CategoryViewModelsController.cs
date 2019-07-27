using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using EcommerceCore.Domain;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Services.Infrastructure.Services;
using EcommerceCore.Websites.Models;

namespace EcommerceCore.Websites.Controllers
{
    public class CategoryViewModelsController : ApiController
    {
        private readonly EcommerceDbContext  db = new EcommerceDbContext();

        // GET: api/CategoryViewModels
        [Route("api/categories/list")]
        [HttpGet]
        public IList<Category> GetCategoryViewModels()
        {
            var categories = db.Categories.ToList();           
            return categories;
        }   
    }
}