namespace Library.Web.Controllers
{
    using Library.Services.Data;
    using Library.Web.ViewModels.Books;
    using Library.Web.ViewModels.Category;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IBookService bookService;

        public CategoriesController(ICategoryService categoryService, IBookService bookService)
        {
            this.categoryService = categoryService;
            this.bookService = bookService;
        }

        public IActionResult All()
        {
            var categories = this.categoryService
                .GetAllCategories<CategoryViewModel>();

            var model = new AllViewModel
            {
                Categories = categories,
            };

            return this.View(model);
        }

        public IActionResult Books(int id)
        {
            var books = this.bookService
                .GetBooksByCategoryId<BookViewModel>(id);

            var model = new AllBookViewModel
            {
                Books = books,
            };

            return this.View(model);
        }
    }
}
