using System.Windows.Forms;

namespace space_browser
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.File = new System.Windows.Forms.ToolStripDropDownButton();
            this.Browser = new System.Windows.Forms.ToolStripButton();
            this.Organiser = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.Status = new System.Windows.Forms.ColumnHeader();
            this.MissionName = new System.Windows.Forms.ColumnHeader();
            this.Payloads = new System.Windows.Forms.ColumnHeader();
            this.Rocket = new System.Windows.Forms.ColumnHeader();
            this.Country = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.EditButton = new System.Windows.Forms.Button();
            this.PropertiesButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.Launches = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Ships = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Rockets = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File,
            this.Browser,
            this.Organiser});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // File
            // 
            this.File.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.File.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(38, 22);
            this.File.Text = "File";
            // 
            // Browser
            // 
            this.Browser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Browser.Image = ((System.Drawing.Image)(resources.GetObject("Browser.Image")));
            this.Browser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Browser.Name = "Browser";
            this.Browser.Size = new System.Drawing.Size(53, 22);
            this.Browser.Text = "Browser";
            // 
            // Organiser
            // 
            this.Organiser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Organiser.Image = ((System.Drawing.Image)(resources.GetObject("Organiser.Image")));
            this.Organiser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Organiser.Name = "Organiser";
            this.Organiser.Size = new System.Drawing.Size(62, 22);
            this.Organiser.Text = "Organizer";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 424);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Status,
            this.MissionName,
            this.Payloads,
            this.Rocket,
            this.Country});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 8);
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.Size = new System.Drawing.Size(551, 400);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 28;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            // 
            // MissionName
            // 
            this.MissionName.Text = "Mission Name";
            this.MissionName.Width = 155;
            // 
            // Payloads
            // 
            this.Payloads.Text = "Payloads";
            // 
            // Rocket
            // 
            this.Rocket.Text = "Rocket Name";
            this.Rocket.Width = 155;
            // 
            // Country
            // 
            this.Country.Text = "Country";
            this.Country.Width = 55;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(563, 376);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(237, 35);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(121, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(569, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 365);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Launch:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.64543F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.35457F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(215, 337);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, 163);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(209, 171);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(209, 154);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Controls.Add(this.listView2);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 424);
            this.panel2.TabIndex = 5;
            this.panel2.Visible = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.EditButton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.PropertiesButton, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.DeleteButton, 0, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(640, 25);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(156, 183);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // EditButton
            // 
            this.EditButton.Location = new System.Drawing.Point(3, 3);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(150, 55);
            this.EditButton.TabIndex = 0;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            // 
            // PropertiesButton
            // 
            this.PropertiesButton.Location = new System.Drawing.Point(3, 64);
            this.PropertiesButton.Name = "PropertiesButton";
            this.PropertiesButton.Size = new System.Drawing.Size(150, 55);
            this.PropertiesButton.TabIndex = 1;
            this.PropertiesButton.Text = "Properties";
            this.PropertiesButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(3, 125);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(150, 55);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(12, 25);
            this.listView2.Name = "listView2";
            this.listView2.OwnerDraw = true;
            this.listView2.Size = new System.Drawing.Size(622, 383);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Launches,
            this.toolStripSeparator1,
            this.Ships,
            this.toolStripSeparator2,
            this.Rockets});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(800, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // Launches
            // 
            this.Launches.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Launches.Image = ((System.Drawing.Image)(resources.GetObject("Launches.Image")));
            this.Launches.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Launches.Name = "Launches";
            this.Launches.Size = new System.Drawing.Size(23, 22);
            this.Launches.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Ships
            // 
            this.Ships.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Ships.Image = ((System.Drawing.Image)(resources.GetObject("Ships.Image")));
            this.Ships.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Ships.Name = "Ships";
            this.Ships.Size = new System.Drawing.Size(23, 22);
            this.Ships.Text = "toolStripButton2";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // Rockets
            // 
            this.Rockets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Rockets.Image = ((System.Drawing.Image)(resources.GetObject("Rockets.Image")));
            this.Rockets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Rockets.Name = "Rockets";
            this.Rockets.Size = new System.Drawing.Size(23, 22);
            this.Rockets.Text = "toolStripButton3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpaceX Browser";
            this.Load += new System.EventHandler(this.Browser_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton File;
        private System.Windows.Forms.ToolStripButton Organiser;
        private ToolStripButton Browser;
        private Panel panel1;
        private ListView listView1;
        private ColumnHeader ID;
        private ColumnHeader Status;
        private ColumnHeader MissionName;
        private ColumnHeader Payloads;
        private ColumnHeader Rocket;
        private ColumnHeader Country;
        private TableLayoutPanel tableLayoutPanel2;
        private Button button1;
        private Button button2;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private RichTextBox richTextBox1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Button EditButton;
        private Button PropertiesButton;
        private Button DeleteButton;
        private ListView listView2;
        private ToolStrip toolStrip2;
        private ToolStripButton Launches;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton Ships;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton Rockets;
    }
}

