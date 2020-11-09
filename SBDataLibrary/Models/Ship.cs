using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SBDataLibrary.Models
{
    [Serializable]
    public class Ship : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
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
        public Launch Launch { get; set; }

        public Ship(string shipId, int missions, string name, string type, string homePort, byte[] image)
        {
            ShipId = shipId;
            Missions = missions;
            Name = name;
            Type = type;
            HomePort = homePort;
            Image = image;
        }

        public override string ToString()
        {
            return  $"Ship ID: {ShipId}" + "\n" +
                $"Name : {Name}" + "\n" +
                $"Missions: {Missions}" + "\n" +
                $"Type : {Type}" + "\n" +
                $"HomePort: {HomePort}" + "\n";
        }

        public override void Set(params object[] data)
        {
            Name = (string)data[0];
            int missions;
            if (int.TryParse((string)data[1], out missions))
                Missions = missions;
            Type = (string)data[2];
            HomePort = (string)data[3];
        }

        public override string[] GetFields()
        {
            return new[] { "Name: ", "Missions: ", "Type: ", "HomePort: " };
        }
    }
}
