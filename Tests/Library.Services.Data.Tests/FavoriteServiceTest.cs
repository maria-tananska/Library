namespace Library.Services.Data.Tests
{
    using Library.Data;
    using Library.Data.Models;
    using Library.Data.Repositories;
    using Library.Services.Data.Tests.Common;
    using Library.Services.Mapping;
    using Library.Web.ViewModels.Books;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class FavoriteServiceTest
    {
        [Fact]
        public async Task IsExistShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.Seed(dbContext);

            var repository = new EfDeletableEntityRepository<Favorite>(dbContext);
            var service = new FavoriteService(repository);
            var favoriteBook = repository.All().FirstOrDefault();

            var actualResult = service.IsExist(favoriteBook.UserId, favoriteBook.BookId);

            Assert.True(actualResult == true);
        }

        [Fact]
        public void IsExistShouldReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Favorite>(dbContext);
            var service = new FavoriteService(repository);
            var favoriteBook = repository.All().FirstOrDefault();

            var actualResult = service.IsExist("UserId", 1);

            Assert.True(actualResult == false);
        }

        [Fact]
        public async Task RemoveFromFavoriteShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.Seed(dbContext);

            var repository = new EfDeletableEntityRepository<Favorite>(dbContext);
            var service = new FavoriteService(repository);
            var favoriteBook = repository.All().FirstOrDefault();

            await service.RemoveFromFavoriteAsync(favoriteBook.UserId, favoriteBook.BookId);

            Assert.Equal(2, repository.All().Count());
        }

        [Fact]
        public async Task AddShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Favorite>(dbContext);
            var service = new FavoriteService(repository);
            var favoriteBook = repository.All().FirstOrDefault();

            await service.AddToFavoriteAsync("UserId", 1);

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task AddShouldSaveCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Favorite>(dbContext);
            var service = new FavoriteService(repository);
            var favoriteBook = repository.All().FirstOrDefault();
            var userId = "UserId";
            var bookId = 1;
            await service.AddToFavoriteAsync(userId, bookId);
            var actualResult = repository.All().FirstOrDefault();

            Assert.Equal(userId, actualResult.UserId);
            Assert.Equal(bookId, actualResult.BookId);
        }

        [Fact]
        public async Task FavoriteBookShouldReturnCorrectCount()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Favorite>(dbContext);
            var service = new FavoriteService(repository);
            var userId = "UserId";
            await service.AddToFavoriteAsync(userId, 1);
            await service.AddToFavoriteAsync(userId, 2);
            await service.AddToFavoriteAsync("UserIdDiffrent", 1);

            var actualResult = service.FavoriteBook<FavoriteModel>(userId).ToList();

            Assert.Equal(2, actualResult.Count);
        }

        [Fact]
        public async Task FavoriteBookShouldReturnCorrectData()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Favorite>(dbContext);
            var service = new FavoriteService(repository);
            var userId = "UserId";
            await service.AddToFavoriteAsync(userId, 1);
            await service.AddToFavoriteAsync(userId, 2);
            await service.AddToFavoriteAsync("UserIdDiffrent", 1);

            var actualResult = service.FavoriteBook<FavoriteModel>(userId).ToList();
            var expectedResult = repository.All()
                .Where(x => x.UserId == userId)
                .ToList();

            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].UserId, actualResult[i].UserId);
            }
        }


        private async Task Seed(ApplicationDbContext dbContext)
        {
            await dbContext.Favorites.AddAsync(new Favorite
            {
                BookId = 1,
                UserId = "User1",
            });

            await dbContext.Favorites.AddAsync(new Favorite
            {
                BookId = 2,
                UserId = "User1",
            });

            await dbContext.AddAsync(new Favorite
            {
                BookId = 1,
                UserId = "User2",
            });

            await dbContext.SaveChangesAsync();
        }

        private void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(FavoriteModel).GetTypeInfo().Assembly,
                typeof(Favorite).GetTypeInfo().Assembly);
        }
    }
}
