using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCore.Domain.Entities
{
    public class CatalogCoupon : BaseEntity
    {
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? SiteId { get; set; }

        #region Relation
        [ForeignKey("Category")]
        public Guid? ApplyForCategory { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("Product")]
        public Guid? ApplyForProduct { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
