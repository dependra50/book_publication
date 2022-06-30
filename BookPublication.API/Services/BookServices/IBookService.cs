using System;
using BookPublication.API.Dtos.BookDtos;
using BookPublication.API.Models;

namespace BookPublication.API.Services.BookServices
{
	public interface IBookService
	{
        public Task AddBookAsync(AddBookDto addBookDto);
        public Task<Book?> GetBookBYIdAsync(int bookId);
        public Task<Book?> GetBookByISBNAsync(string isbn);
        public Task<List<Book>> GetBookByPublicationIdAsync(int publicationId);
        public Task DeleteBookAsync(Book book);
        public Task UpdateBookAsync(Book book);
    }
}

