namespace Library.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
       Task<int> CreateAsync(string name);

       Task<bool> DeleteByIdAsync(int id);

       IEnumerable<T> GetAllCategories<T>();
    }
}
