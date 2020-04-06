namespace Library.Web.ViewModels.Admin.Books
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class BookViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
