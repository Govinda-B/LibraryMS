using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DTO;
using Entities.Models;

namespace LibraryMS.BAL
{
    public interface ICategoriesBAL
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        public Task<CategoryDto> GetCategoryAsync(int CategoryId);
        public CategoryDto GetCategory(int CategoryId);
        Task<Category> CreateCategory(CategoryCreateDto category);
        public Task<bool> DeleteCategory(int id);
        public Task<string> UpdateCategoryAsync(int id, CategoryCreateDto category);
    }
}
