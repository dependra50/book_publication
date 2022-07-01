using System;
using BookPublication.API.Contracts.Requests.Queries;
using Microsoft.AspNetCore.WebUtilities;

namespace BookPublication.API.Services.UriServices
{
	public class UriService : IUriService
	{
        private readonly string _baseUri;

        public UriService(string baseUri)
		{
			_baseUri = baseUri;
		}

        public Uri GetAllBooksByPublicationIdUri(int publicationId, PaginationQuery paginationQuery = null)
        {
            var modifiedUri = string.Concat(_baseUri, "api/Publication/allpublications/{publicationId}/");
            if (paginationQuery == null)
            {
                var uri = new Uri(modifiedUri.Replace("{publicationId}", publicationId.ToString()));
                return uri;
            }

            var finalUri = QueryHelpers.AddQueryString(modifiedUri, "pageNumber", paginationQuery.PageNumber.ToString());

            finalUri = QueryHelpers.AddQueryString(finalUri, "pageSize", paginationQuery.PageSize.ToString());

            return new Uri(finalUri.Replace("{publicationId}", publicationId.ToString()));
        }

        public Uri GetAllBooksUri(PaginationQuery paginationQuery = null)
        {
            var modifiedUri = string.Concat(_baseUri, "api/Book/allbooks/");
            if (paginationQuery == null)
            {
                var uri = new Uri(modifiedUri);
                return uri;
            }

            var finalUri = QueryHelpers.AddQueryString(modifiedUri, "pageNumber", paginationQuery.PageNumber.ToString());

            finalUri = QueryHelpers.AddQueryString(finalUri, "pageSize", paginationQuery.PageSize.ToString());

            return new Uri(finalUri);
        }

        public Uri GetAllPublicaitonWithBooksUri(PaginationQuery paginationQuery = null)
        {
            var modifiedUri = string.Concat(_baseUri, "api/Publication/allpublicationwithbooks/");
            if (paginationQuery == null)
            {
                var uri = new Uri(modifiedUri);
                return uri;
            }

            var finalUri = QueryHelpers.AddQueryString(modifiedUri, "pageNumber", paginationQuery.PageNumber.ToString());

            finalUri = QueryHelpers.AddQueryString(finalUri, "pageSize", paginationQuery.PageSize.ToString());

            return new Uri(finalUri);
        }

        public Uri GetAllPublicationWithoutBooksUri(PaginationQuery paginationQuery = null)
        {
            var modifiedUri = string.Concat(_baseUri, "api/Publication/allpublicationwithoutbooks/");
            if (paginationQuery == null)
            {
                var uri = new Uri(modifiedUri);
                return uri;
            }

            var finalUri = QueryHelpers.AddQueryString(modifiedUri, "pageNumber", paginationQuery.PageNumber.ToString());

            finalUri = QueryHelpers.AddQueryString(finalUri, "pageSize", paginationQuery.PageSize.ToString());

            return new Uri(finalUri);
        }
    }
}
