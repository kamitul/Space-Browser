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
            Country = country;
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
            Country = country;
            LaunchDate = launchDate;
            MissionName = missionName;
        }

        public override string ToString()
        {
            string rocketInfo = string.Empty;
            string shipInfo = string.Empty;
            if (Rocket != null)
            {
                List<string> rocket = new List<string>();
                Rocket.ToString().Split('\n').ToList().ForEach(x => rocket.Add("\t" + x + "\n"));
                rocketInfo = string.Concat(rocket);
            }
            if (Ships != null)
            {
                List<string> ships = new List<string>();
                string.Concat(Ships).Split('\n').ToList().ForEach(x => ships.Add("\t" + x + "\n"));
                shipInfo = string.Concat(ships);
            }

            return $"Launch:" + "\n" +
                $"Flight ID: {FlightId}" + "\n" +
                $"Name : {Name}" + "\n" +
                $"Country: {Country}" + "\n" +
                $"Status : {Status}" + "\n" +
                $"Payloads: {Payloads}" + "\n" +
                $"Launch Data: {LaunchDate}" + "\n" +
                $"Mission Name: {MissionName}" + "\n\n" +
                $"Rocket :  \n{rocketInfo}" + "\n" +
                $"Ships : \n{shipInfo}" + "\n";
        }

        public override void Set(params object[] data)
        {
            Name = (string)data[0];
            Country = (string)data[1];
            int state;
            if (int.TryParse((string)data[3], out state))
                Status = (State)state;
            int payloads;
            if (int.TryParse((string)data[3], out payloads))
                Payloads = payloads;
            LaunchDate = (string)data[4];
            MissionName = (string)data[5];
        }

        public override string[] GetFields()
        {
            return new []{ "Name: ", "Country: ", "Status: ", "Payloads: ", "Launch Date: ", "Mission Name: " };
        }

    }
}
