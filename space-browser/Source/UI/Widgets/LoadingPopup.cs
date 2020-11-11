using SBDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser.Source.UI.Widgets
{
    public partial class LoadingPopup : Popup
    {
        public LoadingPopup()
        {
            InitializeComponent();
        }

        public override IPayload IPayload { get => payload; }
        private Payload payload;
        private CancellationTokenSource cts;

        public LoadingPopup(Payload payload) : base()
        {
            this.payload = payload;
            InitializeComponent();
            cts = new CancellationTokenSource();

            this.FormClosed += CancelLoading;
        }

        private void CancelLoading(object sender, FormClosedEventArgs e)
        {
            cts.Cancel();
        }

        public class Payload : IPayload
        {
            public string[] Content { get; set; }
            public int Index { get; set; }
            public float Frequency { get; set; }
            public List<Task<dynamic>> Tasks { get; set; }
            public Payload(string text, float frequency, params Func<Task<dynamic>>[] tasks)
            {
                Content = new string[] { text + ".", text + "..", text + "..." };
                Index = 0;
                Frequency = frequency;
                Tasks = new List<Task<dynamic>>();
                Tasks.Add(Task.Run(() => tasks[0].Invoke()));         
            }
        }

        public async Task<dynamic> StartUpdating()
        {
            this.Show();
            var result = await Task.WhenAny(LoadingTask(), payload.Tasks[0]);
            this.Close();
            return await result;
        }

        private async Task<dynamic> LoadingTask()
        {
            while (!cts.IsCancellationRequested)
            {
                richTextBox1.Text = payload.Content[payload.Index++ % payload.Content.Length];
                await Task.Delay((int)payload.Frequency);
            }
            return null;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingPopup));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(236, 36);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(230, 30);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "Loading...";
            // 
            // LoadingPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 61);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadingPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loading";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal void EndUpdating()
        {
            throw new NotImplementedException();
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}
