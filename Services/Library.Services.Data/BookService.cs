namespace Library.Services.Data
{
    using Library.Data.Common.Repositories;
    using Library.Data.Models;
    using Library.Services.Mapping;
    using System;
    using System.Collections.Generic;

    public class BookService : IBookService
    {
        private readonly IDeletableEntityRepository<Book> bookRepository;

        public BookService(IDeletableEntityRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IEnumerable<T> All<T>()
        {
            var book = this.bookRepository.All()
                .To<T>();

            return book;
        }
    }
}
