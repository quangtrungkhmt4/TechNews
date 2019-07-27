using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Domain.ViewModels
{
   public class CatalogCouponViewModel
    {
        public Guid Id { set; get; }
        [Display(Name ="Tên sản phẩm")]
        public string Name { set; get; }
        [Display(Name = "Code Name")]
        public string CodeName { set; get; }
        [Display(Name = "Mô tả")]
        public string Description { set; get; }
        [Display(Name = "Giá ")]
        [Required(ErrorMessage ="Phải nhập trường này")]
        public decimal Amount { set; get; }
        [Required(ErrorMessage = "Danh cho sản phẩm")]

        public Guid ProductId { set; get; }
        [Required(ErrorMessage = "Danh cho loại sản phẩm")]

        public Guid CategoryId { set; get; }
        [Display(Name = "Thời gian bắt đầu")]
        public DateTime? StartTime { get; set; }
        [Display(Name = "Thởi gian kết thúc")]
        public DateTime? EndTime { get; set; }
    }
}
