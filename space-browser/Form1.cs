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
        private DataController dataController;
        private Dictionary<string, Source.PanelView> panels = new Dictionary<string, Source.PanelView>();

        public Form1()
        {
            InitializeComponent();
            panel1.Parent = this;
            panel2.Parent = this;
            toolStrip1.Parent = this;
            panels.Add("Organiser", new Source.OrganiserPanel(new Source.OrganiserPanel.OrganiserData(panel2, listView2, new List<Button>() { })));
            panels.Add("Browser", new Source.BrowserPanel(new Source.BrowserPanel.BrowserData(panel1, listView1, richTextBox1, pictureBox1)));
            InitializeFields();
            this.listView1.ColumnWidthChanging += new ColumnWidthChangingEventHandler(ResizeColumn);
            this.listView1.DrawColumnHeader += DrawColumnHeader;
            this.listView1.DrawItem += DrawItem;
            this.listView1.DrawSubItem += DrawSubItem;

            this.listView2.ColumnWidthChanging += new ColumnWidthChangingEventHandler(ResizeColumn);
            this.listView2.DrawColumnHeader += DrawColumnHeader;
            this.listView2.DrawItem += DrawItem;
            this.listView2.DrawSubItem += DrawSubItem;
        }

        private void InitializeFields()
        {
            this.browserData = new BrowserData();
            this.dataController = new DataController();
        }

        private void DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(SystemBrushes.Menu, e.Bounds);
            e.Graphics.DrawRectangle(SystemPens.GradientInactiveCaption,
                new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

            string text = panels.First(x=>x.Value.IsActive).Value.Data.ListView.Columns[e.ColumnIndex].Text;
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
            foreach (var elem in panels)
                elem.Value.Hide();
            panels["Browser"].Init();
            var result = await browserData.LoadData();
            panels["Browser"].SetView<Launch>(result);
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                richTextBox1.Text = $"Rocket Data:\r\n" +
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
            await LoadForm();
        }

        private async void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (var elem in panels)
                elem.Value.Hide();
            panels[e.ClickedItem.Text].Init();

            if (e.ClickedItem.Text.Equals("Browser"))
                panels[e.ClickedItem.Text].SetView<Launch>(browserData.Launches);
            if (e.ClickedItem.Text.Equals("Organiser"))
            {
                var result = await dataController.GetLaunches();
                panels[e.ClickedItem.Text].SetView<Launch>(result);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
    