using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SBDataLibrary.Models;
using SBDataLibrary.Server;
using space_browser.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser
{
    public partial class Form1 : Form
    {
        private DataController dataController;
        private Connection connection;

        public Form1(DataController dataController)
        {
            InitializeComponent();
            this.listView1.ColumnWidthChanging += new ColumnWidthChangingEventHandler(ResizeColumn);
            this.listView1.DrawColumnHeader += DrawColumnHeader;
            this.listView1.DrawItem += DrawItem;
            this.listView1.DrawSubItem += DrawSubItem;
            this.dataController = dataController;
            this.connection = new Connection(100000000);
        }

        private void DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(SystemBrushes.Menu, e.Bounds);
            e.Graphics.DrawRectangle(SystemPens.GradientInactiveCaption,
                new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

            string text = listView1.Columns[e.ColumnIndex].Text;
            TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                  | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, text, listView1.Font, e.Bounds, Color.Black, cFlag);
        }

        private void DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void ResizeColumn(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = this.listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private async void Browser_Load(object sender, EventArgs e)
        {
            var parsedResults = await connection.CreateGet("https://api.spacexdata.com/v3/launches");
            var content = parsedResults.Content.ReadAsStringAsync().Result;
            var parsedData = CollectData(content);
            for (int i = 0; i < parsedData.Count; ++i)
            {
                ListViewItem item = new ListViewItem(parsedData[i].Launch.Id.ToString());
                item.SubItems.Add(parsedData[i].Launch.Status.ToString());
                item.SubItems.Add(parsedData[i].Launch.Name);
                item.SubItems.Add(parsedData[i].Launch.Payloads.ToString());
                item.SubItems.Add(parsedData[i].Launch.RocketName);
                item.SubItems.Add(parsedData[i].Launch.Country);
                listView1.Items.Add(item);
            }
            this.listView1.Items[0].Selected = true;
        }

        private List<JSONData> CollectData(string launches)
        {
            return PopulateData("{ launches: " + launches + "}");
        }

        private List<JSONData> PopulateData(string launchesJSON)
        {
            List<JSONData> collectedData = new List<JSONData>();

            JObject launchesParsed = JObject.Parse(launchesJSON);

            for (int i = 0; i < launchesParsed["launches"].Count(); ++i)
            {
                Launch launchInfo = SetLaunchInfo(launchesParsed, i);
                collectedData.Add(new JSONData(launchInfo));
            }

            return collectedData;
        }

        private Launch SetLaunchInfo(JObject launchesJSON, int index)
        {
            return new Launch(
                            (int)launchesJSON["launches"][index]["flight_number"],
                            (State)(int)launchesJSON["launches"][index]["upcoming"],
                            (string)launchesJSON["launches"][index]["mission_name"],
                            launchesJSON["launches"][index]["rocket"]["second_stage"]["payloads"].Count(),
                            (string)launchesJSON["launches"][index]["rocket"]["rocket_name"],
                            (string)launchesJSON["launches"][index]["rocket"]["second_stage"]["payloads"][0]["nationality"]);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
    