using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPublication.API.Models
{
	public class Book
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]        
		public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        [Required]
        public string Edition { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }


        public int PublicationId { get; set; }
        public Publication Publication { get; set; }

       

	}
}

