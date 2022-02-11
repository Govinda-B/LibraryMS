using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using LoggerService;

namespace LibraryMS.BAL
{
    public class CategoriesBAL : ICategoriesBAL
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CategoriesBAL(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Category> CreateCategory(CategoryCreateDto category)
        {
            if (category == null)
            {
                _logger.LogError("category sent from client is null.");
                return null;
            }
            var categoryEntity = _mapper.Map<Category>(category);
            _repository.Category.CreateCategory(categoryEntity);
            await _repository.SaveAsync();
            return categoryEntity;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            Category category = await _repository.Category.GetCategoryAsync(id);
            if (category == null)
            {
                _logger.LogInfo($"Category with id: {id} doesn't exist in the database.");
                //return NotFound("The category record couldn't be found.");
                return false;
            }
            _repository.Category.DeleteCategory(category);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categories = await _repository.Category.GetAllCategoriesAsync();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }

        public CategoryDto GetCategory(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDto> GetCategoryAsync(int CategoryId)
        {
            var category = await _repository.Category.GetCategoryAsync(CategoryId);
            if (category == null)
            {
                _logger.LogInfo($"Book with id: {CategoryId} doesn't exist in the database.");
                return null;
            }
            return _mapper.Map<CategoryDto>(category);
            
        }

        public async Task<string> UpdateCategoryAsync(int id, CategoryCreateDto category)
        {
            if (category == null)
            {
                return "category is null.";
            }
            Category categoryToUpdate = await _repository.Category.GetCategoryAsync(id);
            if (categoryToUpdate == null)
            {
                return "The category record couldn't be found.";
            }
            _mapper.Map(category, categoryToUpdate);
            _repository.Save();
            return null;
        }
    }
}
