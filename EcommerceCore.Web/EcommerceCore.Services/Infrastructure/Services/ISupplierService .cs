using System;
using EcommerceCore.Common.Service;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Services.Infrastructure.Services
{
    public interface ISupplierService : IEntityService<Supplier>
    {
        Supplier GetByProduct(Guid productId);
    }
}
