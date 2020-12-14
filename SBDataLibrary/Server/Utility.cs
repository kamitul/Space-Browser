using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SBDataLibrary.Server
{
    /// <summary>
    /// Utility class
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// Gets country code from name
        /// </summary>
        /// <param name="countryUI">Country name</param>
        /// <returns>Three letter ISO region name</returns>
        public static string GetCountry(string countryUI)
        {
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            var englishRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(countryUI));
            return englishRegion != null ? englishRegion.ThreeLetterISORegionName : countryUI.Substring(0, 2);
        }

    }
}
