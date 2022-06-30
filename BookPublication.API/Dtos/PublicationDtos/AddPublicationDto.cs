using System;
namespace BookPublication.API.Dtos.PublicationDtos
{
	public class AddPublicationDto
	{     
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Country { get; set; }

        public int PublicationOpenYear { get; set; }
    }
}

