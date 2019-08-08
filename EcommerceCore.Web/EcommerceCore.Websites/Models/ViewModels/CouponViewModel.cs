using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCore.Websites.Models.ViewModels
{
    public class CouponViewModel : BaseEntityViewModel
    {
        [Required]
        [Display(Name = "Mã phiếu mua hàng")]
        public string CouponCode { get; set; }
        [Required]
        [Display(Name = "Tên phiếu")]
        public string Name { get; set; }
        [Display(Name = "Số tiền")]
        public decimal? Amount { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [Display(Name = "Thời gian bắt đầu")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [Display(Name = "Thời gian kết thúc")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }
        [Display(Name = "Mã trang")]
        public Guid? SiteId { get; set; }
    }
}
