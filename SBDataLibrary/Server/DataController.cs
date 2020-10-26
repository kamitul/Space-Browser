using Microsoft.EntityFrameworkCore;
using SBDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBDataLibrary.Server
{
    public class DataController
    {
        private SBDataContext dataContext;

        public DataController()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SpaceDB;Integrated Security=True;");
            dataContext = new SBDataContext(builder.Options);
        }

        public async void Add(Launch launch)
        {
            dataContext.Database.OpenConnection();
            dataContext.Launches.Add(launch);
            dataContext.Rockets.Add(launch.Rocket);
            dataContext.Ships.AddRange(launch.Ships);
            await dataContext.SaveChangesAsync();
            dataContext.Database.CloseConnection();
        }
    }
}
