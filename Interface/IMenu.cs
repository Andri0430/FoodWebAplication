using FoodAplication.Dtos;
using FoodAplication.Models;

namespace FoodAplication.Interface
{
    public interface IMenu
    {
        Menu GetMenuById(int id);
        Task<Menu> GetMenuByName(string name);
        Task<List<Menu>> GetListMenu();
        Task<List<Menu>> GetMenuByCategory(int idCategory);
        Task<Object> CreateMenu(MenuDto menuDto);
        Task<Object> UpdateMenu(MenuDto menuDto,int id);
        Task<Object> DeleteMenu(int id);
    }
}