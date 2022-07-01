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

        public async Task<Publication> GetAllBookByPublicationId(int publicationId)
        {
            var publicationFromRepo = await _dataContext.Publications
                                                        .SingleAsync(p => p.Id == publicationId);
            await _dataContext.Entry(publicationFromRepo)
                        .Collection(c => c.Books)
                        .LoadAsync();

            return publicationFromRepo;
                                                        
                                                                     
        }

        public async Task<Publication> GetAllBookByPublicationName(string publicationName)
        {
            var publicationFromRepo = await _dataContext.Publications
                                                        .SingleAsync(p => p.Name == publicationName);
            await _dataContext.Entry(publicationFromRepo)
                        .Collection(c => c.Books)
                        .LoadAsync();

            return publicationFromRepo;

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

        public async Task<bool> IsPublicationExistById(int publicationId)
        {
            var isExist = await _dataContext.Publications.AnyAsync(p => p.Id == publicationId);
            return isExist;
        }

        public async Task<bool> IsPublicationExistByName(string publicationName)
        {
            var isExist = await _dataContext.Publications.AnyAsync(p => p.Name == publicationName);
            return isExist;
        }

        public async Task UpdatePublication(Publication publication)
        {
            _dataContext.Publications.Update(publication);
            await _dataContext.SaveChangesAsync();

        }
    }
}

