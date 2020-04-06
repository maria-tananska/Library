namespace Library.Web.ViewModels.Admin.Author
{
    using Library.Data.Models;
    using Library.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class EditViewModel : IMapFrom<Autor>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
    }
}