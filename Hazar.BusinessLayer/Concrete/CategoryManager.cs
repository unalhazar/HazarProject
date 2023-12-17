using AutoMapper;
using Hazar.BusinessLayer.Abstract;
using Hazar.DataAccessLayer.Abstract;
using Hazar.EntityLayer.DTOs;
using Hazar.EntityLayer.Entities;
using Hazar.EntityLayer.Enums;
using Microsoft.Extensions.Logging;

namespace Hazar.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryManager> _logger;

        public CategoryManager(ICategoryRepository repository, IMapper mapper, ILogger<CategoryManager> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            _logger.LogInformation("Kategori listesi getirme işlemi başladı");
            var categories = await _repository.GetAllAsync();
            _logger.LogInformation("Kategori listesi başarıyla getirildi");
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"ID'si {id} olan kategorinin getirilme işlemi başladı");
                var category = await _repository.GetByIdAsync(id);
                if (category == null)
                {
                    _logger.LogWarning($"ID'si {id} olan kategori bulunamadı");
                }
                _logger.LogInformation($"ID'si {id} olan kategori başarıyla getirildi");
                return _mapper.Map<CategoryDTO>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan kategoriyi getirirken hata oluştu");
                throw;
            }
        }

        public async Task UpdateAsync(CategoryDTO entity)
        {
            try
            {
                _logger.LogInformation($"{entity.Name} adlı kategorinin güncelleme işlemi başladı");
                var result = _mapper.Map<Category>(entity);
                result.ActiveState = (int)ActiveState.Active;
                result.UpdatedTime = DateTime.Now;
                await _repository.UpdateAsync(result);
                _logger.LogInformation($"{entity.Name} adlı kategori başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{entity.Name} adlı kategorinin güncellenmesi sırasında bir hata oluştu");
                throw;
            }
        }

        public async Task<bool> CreateAsync(CategoryDTO entity)
        {
            try
            {
                _logger.LogInformation($"{entity.Name} adlı yeni kategorinin oluşturulma işlemi başladı");
                var result = _mapper.Map<Category>(entity);
                if (result != null)
                {
                    result.ActiveState = (int)ActiveState.Active;
                    result.CreatedTime = DateTime.Now;
                    await _repository.InsertAsync(result);
                    _logger.LogInformation($"{entity.Name} adlı yeni kategori başarıyla oluşturuldu");
                    return true;
                }
                _logger.LogWarning($"{entity.Name} adlı yeni kategori oluşturulamadı");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{entity.Name} adlı yeni kategorinin oluşturulması sırasında bir hata oluştu");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"ID'si {id} olan kategorinin silme işlemi başladı");
                if (id > 0)
                {
                    await _repository.DeleteAsync(id);
                    _logger.LogInformation($"ID'si {id} olan kategori başarıyla silindi");
                }
                else
                {
                    _logger.LogWarning($"Geçersiz ID: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan kategoriyi silerken hata oluştu");
                throw;
            }
        }
    }
}
