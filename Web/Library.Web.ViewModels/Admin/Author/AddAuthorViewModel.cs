namespace Library.Web.ViewModels.Admin.Author
{
    using System.ComponentModel.DataAnnotations;

    public class AddAuthorViewModel
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
    }
}
