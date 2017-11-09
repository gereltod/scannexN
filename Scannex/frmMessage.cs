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
    public partial class frmMessage : Form
    {
        public frmMessage()
        {
            InitializeComponent();
            lblName.Text = string.Format("for {0}", Constants.USERNAME);
        }

        public void Message(string msg)
        {
            lblMessage.Text = msg;
            lblName.Location = new Point(lblMessage.Location.X + lblMessage.Size.Width + 3, lblMessage.Location.Y);
        }

        public void Done()
        {
            this.Invoke(new Action(() => { pictureBox1.Visible = false; }));
            this.Invoke(new Action(() => { pictureBox3.Visible = true; }));
        }

        public void Title(string title)
        {
            label1.Text = title;
        }
        public void CloseForm()
        {
            DialogResult = DialogResult.No;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void lblDone_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
