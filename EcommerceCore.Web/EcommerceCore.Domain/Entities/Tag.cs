using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
