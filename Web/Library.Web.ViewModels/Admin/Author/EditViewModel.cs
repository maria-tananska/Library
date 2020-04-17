namespace Library.Web.ViewModels.Admin.Author
{
    using System.ComponentModel.DataAnnotations;

    using Library.Data.Models;
    using Library.Services.Mapping;

    public class EditViewModel : IMapFrom<Autor>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }
    }
}
