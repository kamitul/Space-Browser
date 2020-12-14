using SBDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser.Source.UI.Widgets
{
    /// <summary>
    /// Processing data popup class
    /// </summary>
    public partial class ProcessingPopup : Popup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProcessingPopup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Property for payload data of the screen
        /// </summary>
        public override IPayload IPayload { get => payload; }
        /// <summary>
        /// Payload data field
        /// </summary>
        private Payload payload;
        /// <summary>
        /// Cancellation token for asynchhrounous methods to stop them
        /// </summary>
        private CancellationTokenSource cts;

        /// <summary>
        /// Constructor with passed payload data
        /// </summary>
        /// <param name="payload">Passed payload data</param>
        public ProcessingPopup(Payload payload) : base()
        {
            this.payload = payload;
            InitializeComponent();
            cts = new CancellationTokenSource();

            FormClosed += CancelLoading;
        }

        /// <summary>
        /// Cancel loading popup
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e"> events passed</param>
        private void CancelLoading(object sender, FormClosedEventArgs e)
        {
            cts.Cancel();
        }

        /// <summary>
        /// Payload data of processing popup screen
        /// </summary>
        public class Payload : IPayload
        {
            /// <summary>
            /// Content of payload
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// Loading string elements to display while loading
            /// </summary>
            public string[] LoadingElements { get; set; }
            /// <summary>
            /// Index of current string element
            /// </summary>
            public int Index { get; set; }
            /// <summary>
            /// Frequency of information displayed progress
            /// </summary>
            public float Frequency { get; set; }
            /// <summary>
            /// Container for asynchrounus tasks while processing window is being opened
            /// </summary>
            public List<Task<dynamic>> Tasks { get; set; }

            /// <summary>
            /// Conctructs payload
            /// </summary>
            /// <param name="text">Text do be displayed</param>
            /// <param name="frequency">Frequency of tasks</param>
            /// <param name="tasks">Asynchrounus functions to be processed</param>
            public Payload(string text, float frequency, params Func<Task<dynamic>>[] tasks)
            {
                Content = text;
                LoadingElements = new string[] { Content + ".", Content + "..", Content + "..." };
                Index = 0;
                Frequency = frequency;
                Tasks = new List<Task<dynamic>>();
                Tasks.Add(Task.Run(() => tasks[0].Invoke()));         
            }

            /// <summary>
            /// Constructs payload
            /// </summary>
            /// <param name="text">Text do be displayed</param>
            /// <param name="frequency">Frequency of tasks</param>
            /// <param name="tasks">Asynchrounus functions to be processed</param>
            public Payload(string text, float frequency, params Func<Task>[] tasks)
            {
                Content = text;
                LoadingElements = new string[] { Content + ".", Content + "..", Content + "..." };
                Index = 0;
                Frequency = frequency;
                Tasks = new List<Task<dynamic>>();
                Tasks.Add(Task.Run(new Func<dynamic>(() => tasks[0].Invoke())));
            }
        }

        /// <summary>
        /// Starts updating all tasks and processing them
        /// </summary>
        /// <returns>Awaiting task for all functionalites</returns>
        public async Task<dynamic> StartUpdating()
        {
            Show();
            var result = await Task.WhenAny(LoadingTask(), payload.Tasks[0]);
            Close();
            return await result;
        }

        /// <summary>
        /// Loads task
        /// </summary>
        /// <returns>Awaitng task for loading functionalites</returns>
        private async Task<dynamic> LoadingTask()
        {
            while (!cts.IsCancellationRequested)
            {
                richTextBox1.Text = payload.LoadingElements[payload.Index++ % payload.LoadingElements.Length];
                await Task.Delay((int)payload.Frequency);
            }
            return null;
        }
        
        /// <summary>
        /// Initializes components
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ProcessingPopup));
            tableLayoutPanel1 = new TableLayoutPanel();
            richTextBox1 = new RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(richTextBox1, 0, 0);
            tableLayoutPanel1.Location = new Point(13, 13);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(236, 36);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            richTextBox1.Location = new Point(3, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(230, 30);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "Loading...";
            // 
            // ProcessingPopup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size(261, 61);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = ((Icon)(resources.GetObject("$this.Icon")));
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProcessingPopup";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Processing...";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);

        }

        private TableLayoutPanel tableLayoutPanel1;
        private RichTextBox richTextBox1;
    }
}
