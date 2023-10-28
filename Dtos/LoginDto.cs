using System.ComponentModel.DataAnnotations;

namespace FoodAplication.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email harus di isi")]
        [RegularExpression(@"^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+\.[A-Za-z]+$", ErrorMessage = "Alamat email tidak valid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password harus di isi")]
        [RegularExpression(@"^.{8,}$", ErrorMessage = "Password harus terdiri dari minimal 8 karakter")]
        public string Password { get; set; } = string.Empty;
    }
}