using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SBDataLibrary.Server
{
    public class Utility
    {
        public static string GetCountry(string countryUI)
        {
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            var englishRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(countryUI));
            return englishRegion != null ? englishRegion.ThreeLetterISORegionName : countryUI.Substring(0, 2);
        }

    }
}
