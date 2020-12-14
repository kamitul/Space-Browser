using SBDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace space_browser.Source.UI.Widgets
{
    /// <summary>
    /// Edit popup class
    /// </summary>
    public class EditPopup : Popup
    {
        private TableLayoutPanel tableLayoutPanel1;

        /// <summary>
        /// Payload property of data of this screen
        /// </summary>
        public override IPayload IPayload { get => payload; }

        /// <summary>
        /// Action when element is edited
        /// </summary>
        public Action<Entity> OnElementEdited;

        private Payload payload;
        private Button editButton;
        private Button cancelButton;
        private List<RichTextBox> textBoxes = new List<RichTextBox>();

        /// <summary>
        /// Edit popup constructor
        /// </summary>
        /// <param name="payload">Passed data to popup</param>
        public EditPopup(Payload payload) : base()
        {
            this.payload = payload;
            InitializeComponent();
        }

        /// <summary>
        /// Initiliazes edited entity
        /// </summary>
        /// <param name="payload">Passed payload data</param>
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

        /// <summary>
        /// Edit popup payload class
        /// </summary>
        public class Payload : IPayload
        {
            /// <summary>
            /// Edited entity info
            /// </summary>
            public string[] EntityInfo;
            /// <summary>
            /// Edited entity reference
            /// </summary>
            public Entity Entity;

            /// <summary>
            /// Constructs data payload
            /// </summary>
            /// <param name="entity">Edited entity</param>
            public Payload(Entity entity)
            {
                EntityInfo = entity.GetFields();
                Content = string.Join(string.Empty, entity.GetFields());
                Entity = entity;
            }

            /// <summary>
            /// Content property
            /// </summary>
            public string Content { get; set; }
        }

        /// <summary>
        /// Initializes poups
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPopup));
            tableLayoutPanel1 = new TableLayoutPanel();
            editButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Size = new Size(457, 409);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            editButton.Location = new Point(249, 427);
            editButton.Name = "button1";
            editButton.Size = new Size(220, 33);
            editButton.TabIndex = 1;
            editButton.Text = "Edit";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += new EventHandler(EditEntity);
            // 
            // button2
            // 
            cancelButton.Location = new Point(12, 427);
            cancelButton.Name = "button2";
            cancelButton.Size = new Size(220, 33);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += new EventHandler(CancelEdition);
            // 
            // EditPopup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size(481, 480);
            Controls.Add(cancelButton);
            Controls.Add(editButton);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = ((Icon)(resources.GetObject("$this.Icon")));
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditPopup";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Edit Entity";
            Load += new EventHandler(EditPopup_Load);
            ResumeLayout(false);

        }

        /// <summary>
        /// Called on popup loaded
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Passed arguments</param>
        private void EditPopup_Load(object sender, EventArgs e)
        {
            InitialzeEntity(payload);
        }

        /// <summary>
        /// Cancels edition of entity
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Passed arguments</param>
        private void CancelEdition(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Called after entity is edited
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Passed arguments</param>
        private void EditEntity(object sender, EventArgs e)
        {
            payload.Entity.Set(textBoxes.Select(x => x.Text).ToArray());
            OnElementEdited?.Invoke(payload.Entity);
            Close();
        }
    }
}
