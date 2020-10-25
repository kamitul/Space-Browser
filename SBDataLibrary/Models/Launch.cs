using System;
using System.Collections.Generic;
using System.Text;

namespace SBDataLibrary.Models
{
    [System.Serializable]
    public class Launch
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public int Payloads { get; set; }
        public string RocketName { get; set; }
        public string Country { get; set; }
        public List<Ship> Ships { get; set; } = new List<Ship>();
    }
}
