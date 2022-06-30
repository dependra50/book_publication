using System;
using BookPublication.API.Models;

namespace BookPublication.API.Services.PublicationServices
{
	public interface IPublicationService
	{
        public Task AddPublicationAsync(Publication publication);
        public Task<Publication?> GetPublicationBYIdAsync(int publicationId);
        public Task<List<Publication>> GetAllPublicationAsync();
        public Task DeletePublicationAsync(Publication publication);
        public Task UpdatePublicationAsync(Publication publication);
    }
}

