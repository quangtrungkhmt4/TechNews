using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcommerceCore.Domain.Enums;

namespace EcommerceCore.Websites.Models.ViewModels
{
    public class BaseEntityViewModel
    {
        public BaseEntityViewModel()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            IsDeleted = false;
        }

        [Key]
        public Guid Id { get; set; }
        [DisplayName("Ngày tạo")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? CreatedDate { get; set; }
        [DisplayName("Người tạo")]
        public Guid? CreatedBy { get; set; }
        [DisplayName("Ngày cập nhật")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? UpdatedDate { get; set; }
        [DisplayName("Người cập nhật")]
        public Guid? UpdatedBy { get; set; }
        [DisplayName("Trạng thái")]
        public CommonStatus Status { get; set; }
        [DisplayName("Đã xóa")]
        public bool IsDeleted { get; set; }
    }
}
