using System;
using EcommerceCore.Domain.Enums;

namespace EcommerceCore.Services.Infrastructure.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string SupplierName { get; set; }
        public decimal Price { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int View { get; set; }
        public CommonStatus Status { get; set; }

    }
}