namespace Library.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Library.Data.Common.Repositories;
    using Library.Data.Models;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<int> CreateAsync(string name)
        {
            var category = new Category
            {
                Name = name,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();

            return category.Id;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var category = this.categoryRepository.All()
                .Where(c => c.Id == id);

            await this.categoryRepository.GetByIdWithDeletedAsync(category);
            var result = await this.categoryRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
