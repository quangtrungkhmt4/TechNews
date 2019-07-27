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
    public class ManufacturerService: EntityService<Manufacturer>, IManufacturerService
    {
        private readonly IManufacturerRepository _ManufacturerRepository;

        public ManufacturerService(IManufacturerRepository ManufacturerRepository) : base(ManufacturerRepository)
        {
            _ManufacturerRepository = ManufacturerRepository;
        }

        public Manufacturer GetByProduct(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
