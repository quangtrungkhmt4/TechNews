using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCore.Domain.Entities
{
    public class ProductPrice : BaseEntity
    {

        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public decimal Value { get; set; }

        #region Relation
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
