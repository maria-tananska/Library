namespace Library.Web.Controllers
{
    using Library.Services.Data;
    using Library.Web.ViewModels.Category;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
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
    }
}
