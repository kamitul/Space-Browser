using System.Windows.Forms;

namespace space_browser.Source.UI
{
    public class Popup : Form
    {
        public Popup()
        {
            TopMost = true;
        }
        public virtual IPayload IPayload { get; protected set; }
    }
}
