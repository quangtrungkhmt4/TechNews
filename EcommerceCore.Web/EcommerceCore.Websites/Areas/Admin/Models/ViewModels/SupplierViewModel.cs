using EcommerceCore.Websites.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceCore.Websites.Areas.Admin.Models.ViewModels
{
    public class SupplierViewModel : BaseEntityViewModel
    {
        [Required]
        [MaxLength(250)]
        [DisplayName("Nhà sản xuất")]
        public string Name { get; set; }

        [MaxLength(250)]
        public string CodeName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        public string Fax { get; set; }

        //public Guid? SiteId { get; set; }
    }
}