using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateCategory(Category category)
        {
            Create(category);
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await FindAll().OrderBy(c => c.CategoryName).ToListAsync();
        }

        public Category GetCategory(int CategoryId)
        {
            return FindByCondition(c => c.CategoryId.Equals(CategoryId)).SingleOrDefault();
        }

        public async Task<Category> GetCategoryAsync(int CategoryId)
        {
            return await FindByCondition(c => c.CategoryId.Equals(CategoryId)).SingleOrDefaultAsync();
        }

        public void UpdateCategoryAsync(Category category)
        {
            Category categoryEntity = GetCategory(category.CategoryId);
            if (categoryEntity != null)
                UpdateAsync(category);
        }
    }
}
