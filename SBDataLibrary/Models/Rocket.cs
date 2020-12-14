using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBDataLibrary.Models
{
    /// <summary>
    /// Rocket entity object
    /// </summary>
    [Serializable]
    public class Rocket : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("newid()")]
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
        [MaxLength(3)]
        public string Country { get; set; }
        public int Mass { get; set; }
        public byte[] Image { get; set; }
        public string LaunchID { get; set; }
        public Launch Launch { get; set; }

        /// <summary>
        /// Constructs rocket
        /// </summary>
        /// <param name="rocketId">Rocket  ID</param>
        /// <param name="name">Rocket name</param>
        /// <param name="type">Rocket type</param>
        /// <param name="country">Rocket country</param>
        /// <param name="mass">Rocket mass</param>
        /// <param name="image">Rocket image</param>
        public Rocket(string rocketId, string name, string type, string country, int mass, byte[] image)
        {
            RocketId = rocketId;
            Name = name;
            Type = type;
            Country = country;
            Mass = mass;
            Image = image;
        }
        
        /// <summary>
        /// Parses rocket to string
        /// </summary>
        /// <returns>Parsed rocket</returns>
        public override string ToString()
        {
            return $"Rocket ID: {RocketId}" + "\n" +
                $"Name : {Name}" + "\n" +
                $"Country: {Country}" + "\n" +
                $"Type : {Type}" + "\n" +
                $"Mass: {Mass}" + "\n";
        }

        /// <summary>
        /// Sets entity data
        /// </summary>
        /// <param name="data">Passed data</param>
        public override void Set(params object[] data)
        {
            Name = (string)data[0];
            Country = (string)data[1];
            Type = (string)data[2];
            int mass;
            if (int.TryParse((string)data[3], out mass))
                Mass = mass;
        }

        /// <summary>
        /// Gets fields as string
        /// </summary>
        /// <returns>Fields names as array</returns>
        public override string[] GetFields()
        {
            return new []{ "Name: ", "Country: ", "Type: ", "Mass: " };
        }

        /// <summary>
        /// Copies exsiting rocket
        /// </summary>
        /// <returns>Copied rocket object</returns>
        public Rocket Copy()
        {
            var rocket = new Rocket(RocketId, Name, Type, Country, Mass, Image);
            rocket.ID = 0;
            return rocket;
        }
    }
}
