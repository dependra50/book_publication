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
        [Column(TypeName = "varchar(40)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string ISBN { get; set; }
        [Required]
        [Column(TypeName = "varchar(40)")]
        public string Author { get; set; }
        [Required]
        public int PublishedYear { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Edition { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Category { get; set; }


        public int PublicationId { get; set; }
        public Publication Publication { get; set; }

       

	}
}

