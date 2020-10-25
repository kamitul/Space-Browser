using Microsoft.EntityFrameworkCore;
using SBDataLibrary.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace space_browser
{
    public partial class Form1 : Form
    {
        private DataController dataController;

        public Form1(DataController dataController)
        {
            InitializeComponent();
            this.dataController = dataController;
        }

        private void Browser_Load(object sender, EventArgs e)
        {

        }
    }
}
    