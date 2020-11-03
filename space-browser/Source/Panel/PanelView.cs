using SBDataLibrary.Server;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser.Source
{
    public abstract class PanelView
    {
        public class PanelData
        {
            public Panel Panel;
            public ListView ListView;
            public IDataGetter DataGetter;

            public PanelData(Panel panel, ListView listView, IDataGetter dataGetter)
            {
                Panel = panel;
                ListView = listView;
                DataGetter = dataGetter;
            }
        }

        public abstract PanelData Data { get; }
        public bool IsActive;
        public abstract void Init();
        public abstract Task SetView<T>();
        public abstract void Hide();
    }
}
