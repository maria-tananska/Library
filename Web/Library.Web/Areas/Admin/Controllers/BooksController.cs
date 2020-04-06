namespace Library.Web.Areas.Admin.Controllers
{
    using Library.Services.Data;
    using Library.Web.ViewModels.Admin.Books;
    using Microsoft.AspNetCore.Mvc;

    [Area("Admin")]
    public class BooksController:Controller
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult All()
        {
            var model = new AllBooksViewModel
            {
                Books = this.bookService.All<BookViewModel>(),
            };

            return this.View(model);
        }
    }
}
