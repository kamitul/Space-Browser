using SBDataLibrary.Models;
using System;
using System.Collections.Generic;

namespace space_browser.Source
{
    /// <summary>
    /// Json data class
    /// </summary>
    [Serializable]
    public class JSONData
    {
        /// <summary>
        /// Launch entity
        /// </summary>
        public Launch Launch;
        /// <summary>
        /// Rocket entity
        /// </summary>
        public Rocket Rocket;
        /// <summary>
        /// Ships entities
        /// </summary>
        public List<Ship> Ships;
        
        /// <summary>
        /// Constructs JSON Data
        /// </summary>
        /// <param name="launchInfo">Launch</param>
        /// <param name="rocket">Rocket</param>
        /// <param name="ships">Ships list</param>
        public JSONData(Launch launchInfo, Rocket rocket, List<Ship> ships)
        {
            Launch = launchInfo;
            Rocket = rocket;
            Ships = ships;
        }
    }

}
