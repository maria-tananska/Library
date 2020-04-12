namespace Library.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Library.Services.Data.DTO;

    public interface IBookService
    {
        IEnumerable<T> All<T>();

        Task<int> AddAsync(string title, string shortContent, string imgUrl, string fileName, int pages, int categoryId, int authorId);

        EditBookDTO GetById(int id);

        T GetByIdTo<T>(int id);

        Task DeleteAsync(int id);

        Task EditAsync(int id, string title, string shortContent, string imgUrl, string fileName, int pages, int categoryId, int authorId);
    }
}
