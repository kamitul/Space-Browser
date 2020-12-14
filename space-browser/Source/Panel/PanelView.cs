using SBDataLibrary.Server;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser.Source
{
    /// <summary>
    /// Panel view base class
    /// </summary>
    public abstract class PanelView
    {
        /// <summary>
        /// Panel data class
        /// </summary>
        public class PanelData
        {
            /// <summary>
            /// Form of each panel
            /// </summary>
            public Form Form;
            /// <summary>
            /// Panel reference
            /// </summary>
            public System.Windows.Forms.Panel Panel;
            /// <summary>
            /// List view on each form
            /// </summary>
            public ListView ListView;
            /// <summary>
            /// Data getters used in current panel
            /// </summary>
            public List<IDataController> DataGetter;

            /// <summary>
            /// Constructs panelview
            /// </summary>
            /// <param name="form">Used form</param>
            /// <param name="panel">Used panel</param>
            /// <param name="listView">Used listview</param>
            /// <param name="dataGetter">Used data getters</param>
            public PanelData(Form form, System.Windows.Forms.Panel panel, ListView listView, params IDataController[] dataGetter)
            {
                Form = form;
                Panel = panel;
                ListView = listView;
                DataGetter = dataGetter.ToList();
            }
        }

        /// <summary>
        /// Data property
        /// </summary>
        public abstract PanelData Data { get; }
        /// <summary>
        /// Activation flag
        /// </summary>
        public bool IsActive;
        /// <summary>
        /// Initializes panel
        /// </summary>
        public abstract void Init();
        /// <summary>
        /// Set Panel view
        /// </summary>
        /// <typeparam name="T">Type of entity to be set</typeparam>
        /// <returns>Seting async task</returns>
        public abstract Task SetView<T>();
        /// <summary>
        /// Hide panel view
        /// </summary>
        public abstract void Hide();
    }
}
