using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBDataLibrary.Models
{
    [Serializable]
    public class Rocket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
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
        public string LaunchID { get; set; }
        public Launch Launch { get; set; }

        public Rocket(string rocketId, string name, string type, string country, int mass, byte[] image)
        {
            RocketId = rocketId;
            Name = name;
            Type = type;
            Country = country;
            Mass = mass;
            Image = image;
        }

        public override string ToString()
        {
            return $"Rocket ID: {RocketId}" + "\n" +
                $"Name : {Name}" + "\n" +
                $"Country: {Country}" + "\n" +
                $"Type : {Type}" + "\n" +
                $"Mass: {Mass}" + "\n";
        }
    }
}
