using FoodAplication.Models;

namespace FoodAplication.Interface
{
    public interface ICart
    {
        Task<List<Cart>> GetCarts();
        Task<Cart> GetCartId(int id);
        Task<Cart> GetCartByEmailandMenuId(int idMenu, string email);
        Task<List<Cart>> GetCartsByEmail(string email);
        Task<List<Cart>> GetCartsByMenuId(int id);
        Task<object> AddCart(Cart cart);
        Task<object> UpdateCart(Cart cart);
        Task<object> DeleteCart(int id);
    }
}
