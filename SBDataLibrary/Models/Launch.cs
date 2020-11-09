using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

namespace SBDataLibrary.Models
{

    public enum State
    {
        Finished,
        Pending
    }

    [Serializable]
    public class Launch : Entity
    {
        [Required]
        [Key]
        public string FlightId { get; set; }
        [Required]
        public State Status { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public int Payloads { get; set; }
        [Required]
        [MaxLength(200)]
        public string RocketName { get; set; }
        [Required]
        [MaxLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string Country { get; set; }
        [MaxLength(200)]
        public string LaunchDate { get; set; }
        [MaxLength(200)]
        public string MissionName { get; set; }
        public Rocket Rocket { get; set; }
        public List<Ship> Ships { get; set; }

        public Launch(string flightId, State status, string name, int payloads, string rocketName, string country, string launchDate, string missionName, Rocket rocket, List<Ship> ships)
        {
            FlightId = flightId;
            Status = status;
            Name = name;
            Payloads = payloads;
            RocketName = rocketName;
            Country = GetCountry(country);
            LaunchDate = launchDate;
            MissionName = missionName;
            Rocket = rocket;
            Ships = ships;
        }

        public Launch(string flightId, State status, string name, int payloads, string rocketName, string country, string launchDate, string missionName)
        {
            FlightId = flightId;
            Status = status;
            Name = name;
            Payloads = payloads;
            RocketName = rocketName;
            Country = GetCountry(country);
            LaunchDate = launchDate;
            MissionName = missionName;
        }

        private string GetCountry(string countryUI)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            List<RegionInfo> regions = new List<RegionInfo>();
            for (int i = 0; i < cultures.Length; ++i)
            {
                regions.Add(new RegionInfo(cultures[i].LCID));
            }
            RegionInfo country = null;
            for (int i = 0; i < regions.Count; ++i)
            {
                if (regions[i].EnglishName.Contains(countryUI))
                {
                    country = regions[i];
                    break;
                }
            }
            return country != null ? country.ToString() : string.Empty;
        }

        public override string ToString()
        {
            List<string> rocket = new List<string>();
            Rocket.ToString().Split('\n').ToList().ForEach(x=> rocket.Add( "\t" + x + "\n"));
            var rocketInfo = string.Concat(rocket);
            List<string> ships = new List<string>();
            string.Concat(Ships).Split('\n').ToList().ForEach(x => ships.Add("\t" + x + "\n"));
            var shipInfo = string.Concat(ships);

            return $"Launch:" + "\n" +
                $"Flight ID: {FlightId}" + "\n" +
                $"Name : {Name}" + "\n" +
                $"Country: {Country}" + "\n" +
                $"Status : {Status}" + "\n" +
                $"Payloads: {Payloads}" + "\n" +
                $"Launch Data: {LaunchDate}" + "\n" +
                $"Mission Name: {MissionName}" + "\n" +
                $"Rocket :  \n{rocketInfo}" + "\n" +
                $"Ships : \n{shipInfo}" + "\n";
        }
    }
}
