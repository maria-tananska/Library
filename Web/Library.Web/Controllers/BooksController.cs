namespace Library.Web.Controllers
{
    using Library.Services.Data;
    using Library.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : BaseController
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult All()
        {
            var books = new AllBookViewModel
            {
                Books = this.bookService.All<BookViewModel>(),
            };

            return this.View(books);
        }

        public IActionResult Detail(int id)
        {
            var book = this.bookService.GetById<BookDetailViewModel>(id);

            return this.View(book);
        }
    }
}
