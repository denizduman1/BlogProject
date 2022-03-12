using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Entities.DTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} alanı max {1} karakter alabilir.")]
        [MinLength(3, ErrorMessage = "{0} alanı min {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} alanı max {1} karakter alabilir.")]
        [MinLength(3, ErrorMessage = "{0} alanı min {1} karakter olmalıdır.")]
        public string Description { get; set; }
        [DisplayName("Kategori Özel Not Alanı")]
        [MaxLength(500, ErrorMessage = "{0} alanı max {1} karakter alabilir.")]
        [MinLength(3, ErrorMessage = "{0} alanı min {1} karakter olmalıdır.")]
        public string Note { get; set; }
        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public bool IsActive { get; set; }
        [DisplayName("Silindi Mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public bool IsDeleted { get; set; }
    }
}
