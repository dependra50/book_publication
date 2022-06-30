using System;
using BookPublication.API.Models;

namespace BookPublication.API.data.Repository
{
	public interface IPublicationRepository
	{
        public Task<Publication> AddPublication(Publication publication);
        public Task<Publication?> GetPublicationBYId(int publicationId);
        public Task<List<Publication>> GetAllPublication();
        public Task DeletePublication(Publication publication);
        public Task UpdatePublication(Publication publication);
    }
}

