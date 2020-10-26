﻿using System;
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
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Type { get; set; }
        public string HomePort { get; set; }
        public byte[] Image { get; set; }
        public Ship(int id, string name, string type, string homePort, byte[] image)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.HomePort = homePort;
            this.Image = image;
        }

    }
}
