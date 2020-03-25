namespace Library.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Library.Data.Common.Repositories;
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class BookService : IBookService
    {
        private readonly IDeletableEntityRepository<Book> bookRepository;

        public BookService(IDeletableEntityRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<int> AddAsync(string title, string shortContent, string imgUrl, string fileName, int pages, int categoryId, int authorId)
        {
            var book = new Book
            {
                Title = title,
                ShortContent = shortContent,
                ImgUrl = imgUrl,
                FileName = fileName,
                Pages = pages,
                AutorId = authorId,
                CategoryId = categoryId,
            };

            await this.bookRepository.AddAsync(book);
            await this.bookRepository.SaveChangesAsync();

            return book.Id;
        }

        public IEnumerable<T> All<T>()
        {
            var books = this.bookRepository.All()
                .To<T>();

            return books;
        }

        public T GetById<T>(int id)
        {
            var book = this.bookRepository.All()
                .Where(b => b.Id == id)
                .To<T>()
                .FirstOrDefault();

            return book;
        }
    }
}
