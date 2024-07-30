using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;



namespace WebApplication2.ConnectionContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Ingredients)
                .HasColumnType("json");
        }


    }
}
