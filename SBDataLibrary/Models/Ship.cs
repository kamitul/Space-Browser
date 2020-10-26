using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SBDataLibrary.Models
{
    [System.Serializable]
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
            this.ShipId = shipId;
            this.Missions = missions;
            this.Name = name;
            this.Type = type;
            this.HomePort = homePort;
            this.Image = image;
        }
    }
}
