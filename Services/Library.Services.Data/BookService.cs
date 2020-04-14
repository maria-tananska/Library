namespace Library.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Library.Data.Common.Repositories;
    using Library.Data.Models;
    using Library.Services.Data.DTO;
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
                .To<T>().ToList();

            return books;
        }

        public int BooksCount()
        {
            var count = this.bookRepository.All().Count();

            return count;
        }

        public async Task DeleteAsync(int id)
        {
            var book = await this.bookRepository.GetByIdWithDeletedAsync(id);

            if (book == null)
            {
                throw new ArgumentException($"Book with id {id} don't exist");
            }

            book.IsDeleted = true;
            await this.bookRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string title, string shortContent, string imgUrl, string fileName, int pages, int categoryId, int authorId)
        {
            var book = await this.bookRepository.GetByIdWithDeletedAsync(id);

            if (book == null)
            {
                throw new ArgumentException($"Book with id {id} don't exist!");
            }

            if (imgUrl != null)
            {
                book.ImgUrl = imgUrl;
            }

            book.Title = title;
            book.ShortContent = shortContent;
            book.FileName = fileName;
            book.AutorId = authorId;
            book.CategoryId = categoryId;
            book.Pages = pages;

            await this.bookRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetBooksByCategoryId<T>(int categoryId)
        {
            var books = this.bookRepository.All()
                .Where(b => b.CategoryId == categoryId)
                .To<T>()
                .ToList();

            return books;
        }

        public EditBookDTO GetById(int id)
        {
            var book = this.bookRepository.All()
                .Where(b => b.Id == id)
                .Select(b => new EditBookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    ShortContent = b.ShortContent,
                    Img = b.ImgUrl,
                    FileName = b.FileName,
                    Pages = b.Pages,
                    AutorId = b.AutorId,
                    CategoryId = b.CategoryId,
                })
                .FirstOrDefault();

            return book;
        }

        public T GetByIdTo<T>(int id)
        {
            var book = this.bookRepository.All()
                .Where(b => b.Id == id)
                .To<T>()
                .FirstOrDefault();

            return book;
        }

        public IEnumerable<T> GetNewBooks<T>()
        {
            var book = this.bookRepository.All()
                .OrderBy(x => x.CreatedOn)
                .Take(3)
                .To<T>()
                .ToList();

            return book;
        }

        public int PagesCount()
        {
            var count = this.bookRepository.All().Sum(b => b.Pages);

            return count;
        }
    }
}
