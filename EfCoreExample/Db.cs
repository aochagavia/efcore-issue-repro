using EfCoreExample.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreExample
{
    public class Db : DbContext
    {
        public DbSet<Registration> Registrations { get; set; }

        public Db(DbContextOptions<Db> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>()
                .OwnsOne(p => p.Contact);
        }
    }
}