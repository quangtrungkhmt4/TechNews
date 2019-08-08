using System;
using System.Collections.Generic;

namespace EcommerceCore.Domain.Entities
{
    public class Manufacturer : BaseEntity
    {

        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public Guid? SiteId { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
