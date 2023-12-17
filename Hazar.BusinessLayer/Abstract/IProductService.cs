using Hazar.EntityLayer.DTOs;
using Hazar.EntityLayer.Entities;

namespace Hazar.BusinessLayer.Abstract
{
    public interface IProductService
    {

        Task<ProductDTO> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<bool> CreateAsync(ProductDTO entity);
        Task UpdateAsync(ProductDTO entity);
        Task DeleteAsync(int id);

    }
}
