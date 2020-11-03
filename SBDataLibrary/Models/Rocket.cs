using System;
using System.ComponentModel.DataAnnotations;

namespace SBDataLibrary.Models
{
    [Serializable]
    public class Rocket
    {
        public int Id { get; set; }
        [Required]
        public int LaunchId { get; set; }
        [Required]
        public string RocketId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Type { get; set; }
        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
        public int Mass { get; set; }
        public byte[] Image { get; set; }

        public Rocket(string rocketId, string name, string type, string country, int mass, byte[] image)
        {
            RocketId = rocketId;
            Name = name;
            Type = type;
            Country = country;
            Mass = mass;
            Image = image;
        }
    }
}
