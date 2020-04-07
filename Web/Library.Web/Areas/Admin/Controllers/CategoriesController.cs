namespace Library.Web.Areas.Admin.Controllers
{
    using Library.Services.Data;
    using Library.Web.ViewModels.Admin.Categories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult All()
        {
            var model = new AllCategoriesViewModel
            {
                Categories = this.categoryService
                .GetAllCategories<CategoryViewModel>(),
            };

            return this.View(model);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoryService.CreateAsync(input.Name, input.Img);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.categoryService.DeleteByIdAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this.categoryService.GetById<EditViewModel>(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
