using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scannex
{
    public partial class frmLogin : Form
    {
        public string _UserName = "";

        
        public frmLogin()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.No;
        }

        private void SaveConfig()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string configFile = System.IO.Path.Combine(appPath, "Scannex.exe.config");
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            config.AppSettings.Settings["LASTUSER"].Value = textBox1.Text;
            config.Save();
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
            if (textBox1.Tag != null && textBox2.Tag != null)
            {
                string ret = ServerConnections.Login(textBox1.Tag.ToString(), textBox2.Tag.ToString());
                if (ret == "200")
                {
                    Constants.USERNAME = textBox1.Text;
                    SaveConfig();
                    DialogResult = DialogResult.OK;
                }
                else if (ret == "Unauthorized")
                {
                    MessageBox.Show("Invalid password or username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                }
                else if (ret == "error")
                {
                    MessageBox.Show("An unknown error has occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                }
            }
            else
            {
                errorProvider1.SetError(textBox2, "Enter your password");
                textBox2.Focus();
                return;
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
            _UserName = System.Configuration.ConfigurationManager.AppSettings["LASTUSER"].ToString();
            if (_UserName.Length > 0)
            {
                textBox1.Text = _UserName.Trim();                
            }
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.No;
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
            {
                if (textBox1.Text == "User email")
                {
                    textBox1.Text = "";                    
                }
                else
                    textBox1.SelectAll();
            }
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
            {
                if (textBox2.Text == "Password")
                {                   
                    textBox2.Text = "";                    
                }
                else
                    textBox2.SelectAll();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else if (textBox2.Tag != null)
            {
                textBox2.UseSystemPasswordChar = true;
                textBox2.Tag = textBox2.Text;
            }
            else
            {
                textBox2.Tag = textBox2.Text;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Text.Length > 0)
                if (e.KeyCode == Keys.Enter)
                    textBox1.Focus();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                if (e.KeyCode == Keys.Enter)
                    button1_Click((object)button1, new EventArgs());
            }                            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = Scannex.Properties.Resources.log_error;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Scannex.Properties.Resources.log_error50;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://smartdrawers.com/password/reset");
            Process.Start(sInfo);
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Image = Scannex.Properties.Resources.Forgot_password_mouse_over;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Scannex.Properties.Resources.Forgot_password_1;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackgroundImage = Scannex.Properties.Resources.login_button;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Scannex.Properties.Resources.login_button_mouse_over;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "User email";
                textBox1.Tag = null;
            }
            else if (textBox1.Text.Length > 0)
                textBox1.Tag = textBox1.Text;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Tag = null;
                textBox2.UseSystemPasswordChar = false;
                textBox2.Text = "Password";
            }
            else
            {
                textBox2.Tag = textBox2.Text;
            }
        }
    }
}
