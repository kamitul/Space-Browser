using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBDataLibrary.Models
{
    /// <summary>
    /// Ship entity class
    /// </summary>
    [Serializable]
    public class Ship : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("newid()")]
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

        /// <summary>
        /// Constructs ship
        /// </summary>
        /// <param name="shipId">Ship ID</param>
        /// <param name="missions">Ship missions</param>
        /// <param name="name">Ship name</param>
        /// <param name="type">Ship type</param>
        /// <param name="homePort">Ship homePort</param>
        /// <param name="image">Ship image</param>
        public Ship(string shipId, int missions, string name, string type, string homePort, byte[] image)
        {
            ShipId = shipId;
            Missions = missions;
            Name = name;
            Type = type;
            HomePort = homePort;
            Image = image;
        }

        /// <summary>
        /// Parses ship to string
        /// </summary>
        /// <returns>Parsed ship</returns>
        public override string ToString()
        {
            return  $"Ship ID: {ShipId}" + "\n" +
                $"Name : {Name}" + "\n" +
                $"Missions: {Missions}" + "\n" +
                $"Type : {Type}" + "\n" +
                $"HomePort: {HomePort}" + "\n\n";
        }

        /// <summary>
        /// Sets entity data
        /// </summary>
        /// <param name="data">Passed data</param>
        public override void Set(params object[] data)
        {
            Name = (string)data[0];
            int missions;
            if (int.TryParse((string)data[1], out missions))
                Missions = missions;
            Type = (string)data[2];
            HomePort = (string)data[3];
        }

        /// <summary>
        /// Gets fields as string
        /// </summary>
        /// <returns>Fields names as array</returns>
        public override string[] GetFields()
        {
            return new[] { "Name: ", "Missions: ", "Type: ", "HomePort: " };
        }

        /// <summary>
        /// Copies ship
        /// </summary>
        /// <returns>Copied ship</returns>
        public Ship Copy()
        {
            var ship = new Ship(ShipId, Missions, Name, Type, HomePort, Image);
            ship.ID = 0;
            return ship;
        }
    }
}
