using EcommerceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Domain.Entities
{
    public class Article : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int IsComment { get; set; }
        public State State { get; set; }

        [ForeignKey("Tag")]
        public Guid? TagID { get; set; }
        public virtual Tag Tag { get; set; }

        [ForeignKey("MediaLibrary")]
        public Guid? MediaID { get; set; }
        public virtual MediaLibrary MediaLibrary { get; set; }

        [ForeignKey("Thematic")]
        public Guid? ThematicID { get; set; }
        public virtual Thematic Thematic { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
