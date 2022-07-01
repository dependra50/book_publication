using System;
namespace BookPublication.API.Contracts.Pagination
{
	public class PaginationFilter
	{
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

