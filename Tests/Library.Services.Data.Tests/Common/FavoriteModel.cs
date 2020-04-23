namespace Library.Services.Data.Tests.Common
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class FavoriteModel : IMapFrom<Favorite>
    {
        public string UserId { get; set; }

        public int CategoryId { get; set; }
    }
}
