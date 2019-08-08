using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.Websites.Models.ViewModels
{
    public class ProductPriceViewModel : BaseEntityViewModel
    {
        [Display(Name = "Số lượng tối thiểu")]
        public int MinQuantity { get; set; }
        [Display(Name = "Số lượng tối đa")]
        public int MaxQuantity { get; set; }
        [Display(Name = "Giá trị")]
        public decimal Value { get; set; }
        [Required]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }
    }
}
