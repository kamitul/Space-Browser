using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace space_browser.Source.UI
{
    public class Popup : Form
    {
        public virtual IPayload IPayload { get; protected set; }
    }
}
