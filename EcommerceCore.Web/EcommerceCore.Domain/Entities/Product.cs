using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCore.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string UrlName { get; set; }
        public string Title { get; set; }
        public string Sku { get; set; }
        public DateTime? PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int View { get; set; }
        public string Keyword { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Depth { get; set; }
        public Guid? SiteId { get; set; }

        #region Relation

        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid? SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey("Manufacturer")]
        public Guid? ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [ForeignKey("ProductStatus")]
        public Guid? ProductStatusId { get; set; }
        public virtual ProductStatus ProductStatus { get; set; }
       

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
        public virtual ICollection<CatalogCoupon> CatalogCoupones { get; set; }

        #endregion
    }
}
