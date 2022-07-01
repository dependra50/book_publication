using System;
namespace BookPublication.API.Contracts.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; set; }
        public int? PageNumber { get; set; }//Nullable
        public int? PageSize { get; set; }
        public int TotalPage { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }


    }
}

