namespace Library.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFavoriteService
    {
       Task AddToFavoriteAsync(string userId, int bookId);

        IEnumerable<T> FavoriteBook<T>(string userId);

        bool IsExist(string userId, int bookid);
    }
}
