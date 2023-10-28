using FoodAplication.Context;
using FoodAplication.Interface;
using FoodAplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAplication.Repository
{
    public class CartRepository : ICart
    {
        private readonly FoodContext _context;
        private readonly IAccount _accountRepo;
        private readonly IMenu _menuRepo;
        public CartRepository(FoodContext context, IAccount account, IMenu menu)
        {
            _context = context;
            _accountRepo = account;
            _menuRepo = menu;
        }

        public async Task<object> AddCart(Cart cart)
        {
            var cartExist = await GetCartByEmailandMenuId(cart.Menu!.Id, cart.Account!.Email);

            if (cartExist != null)
            {
                await UpdateCart(cart);
                return new { success = true, message = "Menu berhasil ditambahkan ke keranjang" };
            }

            _context.Add(cart);
            _context.SaveChanges();
            return new { success = true, message = "Menu berhasil ditambahkan ke keranjang" };
        }

        public async Task<object> DeleteCart(int id)
        {
            var cartDelete = await GetCartId(id);

            if(cartDelete == null)
            {
                return new { success = false, message = "Data tidak ditemukan" };
            }
            _context.Carts.Remove(cartDelete);
            _context.SaveChanges();

            return new { success = true, message = "Data berhasil dihapus" };
        }

        public async Task<Cart> GetCartByEmailandMenuId(int id,string email)
        {
            var cart = await GetCarts();
            return cart.Where(c => c.Account!.Email == email && c.Menu!.Id == id).FirstOrDefault()!;
        }

        public Task<Cart> GetCartId(int id)
        {
            return _context.Carts.Where(c => c.Id == id).FirstOrDefaultAsync()!;

        }

        public Task<List<Cart>> GetCarts()
        {
            return _context.Carts
               .Include(c => c.Account)
               .Include(c => c.Menu).ToListAsync();
        }

        public Task<List<Cart>> GetCartsByEmail(string email)
        {
            return _context.Carts
                .Include(c => c.Account)
                .Include(c => c.Menu).ThenInclude(m => m.Category)
                .Where(c => c.Account!.Email == email).ToListAsync();
        }

        public Task<List<Cart>> GetCartsByMenuId(int id)
        {
            return _context.Carts
               .Include(c => c.Account)
               .Include(c => c.Menu)
               .Where(c => c.Menu!.Id == id).ToListAsync();
        }

        public async Task<object> UpdateCart(Cart cart)
        {
            var cartExist = await GetCartByEmailandMenuId(cart.Menu!.Id, cart.Account!.Email);

            if (cartExist == null)
            {
                return new { success = false, message = "Cart tidak ditemukan" };
            }

            cartExist.Menu = _menuRepo.GetMenuById(cartExist.Menu!.Id);
            cartExist.Quantity += cart.Quantity;
            cartExist.Account = await _accountRepo.GetAccountByEmail(cart.Account.Email);

            _context.Carts.Update(cartExist);
            await _context.SaveChangesAsync();

            return new { success = true, message = "Berhasil memperbarui keranjang" };
        }

    }
}
