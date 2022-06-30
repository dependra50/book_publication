using System;
using BookPublication.API.Models;

namespace BookPublication.API.data.Repository
{
	public interface IBookRepository
	{
		public Task<Book> AddBook(Book book);
		public Task<Book?> GetBookBYId(int bookId);
		public Task<Book?> GetBookByISBN(string isbn);
		public Task<List<Book>> GetBookByPublicationId(int publicationId);
		public Task DeleteBook(Book book);
		public Task UpdateBook(Book book);
	}
}

