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
        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Country { get; set; }
        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public int PublicationOpenYear { get; set; }

        
        public List<Book> Books { get; set; }
	}
}

