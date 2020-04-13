namespace Library.Web.Controllers
{
    using System.Diagnostics;

    using Library.Data.Models;
    using Library.Services.Data;
    using Library.Web.ViewModels;
    using Library.Web.ViewModels.Books;
    using Library.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IFavoriteService favoriteService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoryService categoryService;
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;

        public HomeController(
            IFavoriteService favoriteService,
            UserManager<ApplicationUser> userManager,
            ICategoryService categoryService,
            IBookService bookService,
            IAuthorService authorService)
        {
            this.favoriteService = favoriteService;
            this.userManager = userManager;
            this.categoryService = categoryService;
            this.bookService = bookService;
            this.authorService = authorService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var books = this.bookService.GetNewBooks<BookViewModel>();
            var bookCount = this.bookService.BooksCount();
            var pagesCount = this.bookService.PagesCount();
            var authorsCount = this.authorService.AuthorsCount();
            var categoriesCount = this.categoryService.CategoriesCount();
            var model = new IndexViewModel
            {
                Books = books,
                BooksCount = bookCount,
                PagesCount = pagesCount,
                AuthorsCount = authorsCount,
                CategoriesCount = categoriesCount,
            };

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
