namespace Library.Web.Controllers
{
    using Library.Data.Models;
    using Library.Services.Data;
    using Library.Web.ViewModels.Favorite;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFavoriteService favoriteService;

        public FavoriteController(UserManager<ApplicationUser> userManager, IFavoriteService favoriteService)
        {
            this.userManager = userManager;
            this.favoriteService = favoriteService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(FavoriteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (userId == null)
            {
                throw new ArgumentException($"This user don't exist!");
            }

            await this.favoriteService.AddToFavoriteAsync(userId, input.BookId);

            return this.Ok();
        }
    }
}
