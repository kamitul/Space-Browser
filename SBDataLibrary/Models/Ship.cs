using System;
using System.ComponentModel.DataAnnotations;

namespace SBDataLibrary.Models
{
    [Serializable]
    public class Ship
    {
        public int Id { get; set; }
        [Required]
        public int LaunchId { get; set; }
        [Required]
        public string ShipId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Type { get; set; }
        public int Missions { get; set; }
        public string HomePort { get; set; }
        public byte[] Image { get; set; }

        public Ship(string shipId, int missions, string name, string type, string homePort, byte[] image)
        {
            ShipId = shipId;
            Missions = missions;
            Name = name;
            Type = type;
            HomePort = homePort;
            Image = image;
        }
    }
}
