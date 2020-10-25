using SBDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace space_browser.Source
{
    [System.Serializable]
    public class JSONData
    {
        public Launch Launch;

        public JSONData(Launch launchInfo)
        {
            Launch = launchInfo;
        }
    }

}
