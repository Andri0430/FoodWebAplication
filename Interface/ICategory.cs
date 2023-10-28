using FoodAplication.Models;

namespace FoodAplication.Interface
{
    public interface ICategory
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
    }
}
