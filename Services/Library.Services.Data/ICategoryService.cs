namespace Library.Services.Data
{
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        public Task<int> CreateAsync(string name);

        public Task<bool> DeleteByIdAsync(int id);
    }
}
