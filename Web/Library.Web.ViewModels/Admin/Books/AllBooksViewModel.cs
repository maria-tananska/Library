namespace Library.Web.ViewModels.Admin.Books
{
    using System.Collections.Generic;

    public class AllBooksViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; }
    }
}
