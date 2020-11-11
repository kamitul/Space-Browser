using SBDataLibrary.Server;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser.Source
{
    public abstract class PanelView
    {
        public class PanelData
        {
            public System.Windows.Forms.Form Form;
            public System.Windows.Forms.Panel Panel;
            public ListView ListView;
            public List<IDataController> DataGetter;

            public PanelData(System.Windows.Forms.Form form, System.Windows.Forms.Panel panel, ListView listView, params IDataController[] dataGetter)
            {
                Form = form;
                Panel = panel;
                ListView = listView;
                DataGetter = dataGetter.ToList();
            }
        }

        public abstract PanelData Data { get; }
        public bool IsActive;
        public abstract void Init();
        public abstract Task SetView<T>();
        public abstract void Hide();
    }
}
