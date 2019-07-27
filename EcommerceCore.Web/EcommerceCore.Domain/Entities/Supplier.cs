using System;
using System.Collections.Generic;

namespace EcommerceCore.Domain.Entities
{
    public class Supplier : BaseEntity
    {

        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public Guid? SiteId { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

}
