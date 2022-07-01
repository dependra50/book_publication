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

        public async Task<bool> AnyBookHavingPublicationId(int publicationId)
        {
            return await _dataContext.Books.AnyAsync(b => b.PublicationId == publicationId);
        }

        public async Task DeleteBook(Book book)
        {
            //var bookFromRepo = await _dataContext.Books.FindAsync(bookId);
            _dataContext.Books.Remove(book);
            await _dataContext.SaveChangesAsync();

        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _dataContext.Books.Include(p => p.Publication)
                                           .ToListAsync();
        }

        public async Task<Book?> GetBookBYId(int bookId)
        {
            var bookFromRepo = await _dataContext.Books.Where(b => b.Id == bookId)
                                                        .Include(p=>p.Publication)
                                                        .FirstOrDefaultAsync();
            return bookFromRepo;
        }

        public async Task<Book?> GetBookByISBN(string isbn)
        {
            var bookFromRepo = await _dataContext.Books.Where(b => b.ISBN == isbn)
                                                        .Include(p => p.Publication)
                                                        .FirstOrDefaultAsync();
            return bookFromRepo;
        }

        public async Task<List<Book>> GetBookByPublicationId(int publicationId)
        {
            return await _dataContext.Books.Where(b => b.PublicationId == publicationId)
                                            .Include(p => p.Publication)
                                            .ToListAsync();
        }

        public async Task<bool> IsBookExistById(int bookId)
        {
            return await _dataContext.Books.AnyAsync(b => b.Id == bookId);
        }

        public async Task<bool> IsBookExistByName(string bookName)
        {
            return await _dataContext.Books.AnyAsync(b => b.Title == bookName);
        }

        public async Task UpdateBook(Book book)
        {
            _dataContext.Books.Update(book);
            await _dataContext.SaveChangesAsync();
        }

       
        

        
    }
}

