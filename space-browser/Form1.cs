using SBDataLibrary.Models;
using SBDataLibrary.Server;
using space_browser.Source;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            panels.Add("Organiser", new OrganizerPanel(new OrganizerPanel.Organizer(
                new ServerDataController() ,panel2, listView2, 
                new List<ToolStripButton>() 
                { 
                    toolStrip2.Items[0] as ToolStripButton, 
                    toolStrip2.Items[2] as ToolStripButton, 
                    toolStrip2.Items[4] as ToolStripButton
                }, 
                new List<Button>() 
                {
                    button3,
                    button4,
                    button5,
                })));
            panels.Add("Browser", new BrowserPanel(new BrowserPanel.BrowserData( new BrowserDataController()
                ,panel1, listView1, richTextBox1, pictureBox1)));;

            dataController = new ServerDataController();

            listView1.ColumnWidthChanging += new ColumnWidthChangingEventHandler(ResizeColumn);
            listView1.DrawColumnHeader += DrawColumnHeader;
            listView1.DrawItem += DrawItem;
            listView1.DrawSubItem += DrawSubItem;

            listView2.ColumnWidthChanging += new ColumnWidthChangingEventHandler(ResizeColumn);
            listView2.DrawColumnHeader += DrawColumnHeader;
            listView2.DrawItem += DrawItem;
            listView2.DrawSubItem += DrawSubItem;
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
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                dataController.Add(panels["Browser"].Data.DataGetter.Launches.ElementAt(index));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        public override async void Refresh()
        {
            base.Refresh();
            listView1.Items.Clear();
            richTextBox1.Clear();
            pictureBox1.Image = null;
            await LoadForm();
        }

        private async void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (var elem in panels)
                elem.Value.Hide();
            panels[e.ClickedItem.Text].Init();
            panels[e.ClickedItem.Text].SetView<List<Launch>>();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
    