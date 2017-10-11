using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            if (frmshow.ShowDialog() == DialogResult.OK)
            {
                Constants.ST_LOCATIONS = ServerConnections.ServerGETData<List<Locations>>("api/scannex/v2/locations");
                Constants.ST_EMPLOYEES = ServerConnections.ServerGETData<List<Employees>>("api/scannex/v2/employees");
                Constants.ST_DOCTYPES = ServerConnections.ServerGETData<List<DocTypes>>("api/scannex/v2/doctypes");
                toolStripMenuItem6_Click((Object)toolStripMenuItem6, new EventArgs());

            }
        }
                
        private void Item_Click(object sender, EventArgs e)
        {
            
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
                frmshow = new frmScanner(this);
                frmshow.MdiParent = this;
                frmshow.Show();
            }
        }
    }
}
