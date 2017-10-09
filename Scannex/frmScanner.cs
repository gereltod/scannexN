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
using iTextSharp;
using iTextSharp.text.pdf;

namespace Scannex
{
    public partial class frmScanner : Form
    {
        List<ImageFile> _imageList = new List<ImageFile>();
        List<ImageFile> _imageListUpload = new List<ImageFile>();

        private static AreaSettings AreaSettings = new AreaSettings(TwainDotNet.TwainNative.Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);
        Form _parent;
        Twain _twain;
        ScanSettings _settings;

        int _index = -1;
        int _indexUpload = -1;

        public frmScanner()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            _twain = new Twain(new WinFormsWindowMessageHook(_parent));
            _twain.TransferImage += delegate (Object sender, TransferImageEventArgs args)
            {
                if (args.Image != null)
                {
                    if (_index == -1)
                        _index = 0;

                    pImage.Image = args.Image;
                    ImageFile file = new Scannex.ImageFile();
                    file.FileImage = args.Image;
                    file.FileName = String.Format("{0}{1}", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), ".jpg");
                    _imageList.Add(file);
                    string path = Constants.FILE_PATH_TODAY + "\\" + file.FileName;
                    file.FileImage.Save(path);
                }
            };
            _twain.ScanningComplete += delegate
            {
                Enabled = true;
            };
        }

        public frmScanner(Form parent)
        {
            InitializeComponent();
            _parent = parent;
            Init();
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
                Constants.FILE_PATH_TODAY = path;


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
                        f.FileName = p.Name;
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
            ShowImage();
            pImageUp.Image = Scannex.Properties.Resources.nopicture;
        }

        private void ShowImage()
        {
            if (_imageList.Count() > 0 && _index == -1)
                _index = 0;

            if (_imageList.Count() == 0)
            {
                _index = -1;
                pImage.Image = Scannex.Properties.Resources.nopicture;                
            }
            else
            {
                if (_imageList.Count() == _index)
                {
                    _index = 0;
                }

                if (_index != -1)
                {
                    pImage.Image = _imageList[_index].FileImage;
                    lblFile.Text = String.Format("File name:{0}", _imageList[_index].FileName);
                }
            }

            lblPage.Text = String.Format("Page {0} of {1}", _index + 1, _imageList.Count());           
        }

        private void ShowImageUp()
        {
            if (_imageListUpload.Count() > 0 && _indexUpload==-1)
                _indexUpload = 0;

            if (_imageListUpload.Count() == 0)
            {
                _indexUpload = -1;
                pImageUp.Image = Scannex.Properties.Resources.nopicture;
            }
            else
            {
                if (_imageListUpload.Count() == _indexUpload)
                {
                    _indexUpload = 0;
                }

                if (_indexUpload != -1)
                    pImageUp.Image = _imageListUpload[_indexUpload].FileImage;
                
            }

            lblPageUp.Text = String.Format("Page {0} of {1}", _indexUpload + 1, _imageListUpload.Count());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _index++;
            ShowImage();
        }

        private void pImage_DoubleClick(object sender, EventArgs e)
        {
            frmView frmshow = new Scannex.frmView();
            frmshow.BackgroundImage = _imageList[_index].FileImage;
            frmshow.BackgroundImageLayout = ImageLayout.Stretch;
            frmshow.StartPosition = FormStartPosition.CenterScreen;
            frmshow.ShowDialog();
        }

        private void RotateAndSaveImage(String input, String output)
        {
            //create an object that we can use to examine an image file
            using (Image img = Image.FromFile(input))
            {
                //rotate the picture by 90 degrees and re-save the picture as a Jpeg
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                img.Save(output, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Image img = _imageList[_index].FileImage;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            _imageList[_index].FileImage = img;            
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
                    Init();
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (_index == -1)
                _index = 0;

            openFileDialog1.Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|"
                                            + "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff";
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
                        f.FileName = String.Format("{0}{1}", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), ext);
                        _imageList.Add(f);

                        string path = Constants.FILE_PATH_TODAY + "\\" + f.FileName;
                        f.FileImage.Save(path);

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
                ShowImage();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_index != -1)
            {                       
                _imageList.RemoveAt(_index);
                ShowImage();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _imageList.Clear();
                _index = -1;
                ShowImage();
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

            string file=Convertpdf();
            if (file != "")
            {

            }
        }

        private string Convertpdf()
        {
            string ret = "";
            if (_imageListUpload.Count > 0)
            {
                string path = Constants.FILE_PATH_TODAY + "\\UPLOAD\\";
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                try
                {
                    string name = cmbEmployee.SelectedText;
                    string filename = String.Format("{0}_{1}.{2}", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), name, "pdf");
                    PdfWriter.GetInstance(document, new FileStream(path + filename, FileMode.Create));

                    document.Open();

                    foreach (ImageFile image in _imageListUpload)
                    {

                        iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image.FileImage, System.Drawing.Imaging.ImageFormat.Jpeg);

                        if (pic.Height > pic.Width)
                        {
                            //Maximum height is 800 pixels.
                            float percentage = 0.0f;
                            percentage = 780 / pic.Height;
                            pic.ScalePercent(percentage * 100);
                        }
                        else
                        {
                            //Maximum width is 600 pixels.
                            float percentage = 0.0f;
                            percentage = 580 / pic.Width;
                            pic.ScalePercent(percentage * 100);
                        }

                        document.Add(pic);
                        document.NewPage();
                    }
                    ret = filename;
                }
                catch (iTextSharp.text.DocumentException de)
                {
                    FileLogger.LogStringInFile(de.Message);
                }
                catch (IOException ioe)
                {
                    FileLogger.LogStringInFile(ioe.Message);
                }

                document.Close();
                DeleteUploadFiles(path);
            }
            return ret;
        }

        private void DeleteUploadFiles(string path)
        {
            List<string> delFile = new List<string>();
            foreach (ImageFile image in _imageListUpload)
            {
                image.SaveAll(path);
                delFile.Add(image.FileName);
                image.FileImage.Dispose();
            }
            _imageListUpload.Clear();
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();

            foreach (string f in delFile)
            {
                string p = Constants.FILE_PATH_TODAY + "\\" + f;
                if (File.Exists(p))
                {
                    FileInfo info = new FileInfo(p);
                    info.IsReadOnly = false;
                    File.Delete(p);
                }
            }
            ShowImageUp();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pImage.Image != null)
            {
                if (_index > -1)
                {
                    ImageFile f = new ImageFile();
                    f.FileImage = pImage.Image;
                    f.FileName = _imageList[_index].FileName;
                    _imageListUpload.Add(f);
                                        
                    _imageList.RemoveAt(_index);
                    
                    ShowImage();
                    ShowImageUp();
                }
                else
                    pImage.Image = (Image)Scannex.Properties.Resources.nopicture;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (pImageUp.Image != null)
            {
                if (_indexUpload > -1)
                {
                    ImageFile f = new ImageFile();
                    f.FileImage = pImageUp.Image;
                    f.FileName = _imageListUpload[_indexUpload].FileName;

                    _imageList.Add(f);
                    
                    _imageListUpload.RemoveAt(_indexUpload);
                   
                    ShowImage();
                    ShowImageUp();
                }
                else
                    pImageUp.Image = (Image)Scannex.Properties.Resources.nopicture;
            }
        }

        private void btnNextUp_Click(object sender, EventArgs e)
        {
            _indexUpload++;
            ShowImageUp();
        }
    }
}
