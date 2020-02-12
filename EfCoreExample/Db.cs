using EfCoreExample.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreExample
{
    public class Db : DbContext
    {
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string c = "Server=(localdb)\\mssqllocaldb;Database=EfCoreExample;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(c);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>()
                .OwnsOne(p => p.Contact);
        }
    }
}