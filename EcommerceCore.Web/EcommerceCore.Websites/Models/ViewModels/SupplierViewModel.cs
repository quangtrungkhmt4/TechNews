using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCore.Websites.Models.ViewModels
{
    public class SupplierViewModel : BaseEntityViewModel
    {
        [Required]
        [MaxLength(250)]
        [Column(Order = 1)]
        [Display(Name = "Tên nhà cung cấp")]
        public string Name { get; set; }
        [Column(Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Mã nhà cung cấp")]
        public string CodeName { get; set; }
        [Required]
        [MaxLength(50)]
        [Column(Order = 3)]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        [Column(Order = 4)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [MaxLength(50)]
        [Column(Order = 5)]
        [Display(Name = "Địa chỉ fax")]
        public string Fax { get; set; }
        [Display(Name = "Mã trang")]
        public Guid? SiteId { get; set; }

    }

}
