using AutoMapper;
using FoodAplication.Dtos;
using FoodAplication.Interface;
using FoodAplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodAplication.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMenu _menuRepo;
        private readonly ICategory _categoryRepo;

        public DashboardController(IMapper mapper, IMenu menuRepo, ICategory categoryRepo)
        {
            _mapper = mapper;
            _menuRepo = menuRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpPost]
        public async Task<IActionResult> MenuByCategory(int id)
        {
            List<Menu> menus = await _menuRepo.GetMenuByCategory(id);
            return PartialView("_PartialMenuByCategory",_mapper.Map<List<Menu>>(menus.OrderByDescending(m => m.Id)));
        }

        public async Task<IActionResult> Menu()
        {
            var menus = await _menuRepo.GetMenuByCategory(1);
            return View(_mapper.Map<List<Menu>>(menus.OrderByDescending(m => m.Id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu(MenuDto menu)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {success = false, message = "Data tidak valid atau tidak lengkap" });
            }
            else
            {
                var result = await _menuRepo.CreateMenu(menu) as dynamic;
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
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _menuRepo.DeleteMenu(id);

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> EditMenu(MenuDto menu, int id)
        {
            if (menu.IdCategory == null)
            {
                return Json(new { success = false, message = "Pilih kategori menu" });
            }
            else
            {
                var result = await _menuRepo.UpdateMenu(menu,id) as dynamic;
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
    }
}
