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
    using Library.Web.ViewModels.Admin.Author;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class AuthorServiceTest
    {
        [Fact]
        public async Task AuthorsCountShouldReturnCorrectNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedAuthorsAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Autor>(dbContext);
            var service = new AuthorService(repository);

            Assert.Equal(2, service.AuthorsCount());
        }

        [Fact]
        public async Task CreateAuthorShouldReturnCorrectValue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Autor>(dbContext);
            var service = new AuthorService(repository);
            var firstName = "Author1";
            var lastName = "Author1";

            await service.AddAsync(firstName, lastName);
            var actualResult = repository.All().FirstOrDefault();

            Assert.Equal(firstName, actualResult.FirstName);
            Assert.Equal(lastName, actualResult.LastName);
        }

        [Fact]
        public async Task CreateAuthorShouldThrowExeption()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Autor>(dbContext);
            var service = new AuthorService(repository);
            string firstName = null;
            var lastName = "LastName";

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.AddAsync(firstName, lastName);
            });

        }

        [Fact]
        public async Task EditShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Autor>(dbContext);
            var service = new AuthorService(repository);
            await service.AddAsync("Author1", "Author1");
            var id = repository.All().FirstOrDefault().Id;
            string firstName = "FirstName";
            var lastName = "LastName";

            await service.EditAsync(firstName, lastName, id);
            var actual = repository.All().FirstOrDefault();

            Assert.Equal(firstName, actual.FirstName);
            Assert.Equal(lastName, actual.LastName);
        }

        [Fact]
        public async Task EditShouldThrowExeption()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Autor>(dbContext);
            var service = new AuthorService(repository);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.EditAsync("firstName", "lastName", 1);
            });
        }

        [Fact]
        public async Task DeleteByIdShouldWorkCorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedAuthorsAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Autor>(dbContext);
            var service = new AuthorService(repository);
            var id = repository.All().FirstOrDefault().Id;

            await service.DeleteByIdAsync(id);

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task DeleteByIdShouldThrowExeption()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Autor>(dbContext);
            var service = new AuthorService(repository);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.DeleteByIdAsync(1);
            });
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectData()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedAuthorsAsync(dbContext);
            var repository = new EfDeletableEntityRepository<Autor>(dbContext);
            var service = new AuthorService(repository);
            var id = repository.All().FirstOrDefault().Id;

            var actualResult = service.GetById<AuthorViewModel>(id);
            var expectedResult = repository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.FirstName, actualResult.FirstName);
            Assert.Equal(expectedResult.LastName, actualResult.LastName);
        }

        [Fact]
        public void GetByIdShouldThrowExeption()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var repository = new EfDeletableEntityRepository<Autor>(new ApplicationDbContext(options));
            var service = new AuthorService(repository);

            Assert.Throws<ArgumentException>(() =>
            {
                service.GetById<AuthorViewModel>(1);
            });
        }

        [Fact]
        public async Task GetAuthorShouldReturnCorrectCount()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedAuthorsAsync(dbContext);
            var repository = new EfDeletableEntityRepository<Autor>(dbContext);
            var service = new AuthorService(repository);

            var authorsCount = service.GetAuthors<AuthorViewModel>().Count();

            Assert.Equal(2, authorsCount);
        }

        [Fact]
        public async Task GetAuthorsShouldReturnCorrectData()
        {
            this.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await this.SeedAuthorsAsync(dbContext);
            var repository = new EfDeletableEntityRepository<Autor>(dbContext);
            var service = new AuthorService(repository);

            var actualResult = service.GetAuthors<AuthorViewModel>().ToList();
            var expectedReIsult = repository.All().ToList();

            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(expectedReIsult[i].Id, actualResult[i].Id);
                Assert.Equal(expectedReIsult[i].FirstName, actualResult[i].FirstName);
                Assert.Equal(expectedReIsult[i].LastName, actualResult[i].LastName);
            }
        }

        private async Task SeedAuthorsAsync(ApplicationDbContext dbContext)
        {
            await dbContext.AddAsync(new Autor
            {
                FirstName = "Author1",
                LastName = "Author1",
            });

            await dbContext.AddAsync(new Autor
            {
                FirstName = "Author2",
                LastName = "Author2",
            });

            await dbContext.SaveChangesAsync();
        }

        private void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(AuthorViewModel).GetTypeInfo().Assembly,
                typeof(Autor).GetTypeInfo().Assembly);
        }
    }

}
