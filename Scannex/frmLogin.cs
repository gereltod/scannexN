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
            if (textBox1.Text.Length == 0)
            {
                errorProvider1.SetError(textBox1, "Enter your username");
                textBox1.Focus();
                return;
            }
            if (textBox2.Text.Length == 0)
            {
                errorProvider1.SetError(textBox2, "Enter your password");
                textBox2.Focus();
                return;
            }
            string ret = ServerConnections.Login(textBox1.Text, textBox2.Text);
            if (ret == "200")
            {
                DialogResult = DialogResult.OK;
            }
            else if(ret == "Unauthorized")
            {
                MessageBox.Show("Invalid password or username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ret == "error")
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
