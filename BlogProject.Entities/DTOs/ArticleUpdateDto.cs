using BlogProject.Entities.Concrete;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Entities.DTOs
{
    public class ArticleUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamlıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamlıdır.")]
        public string Title { get; set; }
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamlıdır.")]
        public string Content { get; set; }
        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(250, ErrorMessage = "{0} alanı {1} karakterden büyük olmamlıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamlıdır.")]
        public string Thumbnail { get; set; }
        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayName("Seo Yazar")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden büyük olmamlıdır.")]
        public string SeoAuthor { get; set; }
        [DisplayName("Seo Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden büyük olmamlıdır.")]
        [MinLength(0, ErrorMessage = "{0} alanı {1} karakterden küçük olmamlıdır.")]
        public string SeoDescription { get; set; }
        [DisplayName("Seo Etiketler")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} alanı {1} karakterden büyük olmamlıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamlıdır.")]
        public string SeoTags { get; set; }
        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [DisplayName("Aktif mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public bool IsActive { get; set; }
        [DisplayName("Silinsin mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public bool IsDeleted { get; set; }
    }
}
