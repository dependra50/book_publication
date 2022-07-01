using System;
using BookPublication.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookPublication.API.data.context
{
	public class ApplicationDBContext : DbContext
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<Publication> Publications { get; set; }
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Book>(options =>
			{
				options.HasOne<Publication>(p => p.Publication)
						.WithMany(b => b.Books)
						.HasForeignKey(p => p.PublicationId)
						.OnDelete(DeleteBehavior.NoAction);
			});

		}
	}
}

