using System.Windows.Forms;

namespace space_browser.Source.UI
{
    /// <summary>
    /// Base class for all popups in program
    /// </summary>
    public class Popup : Form
    {
        /// <summary>
        /// Initializes popup
        /// </summary>
        public Popup()
        {
            TopMost = true;
        }

        /// <summary>
        /// Virtual property of base payload interface for each popup
        /// </summary>
        public virtual IPayload IPayload { get; protected set; }
    }
}
