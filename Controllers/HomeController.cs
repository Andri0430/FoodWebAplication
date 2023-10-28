using FoodAplication.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodAplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenu _menuRepo;
        public HomeController(IMenu menuRepo)
        {
            _menuRepo = menuRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PartialRegister()
        {
            return PartialView("_PartialRegister");
        }
        public IActionResult PartialLogin()
        {
            return PartialView("_PartialLogin");
        }
    }
}