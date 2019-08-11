using System;
using System.Collections.Generic;

namespace EcommerceCore.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public int OrderDisplay { get; set; }
        public bool IsShowHomePage { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<CatalogCoupon> CatalogCoupones { get; set; }
    }
}
