using Microsoft.EntityFrameworkCore;
using Sales.Shared.Entities;

namespace Sales.Api.Data
{
    public class DateContext : DbContext
    {
        public DateContext(DbContextOptions<DateContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();   
        }
    }

}
