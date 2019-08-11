using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Domain.Entities
{
    public class Resource : BaseEntity
    {
        public string Logo { get; set; }
        public string Favicon { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Hotline { get; set; }
        public string Map { get; set; }
        public string FanpageFacebook { get; set; }

        [ForeignKey("MediaLibrary")]
        public Guid? MediaLibraryID { get; set; }
        public virtual MediaLibrary MediaLibrary { get; set; }
    }
}
