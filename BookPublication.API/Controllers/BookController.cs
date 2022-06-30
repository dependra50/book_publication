using System;
using BookPublication.API.Dtos.BookDtos;
using Microsoft.AspNetCore.Mvc;

namespace BookPublication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
	{
		public BookController()
		{
		}


		public async Task<IActionResult> AddBookAsync(AddBookDto addBookDto)
        {
			return Ok();

        }
	}
}

