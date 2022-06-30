using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPublication.API.Models
{
	public class Publication
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
		public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int PublicationOpenYear { get; set; }

        
        public List<Book> Books { get; set; }
	}
}

