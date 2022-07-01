using System;
using BookPublication.API.Contracts.Pagination;
using BookPublication.API.Contracts.Requests.Queries;
using BookPublication.API.Contracts.Responses;
using BookPublication.API.data.Repository;
using BookPublication.API.Dtos.BookDtos;
using BookPublication.API.Models;
using BookPublication.API.Services.UriServices;
using Microsoft.AspNetCore.Mvc;

namespace BookPublication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
	{
        private readonly IBookRepository _bookRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly IUriService _uriService;

        public BookController(IBookRepository bookRepository,
                              IPublicationRepository publicationRepository,
                              IUriService uriService)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _publicationRepository = publicationRepository ?? throw new ArgumentNullException(nameof(publicationRepository));
            _uriService = uriService ?? throw new ArgumentNullException(nameof(uriService));
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
        public async Task<IActionResult> GetAllBook([FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = new PaginationFilter
            {
                PageSize = paginationQuery.PageSize,
                PageNumber = paginationQuery.PageNumber
            };

            var result = await _bookRepository.GetAllBooks(paginationFilter);

            if (result == null)
            {
                return Ok("No Data Found!");
            };

            if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return Ok(new PagedResponse<Book>(result.Data));
            }

            var nextPage = paginationFilter.PageNumber >= 1
                           ? _uriService.GetAllBooksUri(new PaginationQuery(paginationFilter.PageNumber + 1, paginationFilter.PageSize)).ToString()
                           : null;

            var previousPage = paginationFilter.PageNumber >= 1
                            ? _uriService.GetAllBooksUri(new PaginationQuery(paginationFilter.PageNumber - 1, paginationFilter.PageSize)).ToString()
                            : null;

            int totalPage = (int)result.Count / paginationFilter.PageSize;
            if (result.Count % paginationFilter.PageSize != 0)
                totalPage = totalPage + 1;

            return Ok(new PagedResponse<Book>
            {
                Data = result.Data,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : (int?)null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : (int?)null,
                NextPage = result.Data.Any() ? nextPage : null,
                PreviousPage = previousPage,
                TotalPage = totalPage

            });
            

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

