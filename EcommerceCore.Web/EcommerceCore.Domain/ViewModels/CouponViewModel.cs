using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Domain.ViewModels
{
   public class CouponViewModel
    {
        public Guid Id { set; get; }
        public string CouponCode { set; get; }
        [Required(ErrorMessage ="Không được để trống")]
        public decimal? Amount { get; set; }
        public int? ApplyType { get; set; }
        public int? DiscountValueType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? SiteId { get; set; }
    }
}
