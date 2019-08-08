﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EcommerceCore.Domain.Enums;
using EcommerceCore.Websites.Models.ViewModels;

namespace EcommerceCore.Websites.Areas.Admin.Models.ViewModels
{
    public class CategoryViewModel : BaseEntityViewModel
    {
        [Required]
        [MaxLength(250)]
        [DisplayName("Tên danh mục")]
        public string Name { get; set; }
        [StringLength(2048)]
        [DisplayName("Mô tả")]
        public string Description { get; set; }        
        [DisplayName("Trạng thái")]
        public CommonStatus? State { get; set; }
    }
}
