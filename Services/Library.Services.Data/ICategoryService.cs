namespace Library.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<int> CreateAsync(string name, string img);

        Task DeleteByIdAsync(int id);

        IEnumerable<T> GetAllCategories<T>();

        T GetById<T>(int id);

        Task EditAsync(int id, string name, string img);

        int CategoriesCount();
    }
}
