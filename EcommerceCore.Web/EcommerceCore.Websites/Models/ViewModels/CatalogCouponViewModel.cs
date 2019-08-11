using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCore.Websites.Models.ViewModels
{
    public class CatalogCouponViewModel : BaseEntityViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CodeName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        public string ProductTitle { get; set; }
        public Guid? ApplyForCategory { get; set; }
        public Guid? ApplyForProduct { get; set; }
        public string CategoryName { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }
        public Guid? SiteId { get; set; }
    }
}
