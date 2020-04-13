namespace Library.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Linq;

    using Library.Web.ViewModels.Books;

    public class IndexViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; }

        public int PagesCount { get; set; }

        public int BooksCount { get; set; }

        public int AuthorsCount { get; set; }

        public int CategoriesCount { get; set; }
    }
}
