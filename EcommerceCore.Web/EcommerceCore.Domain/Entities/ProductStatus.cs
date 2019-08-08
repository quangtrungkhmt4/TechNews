using System.Collections.Generic;

namespace EcommerceCore.Domain.Entities
{
    public class ProductStatus : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
