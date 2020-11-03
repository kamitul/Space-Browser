using SBDataLibrary.Models;
using SBDataLibrary.Server;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace space_browser.Source
{
    public class OrganizerPanel : PanelView
    {
        public class Organizer : PanelData
        {
            public System.Windows.Forms.ToolStripButton Launches;
            public System.Windows.Forms.ToolStripButton Ships;
            public System.Windows.Forms.ToolStripButton Rocket;

            public System.Windows.Forms.Button Edit;
            public System.Windows.Forms.Button Remove;
            public System.Windows.Forms.Button Properties;

            public Organizer(IDataGetter dataGetter, System.Windows.Forms.Panel panel, System.Windows.Forms.ListView listView, List<System.Windows.Forms.ToolStripButton> toolButtons, List<System.Windows.Forms.Button> buttons) : base(panel, listView, dataGetter)
            {
                Panel = panel;
                ListView = listView;
                Launches = toolButtons.Find(x => x.Name.Equals("Launches"));
                Ships = toolButtons.Find(x => x.Name.Equals("Ships"));
                Rocket = toolButtons.Find(x => x.Name.Equals("Rockets"));

                Edit = buttons.Find(x => x.Name.Equals("Edit"));
                Remove = buttons.Find(x => x.Name.Equals("Remove"));
                Properties = buttons.Find(x => x.Name.Equals("Properties"));

            }
        }

        private Organizer data;
        public override PanelData Data => data;

        public OrganizerPanel(PanelData data)
        {
            this.data = data as Organizer;
            this.data.Launches.Click += SwitchLaunchView;
            this.data.Rocket.Click += SwitchRocketView;
            this.data.Ships.Click += SwitchShipView;
        }

        private async void SwitchLaunchView(object sender, EventArgs e)
        {
            await SetView<List<Launch>>();
        }

        private async void SwitchRocketView(object sender, EventArgs e)
        {
            await SetView<List<Rocket>>();
        }

        private async void SwitchShipView(object sender, EventArgs e)
        {
            await SetView<List<Ship>>();
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
            data.ListView.Columns.Clear();

            if(typeof(T) == typeof(List<Launch>))
            {
                var data = await this.data.DataGetter.GetLaunchesAsync();
                AddLaunches(data);
            }
            else if (typeof(T) == typeof(List<Ship>))
            {
                var data = await this.data.DataGetter.GetShipsAsync();
                AddShips(data);
            }
            else if(typeof(T) == typeof(List<Rocket>))
            {
                var data = await this.data.DataGetter.GetRocketsAsync();
                AddRockets(data);
            }
        }

        public override void Hide()
        {
            IsActive = false;
            data.Panel.Visible = false;
        }

        private void AddRockets<T>(List<T> data)
        {
            var launches = data.Select(x => x as Rocket).ToList();
            PopulateColumns(typeof(Rocket));

            for (int i = 0; i < launches.Count; ++i)
            {
                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(launches[i].LaunchId.ToString());
                item.SubItems.Add(launches[i].RocketId.ToString());
                item.SubItems.Add(launches[i].Name.ToString());
                item.SubItems.Add(launches[i].Type);
                item.SubItems.Add(launches[i].Country.ToString());
                item.SubItems.Add(launches[i].Mass.ToString());
                //Add Image
                this.data.ListView.Items.Add(item);
            }
            if(this.data.ListView.SelectedItems.Count > 0)
                this.data.ListView.Items[0].Selected = true;
        }

        private void AddShips<T>(List<T> data)
        {
            var launches = data.Select(x => x as Ship).ToList();

            PopulateColumns(typeof(Ship));

            for (int i = 0; i < launches.Count; ++i)
            {
                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(launches[i].LaunchId.ToString());
                item.SubItems.Add(launches[i].ShipId.ToString());
                item.SubItems.Add(launches[i].Name);
                item.SubItems.Add(launches[i].Type);
                item.SubItems.Add(launches[i].Missions.ToString());
                item.SubItems.Add(launches[i].HomePort);
                //Add image
                this.data.ListView.Items.Add(item);
            }
            if (this.data.ListView.SelectedItems.Count > 0)
                this.data.ListView.Items[0].Selected = true;
        }

        private void AddLaunches<T>(List<T> data)
        {
            var launches = data.Select(x => x as Launch).ToList();
            PopulateColumns(typeof(Launch));

            for (int i = 0; i < launches.Count; ++i)
            {
                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(launches[i].FlightId.ToString());
                item.SubItems.Add(launches[i].Status.ToString());
                item.SubItems.Add(launches[i].Name);
                item.SubItems.Add(launches[i].Payloads.ToString());
                item.SubItems.Add(launches[i].RocketName);
                item.SubItems.Add(launches[i].Country);
                item.SubItems.Add(launches[i].LaunchDate.ToString());
                item.SubItems.Add(launches[i].MissionName);
                item.SubItems.Add(string.Empty);
                item.SubItems.Add(string.Empty);
                this.data.ListView.Items.Add(item);
            }
            if (this.data.ListView.SelectedItems.Count > 0)
                this.data.ListView.Items[0].Selected = true;
        }

        private void PopulateColumns(Type type)
        {
            var fields = type.GetProperties().Select(x => x.Name).ToList();
            for (int i = 1; i < fields.Count; ++i)
            {
                data.ListView.Columns.Add(i.ToString(), fields[i], 100, System.Windows.Forms.HorizontalAlignment.Left, string.Empty);
            }
            data.ListView.View = System.Windows.Forms.View.Details;
        }
    }
}
