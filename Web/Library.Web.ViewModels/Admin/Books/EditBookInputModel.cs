namespace Library.Web.ViewModels.Admin.Books
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Library.Data.Models;
    using Library.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditBookInputModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        public string ShortContent { get; set; }

        public IFormFile Img { get; set; }

        public IFormFile FileName { get; set; }

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
