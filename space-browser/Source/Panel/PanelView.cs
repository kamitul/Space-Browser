using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace space_browser.Source
{
    public abstract class PanelView
    {
        public class PanelData
        {
            public System.Windows.Forms.Panel Panel;
            public System.Windows.Forms.ListView ListView;

            public PanelData(Panel panel, ListView listView)
            {
                Panel = panel;
                ListView = listView;
            }
        }

        public PanelData Data;
        public bool IsActive;
        public abstract void Init();
        public PanelView(PanelData data) => this.Data = data;
        public abstract void SetView<T>(List<T> data);
        public abstract void Hide();
    }
}
