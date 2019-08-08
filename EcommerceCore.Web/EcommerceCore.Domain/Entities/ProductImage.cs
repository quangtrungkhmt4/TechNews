using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCore.Domain.Entities
{
    public class ProductImage : BaseEntity
    {

        public string ImageLink { get; set; }
        public int SequenceNo { get; set; }

        #region Relation
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
