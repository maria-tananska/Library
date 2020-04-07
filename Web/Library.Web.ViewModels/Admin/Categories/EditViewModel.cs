namespace Library.Web.ViewModels.Admin.Categories
{
    using System.ComponentModel.DataAnnotations;

    using Library.Data.Models;
    using Library.Services.Mapping;

    public class EditViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string Img { get; set; }
    }
}
