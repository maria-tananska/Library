namespace Library.Web.ViewModels.Admin.Books
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Library.Data.Models;
    using Library.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class CreateViewModel : IMapTo<Book>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string ShortContent { get; set; }

        [Required]
        public IFormFile Img { get; set; }

        [Required]
        public IFormFile File { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Pages { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoriesDropDownViewModel> Categories { get; set; }

        [DisplayName("Author")]
        public int AutorId { get; set; }

        public IEnumerable<AuthorsDropDownViewModel> Authors { get; set; }

    }
}
