using System;
using BookPublication.API.data.context;
using BookPublication.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookPublication.API.data.Repository
{
	public class BookRepository : IBookRepository
	{
        private readonly ApplicationDBContext _dataContext;

        public BookRepository(ApplicationDBContext dataContext)
		{
			_dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
		}

        public async Task<Book> AddBook(Book book)
        {
            await _dataContext.Books.AddAsync(book);
            await _dataContext.SaveChangesAsync();
            return book;
        }

        public async Task DeleteBook(Book book)
        {
            //var bookFromRepo = await _dataContext.Books.FindAsync(bookId);
            _dataContext.Books.Remove(book);
            await _dataContext.SaveChangesAsync();

        }

        public async Task<Book?> GetBookBYId(int bookId)
        {
            var bookFromRepo = await _dataContext.Books.Where(b => b.Id == bookId)
                                                        .FirstOrDefaultAsync();
            return bookFromRepo;
        }

        public async Task<Book?> GetBookByISBN(string isbn)
        {
            var bookFromRepo = await _dataContext.Books.Where(b => b.ISBN == isbn)
                                                        .FirstOrDefaultAsync();
            return bookFromRepo;
        }

        public async Task<List<Book>> GetBookByPublicationId(int publicationId)
        {
            return await _dataContext.Books.Where(b => b.PublicationId == publicationId)
                                            .ToListAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _dataContext.Books.Update(book);
            await _dataContext.SaveChangesAsync();
        }
    }
}

