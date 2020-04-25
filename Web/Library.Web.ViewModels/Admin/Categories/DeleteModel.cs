namespace Library.Web.ViewModels.Admin.Categories
{
    using System.ComponentModel.DataAnnotations;

    using Library.Data.Models;
    using Library.Services.Mapping;

    public class DeleteModel : IMapFrom<Category>
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
