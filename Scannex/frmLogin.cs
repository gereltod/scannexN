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
        public string _UserName = "";

        public frmLogin()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
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
            if (_UserName.Length > 0)
                textBox1.Text = _UserName;
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                
            }
        }

        private void textBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                textBox1.SelectAll();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                textBox1.SelectAll();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
                textBox2.SelectAll();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
                textBox2.SelectAll();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                textBox2.PasswordChar = ' ';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
