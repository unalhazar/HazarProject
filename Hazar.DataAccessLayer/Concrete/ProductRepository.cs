using Hazar.DataAccessLayer.Abstract;
using Hazar.EntityLayer.Entities;
using Microsoft.Extensions.Configuration;

namespace Hazar.DataAccessLayer.Concrete
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
