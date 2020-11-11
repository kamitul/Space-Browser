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
        private Button editButton;
        private Button cancelButton;
        private List<RichTextBox> textBoxes = new List<RichTextBox>();

        public EditPopup(Payload payload) : base()
        {
            this.payload = payload;
            InitializeComponent();
        }

        private void InitialzeEntity(Payload payload)
        {
            foreach (string fi in payload.EntityInfo)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            for (int i = 0; i < payload.EntityInfo.Length; ++i)
            {
                Label label = new Label();
                label.Text = payload.EntityInfo[i];
                RichTextBox richTextBox = new RichTextBox();
                richTextBox.Name = payload.EntityInfo[i];
                richTextBox.Size = new Size(190, 40);
                richTextBox.Margin = new Padding(3, 3, 3, 3);
                textBoxes.Add(richTextBox);
                tableLayoutPanel1.Controls.Add(label, 0, i);
                tableLayoutPanel1.Controls.Add(richTextBox, 1, i);
            }
        }

        public class Payload : IPayload
        {
            public string[] EntityInfo;
            public Entity Entity;

            public Payload(Entity entity)
            {
                EntityInfo = entity.GetFields();
                Content = string.Join(string.Empty, entity.GetFields());
                Entity = entity;
            }

            public string Content { get; set; }
        }


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPopup));
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            editButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Size = new System.Drawing.Size(457, 409);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            editButton.Location = new System.Drawing.Point(249, 427);
            editButton.Name = "button1";
            editButton.Size = new System.Drawing.Size(220, 33);
            editButton.TabIndex = 1;
            editButton.Text = "Edit";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += new System.EventHandler(EditEntity);
            // 
            // button2
            // 
            cancelButton.Location = new System.Drawing.Point(12, 427);
            cancelButton.Name = "button2";
            cancelButton.Size = new System.Drawing.Size(220, 33);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += new System.EventHandler(CancelEdition);
            // 
            // EditPopup
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(481, 480);
            Controls.Add(cancelButton);
            Controls.Add(editButton);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditPopup";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Edit Entity";
            Load += new System.EventHandler(EditPopup_Load);
            ResumeLayout(false);

        }

        private void EditPopup_Load(object sender, EventArgs e)
        {
            InitialzeEntity(payload);
        }

        private void CancelEdition(object sender, EventArgs e)
        {
            Close();
        }

        private void EditEntity(object sender, EventArgs e)
        {
            payload.Entity.Set(textBoxes.Select(x => x.Text).ToArray());
            OnElementEdited?.Invoke(payload.Entity);
            Close();
        }
    }
}
