using EcommerceCore.Websites.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceCore.Websites.Areas.Admin.Models.ViewModels
{
    public class ProductViewModel:BaseEntityViewModel
    {
        [Required]
        [MaxLength(250)]
        public string UrlName { get; set; }
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int View { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
    }
}