using System;
using BookPublication.API.Contracts.Pagination;
using BookPublication.API.Contracts.Requests.Queries;
using BookPublication.API.Contracts.Responses;
using BookPublication.API.data.Repository;
using BookPublication.API.Dtos.PublicationDtos;
using BookPublication.API.Models;
using BookPublication.API.Services.UriServices;
using Microsoft.AspNetCore.Mvc;

namespace BookPublication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
	{
        private readonly IBookRepository _bookRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly IUriService _uriService;

        public PublicationController(IBookRepository bookRepository ,
                                     IPublicationRepository publicationRepository,
                                     IUriService uriService)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _publicationRepository = publicationRepository ?? throw new ArgumentNullException(nameof(publicationRepository));
            _uriService = uriService ?? throw new ArgumentNullException(nameof(uriService));
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

        [HttpGet]
        [Route("publication/{publicationId}")]
        public async Task<IActionResult> GetPublicationWithoutBooks(int publicationId)
        {
            var result = await _publicationRepository.GetPublicationBYId(publicationId);
            if (result == null)
            {
                return Ok("No Data Found!");
            };
            return Ok(result);

        }

        [HttpGet]
        [Route("publicationwithbooks/{publicationId}")]
        public async Task<IActionResult> GetPublicationWithBooks(int publicationId)
        {
            var result = await _publicationRepository.GetPublicationWithBookById(publicationId);
            if (result == null)
            {
                return Ok("No Data Found!");
            };
            return Ok(result);

        }


        [HttpGet]
        [Route("allpublicationwithoutbooks")]
        public async Task<IActionResult> GetAllPublicationWithoutBooks([FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = new PaginationFilter
            {
                PageSize = paginationQuery.PageSize,
                PageNumber = paginationQuery.PageNumber
            };

            var result = await _publicationRepository.GetAllPublication(paginationFilter);
            if (result == null)
            {
                return Ok("No Data Found!");
            };

            if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return Ok(new PagedResponse<Publication>(result.Data));
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

            return Ok(new PagedResponse<Publication>
            {
                Data = result.Data,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : (int?)null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : (int?)null,
                NextPage = result.Data.Any() ? nextPage : null,
                PreviousPage = previousPage,
                TotalPage = totalPage

            });


            //var result = await _publicationRepository.GetAllPublication();
            //if (result == null)
            //{
            //    return Ok("No Data Found!");
            //};
            //return Ok(result);

        }


        [HttpGet]
        [Route("allpublicationwithbooks")]
        public async Task<IActionResult> GetAllPublicationWithBooks()
        {
            var result = await _publicationRepository.GetAllPublicationWithBooks();
            if (result == null)
            {
                return Ok("No Data Found!");
            };
            return Ok(result);

        }


        [HttpPut]
        [Route("updatePublication")]
        public async Task<IActionResult> UpdatePublication(UpdatePublicationDto updatePublicationDto)
        {
            if (ModelState.IsValid)
            {
                var publicationFromRepo = await _publicationRepository.GetPublicationBYId(updatePublicationDto.Id);
                if(publicationFromRepo == null)
                {
                    return BadRequest("Publication doesnot exist");
                }
                publicationFromRepo.Country = updatePublicationDto.Country;
                publicationFromRepo.PublicationOpenYear = updatePublicationDto.PublicationOpenYear;
                publicationFromRepo.Name = updatePublicationDto.Name;
                publicationFromRepo.Email = updatePublicationDto.Email;
                publicationFromRepo.PhoneNumber = updatePublicationDto.PhoneNumber;

                await _publicationRepository.UpdatePublication(publicationFromRepo);
                return Ok("Publication update successfully");
            }
            ModelState.AddModelError("", "ModelState Error");
            return BadRequest("ModelState Error");

        }

        [HttpDelete]
        [Route("deletepublication/{publicationId}")]
        public async Task<IActionResult> DeletePublication(int publicationId)
        {
            var isBookUsingPubication = await _bookRepository.AnyBookHavingPublicationId(publicationId);
            if (isBookUsingPubication)
                return BadRequest("Cannot deleted publication one of the book already published by publication");
            var publicationFromRepo = await _publicationRepository.GetPublicationBYId(publicationId);
            if (publicationFromRepo == null)
                return BadRequest("Publication doesnot exist");

            return Ok("Publication Deleted successfully");
        }



    }
}

