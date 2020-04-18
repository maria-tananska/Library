namespace Library.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Library.Data.Models;
    using Library.Services.Data;
    using Library.Web.ViewModels.Favorite;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<FavoriteResponseModel>> Post(FavoriteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            string result = string.Empty;

            if (userId == null)
            {
                throw new ArgumentException($"This user don't exist!");
            }

            if (this.favoriteService.IsExist(userId, input.BookId))
            {
               await this.favoriteService.RemoveFromFavoriteAsync(userId, input.BookId);

               result = "Remove from favorite!";
            }
            else
            {
                await this.favoriteService.AddToFavoriteAsync(userId, input.BookId);
                result = "Add to favorite!";
            }

            return new FavoriteResponseModel { Result = result };
        }
    }
}
