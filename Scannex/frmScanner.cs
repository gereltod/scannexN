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
using TwainDotNet;
using TwainDotNet.WinFroms;

namespace Scannex
{
    public partial class frmScanner : Form
    {
        List<Image> _imageList = new List<Image>();
        List<String> _imageName = new List<string>();

        private static AreaSettings AreaSettings = new AreaSettings(TwainDotNet.TwainNative.Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);

        Twain _twain;
        ScanSettings _settings;

        int _index = 0;

        public frmScanner()
        {
            InitializeComponent();          
        }

        public frmScanner(Form parent)
        {
            InitializeComponent();

            _twain = new Twain(new WinFormsWindowMessageHook(parent));
            _twain.TransferImage += delegate (Object sender, TransferImageEventArgs args)
            {
                if (args.Image != null)
                {
                    pImage.Image = args.Image;
                    _imageList.Add(args.Image);
                    
                    //widthLabel.Text = "Width: " + pImage.Image.Width;
                    //heightLabel.Text = "Height: " + pImage.Image.Height;
                }
            };
            _twain.ScanningComplete += delegate
            {
                Enabled = true;
            };
        }
        #region　Dataload

        private void Comboload()
        {
            cmbEmployee.DataSource = Constants.ST_EMPLOYEES;
            cmbEmployee.DisplayMember = "last_name";
            cmbEmployee.ValueMember = "id";
            cmbEmployee.SelectedIndex = -1;

            cmbLocation.DataSource = Constants.ST_LOCATIONS;
            cmbLocation.DisplayMember = "name";
            cmbLocation.ValueMember = "id";
            cmbLocation.SelectedIndex = -1;

            cmbDoctype.DataSource = Constants.ST_DOCTYPES;
            cmbDoctype.DisplayMember = "name";
            cmbDoctype.ValueMember = "id";
            cmbDoctype.SelectedIndex = -1;
        }

        private void LoadFolder()
        {
            try
            {
                string date = DateTime.Now.ToString("yyyyMMdd");
                string path = Constants.FILE_PATH + "\\" + date;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                var fileList = (directoryInfo.GetFiles())
                    .ToList();
                if (fileList != null)
                {
                    foreach(var p in fileList)
                    {
                        System.Drawing.Image img = System.Drawing.Bitmap.FromFile(p.FullName);
                        _imageName.Add(p.Name);
                        _imageList.Add(img);
                    }
                }
            }
            catch(IOException ex)
            {
                FileLogger.LogStringInFile(ex.Message);
            }

        }

        private void LoadImage()
        {
            try
            {
                if (_imageList.Count() > 0)
                {
                    pImage.Image = _imageList[_index];
                    lblPage.Text = String.Format("Page {0} of {1}", _index + 1, _imageList.Count());
                    lblFile.Text = String.Format("File name:{0}", _imageName[_index]);
                }

                if (_imageList.Count() == 0)
                {
                    lblPage.Text = "Page 0 of 0";
                    lblFile.Text = "File name:";
                }
            }
            catch(Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
            }

        }

        #endregion

        private void frmScanner_Load(object sender, EventArgs e)
        {
            Comboload();
            LoadFolder();
            LoadImage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _index++;
            if (_index >= _imageList.Count()) _index = 0;

            LoadImage();
        }

        private void pImage_DoubleClick(object sender, EventArgs e)
        {
            frmView frmshow = new Scannex.frmView();
            frmshow.BackgroundImage = _imageList[_index];
            frmshow.BackgroundImageLayout = ImageLayout.Stretch;
            frmshow.StartPosition = FormStartPosition.CenterScreen;
            frmshow.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Image img = _imageList[_index];
            img.RotateFlip(RotateFlipType.Rotate90FlipX);
            _imageList[_index] = img;
            LoadImage();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Enabled = false;

            _settings = new ScanSettings();
            _settings.UseDocumentFeeder = true;
            _settings.ShowTwainUI = false;
            _settings.ShowProgressIndicatorUI = false;
            _settings.UseDuplex = true;
            _settings.Resolution = ResolutionSettings.ColourPhotocopier;
            _settings.Area = null;
            _settings.ShouldTransferAllPages = true;

            _settings.Rotation = new RotationSettings()
            {
                AutomaticRotate = true,
                AutomaticBorderDetection = true
            };

            try
            {
                _twain.StartScanning(_settings);
                LoadImage();
            }
            catch (TwainException ex)
            {
                MessageBox.Show(ex.Message);
                Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _twain.SelectSource();
        }
    }
}
