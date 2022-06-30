using System;
using BookPublication.API.data.Repository;

namespace BookPublication.API.Services.BookServices
{
	public class BookService
	{
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
		}

	}
}

