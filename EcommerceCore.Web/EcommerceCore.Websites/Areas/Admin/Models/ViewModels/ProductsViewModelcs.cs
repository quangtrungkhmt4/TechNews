using EcommerceCore.Websites.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceCore.Websites.Areas.Admin.Models.ViewModels
{
    public class ProductViewModel : BaseEntityViewModel
    {
        [Required]
        [MaxLength(250)]
        [DisplayName("Tên trên đường dẫn")]
        public string UrlName { get; set; }

        [Required]
        [StringLength(256)]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [StringLength(256)]
        [DisplayName("Mã sku")]
        public string Sku { get; set; }
        //public DateTime? PublicationDate { get; set; }

        [DisplayName("Giá")]
        public decimal Price { get; set; }
        //public int View { get; set; }
        //public string Keyword { get; set; }
        //public DateTime? ExpirationDate { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        //public string ShortDescription { get; set; }
        //public double? Weight { get; set; }
        //public double? Height { get; set; }
        //public double? Width { get; set; }
        //public double? Depth { get; set; }
        //public Guid? SiteId { get; set; }
    }
}