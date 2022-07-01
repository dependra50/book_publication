using System;
using BookPublication.API.data.Repository;
using BookPublication.API.Dtos.BookDtos;
using BookPublication.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookPublication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
	{
        private readonly IBookRepository _bookRepository;
        private readonly IPublicationRepository _publicationRepository;

        public BookController(IBookRepository bookRepository,
                                     IPublicationRepository publicationRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _publicationRepository = publicationRepository ?? throw new ArgumentNullException(nameof(publicationRepository));
        }

        [HttpPost]
        [Route("addbook")]
        public async Task<IActionResult> AddBookAsync(AddBookDto addBookDto)
        {

            if (ModelState.IsValid)
            {

                var bookToRepo = new Book
                {
                   ISBN = addBookDto.ISBN,
                   Author = addBookDto.Author,
                   Category = addBookDto.Category,
                   Edition = addBookDto.Edition,
                   Price = addBookDto.Price,
                   PublishedYear = addBookDto.PublishedYear,
                   Title = addBookDto.Title
                };
                await _bookRepository.AddBook(bookToRepo);
                return Ok("Book added successfully");

            }
            ModelState.AddModelError("", "ModelState Error");
            return BadRequest("ModelState Error");
        }


        [HttpGet]
        [Route("book/{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var result = await _bookRepository.GetBookBYId(bookId);
            if (result == null)
            {
                return Ok("No Data Found!");
            };
            return Ok(result);

        }

        [HttpGet]
        [Route("bookbyname/{isbnNumber}")]
        public async Task<IActionResult> GetBookByName(string isbnNumber)
        {
            var result = await _bookRepository.GetBookByISBN(isbnNumber);
            if (result == null)
            {
                return Ok("No Data Found!");
            };
            return Ok(result);

        }


        [HttpGet]
        [Route("allbooks")]
        public async Task<IActionResult> GetAllBook()
        {
            var result = await _bookRepository.GetAllBooks();
            if (result == null)
            {
                return Ok("No Data Found!");
            }
            return Ok(result);

        }

        [HttpGet]
        [Route("allbooks/{publicationId}")]
        public async Task<IActionResult> GetAllBookBYPublicationId(int publicationId)
        {
            var result = await _bookRepository.GetBookByPublicationId(publicationId);
            if (result == null)
            {
                return Ok("No Data Found!");
            }
            return Ok(result);

        }


        [HttpPut]
        [Route("updatebook")]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                var isExist = await _bookRepository.IsBookExistById(book.Id);
                if (!isExist)
                {
                    return BadRequest("Book doesnot exist");
                }
                await _bookRepository.UpdateBook(book);
                return Ok("book update successfully");
            }
            ModelState.AddModelError("", "ModelState Error");
            return BadRequest("ModelState Error");

        }

        [HttpDelete]
        [Route("deletebook/{bookId}")]
        public async Task<IActionResult> DeleteCallingSale(int bookId)
        {

            //var isExist = await _bookRepository.IsBookExistById(bookId);
            var bookFromRepo = await _bookRepository.GetBookBYId(bookId);
            if (bookFromRepo == null)
            {
                return BadRequest("Book doesnot exists");
            }

            await _bookRepository.DeleteBook(bookFromRepo);
            return Ok("Book deleted successfully");
        }






    }
}

