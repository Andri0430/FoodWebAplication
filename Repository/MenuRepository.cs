using AutoMapper;
using FoodAplication.Context;
using FoodAplication.Dtos;
using FoodAplication.Helper;
using FoodAplication.Interface;
using FoodAplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAplication.Repository
{
    public class MenuRepository : IMenu
    {
        private readonly FoodContext _context;
        private readonly IMapper _mapper;
        public MenuRepository(FoodContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<object> CreateMenu(MenuDto menuDto)
        {
            string uploadFotoFileName = await UploadFoto.FotoUpload(menuDto.Foto!);

            if (await GetMenuByName(menuDto.Name) != null)
            {
                return new { success = false, message = "Nama menu telah digunakan!" };
            }
            if (uploadFotoFileName.StartsWith("Invalid file format") || uploadFotoFileName.StartsWith("Batas Ukuran File"))
            {
                return new { success = false, message = uploadFotoFileName };
            }
                var addMenu = new Menu
                {
                    Foto = uploadFotoFileName,
                    Name = menuDto.Name,
                    Price = menuDto.Price,
                    Description = menuDto.Description,
                    Category = _context.Categories.Where(c => c.Id == menuDto.IdCategory).FirstOrDefault()
                };
                _context.Menus.Add(addMenu);
                await _context.SaveChangesAsync();

            return new { success = true, message = "Menu berhasil ditambahkan" };
        }
        public async Task<List<Menu>> GetListMenu()
        {
            return await _context.Menus.Include(m => m.Category).ToListAsync();
        }

        public async Task<List<Menu>> GetMenuByCategory(int idCategory)
        {
            return await _context.Menus.Include(m => m.Category)
                        .Where(m => m.Category!.Id == idCategory).ToListAsync();
        }

        public Menu GetMenuById(int id)
        {
            return _context.Menus.Where(m => m.Id == id).FirstOrDefault()!;
        }

        public async Task<Object> UpdateMenu(MenuDto menuDto, int id)
        {
            string uploadFotoFileName = "";
            var menu = await GetMenuByName(menuDto.Name);
            var menuId = GetMenuById(id);

            if(menuDto.Foto != null)
            {
                uploadFotoFileName = await UploadFoto.FotoUpload(menuDto.Foto!);
                if (uploadFotoFileName.StartsWith("Invalid file format") || uploadFotoFileName.StartsWith("Batas Ukuran File"))
                {
                    return new { success = false, message = uploadFotoFileName };
                }
            }
            if (menu != null && menuId.Name != menu.Name)
            {
                return new { success = false, message = "Nama menu telah digunakan!" };
            }

            menuId.Name = menuDto.Name;
            menuId.Price = menuDto.Price;
            menuId.Description = menuDto.Description;
            menuId.Category = _context.Categories.Where(c => c.Id == menuDto.IdCategory).FirstOrDefault();

            if(menuDto.Foto != null)
            {
                menuId.Foto = uploadFotoFileName;
            }
            _context.Menus.Update(menuId);
            _context.SaveChanges();
            return new { success = true, message = "Menu berhasil berhasil diperharui" };
        }

        public async Task<Object> DeleteMenu(int id)
        {
            var menuToDelete =  GetMenuById(id);
            if (menuToDelete == null) return new { success = false, message = "Id menu tidak ditemukan" };

            _context.Menus.Remove(menuToDelete);
            await _context.SaveChangesAsync();

            return new { success = true, message = $"Menu {menuToDelete.Name} berhasil di hapus!" };
        }

        public async Task<Menu> GetMenuByName(string name)
        {
            var menu = await _context.Menus.Where(m => m.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync()!;
            return menu!;
        }
    }
}