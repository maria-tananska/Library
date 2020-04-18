namespace Library.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Library.Data.Common.Repositories;
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class FavoriteService : IFavoriteService
    {
        private readonly IDeletableEntityRepository<Favorite> favoriteRepository;

        public FavoriteService(IDeletableEntityRepository<Favorite> favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
        }

        public async Task AddToFavoriteAsync(string userId, int bookId)
        {
            //var isAdded = this.favoriteRepository.All()
            //    .Any(f => f.UserId == userId && f.BookId == bookId);

            //if (isAdded)
            //{
            //    throw new ArgumentException("This book is added to favorite");
            //}

            var favorite = new Favorite
            {
                UserId = userId,
                BookId = bookId,
            };

            await this.favoriteRepository.AddAsync(favorite);
            await this.favoriteRepository.SaveChangesAsync();
        }

        public IEnumerable<T> FavoriteBook<T>(string userId)
        {
            var favoriteBooks = this.favoriteRepository.All()
                .Where(f => f.UserId == userId)
                .To<T>()
                .ToList();

            return favoriteBooks;
        }

        public bool IsExist(string userId, int bookid)
        {
            return this.favoriteRepository.All().Any(f => f.BookId == bookid && f.UserId == userId);
        }

        public async Task RemoveFromFavoriteAsync(string userId, int bookId)
        {
            var favoriteBook = this.favoriteRepository.All()
                 .Where(x => x.UserId == userId && x.BookId == bookId)
                 .FirstOrDefault();

            favoriteBook.IsDeleted = true;
            await this.favoriteRepository.SaveChangesAsync();
        }
    }
}
