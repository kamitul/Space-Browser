using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace SBDataLibrary.Models
{

    public enum State
    {
        Finished,
        Pending
    }

    [System.Serializable]
    public class Launch
    {
        public int Id { get; set; }
        [Required]
        public State Status { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public int Payloads { get; set; }
        [Required]
        [MaxLength(200)]
        public string RocketName { get; set; }
        [Required]
        [MaxLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string Country { get; set; }
        public List<Ship> Ships { get; set; } = new List<Ship>();

        public Launch(int id, State status, string name, int payloads, string rocketname, string country)
        {
            this.Id = id;
            this.Status = status;
            this.Name = name;
            this.Payloads = payloads;
            this.RocketName = rocketname;
            this.Country = GetCountry(country);
        }

        private string GetCountry(string countryUI)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            List<RegionInfo> regions = new List<RegionInfo>();
            for (int i = 0; i < cultures.Length; ++i)
            {
                regions.Add(new RegionInfo(cultures[i].LCID));
            }
            RegionInfo country = null;
            for (int i = 0; i < regions.Count; ++i)
            {
                if (regions[i].EnglishName.Contains(countryUI))
                {
                    country = regions[i];
                    break;
                }
            }
            return country != null ? country.ToString() : string.Empty;
        }
    }
}
