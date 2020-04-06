namespace Library.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<FavoriteBookViewModel> FavouriteBook { get; set; }
    }
}
