using Hazar.DataAccessLayer.Abstract;
using Hazar.EntityLayer.Entities;
using Microsoft.Extensions.Configuration;

namespace Hazar.DataAccessLayer.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
