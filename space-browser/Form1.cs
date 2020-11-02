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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace space_browser
{
    public partial class Form1 : Form
    {
        private BrowserData browserData;
        private List<IRefreshable> refreshables;
        private DataController dataController;
        private Dictionary<string, Panel> panels = new Dictionary<string, Panel>();

        public Form1()
        {
            InitializeComponent();
            panels.Add("Browser", panel1);
            InitializeFields();
            this.listView1.ColumnWidthChanging += new ColumnWidthChangingEventHandler(ResizeColumn);
            this.listView1.DrawColumnHeader += DrawColumnHeader;
            this.listView1.DrawItem += DrawItem;
            this.listView1.DrawSubItem += DrawSubItem;
        }

        private void InitializeFields()
        {
            refreshables = new List<IRefreshable>();
            this.browserData = new BrowserData();
            this.dataController = new DataController();
            refreshables.Add(this.browserData);
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
            await LoadForm();
        }

        private async Task LoadForm()
        {
            await browserData.LoadData();
            for (int i = 0; i < browserData.Launches.Count; ++i)
            {
                ListViewItem item = new ListViewItem(browserData.Launches[i].FlightId.ToString());
                item.SubItems.Add(browserData.Launches[i].Status.ToString());
                item.SubItems.Add(browserData.Launches[i].Name);
                item.SubItems.Add(browserData.Launches[i].Payloads.ToString());
                item.SubItems.Add(browserData.Launches[i].RocketName);
                item.SubItems.Add(browserData.Launches[i].Country);
                listView1.Items.Add(item);
            }
            listView1.Items[0].Selected = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                this.richTextBox1.Text = $"Rocket Data:\r\n" +
                    $"Mission Name: {browserData.Launches[index].MissionName}\r\n" +
                    $"Launch Date: {browserData.Launches[index].LaunchDate}\r\n" +
                    $"Rocket ID: {browserData.Launches[index].Rocket.Id}\r\n" +
                    $"Rocket Type: {browserData.Launches[index].Rocket.Type}\r\n" +
                    $"Rocket Mass: {browserData.Launches[index].Rocket.Mass}\r\n";

                using (var ms = new MemoryStream(browserData.Launches[index].Rocket.Image))
                {               
                    var bmp = new Bitmap(Image.FromStream(ms), new Size(230, 210));
                    this.pictureBox1.Image = bmp;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                dataController.Add(browserData.Launches[index]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        public override async void Refresh()
        {
            base.Refresh();
            this.listView1.Items.Clear();
            this.richTextBox1.Clear();
            this.pictureBox1.Image = null;
            foreach (var refreshable in refreshables)
                refreshable.Refresh();
            await LoadForm();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            panels[e.ClickedItem.Text].Visible = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
    