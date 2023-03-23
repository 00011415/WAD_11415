using API.Model;
using System.Collections.Generic;

namespace API.Repository
{
    public interface ICategoryRepository
    {
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryid);
        Category GetCategoryById(int Id);
        IEnumerable<Category> GetGategories();
    }
}
