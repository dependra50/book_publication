using System;
namespace BookPublication.API.Dtos.BookDtos
{
	public class AddBookDto
	{        
        public string Title { get; set; }
        
        public string ISBN { get; set; }
        
        public string Author { get; set; }
        
        public int  PublishedYear { get; set; }
        
        public string Edition { get; set; }
        
        public decimal Price { get; set; }
        
        public string Category { get; set; }

        public string Publication { get; set; }
        
    }
}

