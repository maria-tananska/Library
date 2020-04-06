namespace Library.Web.ViewModels.Home
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class FavoriteBookViewModel : IMapFrom<Favorite>
    {
        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public string BookImgUrl { get; set; }

        public string BookAutorFirstName { get; set; }

        public string BookAutorLastName { get; set; }
    }
}
