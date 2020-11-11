using Microsoft.EntityFrameworkCore;
using SBDataLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBDataLibrary.Server
{
    public class ServerDataController : IDataController
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

        public async Task Add(Launch launch)
        {
            var rocket = launch.Rocket.Copy();
            var ships = launch.Ships.Select(x => x.Copy()).ToList();
            var lnc = new Launch(launch.FlightId, launch.Status, launch.Name, launch.Payloads, launch.RocketName, launch.Country, launch.LaunchDate, launch.MissionName, rocket, ships);

            dataContext.Database.OpenConnection();
            if (dataContext.Launches.Any(x => x.FlightId == launch.FlightId))
                throw new System.Exception("Launch is already added!");
            dataContext.Launches.Add(lnc);
            dataContext.Rockets.Add(rocket);
            dataContext.Ships.AddRange(ships);
            await dataContext.SaveChangesAsync();
            dataContext.Database.CloseConnection();
        }


        public async Task<List<Launch>> GetLaunchesAsync()
        {
            var res = await dataContext.Launches
                .Include(x => x.Rocket)
                .Include(x => x.Ships)
                .ToListAsync();
            launches = res;
            return res;
        }

        public async Task<List<Ship>> GetShipsAsync()
        {
            var res = await dataContext.Ships
                .Include(x=>x.Launch)
                .ToListAsync();
            ships = res;
            return res;
        }

        public async Task<List<Rocket>> GetRocketsAsync()
        {
            var res = await dataContext.Rockets
                .Include(x => x.Launch)
                .ToListAsync();
            rockets = res;
            return res;
        }

        public async Task DeleteLaunch(Launch launch)
        {
            var ships = dataContext.Ships.Where(b => b.Launch.FlightId == launch.FlightId);
            var rocket = launch.Rocket;
            launch.Rocket = null;
            foreach (var ship in ships)
            {
                launch.Ships.Remove(ship);
            }

            dataContext.Database.OpenConnection();
            dataContext.Launches.Remove(launch);
            dataContext.Ships.RemoveRange(ships);
            dataContext.Rockets.Remove(rocket);
            await dataContext.SaveChangesAsync();
            dataContext.Database.CloseConnection();
        }

        public async Task DeleteRocket(Rocket rocket)
        {
            dataContext.Database.OpenConnection();
            dataContext.Rockets.Remove(rocket);
            await dataContext.SaveChangesAsync();
            dataContext.Database.CloseConnection();
        }

        public async Task DeleteShip(Ship ship)
        {
            dataContext.Database.OpenConnection();
            dataContext.Ships.Remove(ship);
            await dataContext.SaveChangesAsync();
            dataContext.Database.CloseConnection();
        }

        public async Task UpdateLaunch(Launch launch)
        {
            dataContext.Database.OpenConnection();
            dataContext.Launches.Update(launch);
            await dataContext.SaveChangesAsync();

        }

        public async Task UpdateRocket(Rocket rocket)
        {
            dataContext.Database.OpenConnection();
            dataContext.Rockets.Update(rocket);
            await dataContext.SaveChangesAsync();
        }

        public async Task UpdateShip(Ship ship)
        {
            dataContext.Database.OpenConnection();
            dataContext.Ships.Update(ship);
            await dataContext.SaveChangesAsync();
        }
    }
}
