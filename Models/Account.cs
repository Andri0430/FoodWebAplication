using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FoodAplication.Models
{
    public class Account : IdentityUser
    {
        [Key]
        [Required(ErrorMessage ="Email harus di isi")]
        [RegularExpression(@"^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+\.[A-Za-z]+$", ErrorMessage = "Alamat email tidak valid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password harus di isi")]
        [RegularExpression(@"^.{8,}$", ErrorMessage = "Password harus terdiri dari minimal 8 karakter")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nama Lengkap harus di isi")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Nama hanya boleh mengandung huruf dan spasi")]
        public string CompleteName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nomor Telepon harus di isi")]
        [RegularExpression(@"^08\d{9}(\d{2})?$", ErrorMessage = "Nomor Telepon Tidak Sesuai")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Provinsi harus di isi")]
        public string Province { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kabupaten/Kota harus di isi")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kecamatan harus di isi")]
        public string Subdistrict { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kelurahan harus di isi")]
        public string Ward { get; set; } = string.Empty;

        public string? Role { get; set; } = string.Empty;
    }
}
