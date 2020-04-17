namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Library.Data.Common.Models;

    public class Book : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        public string ShortContent { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        [Required]
        public string FileName { get; set; }

        [Range(1, int.MaxValue)]
        public int Pages { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int AutorId { get; set; }

        public Autor Autor { get; set; }
    }
}
