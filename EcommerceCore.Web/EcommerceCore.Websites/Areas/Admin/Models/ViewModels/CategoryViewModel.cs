using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EcommerceCore.Domain.Enums;
using EcommerceCore.Websites.Models.ViewModels;

namespace EcommerceCore.Websites.Areas.Admin.Models.ViewModels
{
    public class CategoryViewModel
    {

        public Guid? Id { get; set; }
        [DisplayName("Ngày tạo")]
        [DataType(DataType.Date)]    
        public DateTime? CreatedDate { get; set; } 
        [Required]
        [MaxLength(250)]
        [DisplayName("Tên danh mục")]
        public string Name { get; set; }
        [StringLength(2048)]
        [DisplayName("Mô tả")]
        public string Description { get; set; }               
        public Guid ParentId { get; set; }

        [DisplayName("Trạng thái")]
        public CommonStatus? Status { get; set; }
        [DisplayName("Thứ tự")]
        public int OrderDisplay { get; set; }
        [DisplayName("Hiện thị trên trang chủ")]
        public bool IsShowHomePage { get; set; }

        
    }
}
