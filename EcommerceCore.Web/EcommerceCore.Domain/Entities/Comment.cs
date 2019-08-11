using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public Guid? ParentID { get; set; }

        [ForeignKey("Article")]
        public Guid? ArticleID { get; set; }
        public virtual Article Article { get; set; }

    }
}
