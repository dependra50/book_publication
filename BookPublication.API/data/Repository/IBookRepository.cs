using System;
using BookPublication.API.Contracts.Pagination;
using BookPublication.API.Contracts.Responses;
using BookPublication.API.Models;

namespace BookPublication.API.data.Repository
{
	public interface IBookRepository
	{
		public Task<Book> AddBook(Book book);
		public Task<Book?> GetBookBYId(int bookId);
		public Task<Book?> GetBookByISBN(string isbn);
		public Task<List<Book>> GetBookByPublicationId(int publicationId);
        public Task<DataResponse<Book>> GetBookByPublicationId(int publicationId , PaginationFilter paginationFilter = null);
        public Task DeleteBook(Book book);
		public Task UpdateBook(Book book);

		public Task<List<Book>> GetAllBooks();
        public Task<DataResponse<Book>> GetAllBooks(PaginationFilter paginationFilter = null);
        public Task<bool> IsBookExistById(int bookId);
		public Task<bool> IsBookExistByName(string bookName);
		public Task<bool> AnyBookHavingPublicationId(int publicationId);
	}
}

