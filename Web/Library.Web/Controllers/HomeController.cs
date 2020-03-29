namespace Library.Web.Controllers
{
    using System.Diagnostics;

    using Library.Data.Models;
    using Library.Services.Data;
    using Library.Web.ViewModels;
    using Library.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IFavoriteService favoriteService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IFavoriteService favoriteService, UserManager<ApplicationUser> userManager)
        {
            this.favoriteService = favoriteService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction(nameof(this.IndexLoggin));
            }

            return this.View();
        }

        [Authorize]
        [Route("/Home/Index")]
        public IActionResult IndexLoggin()
        {
            string userId = this.userManager.GetUserId(this.User);
            var model = new IndexViewModel
            {
               FavouriteBook = this.favoriteService
                .FavoriteBook<FavoriteBookViewModel>(userId),
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
