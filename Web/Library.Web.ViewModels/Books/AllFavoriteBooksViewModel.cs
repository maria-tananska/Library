namespace Library.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class AllFavoriteBooksViewModel
    {
        public IEnumerable<FavoriteBookViewModel> FavouriteBook { get; set; }
    }
}
