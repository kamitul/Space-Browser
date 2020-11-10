using SBDataLibrary.Models;
using SBDataLibrary.Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser.Source
{
    class BrowserPanel : PanelView
    {
        public class BrowserData : PanelData
        {
            public RichTextBox RichTextBox;
            public PictureBox PictureBox;

            public BrowserData(System.Windows.Forms.Panel panel, ListView listView, RichTextBox textBox, PictureBox pictureBox, params IDataController[] dataGetter) : base(panel, listView, dataGetter)
            {
                RichTextBox = textBox;
                PictureBox = pictureBox;
            }

        }

        private BrowserData data;
        public override PanelData Data => data;

        public BrowserPanel(PanelData data)
        {
            this.data = data as BrowserData;
            this.data.ListView.SelectedIndexChanged += SelectedIndexChangedEvent;
        }

        private void SelectedIndexChangedEvent(object sender, EventArgs e)
        {
            if (data.ListView.SelectedItems.Count > 0)
            {
                int index = data.ListView.Items.IndexOf(data.ListView.SelectedItems[0]);
                data.RichTextBox.Text = $"Rocket Data:\r\n" +
                    $"Mission Name: {data.DataGetter[0].Launches.ElementAt(index).MissionName}\r\n" +
                    $"Launch Date: {data.DataGetter[0].Launches.ElementAt(index).LaunchDate}\r\n" +
                    $"Rocket ID: {data.DataGetter[0].Launches.ElementAt(index).Rocket.RocketId}\r\n" +
                    $"Rocket Type: {data.DataGetter[0].Launches.ElementAt(index).Rocket.Type}\r\n" +
                    $"Rocket Mass: {data.DataGetter[0].Launches.ElementAt(index).Rocket.Mass}\r\n";

                using (var ms = new MemoryStream(data.DataGetter[0].Launches.ElementAt(index).Rocket.Image))
                {
                    var bmp = new Bitmap(Image.FromStream(ms), new Size(230, 210));
                    data.PictureBox.Image = bmp;
                }
            }
        }

        public override void Init()
        {
            IsActive = true;
            data.Panel.Visible = true;
            data.Panel.BringToFront();
        }

        public async override Task SetView<T>()
        {
            data.ListView.Items.Clear();
            if (typeof(T) == typeof(List<Launch>))
            {
                var result = await data.DataGetter[0].GetLaunchesAsync();
                AddLaunches(result);
            }
        }

        private void AddLaunches<T>(List<T> data)
        {
            var launches = data.Select(x => x as Launch).ToList();
            for (int i = 0; i < data.Count; ++i)
            {
                ListViewItem item = new ListViewItem(launches[i].FlightId.ToString());
                item.SubItems.Add(launches[i].Status.ToString());
                item.SubItems.Add(launches[i].Name);
                item.SubItems.Add(launches[i].Payloads.ToString());
                item.SubItems.Add(launches[i].RocketName);
                item.SubItems.Add(launches[i].Country);
                this.data.ListView.Items.Add(item);
            }
            if (this.data.ListView.SelectedItems.Count > 0)
                this.data.ListView.Items[0].Selected = true;
        }

        public override void Hide()
        {
            IsActive = false;
            data.Panel.Visible = false;
        }
    }
}
