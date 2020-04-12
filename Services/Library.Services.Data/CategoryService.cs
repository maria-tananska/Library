namespace Library.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Library.Data.Common.Repositories;
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<int> CreateAsync(string name, string img)
        {
            var category = new Category
            {
                Name = name,
                Img = img,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();

            return category.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var category = await this.categoryRepository.GetByIdWithDeletedAsync(id);
            //this.categoryRepository.Delete(category);
            category.IsDeleted = true;
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name, string img)
        {
            var category = await this.categoryRepository.GetByIdWithDeletedAsync(id);

            if (category == null)
            {
                throw new ArgumentException($"Category with id {id} don't exist!");
            }

            if (img != null)
            {
                category.Img = img;
            }

            category.Name = name;
            await this.categoryRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllCategories<T>()
        {
            var categories = this.categoryRepository.All()
                .To<T>()
                .ToList();

            return categories;
        }

        public T GetById<T>(int id)
        {
            var category = this.categoryRepository.All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();

            if (category == null)
            {
                throw new ArgumentException($"Category with id {id} don't exist!");
            }

            return category;
        }
    }
}
