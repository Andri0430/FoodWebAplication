using FoodAplication.Interface;
using FoodAplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodAplication.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cartRepository;
        private readonly IAccount _accountRepo;
        private readonly IMenu _menuRepo;

        public CartController(ICart cart, IAccount account, IMenu menu)
        {
            _cartRepository = cart;
            _accountRepo = account;
            _menuRepo = menu;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCart(int menuId, int qty)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value;
            var cart = new Cart
            {
                Account = await _accountRepo.GetAccountByEmail(email),
                Menu = _menuRepo.GetMenuById(menuId),
                Quantity = qty
            };

            var result = await _cartRepository.AddCart(cart) as dynamic;
            if (result != null && result!.success)
            {
                return Json(result);
            }
            else
            {
                return Json(new { success = false, message = result!.message });
            }
        }

        [Authorize]
        public async Task<IActionResult> CartList()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value;
            var carts = await _cartRepository.GetCartsByEmail(user);
            return PartialView("_PartialModalCart",carts);
        }

    }
}
