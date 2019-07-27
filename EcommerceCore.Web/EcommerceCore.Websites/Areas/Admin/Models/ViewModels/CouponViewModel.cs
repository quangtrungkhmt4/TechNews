using EcommerceCore.Websites.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceCore.Websites.Areas.Admin.Models.ViewModels
{
    public class CouponViewModel : BaseEntityViewModel
    {
        [Required]
        [MaxLength(250)]
        [DisplayName("Mã giảm giá")]
        public string CouponCode { get; set; }

        [DisplayName("Tên danh mục")]
        public string Name { get; set; }

        [DisplayName("Giá trị")]
        public decimal? Amount { get; set; }

        [DisplayName("Thời gian bắt đầu")]
        public DateTime? StartTime { get; set; }

        [DisplayName("Thời gian kết thúc")]
        public DateTime? EndTime { get; set; }

        //public Guid? SiteId { get; set; }
    }
}