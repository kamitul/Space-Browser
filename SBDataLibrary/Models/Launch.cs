using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace SBDataLibrary.Models
{

    public enum State
    {
        Finished,
        Pending
    }

    [System.Serializable]
    public class Launch
    {
        public int Id { get; set; }
        [Required]
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
            this.FlightId = flightId;
            this.Status = status;
            this.Name = name;
            this.Payloads = payloads;
            this.RocketName = rocketName;
            this.Country = GetCountry(country);
            this.LaunchDate = launchDate;
            this.MissionName = missionName;
            this.Rocket = rocket;
            this.Ships = ships;
        }

        public Launch(string flightId, State status, string name, int payloads, string rocketName, string country, string launchDate, string missionName)
        {
            this.FlightId = flightId;
            this.Status = status;
            this.Name = name;
            this.Payloads = payloads;
            this.RocketName = rocketName;
            this.Country = GetCountry(country);
            this.LaunchDate = launchDate;
            this.MissionName = missionName;
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
    }
}
