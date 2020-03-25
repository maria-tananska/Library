﻿namespace Library.Services.Data
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

        public async Task<int> AddToFavoriteAsync(string userId, int bookId)
        {
            var favorite = new Favorite
            {
                UserId = userId,
                BookId = bookId,
            };

            await this.favoriteRepository.AddAsync(favorite);
            await this.favoriteRepository.SaveChangesAsync();

            return favorite.Id;
        }

        public IEnumerable<T> FavoriteBook<T>(string userId)
        {
            var favoriteBooks = this.favoriteRepository.All()
                .Where(f => f.UserId == userId)
                .To<T>().ToList();

            return favoriteBooks;
        }

        public bool IsExist(string userId, int bookid)
        {
            var favoriteBook = this.favoriteRepository.All()
                .Where(f => f.BookId == bookid && f.UserId == userId)
                .FirstOrDefault();
            if (favoriteBook == null)
            {
                return false;
            }

            return true;
        }
    }
}
