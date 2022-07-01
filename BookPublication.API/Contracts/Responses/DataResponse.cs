using System;
namespace BookPublication.API.Contracts.Responses
{
	public class DataResponse<T>
	{
        public DataResponse()
        {

        }
        public DataResponse(IEnumerable<T> data)
        {
            Data = data;

        }

        public DataResponse(IEnumerable<T> data, long count)
        {
            Data = data;
            Count = count;
        }

        public IEnumerable<T> Data { get; set; }
        public long Count { get; set; }
    }
}

