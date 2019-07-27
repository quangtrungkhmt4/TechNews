using EcommerceCore.Websites.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceCore.Websites.Areas.Admin.Models.ViewModels
{
    public class ManufacturerViewModel : BaseEntityViewModel
    {
        [Required]
        [MaxLength(250)]
        [DisplayName("Nhà chế tạo")]
        public string Name { get; set; }

        [DisplayName("Mã Code")]
        public string CodeName { get; set; }

        [StringLength(2048)]
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [StringLength(2048)]
        public string Website { get; set; }
        //public string Logo { get; set; }
        //public Guid? SiteId { get; set; }
    }
}