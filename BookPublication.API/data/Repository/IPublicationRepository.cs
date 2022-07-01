using System;
using BookPublication.API.Contracts.Pagination;
using BookPublication.API.Contracts.Responses;
using BookPublication.API.Models;

namespace BookPublication.API.data.Repository
{
	public interface IPublicationRepository
	{
        public Task<Publication> AddPublication(Publication publication);
        public Task<Publication?> GetPublicationBYId(int publicationId);
        public Task<Publication?> GetPublicationWithBookById(int publicationId);
        public Task<List<Publication>> GetAllPublication();
        public Task<DataResponse<Publication>> GetAllPublication(PaginationFilter paginationFilter = null);


        public Task<List<Publication>> GetAllPublicationWithBooks();
        public Task<DataResponse<Publication>> GetAllPublicationWithBooks(PaginationFilter paginationFilter = null);
        public Task DeletePublication(Publication publication);
        public Task UpdatePublication(Publication publication);
        public Task<Publication> GetAllBookByPublicationName(string publicationName);
        public Task<Publication> GetAllBookByPublicationId(int publicationId);
        public Task<bool> IsPublicationExistByName(string publicationName);
        public Task<bool> IsPublicationExistById(int publicationId);
    }
}

