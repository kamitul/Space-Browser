using SBDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBDataLibrary.Server
{
    public interface IDataController
    {
        IEnumerable<Launch> Launches { get; }
        IEnumerable<Ship> Ships { get; }
        IEnumerable<Rocket> Rockets { get; }
        Task Init();
        Task<List<Launch>> GetLaunchesAsync();
        Task<List<Ship>> GetShipsAsync();
        Task<List<Rocket>> GetRocketsAsync();
        Task DeleteLaunch(Launch launch);
        Task DeleteRocket(Rocket rocket);
        Task DeleteShip(Ship ship);
        Task UpdateLaunch(Launch launch);
        Task UpdateRocket(Rocket rocket);
        Task UpdateShip(Ship ship);
    }
}
