using FoodAplication.Dtos;
using FoodAplication.Interface;
using FoodAplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodAplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _accountRepo;
        public AccountController(IAccount account)
        {
            _accountRepo = account;
        }

        [HttpPost]
        public async Task<IActionResult> Register(Account account)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Data harus lengkap" });
            }
            else
            {
                var result = await _accountRepo.RegisterAccount(account) as dynamic;
                if (result != null && result!.success)
                {
                    return Json(result);
                }
                else
                {
                    return Json(new { success = false, message = result!.message });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Data harus lengkap" });
            }
            else
            {
                var result = await _accountRepo.LoginAccount(login) as dynamic;
                if (result != null && result!.success)
                {
                    return Json(result);
                }
                else
                {
                    return Json(new { success = false, message = result!.message });
                }
            }
        }

        [Authorize]
        public async Task<IActionResult> PartialLoginCheked()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value;

            if (email != null)
            {
                var account = await _accountRepo.GetAccountByEmail(email);
                return PartialView("_PartialIsLogin", account);
            }
            return PartialView("_PartialNotLogin");
        }
    }
}