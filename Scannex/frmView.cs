using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scannex
{
    public partial class frmView : Form
    {
        public frmView()
        {
            InitializeComponent();
        }

        private void frmView_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
