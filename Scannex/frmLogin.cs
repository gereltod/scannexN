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
    public partial class frmLogin : Form
    {
        bool islogin = false;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ret = ServerConnections.Login(textBox1.Text, textBox2.Text);
            DialogResult = DialogResult.OK;
        }




    }
}
