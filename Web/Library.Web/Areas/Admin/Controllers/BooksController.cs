namespace Library.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Library.Common;
    using Library.Services;
    using Library.Services.Data;
    using Library.Web.ViewModels.Admin.Books;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.FileProviders;

    public class BooksController : AdministrationController
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;
        private readonly IAuthorService authorService;
        private readonly ICloudinaryService cloudinaryService;

        public BooksController(
            IBookService bookService,
            ICategoryService categoryService,
            IAuthorService authorService,
            ICloudinaryService cloudinaryService)
        {
            this.bookService = bookService;
            this.categoryService = categoryService;
            this.authorService = authorService;
            this.cloudinaryService = cloudinaryService;
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

            var imgUrl = await this.cloudinaryService.UploadAsync(
                input.Img,
                input.Title,
                GlobalConstants.CloudFolderForBooks);

            var fileUrl = await this.cloudinaryService.UploadAsync(
                input.File,
                input.Title,
                GlobalConstants.CloudFolderForBooksContent);

            await this.bookService
                .AddAsync(input.Title, input.ShortContent, imgUrl, fileUrl, input.Pages, input.CategoryId, input.AutorId);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Edit(int id)
        {
            var categories = this.categoryService
                .GetAllCategories<CategoriesDropDownViewModel>();

            var authors = this.authorService
                .GetAuthors<AuthorsDropDownViewModel>();

            var dto = this.bookService.GetById(id);
            var model = new EditBookInputModel
            {
                Id = dto.Id,
                Title = dto.Title,
                ShortContent = dto.ShortContent,
                Pages = dto.Pages,
                FileName = dto.FileName,
                AutorId = dto.AutorId,
                CategoryId = dto.CategoryId,
                Categories = categories,
                Authors = authors,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string imgUrl = null;
            if (input.Img != null)
            {
               imgUrl = await this.cloudinaryService.UploadAsync(
               input.Img,
               input.Title,
               GlobalConstants.CloudFolderForBooks);
            }

            await this.bookService
                .EditAsync(input.Id, input.Title, input.ShortContent, imgUrl, input.FileName, input.Pages, input.CategoryId, input.AutorId);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.bookService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
