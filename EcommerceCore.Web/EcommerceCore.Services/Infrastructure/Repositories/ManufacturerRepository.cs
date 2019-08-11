using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public class ManufacturerRepository: GenericRepository<EcommerceDbContext,Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(EcommerceDbContext dbContext) : base(dbContext)
        {

        }
    }
}
