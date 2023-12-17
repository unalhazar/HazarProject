using Hazar.BusinessLayer.Abstract;
using Hazar.EntityLayer.Entities;

namespace Hazar.BusinessLayer.Concrete
{
    public class ProductDataManager : IProductDataService
    {
        public List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    Name = "Ürün 1",
                    Price = 19.99m,
                    Stock = 50
                },
                new Product
                {
                    Id = 2,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    Name = "Ürün 2",
                    Price = 29.99m,
                    Stock = 30
                },
                new Product
                {
                    Id = 3,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    Name = "Ürün 3",
                    Price = 39.99m,
                    Stock = 20
                },
                new Product
                {
                    Id = 4,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    Name = "Ürün 4",
                    Price = 49.99m,
                    Stock = 10
                },
                new Product
                {
                    Id = 5,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    Name = "Ürün 5",
                    Price = 59.99m,
                    Stock = 5
                }
            };
        }
    }
}
