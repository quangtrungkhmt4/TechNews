using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.Websites.Models.ViewModels
{
    public class ProductImageViewModel : BaseEntityViewModel
    {

        [StringLength(256)]
        [Display(Name = "Liên kết ảnh")]
        public string ImageLink { get; set; }
        [Display(Name = "Thứ tự")]
        public int SequenceNo { get; set; }
        [Required]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }
    }
}
