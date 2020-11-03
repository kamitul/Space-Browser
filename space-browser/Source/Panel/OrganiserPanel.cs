using SBDataLibrary.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace space_browser.Source
{
    public class OrganiserPanel : PanelView
    {
        public class OrganiserData : PanelData
        {
            public System.Windows.Forms.Button Launches;
            public System.Windows.Forms.Button Ships;
            public System.Windows.Forms.Button Rocket;

            public System.Windows.Forms.Button Edit;
            public System.Windows.Forms.Button Remove;
            public System.Windows.Forms.Button Properties;

            public OrganiserData(System.Windows.Forms.Panel panel, System.Windows.Forms.ListView listView, List<System.Windows.Forms.Button> buttons) : base(panel, listView)
            {
                Panel = panel;
                ListView = listView;
                Launches = buttons.Find(x => x.Text.Equals("Launches"));
                Ships = buttons.Find(x => x.Text.Equals("Ships"));
                Rocket = buttons.Find(x => x.Text.Equals("Rockets"));

                Edit = buttons.Find(x => x.Text.Equals("Edit"));
                Remove = buttons.Find(x => x.Text.Equals("Remove"));
                Properties = buttons.Find(x => x.Text.Equals("Properties"));
            }
        }

        private OrganiserData data;

        public OrganiserPanel(PanelData data) : base(data)
        {
            this.data = data as OrganiserData;
        }

        public override void Init()
        {
            IsActive = true;
            data.Panel.Visible = true;
            data.Panel.BringToFront();
        }

        public override void SetView<T>(List<T> data)
        {
            this.data.ListView.Items.Clear();
            this.data.ListView.Columns.Clear();

            if(data.GetType() == typeof(List<Launch>))
            {
                AddLaunches(data);
            }
            else if (data.GetType() == typeof(List<Ship>))
            {
                AddShips(data);
            }
            else if(data.GetType() == typeof(List<Rocket>))
            {
                AddRockets(data);
            }
        }

        public override void Hide()
        {
            IsActive = false;
            this.data.Panel.Visible = false;
        }

        private void AddRockets<T>(List<T> data)
        {
            var launches = data.Select(x => x as Rocket).ToList();
            PopulateColumns(typeof(Rocket));

            for (int i = 0; i < launches.Count; ++i)
            {
                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(launches[i].RocketId.ToString());
                item.SubItems.Add(launches[i].Name.ToString());
                item.SubItems.Add(launches[i].Type);
                item.SubItems.Add(launches[i].Mass.ToString());
                //Add Image
                this.data.ListView.Items.Add(item);
            }
            this.data.ListView.Items[0].Selected = true;
        }

        private void AddShips<T>(List<T> data)
        {
            var launches = data.Select(x => x as Ship).ToList();

            PopulateColumns(typeof(Ship));

            for (int i = 0; i < launches.Count; ++i)
            {
                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(launches[i].ShipId.ToString());
                item.SubItems.Add(launches[i].Name.ToString());
                item.SubItems.Add(launches[i].HomePort);
                item.SubItems.Add(launches[i].Missions.ToString());
                item.SubItems.Add(launches[i].Type);
                //Add image
                this.data.ListView.Items.Add(item);
            }
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
            this.data.ListView.Items[0].Selected = true;
        }

        private void PopulateColumns(Type type)
        {
            var fields = type.GetProperties().Select(x => x.Name).ToList();
            for (int i = 1; i < fields.Count; ++i)
            {
                this.data.ListView.Columns.Add(i.ToString(), fields[i], 100, System.Windows.Forms.HorizontalAlignment.Left, string.Empty);
            }
            this.data.ListView.View = System.Windows.Forms.View.Details;
        }
    }
}
