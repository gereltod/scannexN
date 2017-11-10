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
    public partial class frmTwainLoading : Form
    {
        public frmTwainLoading()
        {
            InitializeComponent();
        }

        public void CloseForm()
        {
            DialogResult = DialogResult.No;
        }

        private void frmTwainLoading_Load(object sender, EventArgs e)
        {
           
        }
    }
}
