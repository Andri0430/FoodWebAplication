using System.ComponentModel.DataAnnotations;

namespace FoodAplication.Dtos
{
    public class MenuDto
    {
        [Required]
        public IFormFile? Foto { get; set; }

        [Required(ErrorMessage = "Nama menu harus diisi")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Nama hanya boleh mengandung huruf dan spasi")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(150, ErrorMessage = "Deskripsi tidak boleh melebihi 150 karakter.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Harga harus diisi")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Harga hanya boleh berisi angka")]
        [Range(100, int.MaxValue, ErrorMessage = "Harga tidak sesuai")]
        public int Price { get; set; }
        [Required(ErrorMessage ="Harus memilih kategori")]
        public int? IdCategory { get; set; }
    }
}
