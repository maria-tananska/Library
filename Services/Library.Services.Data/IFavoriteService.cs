namespace Library.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFavoriteService
    {
        public Task<int> AddToFavoriteAsync(string userId, int bookId);

        public IEnumerable<T> FavoriteBook<T>(string userId);

        public bool IsExist(string userId, int bookid);
    }
}
