using SBDataLibrary.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static space_browser.Source.PanelView;

namespace space_browser.Source.Panel
{
    /// <summary>
    /// File panel class
    /// </summary>
    public class FilePanel : PanelView
    {
        /// <summary>
        /// File panel data
        /// </summary>
        public class FileData : PanelData
        {
            public FileData(Form form, System.Windows.Forms.Panel panel, ListView listView, params IDataController[] dataGetter) : base(form, panel, listView, dataGetter)
            {
            }
        }

        private FileData data;
        public override PanelData Data => data;

        public FilePanel(PanelData data)
        {
            this.data = data as FileData;
        }

        public async Task Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                var launches = await data.DataGetter[0].GetLaunchesAsync();
                var rockets = await data.DataGetter[0].GetRocketsAsync();
                var ships = await data.DataGetter[0].GetShipsAsync();

                string buffer = string.Join(string.Empty, launches.Select(x => x.ToString()));
                buffer += string.Join(string.Empty, rockets.Select(x => x.ToString()));
                buffer += string.Join(string.Empty, ships.Select(x => x.ToString()));

                DateTime beginTime = DateTime.Now;

                using (Stream stream = File.Open(filePath, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        Task task = sw.WriteAsync(buffer);
                        while (!task.IsCompleted)
                        {
                            await Task.Yield();
                        }
                    }
                }
            }
        }

        public override void Init()
        {
            
        }

        public override Task SetView<T>()
        {
            return null;
        }

        public override void Hide()
        {
            
        }
    }
}
