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
using System.Globalization;

namespace Scannex
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += _work_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;

        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tProgressBar.Value = 0;
            });
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tProgressBar.Value = e.ProgressPercentage;
            });
        }

        void _work_DoWork(object sender, DoWorkEventArgs e)
        {
            int maxCount = 0;
            foreach (string dir in Directory.GetDirectories(Constants.FILE_PATH))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                DateTime dateTime = DateTime.ParseExact(directoryInfo.Name, "yyyyMMdd", CultureInfo.InvariantCulture);

                if (dateTime.AddDays(Constants.DELETE_DAY).Date < DateTime.Now.Date)
                {
                    maxCount += 1;
                }
            }
            int i = 1;
            foreach (string dir in Directory.GetDirectories(Constants.FILE_PATH))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);

                string dirName = directoryInfo.Name;
                DateTime dateTime = DateTime.ParseExact(dirName, "yyyyMMdd", CultureInfo.InvariantCulture);

                if (dateTime.AddDays(Constants.DELETE_DAY).Date < DateTime.Now.Date)
                {
                    int percents = (100 / maxCount) * i;
                    ((BackgroundWorker)sender).ReportProgress(percents);
                    EraseDirectory(dir, true);
                }
                i++;
            }
        }

        public bool EraseDirectory(string folderPath, bool recursive)
        {         
            if (!Directory.Exists(folderPath))
                return false;

            foreach (string file in Directory.GetFiles(folderPath))
            {
                File.Delete(file);
            }
            
            if (recursive)
            {
                foreach (string dir in Directory.GetDirectories(folderPath))
                {
                    EraseDirectory(dir, recursive);
                }
            }            
            Directory.Delete(folderPath);
            return true;
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
                                               
                
                tProgressBar.Step = 1;
                backgroundWorker1.WorkerReportsProgress = true;
                backgroundWorker1.RunWorkerAsync();

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

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            bool isBe = false;
            frmOptions f = null;
            foreach(Form frm in this.MdiChildren)
            {
                if(typeof(frmOptions)==frm.GetType())
                {
                    f = (frmOptions)frm;
                    f.Activate();
                    isBe = true;
                    break;
                }
            }
            if (!isBe)
            {
                f = new frmOptions();
                f.MdiParent = this;
                f.Show();
            }
        }
    }
}
