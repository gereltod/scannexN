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

namespace Scannex
{
    public partial class frmMessage : Form
    {
        public CancellationTokenSource tokenSource = new CancellationTokenSource();

        public frmMessage()
        {
            InitializeComponent();
            
        }

        public void Message(string msg)
        {
            lblName.Text = string.Format("{0}", Constants.USER.name);
            lblMessage.Text = msg;
            label3.Location = new Point(lblMessage.Location.X + lblMessage.Size.Width + 3, lblMessage.Location.Y);
            lblName.Location = new Point(lblMessage.Location.X + lblMessage.Size.Width + 28, lblMessage.Location.Y);
        }

        public void Done()
        {
            this.Invoke(new Action(() => { pictureBox1.Visible = false;
                lblCancel.Visible = false;
            }));
            this.Invoke(new Action(() => { pictureBox3.Visible = true;
                lblDone.Visible = true;
            }));
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

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Scannex.Properties.Resources.error_over;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Scannex.Properties.Resources.error50;
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            DialogResult = DialogResult.No;
        }
    }
}
