namespace space_browser.Source.UI.Widgets
{
    public class PorpertiesPopup : Popup
    {
        private System.Windows.Forms.RichTextBox richTextBox1;

        public override IPayload IPayload { get => payload; }
        private Payload payload;

        public PorpertiesPopup(Payload payload) : base()
        {
            this.payload = payload;
            InitializeComponent();
            richTextBox1.Text = payload.Content;
        }

        public class Payload : IPayload
        {
            public Payload(string text)
            {
                Content = text;
            }

            public string Content { get; set; }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PorpertiesPopup));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.Location = new System.Drawing.Point(13, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(417, 351);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // PorpertiesPopup
            // 
            this.ClientSize = new System.Drawing.Size(442, 376);
            this.Controls.Add(this.richTextBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PorpertiesPopup";
            this.Text = "Entity Properties";
            this.ResumeLayout(false);

        }
    }
}
