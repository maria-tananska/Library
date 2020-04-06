namespace Library.Web.ViewModels.Admin.Author
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class AuthorViewModel : IMapFrom<Autor>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
