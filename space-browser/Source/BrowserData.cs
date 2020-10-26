using Newtonsoft.Json.Linq;
using SBDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace space_browser.Source
{
    public class BrowserData : IRefreshable
    {
        public List<Launch> Launches;
        private Connection connection;
        public BrowserData()
        {
            connection = new Connection(60);
            Launches = new List<Launch>();
        }

        public async Task LoadData()
        {
            Refresh();
            var result = await Task.WhenAll(Connect("https://api.spacexdata.com/v3/launches"), Connect("https://api.spacexdata.com/v3/ships"), Connect("https://api.spacexdata.com/v3/rockets"));
            var parsedData = await CollectData(result[0], result[2], result[1]);
            for (int i = 0; i < parsedData.Count; ++i)
            {
                Launches.Add(parsedData[i].Launch);
            }
        }

        public void Refresh()
        {
            Launches.Clear();
        }

        private async Task<string> Connect(string url)
        {
            var task = connection.CreateGet(url);
            var response = await task.Send();
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            return content;
        }

        private async Task<List<JSONData>> CollectData(string launches, string rockets, string ships)
        {
            var result = await PopulateData(
                "{ launches: " + launches + "}",
                "{ ships: " + ships + "}",
                "{ rockets: " + rockets + "}"
                );
            return result;
        }

        private async Task<List<JSONData>> PopulateData(string launchesJSON, string shipsJSON, string rocketsJSON)
        {
            List<JSONData> collectedData = new List<JSONData>();

            JObject launchesParsed = JObject.Parse(launchesJSON);
            JObject shipsParsed = JObject.Parse(shipsJSON);
            JObject rocketsParsed = JObject.Parse(rocketsJSON);

            List<Rocket> rockets = await SetRocketInfo(rocketsParsed);
            List<Ship> ships = await SetShipsInfo(shipsParsed);

            for (int i = 0; i < launchesParsed["launches"].Count(); ++i)
            {
                Launch launch = SetLaunchInfo(launchesParsed, i, ships, rockets);
                collectedData.Add(new JSONData(launch));
            }

            return collectedData;
        }

        private Launch SetLaunchInfo(JObject launchesParsed, int i, List<Ship> shipsInfo, List<Rocket> rockets)
        {
            var ships_id = launchesParsed["launches"][i]["ships"];
            var rocket_id = (string)launchesParsed["launches"][i]["rocket"]["rocket_id"];
            return new Launch(
                        (string)launchesParsed["launches"][i]["flight_number"],
                        (State)(int)launchesParsed["launches"][i]["upcoming"],
                        (string)launchesParsed["launches"][i]["mission_name"],
                        launchesParsed["launches"][i]["rocket"]["second_stage"]["payloads"].Count(),
                        (string)launchesParsed["launches"][i]["rocket"]["rocket_name"],
                        (string)launchesParsed["launches"][i]["rocket"]["second_stage"]["payloads"][0]["nationality"],
                        (string)launchesParsed["launches"][i]["launch_date_utc"],
                        (string)launchesParsed["launches"][i]["mission_name"],
                        rockets.Find(x=>x.RocketId == rocket_id),
                        shipsInfo.FindAll(x=>ships_id.Contains(x.Id)));
        }

        private async Task<List<Rocket>> SetRocketInfo(JObject rocketJSON)
        {
            List<Rocket> rockets = new List<Rocket>();
            for (int j = 0; j < rocketJSON["rockets"].Count(); ++j)
            {
                var url = $"http://en.wikipedia.org/w/api.php?action=query&titles={((string)rocketJSON["rockets"][j]["wikipedia"]).Split('/').Last()}&prop=pageimages&format=json&pithumbsize=220";
                var task = connection.CreateGet(url);
                var response = await task.Send();
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;
                JObject urlParsed = JObject.Parse("{ image: " + content + "}");

                var firstName = ((JProperty)urlParsed["image"]["query"]["pages"].ElementAt(0)).Name;
                byte[] image = await GetImage((string)urlParsed["image"]["query"]["pages"][firstName]["thumbnail"]["source"]);

                rockets.Add(new Rocket(
                                (string)rocketJSON["rockets"][j]["rocket_id"],
                                (string)rocketJSON["rockets"][j]["rocket_name"],
                                (string)rocketJSON["rockets"][j]["rocket_type"],
                                (string)rocketJSON["rockets"][j]["country"],
                                (int)rocketJSON["rockets"][j]["mass"]["kg"],
                                image));
            }
            return rockets;
        }

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
    }
}
