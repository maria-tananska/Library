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
    using Library.Web.ViewModels.Books;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class BookServiceTest
    {
        [Fact]
        public async Task BookCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedBooksAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);

            Assert.Equal(3, service.BooksCount());
        }

        [Fact]
        public async Task PagesCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedBooksAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);

            Assert.Equal(60, service.PagesCount());
        }

        [Fact]
        public async Task AddShouldSaveCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);

            await service.AddAsync("Title", "ShortContent", "ImgUrl", "FileUrl", 30, 1, 1);

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task AddShouldSaveCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);
            var title = "Title";
            var shortContent = "ShortContent";
            var imgUrl = "ImgUrl";
            var fileUrl = "fileUrl";
            var pages = 20;
            var authorId = 1;
            var categoryId = 1;

            await service.AddAsync(title, shortContent, imgUrl, fileUrl, pages, categoryId, authorId);
            var actualResult = repository.All().FirstOrDefault();

            Assert.Equal(title, actualResult.Title);
            Assert.Equal(shortContent, actualResult.ShortContent);
            Assert.Equal(imgUrl, actualResult.ImgUrl);
            Assert.Equal(fileUrl, actualResult.FileName);
            Assert.Equal(pages, actualResult.Pages);
            Assert.Equal(authorId, actualResult.AutorId);
            Assert.Equal(categoryId, actualResult.CategoryId);

        }

        [Fact]
        public async Task EditShouldSaveCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedBooksAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);
            var id = repository.All().FirstOrDefault().Id;
            var title = "Title";
            var shortContent = "ShortContent";
            var imgUrl = "ImgUrl";
            var fileUrl = "fileUrl";
            var pages = 20;
            var authorId = 1;
            var categoryId = 1;

            await service.EditAsync(id, title, shortContent, imgUrl, fileUrl, pages, categoryId, authorId);
            var actualResult = repository.All()
                .Where(b => b.Id == id)
                .FirstOrDefault();

            Assert.Equal(title, actualResult.Title);
            Assert.Equal(shortContent, actualResult.ShortContent);
            Assert.Equal(imgUrl, actualResult.ImgUrl);
            Assert.Equal(fileUrl, actualResult.FileName);
            Assert.Equal(pages, actualResult.Pages);
            Assert.Equal(authorId, actualResult.AutorId);
            Assert.Equal(categoryId, actualResult.CategoryId);
        }

        [Fact]
        public async Task EditShouldThrowExeption()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.EditAsync(1, "Title", "ShortContent", "ImgUrl", "FileUrl", 30, 1, 1);
            });
        }

        [Fact]
        public async Task DeleteShouldDeleteCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedBooksAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);
            var id = repository.All().FirstOrDefault().Id;

            await service.DeleteAsync(id);

            Assert.Equal(2, repository.All().Count());
        }

        [Fact]
        public async Task DeleteShouldThrowExeption()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.DeleteAsync(1);
            });
        }

        [Fact]
        public async Task GetBooksByCategoryIdShouldReturnCorrectResult()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedBooksAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);
            var categoryId = categoryRepository.All().FirstOrDefault().Id;

            var actualResult = service.GetBooksByCategoryId<BookViewModel>(categoryId)
                .ToList();
            var expectedResult = repository.All()
                .Where(b => b.CategoryId == categoryId).ToList();

            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Id, actualResult[i].Id);
                Assert.Equal(expectedResult[i].Title, actualResult[i].Title);
            }

        }

        [Fact]
        public void GetBooksByCategoryIdShouldThrowExeption()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);

            Assert.Throws<ArgumentException>(() => service.GetBooksByCategoryId<BookDetailViewModel>(1));
        }

        [Fact]
        public async Task AllShouldReturnCorrectCount()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedBooksAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);

            var actualResult = service.All<BookDetailViewModel>().Count();

            Assert.Equal(repository.All().Count(), actualResult);
        }

        [Fact]
        public async Task AllShouldReturnCorrectData()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedBooksAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Book>(dbContext);
            var categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new BookService(repository, categoryRepository);

            var actualResult = service.All<BookDetailViewModel>().ToList();
            var expectedResult = repository.All().ToList();

            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Id, actualResult[i].Id);
                Assert.Equal(expectedResult[i].Title, actualResult[i].Title);
            }
        }

        private async Task SeedBooksAsync(ApplicationDbContext dbContext)
        {
            await dbContext.Categories.AddAsync(new Category { Name = "Name", Img = "ImgUrl" });
            await dbContext.SaveChangesAsync();
            var categoryId = dbContext.Categories.FirstOrDefault().Id;
            await dbContext.Books.AddAsync(new Book
            {
                Title = "Book1",
                ShortContent = "Book1",
                ImgUrl = "ImgUrl1",
                FileName = "FileUrl1",
                Pages = 20,
                CategoryId = categoryId,
                AutorId = 1,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Book2",
                ShortContent = "Book2",
                ImgUrl = "ImgUrl2",
                FileName = "FileUrl2",
                Pages = 20,
                CategoryId = 2,
                AutorId = 1,
            });
            await dbContext.Books.AddAsync(new Book
            {
                Title = "Book3",
                ShortContent = "Book3",
                ImgUrl = "ImgUrl3",
                FileName = "FileUrl3",
                Pages = 20,
                CategoryId = categoryId,
                AutorId = 1,
            });

            await dbContext.SaveChangesAsync();
        }

        private void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(BookDetailViewModel).GetTypeInfo().Assembly,
                typeof(Book).GetTypeInfo().Assembly);
        }
    }
}
