using System;
namespace BookPublication.API.Contracts.Responses
{
	public class Response<T>
	{
        public Response(T response)
        {
            Data = response;
        }

        public T Data { get; set; }
    }
}

