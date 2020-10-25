using Microsoft.EntityFrameworkCore;
using SBDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBDataLibrary.Server
{
    public class SBDataContext : DbContext
    {
        public SBDataContext(DbContextOptions options) : base(options) { }
        public DbSet<Launch> Launches { get; set; }
        public DbSet<Ship> Ships { get; set; }
    }
}
