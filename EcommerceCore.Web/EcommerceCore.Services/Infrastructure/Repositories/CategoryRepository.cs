using EcommerceCore.Common;
using EcommerceCore.Common.Repository;
using EcommerceCore.Domain;
using EcommerceCore.Domain.Entities;

namespace EcommerceCore.Services.Infrastructure.Repositories
{
    public class CategoryRepository: GenericRepository<EcommerceDbContext,Category>, ICategoryRepository
    {
        public CategoryRepository(EcommerceDbContext dbContext) : base(dbContext)
        {

        }
    }
}
