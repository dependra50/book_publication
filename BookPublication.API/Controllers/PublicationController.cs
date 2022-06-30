using System;
using BookPublication.API.Dtos.PublicationDtos;
using Microsoft.AspNetCore.Mvc;

namespace BookPublication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
	{
		public PublicationController()
		{
		}

        public async Task<IActionResult> RegisterPublicationAsync(AddPublicationDto addPublicationDto)
        {
            return Ok();

        }
    }
}

