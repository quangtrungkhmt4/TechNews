using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public class SupplierRepository: GenericRepository<EcommerceDbContext,Supplier>, ISupplierRepository
    {
        public SupplierRepository(EcommerceDbContext dbContext) : base(dbContext)
        {

        }
    }
}
