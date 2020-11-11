using SBDataLibrary.Models;
using SBDataLibrary.Server;
using space_browser.Source;
using space_browser.Source.Panel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser
{
    public partial class Form1 : Form
    {
        private ServerDataController dataController;
        private Dictionary<string, PanelView> panels = new Dictionary<string, PanelView>();

        public Form1()
        {
            InitializeComponent();
            panel1.Parent = this;
            panel2.Parent = this;
            toolStrip1.Parent = this;
            AddPanels();

            dataController = new ServerDataController();

            listView1.ColumnWidthChanging += new ColumnWidthChangingEventHandler((object sender, ColumnWidthChangingEventArgs e) => ResizeColumn(listView1, e));
            listView1.DrawColumnHeader += DrawColumnHeader;
            listView1.DrawItem += DrawItem;
            listView1.DrawSubItem += DrawSubItem;

            listView2.ColumnWidthChanging += new ColumnWidthChangingEventHandler((object sender, ColumnWidthChangingEventArgs e) => ResizeColumn(listView2, e));
            listView2.DrawColumnHeader += DrawColumnHeader;
            listView2.DrawItem += DrawItem;
            listView2.DrawSubItem += DrawSubItem;

            SaveButton.Click += SaveToFile;
        }

        private async void SaveToFile(object sender, EventArgs e)
        {
            await (panels["File"] as FilePanel).Save();
        }

        private void AddPanels()
        {
            panels.Add("Organizer", new OrganizerPanel(
                new OrganizerPanel.OrganizerData(
                this, panel2, listView2,
                new List<ToolStripButton>()
                {
                    toolStrip2.Items[0] as ToolStripButton,
                    toolStrip2.Items[2] as ToolStripButton,
                    toolStrip2.Items[4] as ToolStripButton
                },
                new List<Button>()
                {
                    EditButton,
                    PropertiesButton,
                    DeleteButton,
                },
                new ServerDataController())));
            panels.Add("Browser", new BrowserPanel(
                new BrowserPanel.BrowserData(
                this, panel1, listView1,
                richTextBox1,
                pictureBox1,
                new List<Button>()
                {
                    AddButton,
                    RefreshButton
                },
                new BrowserDataController(),
                new ServerDataController())));
            panels.Add("File", new FilePanel(
                new FilePanel.FileData(
                    null,
                    null,
                    null,
                    new ServerDataController())));
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

        private void ResizeColumn(ListView listview, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listview.Columns[e.ColumnIndex].Width;
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
            await panels["Browser"].SetView<List<Launch>>();
        }

        private async void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text != "File")
            {
                foreach (var elem in panels)
                    elem.Value.Hide();
                panels[e.ClickedItem.Text].Init();
                panels[e.ClickedItem.Text].SetView<List<Launch>>();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
    