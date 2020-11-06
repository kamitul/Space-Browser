using Microsoft.EntityFrameworkCore;
using SBDataLibrary.Models;

namespace SBDataLibrary.Server
{
    public class SBDataContext : DbContext
    {
        public SBDataContext() { }
        public SBDataContext(DbContextOptions options) : base(options) { }
        public DbSet<Launch> Launches { get; set; }
        public DbSet<Rocket> Rockets { get; set; }
        public DbSet<Ship> Ships { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SpaceDB;Integrated Security=True;");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Launch>()
                .HasOne(a => a.Rocket)
                .WithOne(b => b.Launch)
                .HasForeignKey<Rocket>(b => b.LaunchID);
            modelBuilder.Entity<Launch>()
               .HasMany(c => c.Ships)
               .WithOne(e => e.Launch);
        }
    }
}
