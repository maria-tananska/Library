namespace Library.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Library.Data;
    using Library.Data.Models;
    using Library.Data.Repositories;
    using Library.Services.Mapping;
    using Library.Web.ViewModels.Category;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoryServiceTest
    {
        [Fact]
        public async Task GetCountShouldReturnCorrectValue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedCategoriesAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);

            Assert.Equal(2, service.CategoriesCount());
        }

        [Fact]
        public async Task EditShouldSaveCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedCategoriesAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);
            var name = "EditName";
            var imgUrl = "EditImgUrl";

            var id = repository.All().FirstOrDefault().Id;
            await service.EditAsync(id, name, imgUrl);
            var expected = repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            Assert.Equal(expected.Name, name);
            Assert.Equal(expected.Img, imgUrl);
        }

        [Fact]
        public async Task EditShouldThrowExeption()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.EditAsync(1, "Name", "ImgUrl");
            });
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectResult()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedCategoriesAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);
            var id = repository.All().FirstOrDefault().Id;

            var actualResult = service.GetById<CategoryViewModel>(id);
            var expectedResult = repository.All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Name, actualResult.Name);
            Assert.Equal(expectedResult.Img, actualResult.Img);
        }

        [Fact]
        public void GetByIdShouldThrowExeption()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);

            Assert.Throws<ArgumentException>(() =>
                service.GetById<CategoryViewModel>(1));
        }

        [Fact]
        public async Task DeleteShouldDeleteCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedCategoriesAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);
            var id = repository.All().FirstOrDefault().Id;

            await service.DeleteByIdAsync(id);

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task DeleteShouldThrowExeption()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.DeleteByIdAsync(1);
            });
        }

        [Fact]
        public async Task CreateShouldSaveCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);
            var name = "Name";
            var imgUrl = "ImgUrl";

            await service.CreateAsync(name, imgUrl);

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task CreateShouldSaveDataCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);
            var name = "Name";
            var imgUrl = "ImgUrl";

            await service.CreateAsync(name, imgUrl);
            var actualResult = repository.All().FirstOrDefault();

            Assert.Equal(name, actualResult.Name);
            Assert.Equal(imgUrl, actualResult.Img);
        }

        [Fact]
        public async Task GetAllShouldReturnCorrectCount()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedCategoriesAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);

            var booksCount = service.GetAllCategories<CategoryViewModel>().Count();

            Assert.Equal(2, booksCount);
        }

        [Fact]
        public async Task GetAllShouldReturnCorrectData()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedCategoriesAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);

            var actualResult = service.GetAllCategories<CategoryViewModel>().ToList();
            var expectedResult = repository.All().ToList();

            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Id, actualResult[i].Id);
                Assert.Equal(expectedResult[i].Name, actualResult[i].Name);
                Assert.Equal(expectedResult[i].Img, actualResult[i].Img);
            }
        }

        private async Task SeedCategoriesAsync(ApplicationDbContext dbContext)
        {
            await dbContext.Categories.AddAsync(
                new Category
                {
                    Name = "Category1",
                    Img = "imgUrl1",
                });
            await dbContext.Categories.AddAsync(
               new Category
               {
                   Name = "Category2",
                   Img = "imgUrl2",
               });

            await dbContext.SaveChangesAsync();
        }

        private void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(CategoryViewModel).GetTypeInfo().Assembly,
                typeof(Category).GetTypeInfo().Assembly);
        }

    }
}
