using SBDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace space_browser.Source.UI.Widgets
{
    public class EditPopup : Popup
    {
        private TableLayoutPanel tableLayoutPanel1;

        public override IPayload IPayload { get => payload; }
        public Action<Entity> OnElementEdited;

        private Payload payload;
        private Button button1;
        private Button button2;
        private List<RichTextBox> textBoxes = new List<RichTextBox>();

        public EditPopup(Payload payload) : base()
        {
            this.payload = payload;
            InitializeComponent();
        }

        private void InitialzeEntity(Payload payload)
        {
            foreach (string fi in payload.Content)
            {
                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            for (int i = 0; i < payload.Content.Length; ++i)
            {
                Label label = new Label();
                label.Text = payload.Content[i];
                RichTextBox richTextBox = new RichTextBox();
                richTextBox.Name = payload.Content[i];
                richTextBox.Size = new Size(190, 40);
                richTextBox.Margin = new Padding(3, 3, 3, 3);
                textBoxes.Add(richTextBox);
                this.tableLayoutPanel1.Controls.Add(label, 0, i);
                this.tableLayoutPanel1.Controls.Add(richTextBox, 1, i);
            }
        }

        public class Payload : IPayload
        {
            public string[] Content;
            public Entity Entity;

            public Payload(Entity entity)
            {
                Content = entity.GetFields();
                Entity = entity;
            }
        }


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPopup));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Size = new System.Drawing.Size(457, 409);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 427);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 427);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(220, 33);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // EditPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 480);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Entity";
            this.Load += new System.EventHandler(this.EditPopup_Load);
            this.ResumeLayout(false);

        }

        private void EditPopup_Load(object sender, EventArgs e)
        {
            InitialzeEntity(payload);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            payload.Entity.Set(textBoxes.Select(x => x.Text).ToArray());
            OnElementEdited?.Invoke(payload.Entity);
            this.Close();
        }
    }
}
