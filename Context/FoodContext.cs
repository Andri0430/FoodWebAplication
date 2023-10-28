using FoodAplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAplication.Context
{
    public class FoodContext : DbContext
    {
        public FoodContext(DbContextOptions options) : base(options) { }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
