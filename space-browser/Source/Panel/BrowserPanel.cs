using SBDataLibrary.Models;
using SBDataLibrary.Server;
using space_browser.Source.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser.Source
{
    /// <summary>
    /// Browser panel class
    /// </summary>
    class BrowserPanel : PanelView
    {
        /// <summary>
        /// Browser data class
        /// </summary>
        public class BrowserData : PanelData
        {
            public RichTextBox RichTextBox;
            public PictureBox PictureBox;

            public Button AddButton;
            public Button RefreshButton;

            public BrowserData(Form form, System.Windows.Forms.Panel panel, ListView listView, RichTextBox textBox, PictureBox pictureBox, List<Button> buttons, params IDataController[] dataGetter) : base(form, panel, listView, dataGetter)
            {
                RichTextBox = textBox;
                PictureBox = pictureBox;

                AddButton = buttons.Find(x => x.Name.Equals("AddButton"));
                RefreshButton = buttons.Find(x => x.Name.Equals("RefreshButton"));
            }

        }

        private BrowserData data;
        public override PanelData Data => data;

        public BrowserPanel(PanelData data)
        {
            this.data = data as BrowserData;
            this.data.ListView.SelectedIndexChanged += SelectedIndexChangedEvent;

            this.data.AddButton.Click += AddElementToDB;
            this.data.RefreshButton.Click += RefreshPanel;
            this.data.ListView.ColumnClick += SortElements;
        }

        private void SortElements(object sender, ColumnClickEventArgs e)
        {
            int index = e.Column;
            data.DataGetter[0].Launches.Sort((p,q) => Compare(p, q, index));
            AddLaunches(data.DataGetter[0].Launches);
        }

        private int Compare(Launch p, Launch q, int index)
        {
            var first = p.GetType().GetProperties()[index].GetValue(p).ToString();
            var second = q.GetType().GetProperties()[index].GetValue(q).ToString();

            decimal num1, num2;

            if(decimal.TryParse(first, out num1) && decimal.TryParse(second, out num2))
            {
                return num1.CompareTo(num2);
            }
            else
            {
                return first.CompareTo(second);
            }
        }

        private async void RefreshPanel(object sender, EventArgs e)
        {
            await Refresh();
        }

        public async Task Refresh()
        {
            data.Form.Refresh();
            data.ListView.Items.Clear();
            data.RichTextBox.Clear();
            data.PictureBox.Image = null;
            Init();
            await SetView<List<Launch>>();
        }


        private async void AddElementToDB(object sender, EventArgs e)
        {
            if (data.ListView.SelectedItems.Count > 0)
            {
                int index = data.ListView.Items.IndexOf(data.ListView.SelectedItems[0]);
                var launch = data.DataGetter[0].Launches.ElementAt(index);
                var serverController = data.DataGetter[1] as ServerDataController;

                try
                {
                    var processingPopup = new ProcessingPopup(new ProcessingPopup.Payload("Adding to database", 100f, async () => await serverController.Add(launch)));
                    await processingPopup.StartUpdating();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            data.ListView.Items.Clear();
            data.PictureBox.Image = null;
            data.RichTextBox.Text = string.Empty;
        }

        public async override Task SetView<T>()
        {
            if (typeof(T) == typeof(List<Launch>))
            {
                data.Form.Enabled = false;
                var loadingPopup = new ProcessingPopup(new ProcessingPopup.Payload("Connecting to SpaceX API", 100f, async () => await data.DataGetter[0].GetLaunchesAsync()));
                loadingPopup.FormClosed += (object sender, FormClosedEventArgs e) => { data.Form.Enabled = true; };
                var result = await loadingPopup.StartUpdating();
                if(result != null)
                    AddLaunches(result as List<Launch>);
            }
        }

        private void AddLaunches<T>(List<T> data)
        {
            this.data.ListView.Items.Clear();

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
