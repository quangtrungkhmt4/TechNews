using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Domain.Entities
{
    public class MediaLibrary : BaseEntity
    {
        public string Path { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
