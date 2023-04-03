using API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    // Category repository with all CRUD logic
    public class CategoryRepository : ICategoryRepository
    {
        private JobDbContext _dbContext;

        public CategoryRepository(JobDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _dbContext.Categories.Find(categoryId);
            _dbContext.Categories.Remove(category);
            Save();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _dbContext.Categories.Find(categoryId);
        }

        public IEnumerable<Category> GetGategories()
        {
            return _dbContext.Categories.ToList();
        }

        public void InsertCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            Save();
        }

        public void UpdateCategory(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            Save();
        }

        private void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
