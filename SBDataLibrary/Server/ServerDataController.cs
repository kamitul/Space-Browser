using Microsoft.EntityFrameworkCore;
using SBDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBDataLibrary.Server
{
    public class ServerDataController : IDataGetter
    {
        private SBDataContext dataContext;

        private List<Launch> launches;
        private List<Rocket> rockets;
        private List<Ship> ships;

        public IEnumerable<Launch> Launches => launches;
        public IEnumerable<Ship> Ships => ships;
        public IEnumerable<Rocket> Rockets => rockets;

        public ServerDataController()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SpaceDB;Integrated Security=True;");
            dataContext = new SBDataContext(builder.Options);
        }

        public async Task Init()
        {

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


        public async Task<List<Launch>> GetLaunchesAsync()
        {
            var res = await dataContext.Launches.ToListAsync();
            launches = res;
            return res;
        }

        public async Task<List<Ship>> GetShipsAsync()
        {
            var res = await dataContext.Ships.ToListAsync();
            ships = res;
            return res;
        }

        public async Task<List<Rocket>> GetRocketsAsync()
        {
            var res = await dataContext.Rockets.ToListAsync();
            rockets = res;
            return res;
        }
    }
}
