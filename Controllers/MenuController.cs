using FoodAplication.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodAplication.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenu _menuRepo;
        public MenuController(IMenu menu)
        {
            _menuRepo = menu;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ChooseMenu()
        {
            var menus = await _menuRepo.GetListMenu();
            return PartialView("_PartialMenuData", menus);
        }

        [HttpPost]
        public async Task<IActionResult> FilteredMenu(int? idCategory, string? search)
        {
            var menus = await _menuRepo.GetListMenu();
            if(idCategory.HasValue)
            {
                if (!string.IsNullOrEmpty(search))
                {
                    menus = menus.Where(m => m.Name.ToLower().Contains(search.ToLower(), StringComparison.OrdinalIgnoreCase)).ToList();
                }
                return PartialView("_PartialMenuData", menus.Where(m => m.Category?.Id == idCategory).ToList());
            }

            if (!string.IsNullOrEmpty(search))
            {
                menus = menus.Where(m => m.Name.ToLower().Contains(search.ToLower(), StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return PartialView("_PartialMenuData",menus);
        }
    }
}
