using FoodAplication.Context;
using FoodAplication.Interface;
using FoodAplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAplication.Repository
{
    public class CategoryRepository : ICategory
    {
        private readonly FoodContext _context;
        public CategoryRepository(FoodContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        public Task<Category> GetCategoryById(int id)
        {
            return  _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync()!;
        }
    }
}
