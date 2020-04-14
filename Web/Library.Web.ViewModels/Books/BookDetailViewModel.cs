namespace Library.Web.ViewModels.Books
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class BookDetailViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortContent { get; set; }

        public string FileName { get; set; }

        public string ImgUrl { get; set; }
    }
}
