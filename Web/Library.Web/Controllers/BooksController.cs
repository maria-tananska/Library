namespace Library.Web.Controllers
{
    using Library.Data.Models;
    using Library.Services.Data;
    using Library.Web.ViewModels.Books;
    using Library.Web.ViewModels.Favorite;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : BaseController
    {
        private readonly IBookService bookService;
        private readonly IFavoriteService favoriteService;
        private readonly UserManager<ApplicationUser> userManager;

        public BooksController(
            IBookService bookService,
            IFavoriteService favoriteService,
            UserManager<ApplicationUser> userManager)
        {
            this.bookService = bookService;
            this.favoriteService = favoriteService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var books = new AllBookViewModel
            {
                Books = this.bookService.All<BookViewModel>(),
            };

            return this.View(books);
        }

        [HttpPost]
        public IActionResult All(AllBookViewModel input)
        {
            var model = new AllBookViewModel
            {
                Books = this.bookService
                .SearchBooks<BookViewModel>(input.SearchText),

            };

            return this.View(model);
        }

        public IActionResult Detail(int id)
        {
            var book = this.bookService.GetByIdTo<BookDetailViewModel>(id);

            return this.View(book);
        }

        [Authorize]
        public IActionResult Favorite()
        {
            string userId = this.userManager.GetUserId(this.User);
            var model = new AllFavoriteBooksViewModel
            {
                FavouriteBook = this.favoriteService
                .FavoriteBook<FavoriteBookViewModel>(userId),
            };

            return this.View(model);
        }
    }
}
