using System;
using BookPublication.API.data.Repository;

namespace BookPublication.API.Services.PublicationServices
{
	public class PublicationService 
	{
        private readonly IPublicationRepository _publicationRepository;

        public PublicationService(IPublicationRepository publicationRepository)
		{
			_publicationRepository = publicationRepository ?? throw new ArgumentNullException(nameof(publicationRepository));
		}
	}
}

