using SBDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBDataLibrary.Server
{
    public interface IDataController
    {
        List<Launch> Launches { get; set; }
        List<Ship> Ships { get; set; }
        List<Rocket> Rockets { get; set; }
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
