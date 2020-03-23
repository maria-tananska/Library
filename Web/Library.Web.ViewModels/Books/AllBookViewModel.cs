namespace Library.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class AllBookViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; }
    }
}
