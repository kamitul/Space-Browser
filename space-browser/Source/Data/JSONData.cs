using SBDataLibrary.Models;
using System;
using System.Collections.Generic;

namespace space_browser.Source
{
    [Serializable]
    public class JSONData
    {
        public Launch Launch;
        public Rocket Rocket;
        public List<Ship> Ships;

        public JSONData(Launch launchInfo, Rocket rocket, List<Ship> ships)
        {
            Launch = launchInfo;
            Rocket = rocket;
            Ships = ships;
        }
    }

}
