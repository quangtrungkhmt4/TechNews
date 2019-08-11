using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCore.Websites.Models.ViewModels
{
    public class ProductViewModel : BaseEntityViewModel
    {
       
        [StringLength(256)]
        [DisplayName("Tên trên đường dẫn")]
        public string UrlName { get; set; }
        [Required]
        [StringLength(256)]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }
        [StringLength(256)]
        [DisplayName("Mã sku")]
        public string Sku { get; set; }
        [DisplayName("Ngày ra mắt")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PublicationDate { get; set; }
        [Required]
        [DisplayName("Giá")]
        public decimal Price { get; set; }
        [DisplayName("Lượt xem")]
        public int View { get; set; }
        [StringLength(256)]
        [DisplayName("Từ khóa")]
        public string Keyword { get; set; }
        [DisplayName("Ngày hết hạn")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpirationDate { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [StringLength(2048)]
        [DisplayName("Mô tả tóm tắt")]
        public string ShortDescription { get; set; }
        [DisplayName("Cân nặng")]
        public double? Weight { get; set; }
        [DisplayName("Chiều cao")]
        public double? Height { get; set; }
        [DisplayName("Chiều rộng")]
        public double? Width { get; set; }
        [DisplayName("Độ sâu")]
        public double? Depth { get; set; }
        [DisplayName("Mã trang")]
        public Guid? SiteId { get; set; }
        [DisplayName("Tên danh mục")]
        public Guid? CategoryId { get; set; }
        [DisplayName("Tên danh mục")]
        public string CategoryName { get; set; }
        [DisplayName("Tên nhà cung cấp")]
        public Guid? SupplierId { get; set; }
        [DisplayName("Tên nhà cung cấp")]
        public string SupplierName { get; set; }
        [DisplayName("Tên nhà sản xuất")]
        public Guid? ManufacturerId { get; set; }
        [DisplayName("Tên nhà sản xuất")]
        public string ManufacturerName { get; set; }
        [DisplayName("Trạng thái sản phẩn")]
        public Guid? ProductStatusId { get; set; }
        [DisplayName("Trạng thái sản phẩn")]
        public string ProductStatusName { get; set; }
    }
}
