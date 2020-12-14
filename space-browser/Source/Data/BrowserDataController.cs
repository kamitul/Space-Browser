using Newtonsoft.Json.Linq;
using SBDataLibrary.Models;
using SBDataLibrary.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace space_browser.Source
{
    /// <summary>
    /// Browser data controller class
    /// </summary>
    public class BrowserDataController : IDataController
    {
        private Connection connection;
        private List<Launch> launches;
        private List<Rocket> rockets;
        private List<Ship> ships;

        /// <summary>
        /// Launches local list
        /// </summary>
        public List<Launch> Launches { get => launches; set => launches = value; }
        /// <summary>
        /// Ships local list
        /// </summary>
        public List<Ship> Ships { get => ships; set => ships = value; }
        /// <summary>
        /// Rockets local list
        /// </summary>
        public List<Rocket> Rockets { get => rockets; set => rockets = value; }

        /// <summary>
        /// Constructs browser data controller
        /// </summary>
        public BrowserDataController()
        {
            connection = new Connection(60);
        }

        public async Task Init()
        {
            await LoadData();
        }

        /// <summary>
        /// Loads data from REST API
        /// </summary>
        /// <returns>Loading data task</returns>
        public async Task LoadData()
        {
            var result = await Task.WhenAll(Connect("https://api.spacexdata.com/v3/launches"), Connect("https://api.spacexdata.com/v3/ships"), Connect("https://api.spacexdata.com/v3/rockets"));
            var parsedData = await CollectData(result[0], result[2], result[1]);

            launches = parsedData.Select(x=>x.Launch).ToList();
            rockets = parsedData.Select(x => x.Rocket).ToList();
            ships = parsedData.SelectMany(x => x.Ships).Distinct().ToList();
        }

        /// <summary>
        /// Connects to specific URL
        /// </summary>
        /// <param name="url">URL to be connected to</param>
        /// <returns>Connection async task</returns>
        private async Task<string> Connect(string url)
        {
            var task = connection.CreateGet(url);
            var response = await task.Send();
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            return content;
        }

        /// <summary>
        /// Collects data as JSON
        /// </summary>
        /// <param name="launches">JSON launches data</param>
        /// <param name="rockets">JSON rockets data</param>
        /// <param name="ships">JSON ships data</param>
        /// <returns>Collection JSON data async task</returns>
        private async Task<List<JSONData>> CollectData(string launches, string rockets, string ships)
        {
            var result = await PopulateData(
                "{ launches: " + launches + "}",
                "{ ships: " + ships + "}",
                "{ rockets: " + rockets + "}"
                );
            return result;
        }

        /// <summary>
        /// Populates data
        /// </summary>
        /// <param name="launchesJSON">JSON launches data</param>
        /// <param name="rocketsJSON">JSON rockets data</param>
        /// <param name="shipsJSON">JSON ships data</param>
        /// <returns>Collection JSON data async task</returns>
        private async Task<List<JSONData>> PopulateData(string launchesJSON, string shipsJSON, string rocketsJSON)
        {
            List<JSONData> collectedData = new List<JSONData>();

            JObject launchesParsed = JObject.Parse(launchesJSON);
            JObject shipsParsed = JObject.Parse(shipsJSON);
            JObject rocketsParsed = JObject.Parse(rocketsJSON);

            List<Rocket> rockets = await SetRocketInfo(rocketsParsed);
            List<Ship> ships = await SetShipsInfo(shipsParsed);
            List<Launch> launches = await SetLaunchInfo(launchesParsed, ships, rockets);


            for (int i = 0; i < launches.Count; ++i)
            {
                collectedData.Add(new JSONData(launches[i], launches[i].Rocket, launches[i].Ships));
            }

            return collectedData;
        }

        /// <summary>
        /// Collects launch info from json
        /// </summary>
        /// <param name="launchesJSON">JSON launches data</param>
        /// <param name="ships">Ships list</param>
        /// <param name="rockets">Rocket list</param>
        /// <returns>Launches collecting async task</returns>
        private async Task<List<Launch>> SetLaunchInfo(JObject launchesJSON, List<Ship> ships, List<Rocket> rockets)
        {
            List<Launch> ret = new List<Launch>();

            for (int i = 0; i < launchesJSON["launches"].Count(); ++i)
            {
                var ships_id = launchesJSON["launches"][i]["ships"].ToObject<string[]>();
                var rocket_id = (string)launchesJSON["launches"][i]["rocket"]["rocket_id"];
                ret.Add(new Launch(
                                (string)launchesJSON["launches"][i]["flight_number"],
                                (State)(int)launchesJSON["launches"][i]["upcoming"],
                                (string)launchesJSON["launches"][i]["mission_name"],
                                launchesJSON["launches"][i]["rocket"]["second_stage"]["payloads"].Count(),
                                (string)launchesJSON["launches"][i]["rocket"]["rocket_name"],
                                Utility.GetCountry((string)launchesJSON["launches"][i]["rocket"]["second_stage"]["payloads"][0]["nationality"]),
                                (string)launchesJSON["launches"][i]["launch_date_utc"],
                                (string)launchesJSON["launches"][i]["mission_name"],
                                rockets.Find(x => x.RocketId == rocket_id),
                                ships.FindAll(x => ships_id.Contains(x.ShipId))));
            }
            return ret;
        }

        /// <summary>
        /// Sets rocket list data from rocket json data
        /// </summary>
        /// <param name="rocketJSON">JSON Rocket data</param>
        /// <returns>Seting rocket info list async task</returns>
        private async Task<List<Rocket>> SetRocketInfo(JObject rocketJSON)
        {
            List<Rocket> rockets = new List<Rocket>();
            for (int j = 0; j < rocketJSON["rockets"].Count(); ++j)
            {
                byte[] image = await GetImage(rocketJSON["rockets"][j]["flickr_images"][0].ToString());

                rockets.Add(new Rocket(
                                (string)rocketJSON["rockets"][j]["rocket_id"],
                                (string)rocketJSON["rockets"][j]["rocket_name"],
                                (string)rocketJSON["rockets"][j]["rocket_type"],
                                Utility.GetCountry((string)rocketJSON["rockets"][j]["country"]),
                                (int)rocketJSON["rockets"][j]["mass"]["kg"],
                                image));
            }
            return rockets;
        }

        /// <summary>
        /// Sets ships list data from rocket json data
        /// </summary>
        /// <param name="shipsJSON">JSON Ship data</param>
        /// <returns>Seting ship info list async task</returns>
        private async Task<List<Ship>> SetShipsInfo(JObject shipsJSON)
        {
            List<Ship> shipInfos = new List<Ship>();
            for (int j = 0; j < shipsJSON["ships"].Count(); ++j)
            {
                    byte[] image = await GetImage((string)shipsJSON["ships"][j]["image"]);
                    shipInfos.Add(new Ship(
                        (string)shipsJSON["ships"][j]["ship_id"],
                        shipsJSON["ships"][j]["missions"].Count(),
                        (string)shipsJSON["ships"][j]["ship_name"],
                        (string)shipsJSON["ships"][j]["ship_type"],
                        (string)shipsJSON["ships"][j]["home_port"],
                        image));
            }

            return shipInfos;
        }

        /// <summary>
        /// Gets image from specific url
        /// </summary>
        /// <param name="url">Image URL</param>
        /// <returns>Getting image async task</returns>
        private  async Task<byte[]> GetImage(string url)
        {
            if (url != null && !url.Equals(string.Empty))
            {
                byte[] image;
                using (WebClient client = new WebClient())
                {
                    image = await client.DownloadDataTaskAsync(url);
                }
                return image;
            }
            else
                return null;
        }

        public async Task<List<Launch>> GetLaunchesAsync()
        {
            await Init();
            return launches;
        }

        public async Task<List<Ship>> GetShipsAsync()
        {
            await Init();
            return ships;
        }

        public async Task<List<Rocket>> GetRocketsAsync()
        {
            await Init();
            return rockets;
        }

        public async Task DeleteLaunch(Launch launch)
        {
            await Task.Yield();
            launches.Remove(launch);
        }

        public async Task DeleteRocket(Rocket rocket)
        {
            await Task.Yield();
            rockets.Remove(rocket);
        }

        public async Task DeleteShip(Ship ship)
        {
            await Task.Yield();
            ships.Remove(ship);
        }

        public async Task UpdateLaunch(Launch launch)
        {
            await Task.Yield();
            var elem = launches.Find(x => x.FlightId == launch.FlightId);
            if(elem != null)
                elem.Set(launch.Name, launch.Country, launch.Status, launch.Payloads, launch.LaunchDate, launch.MissionName);
        }

        public async Task UpdateRocket(Rocket rocket)
        {
            await Task.Yield();
            var elem = rockets.Find(x => x.ID == rocket.ID);
            if (elem != null)
                elem.Set(rocket.Name, rocket.Country, rocket.Type, rocket.Mass);
        }

        public async Task UpdateShip(Ship ship)
        {
            await Task.Yield();
            var elem = ships.Find(x => x.ID == ship.ID);
            if (elem != null)
                elem.Set(ship.Name, ship.Missions, ship.Type, ship.HomePort);
        }
    }
}
