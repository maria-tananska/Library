namespace Library.Web.Areas.Admin.Controllers
{
    using Library.Services.Data;
    using Library.Web.ViewModels.Admin.Books;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class BooksController:Controller
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;
        private readonly IAuthorService authorService;

        public BooksController(IBookService bookService, ICategoryService categoryService, IAuthorService authorService)
        {
            this.bookService = bookService;
            this.categoryService = categoryService;
            this.authorService = authorService;
        }

        public IActionResult All()
        {
            var model = new AllBooksViewModel
            {
                Books = this.bookService.All<BookViewModel>(),
            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = this.categoryService
                .GetAllCategories<CategoriesDropDownViewModel>();
            var authors = this.authorService
                .GetAuthors<AuthorsDropDownViewModel>();

            var model = new CreateViewModel
            {
                Categories = categories,
                Authors = authors,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.bookService
                .AddAsync(input.Title, input.ShortContent, input.ImgUrl, input.FileName, input.Pages, input.CategoryId, input.AutorId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.bookService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

    }
}
