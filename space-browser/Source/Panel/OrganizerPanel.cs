using SBDataLibrary.Models;
using SBDataLibrary.Server;
using space_browser.Source.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser.Source
{
    public class OrganizerPanel : PanelView
    {
        private enum OrganizerType
        {
            SHIP,
            ROCKET,
            LAUNCH
        }

        public class OrganizerData : PanelData
        {
            public System.Windows.Forms.ToolStripButton Launches;
            public System.Windows.Forms.ToolStripButton Ships;
            public System.Windows.Forms.ToolStripButton Rocket;

            public System.Windows.Forms.Button Edit;
            public System.Windows.Forms.Button Remove;
            public System.Windows.Forms.Button Properties;

            public OrganizerData(System.Windows.Forms.Form form, System.Windows.Forms.Panel panel, System.Windows.Forms.ListView listView, List<System.Windows.Forms.ToolStripButton> toolButtons, List<System.Windows.Forms.Button> buttons, params IDataController[] dataGetter) : base(form, panel, listView, dataGetter)
            {
                Launches = toolButtons.Find(x => x.Name.Equals("Launches"));
                Ships = toolButtons.Find(x => x.Name.Equals("Ships"));
                Rocket = toolButtons.Find(x => x.Name.Equals("Rockets"));

                Edit = buttons.Find(x => x.Name.Equals("EditButton"));
                Remove = buttons.Find(x => x.Name.Equals("DeleteButton"));
                Properties = buttons.Find(x => x.Name.Equals("PropertiesButton"));

            }
        }

        private OrganizerType type;
        private OrganizerData data;
        public override PanelData Data => data;

        public OrganizerPanel(PanelData data)
        {
            this.data = data as OrganizerData;
            this.data.Launches.Click += SwitchLaunchView;
            this.data.Rocket.Click += SwitchRocketView;
            this.data.Ships.Click += SwitchShipView;
            this.data.Properties.Click += ShowPopertiesPopup;
            this.data.Remove.Click += DeleteElementFromDB;
            this.data.Edit.Click += EditElementFromDB;
            this.data.ListView.ColumnClick += SortElements;
        }

        private void SortElements(object sender, ColumnClickEventArgs e)
        {
            int index = e.Column;
            switch (type)
            {
                case OrganizerType.LAUNCH:
                    var launches = this.data.DataGetter[0].Launches.ToList();
                    launches.Sort((p, q) => Compare(p, q, index));
                    AddLaunches(launches);
                    break;
                case OrganizerType.ROCKET:
                    var rockets = this.data.DataGetter[0].Rockets.ToList();
                    rockets.Sort((p, q) => Compare(p, q, index));
                    AddRockets(rockets);
                    break;
                case OrganizerType.SHIP:
                    var ships = this.data.DataGetter[0].Ships.ToList();
                    ships.Sort((p, q) => Compare(p, q, index));
                    AddShips(ships);
                    break;
            }
        }

        private int Compare(Entity p, Entity q, int index)
        {
            var first = p.GetType().GetProperties()[index].GetValue(p).ToString();
            var second = q.GetType().GetProperties()[index].GetValue(q).ToString();

            decimal num1, num2;

            if (decimal.TryParse(first, out num1) && decimal.TryParse(second, out num2))
            {
                return num1.CompareTo(num2);
            }
            else
            {
                return first.CompareTo(second);
            }
        }

        private void EditElementFromDB(object sender, EventArgs e)
        {
            if (data.ListView.SelectedItems.Count > 0)
            {
                int index = data.ListView.Items.IndexOf(data.ListView.SelectedItems[0]);
                EditPopup window = null;
                switch (type)
                {
                    case OrganizerType.LAUNCH:
                        window = new EditPopup(new EditPopup.Payload(data.DataGetter[0].Launches.ElementAt(index)));
                        window.OnElementEdited += EditLaunch;
                        break;
                    case OrganizerType.ROCKET:
                        window = new EditPopup(new EditPopup.Payload(data.DataGetter[0].Rockets.ElementAt(index)));
                        window.OnElementEdited += EditRocket;
                        break;
                    case OrganizerType.SHIP:
                        window = new EditPopup(new EditPopup.Payload(data.DataGetter[0].Ships.ElementAt(index)));
                        window.OnElementEdited += EditShip;
                        break;
                }
                if (window != null)
                    window.Show(); 
            }
        }

        private async void EditShip(Entity obj)
        {
            try
            {
                await data.DataGetter[0].UpdateShip(obj as Ship);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void EditRocket(Entity obj)
        {
            try
            {
                await data.DataGetter[0].UpdateRocket(obj as Rocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void EditLaunch(Entity obj)
        {
            try
            {
                await data.DataGetter[0].UpdateLaunch(obj as Launch);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteElementFromDB(object sender, EventArgs e)
        {
            if (data.ListView.SelectedItems.Count > 0)
            {
                int index = data.ListView.Items.IndexOf(data.ListView.SelectedItems[0]);
                switch (type)
                {
                    case OrganizerType.LAUNCH:
                        data.DataGetter[0].DeleteLaunch(data.DataGetter[0].Launches.ElementAt(index));
                        break;
                    case OrganizerType.ROCKET:
                        data.DataGetter[0].DeleteRocket(data.DataGetter[0].Rockets.ElementAt(index));
                        break;
                    case OrganizerType.SHIP:
                        data.DataGetter[0].DeleteShip(data.DataGetter[0].Ships.ElementAt(index));
                        break;
                }
            }
        }

        private void ShowPopertiesPopup(object sender, EventArgs e)
        {
            if (data.ListView.SelectedItems.Count > 0)
            {
                int index = data.ListView.Items.IndexOf(data.ListView.SelectedItems[0]);
                PorpertiesPopup window = null;
                switch (type)
                {
                    case OrganizerType.LAUNCH:
                        window = new PorpertiesPopup(new PorpertiesPopup.Payload(data.DataGetter[0].Launches.ElementAt(index).ToString()));
                        break;
                    case OrganizerType.ROCKET:
                        var rocket = data.DataGetter[0].Rockets.ElementAt(index);
                        window = new PorpertiesPopup(new PorpertiesPopup.Payload(rocket.ToString(), new PorpertiesPopup.Payload.Image(rocket.Image)));
                        break;
                    case OrganizerType.SHIP:
                        var ship = data.DataGetter[0].Ships.ElementAt(index);
                        window = new PorpertiesPopup(new PorpertiesPopup.Payload(ship.ToString(), new PorpertiesPopup.Payload.Image(ship.Image)));
                        break;
                }
                if (window != null)
                    window.Show();
            }
        }

        private async void SwitchLaunchView(object sender, EventArgs e)
        {
            data.ListView.Items.Clear();
            data.ListView.Columns.Clear();

            data.Form.Enabled = false;
            var loadingPopup = new ProcessingPopup(new ProcessingPopup.Payload("Connecting to DB", 100f, async () => await data.DataGetter[0].GetLaunchesAsync()));
            loadingPopup.FormClosed += (object sender, FormClosedEventArgs e) => { data.Form.Enabled = true; };
            var result = await loadingPopup.StartUpdating();
            if (result != null)
            {
                type = OrganizerType.LAUNCH;
                AddLaunches(result as List<Launch>);
            }
        }

        private async void SwitchRocketView(object sender, EventArgs e)
        {
            data.ListView.Items.Clear();
            data.ListView.Columns.Clear();

            data.Form.Enabled = false;
            data.Form.Enabled = false;
            var loadingPopup = new ProcessingPopup(new ProcessingPopup.Payload("Connecting to DB", 100f, async () => await data.DataGetter[0].GetRocketsAsync()));
            loadingPopup.FormClosed += (object sender, FormClosedEventArgs e) => { data.Form.Enabled = true; };
            var result = await loadingPopup.StartUpdating();
            if (result != null)
            {
                type = OrganizerType.ROCKET;
                AddRockets(result as List<Rocket>);
            }
        }

        private async void SwitchShipView(object sender, EventArgs e)
        {
            data.ListView.Items.Clear();
            data.ListView.Columns.Clear();

            data.Form.Enabled = false;
            data.Form.Enabled = false;
            var loadingPopup = new ProcessingPopup(new ProcessingPopup.Payload("Connecting to DB", 100f, async () => await data.DataGetter[0].GetShipsAsync()));
            loadingPopup.FormClosed += (object sender, FormClosedEventArgs e) => { data.Form.Enabled = true; };
            var result = await loadingPopup.StartUpdating();
            if (result != null)
            {
                type = OrganizerType.SHIP;
                AddShips(result as List<Ship>);
            }
        }

        public override void Init()
        {
            IsActive = true;
            data.Panel.Visible = true;
            data.Panel.BringToFront();
        }

        public async override Task SetView<T>()
        {
            if(typeof(T) == typeof(List<Launch>))
            {
                type = OrganizerType.LAUNCH;
                var data = await this.data.DataGetter[0].GetLaunchesAsync();
                AddLaunches(data);
            }
            else if (typeof(T) == typeof(List<Ship>))
            {
                type = OrganizerType.SHIP;
                var data = await this.data.DataGetter[0].GetShipsAsync();
                AddShips(data);
            }
            else if(typeof(T) == typeof(List<Rocket>))
            {
                var data = await this.data.DataGetter[0].GetRocketsAsync();
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
            this.data.ListView.Items.Clear();
            this.data.ListView.Columns.Clear();

            var rockets = data.Select(x => x as Rocket).ToList();
            PopulateColumns(typeof(Rocket));

            for (int i = 0; i < rockets.Count; ++i)
            {
                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(rockets[i].ID.ToString());
                item.SubItems.Add(rockets[i].RocketId.ToString());
                item.SubItems.Add(rockets[i].Name.ToString());
                item.SubItems.Add(rockets[i].Type);
                item.SubItems.Add(rockets[i].Country.ToString());
                item.SubItems.Add(rockets[i].Mass.ToString());
                item.SubItems.Add(rockets[i].Image.ToString());
                item.SubItems.Add(rockets[i].LaunchID != null ? rockets[i].LaunchID.ToString() : "----");
                item.SubItems.Add(rockets[i].Launch != null ? rockets[i].Launch.FlightId.ToString() : "----");
                this.data.ListView.Items.Add(item);
            }
            if(this.data.ListView.SelectedItems.Count > 0)
                this.data.ListView.Items[0].Selected = true;
        }

        private void AddShips<T>(List<T> data)
        {
            this.data.ListView.Items.Clear();
            this.data.ListView.Columns.Clear();

            var ships = data.Select(x => x as Ship).ToList();

            PopulateColumns(typeof(Ship));

            for (int i = 0; i < ships.Count; ++i)
            {
                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(ships[i].ID.ToString());
                item.SubItems.Add(ships[i].ShipId);
                item.SubItems.Add(ships[i].Name);
                item.SubItems.Add(ships[i].Type);
                item.SubItems.Add(ships[i].Missions.ToString());
                item.SubItems.Add(ships[i].HomePort);
                item.SubItems.Add(ships[i].Image.ToString());
                item.SubItems.Add(ships[i].Launch != null ? ships[i].Launch.FlightId.ToString() : "----");
                this.data.ListView.Items.Add(item);
            }
            if (this.data.ListView.SelectedItems.Count > 0)
                this.data.ListView.Items[0].Selected = true;
        }

        private void AddLaunches<T>(List<T> data)
        {
            this.data.ListView.Items.Clear();
            this.data.ListView.Columns.Clear();

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
                item.SubItems.Add(launches[i].Rocket != null ? launches[i].Rocket.ID.ToString() : "------");
                item.SubItems.Add(launches[i].Ships != null ? "[ " + string.Join(",", launches[i].Ships.Select(x=>x.ID)) + "]" : "------");
                this.data.ListView.Items.Add(item);
            }
            if (this.data.ListView.SelectedItems.Count > 0)
                this.data.ListView.Items[0].Selected = true;
        }

        private void PopulateColumns(Type type)
        {
            var fields = type.GetProperties().Select(x => x.Name).ToList();
            for (int i = 0; i < fields.Count; ++i)
            {
                data.ListView.Columns.Add(i.ToString(), fields[i], 100, System.Windows.Forms.HorizontalAlignment.Left, string.Empty);
            }
            data.ListView.View = System.Windows.Forms.View.Details;
        }
    }
}
