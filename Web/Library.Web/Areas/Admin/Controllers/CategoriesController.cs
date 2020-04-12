namespace Library.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Library.Common;
    using Library.Services;
    using Library.Services.Data;
    using Library.Web.ViewModels.Admin.Categories;
    using Microsoft.AspNetCore.Mvc;

    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ICloudinaryService cloudinaryService;

        public CategoriesController(ICategoryService categoryService, ICloudinaryService cloudinaryService)
        {
            this.categoryService = categoryService;
            this.cloudinaryService = cloudinaryService;
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

            var imgUrl = await this.cloudinaryService.UploadPhotoAsync(
            input.Img,
            $"{input.Name}",
            GlobalConstants.CloudFolderForCategories);

            await this.categoryService.CreateAsync(input.Name, imgUrl);

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

            string imgUrl = null;
            if (input.ImgUrl != null)
            {
                imgUrl = await this.cloudinaryService.UploadPhotoAsync(
               input.ImgUrl,
               $"{input.Name}",
               GlobalConstants.CloudFolderForCategories);
            }

            await this.categoryService
                .EditAsync(input.Id, input.Name, imgUrl);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
