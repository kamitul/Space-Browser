using SBDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace space_browser.Source
{
    class BrowserPanel : PanelView
    {
        public class BrowserData : PanelData
        {
            public System.Windows.Forms.RichTextBox RichTextBox;
            public System.Windows.Forms.PictureBox PictureBox;

            public BrowserData(System.Windows.Forms.Panel panel, System.Windows.Forms.ListView listView, RichTextBox textBox, PictureBox pictureBox) : base(panel, listView)
            {
                RichTextBox = textBox;
                PictureBox = pictureBox;
            }
        }

        private BrowserData data;

        public BrowserPanel(PanelData data) : base(data)
        {
            this.data = data as BrowserData;
            this.data.ListView.SelectedIndexChanged += ListView_SelectedIndexChanged;
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.data.ListView.SelectedItems.Count > 0)
            //{
            //    int index = this.Data.ListView.Items.IndexOf(this.Data.ListView.SelectedItems[0]);
            //    this.data.RichTextBox.Text = $"Rocket Data:\r\n" +
            //        $"Mission Name: {browserData.Launches[index].MissionName}\r\n" +
            //        $"Launch Date: {browserData.Launches[index].LaunchDate}\r\n" +
            //        $"Rocket ID: {browserData.Launches[index].Rocket.Id}\r\n" +
            //        $"Rocket Type: {browserData.Launches[index].Rocket.Type}\r\n" +
            //        $"Rocket Mass: {browserData.Launches[index].Rocket.Mass}\r\n";

            //    using (var ms = new MemoryStream(browserData.Launches[index].Rocket.Image))
            //    {
            //        var bmp = new Bitmap(Image.FromStream(ms), new Size(230, 210));
            //        this.pictureBox1.Image = bmp;
            //    }
            //}

        }

        public override void Init()
        {
            IsActive = true;
            this.data.Panel.Visible = true;
            this.data.Panel.BringToFront();
        }

        public override void SetView<T>(List<T> data)
        {
            this.data.ListView.Items.Clear();
            if (data.GetType() == typeof(List<Launch>))
            {
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
            this.data.Panel.Visible = false;
        }
    }
}
