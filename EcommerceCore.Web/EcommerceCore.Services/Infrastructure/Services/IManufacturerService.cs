using System;
using EcommerceCore.Common.Service;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Services.Infrastructure.Services
{
    public interface IManufacturerService : IEntityService<Manufacturer>
    {
        Manufacturer GetByProduct(Guid productId);
    }
}
