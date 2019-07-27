using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceCore.Common.Service;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Services.Infrastructure.Repositories;

namespace EcommerceCore.Services.Infrastructure.Services
{
    public class SupplierService: EntityService<Supplier>, ISupplierService
    {
        private readonly ISupplierRepository _SupplierRepository;

        public SupplierService(ISupplierRepository SupplierRepository) : base(SupplierRepository)
        {
            _SupplierRepository = SupplierRepository;
        }

        public Supplier GetByProduct(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
