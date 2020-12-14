using SBDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBDataLibrary.Server
{
    /// <summary>
    /// Extends class for data handling
    /// </summary>
    public interface IDataController
    {
        /// <summary>
        /// Launches list
        /// </summary>
        List<Launch> Launches { get; set; }
        /// <summary>
        /// Ships list
        /// </summary>
        List<Ship> Ships { get; set; }
        /// <summary>
        /// Rockets list
        /// </summary>
        List<Rocket> Rockets { get; set; }
        /// <summary>
        /// Initializes data controller
        /// </summary>
        /// <returns>Initialization task</returns>
        Task Init();
        /// <summary>
        /// Gets launches async
        /// </summary>
        /// <returns>Async launches getting task</returns>
        Task<List<Launch>> GetLaunchesAsync();
        /// <summary>
        /// Gets ships async
        /// </summary>
        /// <returns>Async ships getting task</returns>
        Task<List<Ship>> GetShipsAsync();
        /// <summary>
        /// Gets rockets async
        /// </summary>
        /// <returns>Async rockets getting task</returns>
        Task<List<Rocket>> GetRocketsAsync();
        /// <summary>
        /// Deletes launch
        /// </summary>
        /// <param name="launch">Launch to delete</param>
        /// <returns></returns>
        Task DeleteLaunch(Launch launch);
        /// <summary>
        /// Delete rocket
        /// </summary>
        /// <param name="rocket">Rocket to delete</param>
        /// <returns></returns>
        Task DeleteRocket(Rocket rocket);
        /// <summary>
        /// Delete ship
        /// </summary>
        /// <param name="ship">Ship to delete</param>
        /// <returns></returns>
        Task DeleteShip(Ship ship);
        /// <summary>
        /// Updates launch
        /// </summary>
        /// <param name="launch">Launch to be updated</param>
        /// <returns></returns>
        Task UpdateLaunch(Launch launch);
        /// <summary>
        /// Updates rocket
        /// </summary>
        /// <param name="rocket">Rocket to be updated</param>
        /// <returns></returns>
        Task UpdateRocket(Rocket rocket);
        /// <summary>
        /// Updates ship
        /// </summary>
        /// <param name="ship">Ship to be updated</param>
        /// <returns></returns>
        Task UpdateShip(Ship ship);
    }
}
