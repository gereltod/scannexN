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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmLogin frmshow = new frmLogin();
            frmshow.ShowDialog();

            if (Constants.ISLOGIN)
            {
                Constants.ST_LOCATIONS = ServerConnections.ServerGETData<List<Locations>>("/api/locs");
                Constants.ST_EMPLOYEES = ServerConnections.ServerGETData<List<Employees>>("/api/employees");
                Constants.ST_DOCTYPES = ServerConnections.ServerGETData<List<DocTypes>>("/api/doctypes");
                toolStripMenuItem6.Enabled = true;
            }

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            bool isBe=false;
            frmScanner frmshow = null;
            foreach (Form frm in this.MdiChildren)
            {
                if (typeof(frmScanner) == frm.GetType())
                {
                    frmshow = (frmScanner)frm;
                    frmshow.Activate();
                    isBe = true;
                    break;
                }
            }
            if (!isBe)
            {
                frmshow = new frmScanner();
                frmshow.MdiParent = this;
                frmshow.Show();
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            bool isBe = false;
            frmLogin frmshow = null;
            foreach (Form frm in this.MdiChildren)
            {
                if (typeof(frmLogin) == frm.GetType())
                {
                    frmshow = (frmLogin)frm;
                    frmshow.Activate();
                    isBe = true;
                    break;
                }
            }
            if (!isBe)
            {
                frmshow = new frmLogin();
                frmshow.MdiParent = this;
                frmshow.Show();
            }
        }
    }
}
