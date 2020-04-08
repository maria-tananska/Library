namespace Library.Web.ViewModels.Admin.Categories
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public IFormFile Img { get; set; }
    }
}
