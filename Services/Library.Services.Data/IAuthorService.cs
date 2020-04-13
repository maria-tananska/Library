namespace Library.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAuthorService
    {
        Task<int> AddAsync(string firstName, string lastName);

        Task DeleteByIdAsync(int id);

        IEnumerable<T> GetAuthors<T>();

        T GetById<T>(int id);

        Task EditAsync(string firstName, string lastName, int id);

        int AuthorsCount();
    }
}
