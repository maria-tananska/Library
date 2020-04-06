namespace Library.Web.Areas.Admin.Controllers
{
    using Library.Services.Data;
    using Library.Web.ViewModels.Admin.Categories;
    using Microsoft.AspNetCore.Mvc;

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
    }
}
