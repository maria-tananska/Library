﻿namespace Library.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Library.Data.Common.Repositories;
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class AuthorService : IAuthorService
    {
        private readonly IDeletableEntityRepository<Autor> authorRepository;

        public AuthorService(IDeletableEntityRepository<Autor> authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<int> AddAsync(string firstName, string lastName)
        {
            if (firstName == null || lastName == null)
            {
                throw new ArgumentException("Properti can not be null");
            }


            var author = new Autor
            {
                FirstName = firstName,
                LastName = lastName,
            };

            await this.authorRepository.AddAsync(author);
            await this.authorRepository.SaveChangesAsync();

            return author.Id;
        }

        public int AuthorsCount()
        {
            var count = this.authorRepository.All().Count();

            return count;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var author = await this.authorRepository.GetByIdWithDeletedAsync(id);

            if (author == null)
            {
                throw new ArgumentException($"Author with id:{id} dont exist!");
            }

            this.authorRepository.Delete(author);
            await this.authorRepository.SaveChangesAsync();
        }

        public async Task EditAsync(string firstName, string lastName, int id)
        {
            var author = await this.authorRepository.GetByIdWithDeletedAsync(id);

            if (author == null)
            {
                throw new ArgumentException($"Author with this {id} doesn't exist.");
            }

            if (firstName == null || lastName == null)
            {
                throw new ArgumentException("One or more required properties are null!");
            }

            author.FirstName = firstName;
            author.LastName = lastName;

            this.authorRepository.Update(author);
            await this.authorRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAuthors<T>()
        {
            var authors = this.authorRepository
                .All()
                .To<T>().ToList();

            return authors;
        }

        public T GetById<T>(int id)
        {
            var exist = this.authorRepository.All().Any(a => a.Id == id);

            if (!exist)
            {
                throw new ArgumentException($"Category with id: {id} doesn't exist!");
            }

            var author = this.authorRepository.All()
                .Where(a => a.Id == id)
                .To<T>()
                .FirstOrDefault();

            return author;
        }
    }
}
