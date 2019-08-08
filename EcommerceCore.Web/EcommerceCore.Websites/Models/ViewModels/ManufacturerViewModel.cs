using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCore.Websites.Models.ViewModels
{
    public class ManufacturerViewModel : BaseEntityViewModel
    {
        [Required]
        [MaxLength(250)]
        [DisplayName("Tên nhà sản xuất")]
        public string Name { get; set; }
        [MaxLength(250)]
        [DisplayName("Mã nhà sản xuất")]
        public string CodeName { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [MaxLength(250)]
        [DisplayName("Website")]
        public string Website { get; set; }
        [Column("LogoPath")]
        public string Logo { get; set; }
        public Guid? SiteId { get; set; }
    }
}
