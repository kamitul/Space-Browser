using SBDataLibrary.Models;
using SBDataLibrary.Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace space_browser.Source
{
    class BrowserPanel : PanelView
    {
        public class BrowserData : PanelData
        {
            public RichTextBox RichTextBox;
            public PictureBox PictureBox;

            public BrowserData(IDataGetter dataGetter, Panel panel, ListView listView, RichTextBox textBox, PictureBox pictureBox) : base(panel, listView, dataGetter)
            {
                RichTextBox = textBox;
                PictureBox = pictureBox;
            }

        }

        private BrowserData data;

        public BrowserPanel(PanelData data) : base(data)
        {
            this.data = data as BrowserData;
            this.data.ListView.SelectedIndexChanged += SelectedIndexChangedEvent;
        }

        private void SelectedIndexChangedEvent(object sender, EventArgs e)
        {
            if (data.ListView.SelectedItems.Count > 0)
            {
                int index = Data.ListView.Items.IndexOf(Data.ListView.SelectedItems[0]);
                data.RichTextBox.Text = $"Rocket Data:\r\n" +
                    $"Mission Name: {Data.DataGetter.Launches.ElementAt(index).MissionName}\r\n" +
                    $"Launch Date: {Data.DataGetter.Launches.ElementAt(index).LaunchDate}\r\n" +
                    $"Rocket ID: {Data.DataGetter.Launches.ElementAt(index).Rocket.Id}\r\n" +
                    $"Rocket Type: {Data.DataGetter.Launches.ElementAt(index).Rocket.Type}\r\n" +
                    $"Rocket Mass: {Data.DataGetter.Launches.ElementAt(index).Rocket.Mass}\r\n";

                using (var ms = new MemoryStream(Data.DataGetter.Launches.ElementAt(index).Rocket.Image))
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

        public async override void SetView<T>()
        {
            data.ListView.Items.Clear();
            if (typeof(T) == typeof(List<Launch>))
            {
                var data = await Data.DataGetter.GetLaunchesAsync();
                AddLaunches(data);
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
            this.data.ListView.Items[0].Selected = true;
        }

        public override void Hide()
        {
            IsActive = false;
            data.Panel.Visible = false;
        }
    }
}
