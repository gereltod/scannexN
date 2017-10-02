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
using System.Security;

namespace Scannex
{
    public partial class frmScanner : Form
    {
        List<ImageFile> _imageList = new List<ImageFile>();
        List<ImageFile> _imageListUpload = new List<ImageFile>();

        private static AreaSettings AreaSettings = new AreaSettings(TwainDotNet.TwainNative.Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);

        Twain _twain;
        ScanSettings _settings;

        int _index = -1;

        int _indexUpload = -1;

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
                    ImageFile file = new Scannex.ImageFile();
                    file.FileImage = args.Image;
                    file.FileName = "";
                    _imageList.Add(file);

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
                    Directory.CreateDirectory(path + "\\UPLOAD");
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                var fileList = (directoryInfo.GetFiles())
                    .ToList();
                if (fileList != null)
                {
                    foreach (var p in fileList)
                    {
                        System.Drawing.Image img = System.Drawing.Bitmap.FromFile(p.FullName);
                        ImageFile f = new ImageFile();
                        f.FileImage = img;
                        f.FileName = "";
                        _imageList.Add(f);
                    }
                }
            }
            catch (IOException ex)
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
                    pImage.Image = _imageList[_index].FileImage;

                    lblPage.Text = String.Format("Page {0} of {1}", _index + 1, _imageList.Count());
                    lblFile.Text = String.Format("File name:{0}", _imageList[_index].FileName);
                }

                if (_imageList.Count() == 0)
                {
                    lblPage.Text = "Page 0 of 0";
                    lblFile.Text = "File name:";
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
            }

        }

        private void LoadImageUpload()
        {
            try
            {
                if (_imageListUpload.Count() > 0)
                {
                    pImageUp.Image = _imageListUpload[_indexUpload].FileImage;

                    lblPageUp.Text = String.Format("Page {0} of {1}", _indexUpload + 1, _imageListUpload.Count());
                }
               

                if (_imageListUpload.Count() == 0)
                {
                    lblPageUp.Text = "Page 0 of 0";
                    _indexUpload = -1;
                    pImageUp.Image = null;
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
            }
        }


        #endregion

        string ComputeFourDigitStringHash(string filepath)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(filepath);
            int hash = filename.GetHashCode() % 10000;
            return hash.ToString("0000");
        }

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
            frmshow.BackgroundImage = _imageList[_index].FileImage;
            frmshow.BackgroundImageLayout = ImageLayout.Stretch;
            frmshow.StartPosition = FormStartPosition.CenterScreen;
            frmshow.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Image img = _imageList[_index].FileImage;
            img.RotateFlip(RotateFlipType.Rotate90FlipX);
            _imageList[_index].FileImage = img;
            LoadImage();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmScanPages frmshow = new frmScanPages();
            frmshow._twain = _twain;
            if (frmshow.ShowDialog() == DialogResult.OK)
            {
                Enabled = false;

                _settings = new ScanSettings();
                _settings.UseDocumentFeeder = frmshow.Feeder;
                _settings.ShowTwainUI = false;
                _settings.ShowProgressIndicatorUI = false;
                _settings.UseDuplex = true;
                if (frmshow.isBlack)
                    _settings.Resolution = ResolutionSettings.Fax;
                else if (frmshow.isGray)
                    _settings.Resolution = ResolutionSettings.Photocopier;
                else
                    _settings.Resolution = ResolutionSettings.ColourPhotocopier;

                _settings.DPI = frmshow.dpi;
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

        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|"
                                            + "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
            openFileDialog1.Title = "Please select an image file";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {                    
                    try
                    {
                        string ext = Path.GetExtension(file);
                        Image loadedImage = Image.FromFile(file);
                        ImageFile f = new ImageFile();
                        f.FileImage = loadedImage;
                        f.FileName = String.Format("{0}.{1}", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), ext);
                        _imageList.Add(f);                                                
                    }
                    catch (SecurityException ex)
                    {
                        FileLogger.LogStringInFile(ex.Message);
                        MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                            "Error message: " + ex.Message + "\n\n" +
                            "Details (send to Support):\n\n" + ex.StackTrace
                        );
                    }
                    catch (Exception ex)
                    {                     
                        MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);
                        FileLogger.LogStringInFile(ex.Message);
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_index != -1)
            {
                _imageList.RemoveAt(_index);
                pImage.Image = _imageList[_index].FileImage;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _imageList.Clear();

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (cmbEmployee.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbEmployee, "Enter your employee information");
                cmbEmployee.Focus();    
                return;
            }
            if (cmbLocation.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbLocation, "Enter your location information");
                cmbLocation.Focus();
                return;
            }
            if (cmbDoctype.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbDoctype, "Enter your doc type information");
                cmbDoctype.Focus();
                return;
            }
            if (_imageListUpload.Count() == 0)
            {
                errorProvider1.SetError(pImageUp, "Enter your upload files");

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pImage.Image != null)
            {
                ImageFile f = new ImageFile();
                f.FileImage = pImage.Image;
                f.FileName = _imageList[_index].FileName;
                _imageListUpload.Add(f);
                _indexUpload++;
                _imageList.RemoveAt(_index);
                LoadImageUpload();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (pImageUp.Image != null)
            {
                if (_indexUpload != -1)
                {
                    ImageFile f = new ImageFile();
                    f.FileImage = pImageUp.Image;
                    f.FileName = _imageListUpload[_index].FileName;

                    _imageList.Add(f);
                    LoadImageUpload();
                }
            }
        }
    }
}
