namespace Library.Web.ViewModels.Admin.Books
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class DeleteModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
