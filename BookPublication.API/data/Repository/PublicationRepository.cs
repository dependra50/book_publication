using System;
using BookPublication.API.data.context;
using BookPublication.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookPublication.API.data.Repository
{
	public class PublicationRepository : IPublicationRepository
	{
        private readonly ApplicationDBContext _dataContext;

        public PublicationRepository(ApplicationDBContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<Publication> AddPublication(Publication publication)
        {
            await _dataContext.Publications.AddAsync(publication);
            await _dataContext.SaveChangesAsync();
            return publication;
        }

        public async Task DeletePublication(Publication publication)
        {

            _dataContext.Publications.Remove(publication);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Publication>> GetAllPublication()
        {
            return  await _dataContext.Publications.ToListAsync();
        }

        public async Task<Publication?> GetPublicationBYId(int publicationId)
        {
            return await _dataContext.Publications.Where(p => p.Id == publicationId)
                                                   .FirstOrDefaultAsync();
        }

        public async Task UpdatePublication(Publication publication)
        {
            _dataContext.Publications.Update(publication);
            await _dataContext.SaveChangesAsync();

        }
    }
}

