namespace Library.Services.Data.Tests
{
    using Library.Data;
    using Library.Data.Models;
    using Library.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

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
            var service = new BookService(repository);

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
            var service = new BookService(repository);

            Assert.Equal(60, service.PagesCount());
        }

        private async Task SeedBooksAsync(ApplicationDbContext dbContext)
        {
            await dbContext.Books.AddAsync(new Book
            {
                Title = "Book1",
                ShortContent = "Book1",
                ImgUrl = "ImgUrl1",
                FileName = "FileUrl1",
                Pages = 20,
                CategoryId = 1,
                AutorId = 1,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Book2",
                ShortContent = "Book2",
                ImgUrl = "ImgUrl2",
                FileName = "FileUrl2",
                Pages = 20,
                CategoryId = 1,
                AutorId = 1,
            });
            await dbContext.Books.AddAsync(new Book
            {
                Title = "Book3",
                ShortContent = "Book3",
                ImgUrl = "ImgUrl3",
                FileName = "FileUrl3",
                Pages = 20,
                CategoryId = 1,
                AutorId = 1,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
