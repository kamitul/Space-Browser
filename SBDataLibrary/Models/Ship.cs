using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SBDataLibrary.Models
{
    [System.Serializable]
    public class Ship
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string HomePort { get; set; }
        public Bitmap Image { get; set; }
    }
}
