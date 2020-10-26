using Microsoft.EntityFrameworkCore;
using SBDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBDataLibrary.Server
{
    public class SBDataContext : DbContext
    {
        public SBDataContext() { }
        public SBDataContext(DbContextOptions options) : base(options) { }
        public DbSet<Launch> Launches { get; set; }
        public DbSet<Rocket> Ships { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SpaceDB;Integrated Security=True;");
    }
}
