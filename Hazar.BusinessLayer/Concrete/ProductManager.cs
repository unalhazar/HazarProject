using AutoMapper;
using Hazar.BusinessLayer.Abstract;
using Hazar.DataAccessLayer.Abstract;
using Hazar.EntityLayer.DTOs;
using Hazar.EntityLayer.Entities;
using Hazar.EntityLayer.Enums;
using Microsoft.Extensions.Logging;

namespace Hazar.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductManager> _logger;

        public ProductManager(IProductRepository repository, ICategoryRepository categoryRepository, IMapper mapper, ILogger<ProductManager> logger)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            _logger.LogInformation("Ürünlerin hepsini getirme işlemi başladı");
            var products = await _repository.GetAllAsync();
            _logger.LogInformation("Ürünlerin hepsi başarıyla getirildi");
            return products.ToList();
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"ID'si {id} olan ürünün getirilme işlemi başladı");
                var product = await _repository.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"ID'si {id} olan ürün bulunamadı");
                }
                var result = _mapper.Map<ProductDTO>(product);
                _logger.LogInformation($"ID'si {id} olan ürün başarıyla getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan ürünü getirirken hata oluştu");
                throw;
            }
        }

        public async Task UpdateAsync(ProductDTO entity)
        {
            try
            {
                _logger.LogInformation($"{entity.Name} adlı ürünün güncelleme işlemi başladı");
                var result = _mapper.Map<Product>(entity);
                if (result != null)
                {
                    result.ActiveState = (int)ActiveState.Active;
                    result.UpdatedTime = DateTime.Now;
                    var categoryObject = await _categoryRepository.GetByIdAsync(result.CategoryId);
                    var categoryName = categoryObject.Name;
                    result.CategoryName = categoryName;
                    await _repository.UpdateAsync(result);
                    _logger.LogInformation($"{entity.Name} adlı ürün başarıyla güncellendi");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{entity.Name} adlı ürünün güncellenmesi sırasında bir hata oluştu");
                throw;
            }
        }

        public async Task<bool> CreateAsync(ProductDTO entity)
        {
            try
            {
                _logger.LogInformation($"{entity.Name} adlı yeni ürünün oluşturulma işlemi başladı");
                var result = _mapper.Map<Product>(entity);
                if (result != null)
                {
                    result.ActiveState = (int)ActiveState.Active;
                    result.CreatedTime = DateTime.Now;
                    var categoryObject = await _categoryRepository.GetByIdAsync(entity.CategoryId);
                    var categoryName = categoryObject.Name;
                    result.CategoryName = categoryName;
                    await _repository.InsertAsync(result);
                    _logger.LogInformation($"{entity.Name} adlı yeni ürün başarıyla oluşturuldu");
                    return true;
                }
                _logger.LogWarning($"{entity.Name} adlı yeni ürün oluşturulamadı");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{entity.Name} adlı yeni ürünün oluşturulması sırasında bir hata oluştu");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"ID'si {id} olan ürünün silme işlemi başladı");
                if (id > 0)
                {
                    await _repository.DeleteAsync(id);
                    _logger.LogInformation($"ID'si {id} olan ürün başarıyla silindi");
                }
                else
                {
                    _logger.LogWarning($"Geçersiz ID: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan ürünü silerken hata oluştu");
                throw;
            }
        }
    }
}
