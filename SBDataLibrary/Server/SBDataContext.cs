using Microsoft.EntityFrameworkCore;
using SBDataLibrary.Models;

namespace SBDataLibrary.Server
{
    /// <summary>
    /// Data context class for database
    /// </summary>
    public class SBDataContext : DbContext
    {
        /// <summary>
        /// Constructs data context class
        /// </summary>
        public SBDataContext() { }
        /// <summary>
        /// Constrcuts data contex class
        /// </summary>
        /// <param name="options"></param>
        public SBDataContext(DbContextOptions options) : base(options) { }
        /// <summary>
        /// Launches database set
        /// </summary>
        public DbSet<Launch> Launches { get; set; }
        /// <summary>
        /// Rockets database set
        /// </summary>
        public DbSet<Rocket> Rockets { get; set; }
        /// <summary>
        /// Ships database set
        /// </summary>
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
