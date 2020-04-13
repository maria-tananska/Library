namespace Library.Web.ViewModels.Favorite
{
    using System.Collections.Generic;

    public class AllFavoriteBooksViewModel
    {
        public IEnumerable<FavoriteBookViewModel> FavouriteBook { get; set; }
    }
}
