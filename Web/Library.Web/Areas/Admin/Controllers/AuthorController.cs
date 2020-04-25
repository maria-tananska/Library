namespace Library.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Library.Services.Data;
    using Library.Web.ViewModels.Admin.Author;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorController : AdministrationController
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddAuthorViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.authorService.AddAsync(input.FirstName, input.LastName);

            return this.RedirectToAction(nameof(this.Authors));
        }

        public IActionResult Authors()
        {
            var model = new AllAuthorViewModel
            {
                Authors = this.authorService
                .GetAuthors<AuthorViewModel>(),
            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this.authorService.GetById<EditViewModel>(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.authorService.EditAsync(input.FirstName, input.LastName, input.Id);

            return this.RedirectToAction("Authors");
        }

        public IActionResult Delete(int id)
        {
            var model = this.authorService.GetById<DeleteModel>(id);

            return this.View(model);
        }

        [HttpPost]
        [Route("/Admin/Author/Delete/{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            await this.authorService.DeleteByIdAsync(id);

            return this.RedirectToAction(nameof(this.Authors));
        }
    }
}
