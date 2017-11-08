using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwainDotNet;
using TwainDotNet.WinFroms;

namespace Scannex
{
    public partial class frmScanPages : Form
    {
        public bool isBlack = false;
        public bool isColour = true;
        public bool isGray = false;
        public bool Feeder = true;
        public int dpi = 200;
        public Twain _twain = null;

        public frmScanPages()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbSource.SelectedIndex == 0)
                Feeder = true;
            else
                Feeder = false;

            DialogResult = DialogResult.OK;
        }

        private void rd100_CheckedChanged(object sender, EventArgs e)
        {
            if (rd100.Checked)
                dpi = 100;
            else
                dpi = 200;
        }

        private void rd300_CheckedChanged(object sender, EventArgs e)
        {
            if (rd300.Checked)
                dpi = 100;
            else
                dpi = 200;

        }

        private void rdBlack_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBlack.Checked)
            {
                isBlack = true;
                isColour = false;
                isGray = false;
            }
        }

        private void rdColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdColor.Checked)
            {
                isBlack = false;
                isColour = true;
                isGray = false;
            }
        }

        private void rdGray_CheckedChanged(object sender, EventArgs e)
        {
            if (rdGray.Checked)
            {
                isBlack = false;
                isColour = false;
                isGray = true;
            }
        }

        private void PageSize()
        {
            PrinterSettings printerSettings = new PrinterSettings();
            IQueryable<PaperSize> paperSizes = printerSettings.PaperSizes.Cast<PaperSize>().AsQueryable();
            List<String> paper=new List<string>();

            foreach(PaperSize p in paperSizes)
            {
                paper.Add(p.PaperName);
            }
            cmbPage.DataSource = paper;
        }

        private void frmScanPages_Load(object sender, EventArgs e)
        {
            cmbScanner.DataSource = _twain.SourceNames;
            PageSize();
            cmbSource.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
