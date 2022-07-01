using System;
using BookPublication.API.Contracts.Requests.Queries;

namespace BookPublication.API.Services.UriServices
{
	public interface IUriService
	{
		public Uri GetAllBooksUri(PaginationQuery paginationQuery = null);
		public Uri GetAllPublicationWithoutBooksUri(PaginationQuery paginationQuery = null);
		public Uri GetAllPublicaitonWithBooksUri(PaginationQuery paginationQuery = null);

		public Uri GetAllBooksByPublicationIdUri(int publicationId, PaginationQuery paginationQuery = null);

	}
}

