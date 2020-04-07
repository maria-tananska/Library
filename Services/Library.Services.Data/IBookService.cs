namespace Library.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        IEnumerable<T> All<T>();

        Task<int> AddAsync(string title, string shortContent, string imgUrl, string fileName, int pages, int categoryId, int authorId);

        T GetById<T>(int id);

        Task DeleteAsync(int id);
    }
}
