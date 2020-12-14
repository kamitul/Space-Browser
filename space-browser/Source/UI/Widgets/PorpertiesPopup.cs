using System.Drawing;
using System.IO;

namespace space_browser.Source.UI.Widgets
{
    /// <summary>
    /// Popup with properties of entinty screen
    /// </summary>
    public class PorpertiesPopup : Popup
    {
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;

        /// <summary>
        /// Property of data payload of the screen
        /// </summary>
        public override IPayload IPayload { get => payload; }
        private Payload payload;

        /// <summary>
        /// Constructs popup with specific payload
        /// </summary>
        /// <param name="payload">Payload data passed</param>
        public PorpertiesPopup(Payload payload) : base()
        {
            this.payload = payload;
            InitializeComponent();
            richTextBox1.Text = payload.Content;
            if (payload.Images.Length > 0 )
            {
                using (var ms = new MemoryStream(payload.Images[0].img))
                {
                    var bmp = new Bitmap(Image.FromStream(ms), new Size(218, 153));
                    pictureBox1.Image = bmp;
                }
            }
            else
            {
                groupBox1.Hide();
                pictureBox1.Hide();
            }
        }

        /// <summary>
        /// Properties popup data
        /// </summary>
        public class Payload : IPayload
        {
            /// <summary>
            /// Image of payload
            /// </summary>
            public class Image
            {
                public byte[] img;

                /// <summary>
                /// Constructs image
                /// </summary>
                /// <param name="img">Byte array of image</param>
                public Image(byte[] img)
                {
                    this.img = img;
                }
            }

            /// <summary>
            /// Images of payload
            /// </summary>
            public Image[] Images;

            /// <summary>
            /// Constructs payload
            /// </summary>
            /// <param name="text">Text of payload</param>
            /// <param name="images">Payload images</param>
            public Payload(string text, params Image[] images)
            {
                Images = images;
                Content = text;
            }

            public string Content { get; set; }
        }

        /// <summary>
        /// Initializes components
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PorpertiesPopup));
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            richTextBox1.Location = new Point(13, 13);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(417, 351);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(6, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(218, 153);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Location = new Point(199, 184);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(230, 180);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Image";
            // 
            // PorpertiesPopup
            // 
            ClientSize = new Size(442, 376);
            Controls.Add(groupBox1);
            Controls.Add(richTextBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = ((Icon)(resources.GetObject("$this.Icon")));
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PorpertiesPopup";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Entity Properties";
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);

        }
    }
}
