using System;
using BookPublication.API.data.Repository;
using BookPublication.API.Dtos.PublicationDtos;
using BookPublication.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookPublication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
	{
        private readonly IBookRepository _bookRepository;
        private readonly IPublicationRepository _publicationRepository;

        public PublicationController(IBookRepository bookRepository ,
                                     IPublicationRepository publicationRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _publicationRepository = publicationRepository ?? throw new ArgumentNullException(nameof(publicationRepository));
        }
        [HttpPost]
        [Route("addpublication")]
        public async Task<IActionResult> RegisterPublicationAsync(AddPublicationDto addPublicationDto)
        {
            if (ModelState.IsValid)
            {
                var isExist = await _publicationRepository.IsPublicationExistByName(addPublicationDto.Name);
                if (isExist)
                    return BadRequest("Publication Already Exist");

                var publicationToRepo = new Publication
                {
                    Country = addPublicationDto.Country,
                    Name = addPublicationDto.Name,
                    PublicationOpenYear = addPublicationDto.PublicationOpenYear
                };
                await _publicationRepository.AddPublication(publicationToRepo);
                return Ok("Birami Detail Added Successfully");

            }
            ModelState.AddModelError("", "ModelState Error");
            return BadRequest("ModelState Error");

        }
    }
}

