namespace Library.Web.ViewModels.Admin.Books
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class AuthorsDropDownViewModel : IMapFrom<Autor>
    {
        public int Id { get; set; }

        public string FirstName {get; set; }
        
        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";
    }
}
