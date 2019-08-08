using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.Websites.Models.ViewModels
{
    public class ProductStatusViewModel : BaseEntityViewModel
    {
        [Required]
        [MaxLength(250)]
        [Display(Name = "Tên trạng thái")]
        public string Name { get; set; }
        [MaxLength(1024)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
    }
}
