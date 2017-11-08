using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwainDotNet;
using TwainDotNet.WinFroms;

namespace Scannex
{
    public partial class frmScannerNew : Form, IMessageFilter
    {
        Dictionary<int, ImageFile> imageList = new Dictionary<int, ImageFile>();
        List<ImageFile> imageListSelected = new List<ImageFile>();
        int selected = 0;
        int unselected = 0;

        public frmScannerNew()
        {
            InitializeComponent();
            Init();
            (pnlPictures as Control).KeyDown += new KeyEventHandler(FrmScannerNew_KeyDown);

            backgroundWorker1.DoWork += _work_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            mTimer = new System.Windows.Forms.Timer();
            mTimer.Tick += LogoutUser;

            Application.AddMessageFilter(this);
        }

        #region Mouse

        private void FrmScannerNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control && e.KeyCode == Keys.A)
            {
                MessageBox.Show("ctrl +a ");
            }
        }

        private void lblZoomIn_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Black;
        }

        private void lblZoomIn_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.DimGray;
        }

        private void AddPicList(Image img, string ext,string fname, bool saveFile, bool noList)
        {
            pnlPictures.VerticalScroll.Value = 0;
            int maxId = imageList.Count();

            int x = 0;
            int y = 0;

            int col = maxId / Constants.PAGE_SIZE;
            int row = maxId % Constants.PAGE_SIZE;


            x = (row * Constants.PIC_SIZEX) + (row * Constants.PADDING_SIZE) + Constants.PADDING_SIZE;
            y = (col * Constants.PIC_SIZEY) + (col * Constants.PADDING_SIZE) + Constants.PADDING_SIZE;
            
            ImageFile f = new ImageFile();

            f.FileImage = img;
            f.ViewImage = Constants.ResizeImageFixedWidth(img, Constants.IMAGE_WIDTH);
            if (fname == "")
                f.FileName = String.Format("{0}{1}", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), ext);
            else
                f.FileName = fname;

            PictureBox com = new PictureBox();
            com.Location = new Point(x, y);
            com.BackColor = Color.White;
            com.Size = new Size(Constants.PIC_SIZEX, Constants.PIC_SIZEY);
            com.SizeMode = PictureBoxSizeMode.CenterImage;
            com.MouseHover += Com_MouseHover;
            com.MouseLeave += Com_MouseLeave;
            com.MouseClick += Com_MouseClick;            
            com.Image = f.ViewImage;
            com.Name = f.FileName;

            PictureBox check = new PictureBox();
            check.Location = new Point(x, y);
            check.BackColor = Color.Transparent;
            check.Size = new Size(50, 50);
            check.SizeMode = PictureBoxSizeMode.StretchImage;
            check.Visible = false;
            foreach (ImageFile sf in imageListSelected)
            {
                if (sf.FileName == f.FileName)
                {
                    check.Visible = true;
                    break;
                }
            }
            
            check.Image = Scannex.Properties.Resources._checked;
            

            Label lbl = new Label();
            lbl.Location = new Point(x + (Constants.PIC_SIZEX) / 2 - Constants.PADDING_SIZE, y + Constants.PIC_SIZEY + 5);
            lbl.Text = String.Format("Page {0}", maxId + 1);
            lbl.AutoSize = true;
            pnlPictures.Controls.Add(check);
            pnlPictures.Controls.Add(com);
            
            pnlPictures.Controls.Add(lbl);
            f.MyPicture = com;
            f.MyLabel = lbl;
            f.MyCheck = check;

            if (noList)
                imageList.Add(maxId, f);

            if (saveFile)
            {
                string path = Constants.FILE_PATH_TODAY + "\\" + f.FileName;
                f.FileImage.Save(path);
            }
        }

        private void unPicList(Image img, string ext, string fname, bool saveFile, bool noList)
        {
            pnlPictures.VerticalScroll.Value = 0;
            int maxId = unselected;

            int x = 0;
            int y = 0;

            int col = maxId / Constants.PAGE_SIZE;
            int row = maxId % Constants.PAGE_SIZE;


            x = (row * Constants.PIC_SIZEX) + (row * Constants.PADDING_SIZE) + Constants.PADDING_SIZE;
            y = (col * Constants.PIC_SIZEY) + (col * Constants.PADDING_SIZE) + Constants.PADDING_SIZE;
            
            ImageFile f = new ImageFile();

            f.FileImage = img;
            f.ViewImage = Constants.ResizeImageFixedWidth(img, Constants.IMAGE_WIDTH);
            if (fname == "")
                f.FileName = String.Format("{0}{1}", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), ext);
            else
                f.FileName = fname;

            PictureBox com = new PictureBox();
            com.Location = new Point(x, y);
            com.BackColor = Color.White;
            com.Size = new Size(Constants.PIC_SIZEX, Constants.PIC_SIZEY);
            com.SizeMode = PictureBoxSizeMode.CenterImage;          
            com.Image = f.ViewImage;
            com.Name = f.FileName;

            PictureBox check = new PictureBox();
            check.Location = new Point(x, y);
            check.BackColor = Color.Transparent;
            check.Size = new Size(50, 50);
            check.SizeMode = PictureBoxSizeMode.StretchImage;
            check.Visible = false;
            check.Image = Scannex.Properties.Resources._checked;


            Label lbl = new Label();
            lbl.Location = new Point(x + (Constants.PIC_SIZEX) / 2 - Constants.PADDING_SIZE, y + Constants.PIC_SIZEY + 5);
            lbl.Text = String.Format("Page {0}", maxId + 1);
            lbl.AutoSize = true;
            pnlPictures.Controls.Add(check);
            pnlPictures.Controls.Add(com);

            pnlPictures.Controls.Add(lbl);
            f.MyPicture = com;
            f.MyLabel = lbl;
            f.MyCheck = check;

            if (noList)
                imageList.Add(maxId, f);

            if (saveFile)
            {
                string path = Constants.FILE_PATH_TODAY + "\\" + f.FileName;
                f.FileImage.Save(path);
            }
        }
        
        private void SelectedPicList(Image img, string ext, string fname, bool saveFile, bool noList)
        {
            pnlPictures.VerticalScroll.Value = 0;
            int maxId = selected;

            int x = 0;
            int y = 0;

            int col = maxId / Constants.PAGE_SIZE;
            int row = maxId % Constants.PAGE_SIZE;


            x = (row * Constants.PIC_SIZEX) + (row * Constants.PADDING_SIZE) + Constants.PADDING_SIZE;
            y = (col * Constants.PIC_SIZEY) + (col * Constants.PADDING_SIZE) + Constants.PADDING_SIZE;
           
            ImageFile f = new ImageFile();

            f.FileImage = img;
            f.ViewImage = Constants.ResizeImageFixedWidth(img, Constants.IMAGE_WIDTH);
            if (fname == "")
                f.FileName = String.Format("{0}{1}", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), ext);
            else
                f.FileName = fname;

            PictureBox com = new PictureBox();
            com.Location = new Point(x, y);
            com.BackColor = Color.White;
            com.Size = new Size(Constants.PIC_SIZEX, Constants.PIC_SIZEY);
            com.SizeMode = PictureBoxSizeMode.CenterImage;         
            com.Image = f.ViewImage;
            com.Name = f.FileName;

            PictureBox check = new PictureBox();
            check.Location = new Point(x, y);
            check.BackColor = Color.Transparent;
            check.Size = new Size(50, 50);
            check.SizeMode = PictureBoxSizeMode.StretchImage;
            check.Visible = false;
            check.Image = Scannex.Properties.Resources._checked;
            
            Label lbl = new Label();
            lbl.Location = new Point(x + (Constants.PIC_SIZEX) / 2 - Constants.PADDING_SIZE, y + Constants.PIC_SIZEY + 5);
            lbl.Text = String.Format("Page {0}", maxId + 1);
            lbl.AutoSize = true;
            pnlPictures.Controls.Add(check);
            pnlPictures.Controls.Add(com);

            pnlPictures.Controls.Add(lbl);
            f.MyPicture = com;
            f.MyLabel = lbl;
            f.MyCheck = check;
                   

        }

        private void Com_MouseClick(object sender, MouseEventArgs e)
        {
            if (imageList.Count() > 0)
            {
                if (e.Button == MouseButtons.Left)
                    FindSelect(((PictureBox)sender).Name);
                else if (e.Button == MouseButtons.Middle)
                    DeleteList(((PictureBox)sender).Name);

            }
        }

        private void RefreshPnl(bool all)
        {
            pnlPictures.Controls.Clear();
            Dictionary<int, ImageFile> copy = imageList;
            if (all)
                imageList = new Dictionary<int, ImageFile>();
            else
                unselected = 0;

            foreach (ImageFile f in copy.Values)
            {
                string ext = Path.GetExtension(f.FileName);
                if (all)
                    AddPicList(f.FileImage, ext, f.FileName, false, true);
                else
                {
                    bool hs = false;
                    foreach(ImageFile sf in imageListSelected)
                    {
                        if (sf.FileName == f.FileName)
                        {
                            hs = true;
                            break;
                        }
                    }

                    if (!hs)
                    {
                        unPicList(f.FileImage, ext, f.FileName, false, false);
                        unselected++;
                    }
                }
            }
        }

        private void DeleteList(string name)
        {
            //File.Delete(file);
            string fname="";
            foreach (int f in imageList.Keys)
            {
                if (imageList[f].MyPicture.Name == name)
                {
                    fname = imageList[f].FileName;
                    imageList.Remove(f);
                    RefreshPnl(true);
                    break;
                }
            }
            if(fname!="")
            {                
              //  LoadFolder();
            }
        }

        private void FindSelect(string name)
        {
           
            foreach(ImageFile f in imageList.Values)
            {
                if (f.MyPicture.Name == name)
                {
                    if (!f.MyCheck.Visible)
                    {
                        if (!imageListSelected.Contains(f))
                            imageListSelected.Add(f);
                        f.MyCheck.Visible = true;
                      
                    }
                    else
                    {
                        f.MyCheck.Visible = false;
                        for (int i = 0; i < imageListSelected.Count; i++)
                        {
                            if (imageListSelected[i].FileName == f.FileName)
                            {
                                imageListSelected.RemoveAt(i);
                                break;
                            }
                        }
                    }

                    f.MyCheck.Location = new Point(f.MyPicture.Location.X, f.MyPicture.Location.Y);

                   
                    pnlPictures.Controls.Add(f.MyCheck);
                    pnlPictures.Controls.Add(f.MyPicture);
                    pnlPictures.Controls.Add(f.MyLabel);
                    break;
                }
            }
            lblSelected.Text = String.Format("Selected {0} of {1} pages", imageListSelected.Count, imageList.Count);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|"
                                            + "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff";
            openFileDialog1.Title = "Please select an image file";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                foreach (String file in openFileDialog1.FileNames)
                {
                    int m = openFileDialog1.FileNames.Length;
                    try
                    {
                       
                        string ext = Path.GetExtension(file);                       
                        Image loadedImage = Image.FromFile(file);

                        AddPicList(loadedImage, ext, "", true, true);

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

                pnlPictures.VerticalScroll.Value = pnlPictures.VerticalScroll.Maximum;
            }
        }

        private void Com_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        private void Com_MouseHover(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }
      

        private bool mouseDown;
        private Point lastLocation;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {                
                mouseDown = true;
                lastLocation = e.Location;              
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Size = new Size(MousePosition);
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 3;
        }


        #endregion

        private System.Windows.Forms.Timer mTimer;
        private int mDialogCount;
        public bool PreFilterMessage(ref Message m)
        {
            // Monitor message for keyboard and mouse messages
            bool active = m.Msg == 0x100 || m.Msg == 0x101;  // WM_KEYDOWN/UP
            active = active || m.Msg == 0xA0 || m.Msg == 0x200;  // WM_(NC)MOUSEMOVE
            active = active || m.Msg == 0x10;  // WM_CLOSE, in case dialog closes
            if (active)
            {
                mTimer.Enabled = false;
                mTimer.Start();
            }
            return false;
        }

        private void LogoutUser(object sender, EventArgs e)
        {
            // No activity, logout user
            if (mDialogCount > 0) return;
            mTimer.Enabled = false;
            if (Constants.ISLOGIN)
            {
                foreach (Form frm in this.MdiChildren)
                {
                    frmScanner f = null;
                    if (typeof(frmScanner) == frm.GetType())
                    {
                        f = (frmScanner)frm;
                        f.Close();
                        break;
                    }
                }
                Constants.ISLOGIN = false;
                frmLogin frmshow = new frmLogin();
                frmshow._UserName = Constants.USERNAME;
                if (frmshow.ShowDialog() == DialogResult.OK)
                {
                    Constants.ISLOGIN = true;
                }
                else
                    Application.Exit();
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

        private void frmScannerNew_Load(object sender, EventArgs e)
        {
            frmLogin frmshow = new frmLogin();
            if (frmshow.ShowDialog() == DialogResult.OK)
            {
                Constants.ST_LOCATIONS = ServerConnections.ServerGETData<List<Locations>>("api/scannex/v2/locations");
                Constants.ST_EMPLOYEES = ServerConnections.ServerGETData<List<Employees>>("api/scannex/v2/employees");
                Constants.ST_DOCTYPES = ServerConnections.ServerGETData<List<DocTypes>>("api/scannex/v2/doctypes");
               
                mTimer.Interval = Constants.EXPIRE_TIME * 1000;
                mTimer.Enabled = true;
                lblStatus.Text = String.Format("Logged in as {0}", Constants.USERNAME);


                Comboload();
                LoadFolder();

                cmbDoctype.SelectedIndex = -1;
                cmbEmployee.SelectedIndex = -1;
                cmbLocation.SelectedIndex = -1;

                backgroundWorker1.WorkerReportsProgress = true;
                backgroundWorker1.RunWorkerAsync();

            }
            else
                Application.Exit();
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                
            });
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
               
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://smartdrawers.com/support/scannex/2.0");
            Process.Start(sInfo);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Comboload()
        {
            cmbEmployee.DataSource = Constants.ST_EMPLOYEES;
            cmbEmployee.DisplayMember = "name";
            cmbEmployee.ValueMember = "id";            

            cmbLocation.DataSource = Constants.ST_LOCATIONS;
            cmbLocation.DisplayMember = "name";
            cmbLocation.ValueMember = "hashid";
            
            cmbDoctype.DataSource = Constants.ST_DOCTYPES;
            cmbDoctype.DisplayMember = "name";
            cmbDoctype.ValueMember = "hashid";

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
                        string ext = Path.GetFileNameWithoutExtension(p.FullName);
                        AddPicList(img, ext, p.Name, false, true);
                    }
                    lblSelected.Text = String.Format("Selected {0} of {1} pages", 0, imageList.Count);
                }
            }
            catch (IOException ex)
            {
                FileLogger.LogStringInFile(ex.Message);
            }

        }

        private void frmScannerNew_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control)
            {
                MessageBox.Show("aaa");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (imageList.Count() > 0)
            {                
                foreach (ImageFile f in imageList.Values)
                {
                    if (f.MyCheck.Visible)
                    {
                        Image img = f.FileImage;
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        f.ViewImage = Constants.ResizeImageFixedWidth(img, Constants.IMAGE_WIDTH);
                        f.MyPicture.Image = f.ViewImage;

                        pnlPictures.Controls.Add(f.MyCheck);
                        pnlPictures.Controls.Add(f.MyPicture);
                        pnlPictures.Controls.Add(f.MyLabel);                        
                    }
                }

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (imageList.Count() > 0)
            {
                foreach (ImageFile f in imageList.Values)
                {
                    if (f.MyCheck.Visible)
                    {
                        Image img = f.FileImage;
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        f.ViewImage = Constants.ResizeImageFixedWidth(img, Constants.IMAGE_WIDTH);
                        f.MyPicture.Image = f.ViewImage;
                        f.SaveAll(Constants.FILE_PATH_TODAY);

                        pnlPictures.Controls.Add(f.MyCheck);
                        pnlPictures.Controls.Add(f.MyPicture);
                        pnlPictures.Controls.Add(f.MyLabel);
                    }
                }
            }
        }

        private void lblAll_Click(object sender, EventArgs e)
        {
            if (imageList.Count > 0)
            {
                RefreshPnl(true);
                lblOnly.ForeColor = Color.DarkGray;
                pnlOnly.BackColor = Color.DarkGray;
                lblUnselected.ForeColor = Color.DarkGray;
                pnlUnselected.BackColor = Color.DarkGray;
                lblAll.ForeColor = Color.Black;
                pnlAll.BackColor = Color.Red;
            }
        }

        private void lblOnly_Click(object sender, EventArgs e)
        {
            pnlPictures.Controls.Clear();

            lblOnly.ForeColor = Color.Black;
            pnlOnly.BackColor = Color.Red;
            lblUnselected.ForeColor = Color.DarkGray;
            pnlUnselected.BackColor = Color.DarkGray;
            lblAll.ForeColor = Color.DarkGray;
            pnlAll.BackColor = Color.DarkGray;

            selected = 0;
            foreach(ImageFile f in imageListSelected)
            {
                string ext = Path.GetExtension(f.FileName);               
                SelectedPicList(f.FileImage, ext, f.FileName, false, false);
                selected++;
            }
        }

        private void lblUnselected_Click(object sender, EventArgs e)
        {
            pnlPictures.Controls.Clear();

            lblOnly.ForeColor = Color.DarkGray;
            pnlOnly.BackColor = Color.DarkGray;
            lblUnselected.ForeColor = Color.Black;
            pnlUnselected.BackColor = Color.Red;
            lblAll.ForeColor = Color.DarkGray;
            pnlAll.BackColor = Color.DarkGray;

            RefreshPnl(false);
        }

        private void lblZoomOut_Click(object sender, EventArgs e)
        {
            if (lblAll.ForeColor == Color.Black)
            {
                int sizex = Constants.PIC_SIZEX + 50;
                int sizey = Constants.PIC_SIZEY + 50;

                Constants.PIC_SIZEX = sizex;
                Constants.PIC_SIZEY = sizey;
                Constants.IMAGE_WIDTH = Constants.IMAGE_WIDTH + 50;

                RefreshPnl(true);
            }
        }

        private void lblZoomIn_Click(object sender, EventArgs e)
        {
            if (lblAll.ForeColor == Color.Black)
            {
                
                int sizex = Constants.PIC_SIZEX - 50;
                int sizey = Constants.PIC_SIZEY - 50;
                if (sizex > 0)
                {
                    Constants.PIC_SIZEX = sizex;
                    Constants.PIC_SIZEY = sizey;
                    Constants.IMAGE_WIDTH = Constants.IMAGE_WIDTH - 50;
                }
                RefreshPnl(true);
            }
        }


        frmTwainLoading progressDialog = new frmTwainLoading();        
        frmMessage progressLoading = new frmMessage();

        private static AreaSettings AreaSettings = new AreaSettings(TwainDotNet.TwainNative.Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);
        
        Twain _twain;
        ScanSettings _settings;
        public void Init()
        {
            _twain = new Twain(new WinFormsWindowMessageHook(this));
            _twain.TransferImage += delegate (Object sender, TransferImageEventArgs args)
            {
                if (args.Image != null)
                {                   
                    ImageFile file = new Scannex.ImageFile();
                    file.FileImage = args.Image;
                    file.FileName = String.Format("{0}{1}", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), ".jpg");
                    file.ViewImage = args.Image;
                    AddPicList(args.Image, ".jpg", file.FileName, true, true);
                }
                pnlPictures.VerticalScroll.Value = pnlPictures.VerticalScroll.Maximum;
            };
            _twain.ScanningComplete += delegate
            {
                Enabled = true;
            };
        }
        private void Progress()
        {
            progressDialog.ShowDialog();
            progressDialog.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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

                Thread backgroundThread = new Thread(new ThreadStart(Progress));
                progressDialog = new frmTwainLoading();
                backgroundThread.Start();

                try
                {
                    _twain.StartScanning(_settings);
                    progressDialog.CloseForm();
                }
                catch (TwainException ex)
                {
                    FileLogger.LogStringInFile(ex.Message);
                    MessageBox.Show(ex.Message);
                    Enabled = true;
                    progressDialog.CloseForm();
                    Init();
                }
                finally
                {

                }
                backgroundThread.Abort();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            try
            {
                string json = "{";

                if (cmbEmployee.SelectedIndex == -1)
                {
                    if (cmbLocation.SelectedIndex == -1)
                    {
                        json += "\"location\":\"\",";
                        errorProvider1.SetError(cmbLocation, "Enter your location or employee");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(cmbLocation, "");
                        json += "\"location\":\"" + cmbLocation.SelectedValue.ToString() + "\",";
                        cmbEmployee.SelectedIndex = -1;
                        json += "\"employee\":\"\",";
                    }
                }
                else
                {
                    if (cmbLocation.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cmbLocation, "");
                        json += "\"location\":\"\",";
                        errorProvider1.SetError(cmbEmployee, "");
                        json += "\"employee\":\"" + cmbEmployee.SelectedValue.ToString() + "\",";

                    }
                    else
                    {
                        errorProvider1.SetError(cmbLocation, "");
                        json += "\"location\":\"" + cmbLocation.SelectedValue.ToString() + "\",";
                        cmbEmployee.SelectedIndex = -1;
                        json += "\"employee\":\"\",";
                    }
                }

                if (cmbDoctype.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cmbDoctype, "Enter your doc type information");
                    cmbDoctype.Focus();
                    return;
                }
                else
                {
                    errorProvider1.SetError(cmbDoctype, "");
                    json += "\"doc_type_id\":\"" + cmbDoctype.SelectedValue.ToString() + "\",";
                }

                if (imageListSelected.Count() == 0)
                {
                    errorProvider1.SetError(lblOnly, "Enter your upload files");
                    return;
                }

                this.Enabled = false;
                Thread backgroundThread = new Thread(new ThreadStart(MyThreadRoutine));
                progressLoading = new frmMessage();
                progressLoading.Title("Uploading");
                backgroundThread.Start();

                Save(json);

            }
            catch (Exception ex)
            {
                progressLoading.CloseForm();
                FileLogger.LogStringInFile(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressLoading.CloseForm();
            this.Enabled = true;
        }


        private void Save(object param)
        {
            string file = Convertpdf();
            string subjson = GetControlsValue();
            string json = param.ToString();

            if (subjson.Length > 0)
                subjson = subjson.Substring(0, subjson.Length - 1);
            json += "\"comment\":\"" + txtComment.Text + "\",";
            json += "\"fields\":[" + subjson + "],";
            if (file != "")
            {
                string path = Constants.FILE_PATH_TODAY + "\\UPLOAD\\";

                byte[] bytes = System.IO.File.ReadAllBytes(path + file);

                string ret = ServerConnections.ServerFile(file, bytes);
                var postMessage = Constants.Deserialize<PostResponse>(ret);
                if (postMessage != null)
                {
                    string aws = Newtonsoft.Json.JsonConvert.SerializeObject(postMessage);

                    json += "\"s3-response\":" + aws + "}";

                    ret = ServerConnections.ServerPostData("/api/s3doc", json);

                    if (ret == "OK")
                    {
                        progressLoading.CloseForm();
                        this.Activate();
                        MessageBox.Show("File saved successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlPictures.Controls.Clear();
                        imageList = new Dictionary<int, ImageFile>();
                        DeleteUploadFiles(path);
                        ResetControl();
                        LoadFolder();
                    }
                    else
                    {
                        progressLoading.CloseForm();
                        this.TopMost = true;
                        MessageBox.Show(ret, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        #region File
        private void ResetControl()
        {
            cmbEmployee.SelectedIndex = -1;
            cmbLocation.SelectedIndex = -1;
            cmbDoctype.SelectedIndex = -1;
            txtComment.Text = "";
            pnlAdd.Controls.Clear();
            errorProvider1.SetError(cmbEmployee, "");
            errorProvider1.SetError(cmbLocation, "");
            errorProvider1.SetError(cmbDoctype, "");
            errorProvider1.SetError(lblOnly, "");
        }

        private string Convertpdf()
        {
            string ret = "";
            if (imageListSelected.Count > 0)
            {
                string path = Constants.FILE_PATH_TODAY + "\\UPLOAD\\";
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                try
                {
                    string name = cmbEmployee.Text;
                    string filename = String.Format("{0}_{1}.{2}", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), name.Trim(), "pdf");
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(path + filename, FileMode.Create));

                    document.Open();

                    foreach (ImageFile image in imageListSelected)
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
                    MessageBox.Show(ioe.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                document.Close();
            }
            return ret;
        }

        private void DeleteUploadFiles(string path)
        {
            List<string> delFile = new List<string>();
            foreach (ImageFile image in imageListSelected)
            {
                image.SaveAll(path);
                delFile.Add(image.FileName);
                image.FileImage.Dispose();
            }
            imageListSelected.Clear();
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
        }

        #endregion

        private void cmbDoctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDoctype.SelectedIndex != -1)
            {
                pnlAdd.Controls.Clear();
                foreach (DocTypes t in Constants.ST_DOCTYPES)
                {
                    if (t.hashid == cmbDoctype.SelectedValue.ToString())
                    {
                        CreateElem(t.fields);
                        break;
                    }
                }
            }
        }

        #region ComponentForAdd
        private void CreateElem(List<SubTypes> l)
        {
            int p = 0;
            int y = 0;
            int f = 0;
            foreach (SubTypes s in l)
            {
                int add = 18;
                int x = 18;
                y = p + 6;
                Label lb = new System.Windows.Forms.Label();
                lb.Name = Guid.NewGuid().ToString();
                lb.Text = s.name;
                lb.AutoSize = true;
                lb.Location = new Point(x, y);

                switch (s.type)
                {
                    case "date":
                        DateTimePicker dp = new DateTimePicker();
                        dp.Name = Guid.NewGuid().ToString();
                        dp.Tag = s.hashid;
                        dp.CustomFormat = "yyyy-MM-dd";
                        dp.Size = new Size(165, 22);
                        dp.Value = DateTime.Now;
                        dp.Format = DateTimePickerFormat.Custom;
                        dp.Location = new Point(x, y + add);
                        y += 42;
                        pnlAdd.Controls.Add(lb);
                        pnlAdd.Controls.Add(dp);
                        break;
                    case "checkbox":
                        CheckBox chk = new CheckBox();
                        chk.Name = Guid.NewGuid().ToString();
                        chk.Size = new Size(165, 22);
                        chk.Tag = s.hashid;
                        chk.Text = s.name;
                        if (f == 0)
                            chk.Location = new Point(x, y + add);
                        else
                            chk.Location = new Point(x, y + add - 20);
                        y += 42;
                        pnlAdd.Controls.Add(chk);
                        break;
                    case "shorttext":
                        TextBox txt = new TextBox();
                        txt.Name = Guid.NewGuid().ToString();
                        txt.Size = new Size(165, 22);
                        txt.Tag = s.hashid;
                        txt.Location = new Point(x, y + add);                    
                        y += 42;
                        pnlAdd.Controls.Add(lb);
                        pnlAdd.Controls.Add(txt);
                        break;
                    case "longtext":
                        TextBox txtM = new TextBox();
                        txtM.Name = Guid.NewGuid().ToString();
                        txtM.Multiline = true;
                        txtM.Size = new Size(165, 45);
                        txtM.Tag = s.hashid;
                        //y = i * 10 + p + 45;
                        txtM.Location = new Point(x, y + add);
                        y += 62;
                        pnlAdd.Controls.Add(lb);
                        pnlAdd.Controls.Add(txtM);
                        break;
                    case "dropdown":
                        ComboBox cmb = new ComboBox();
                        cmb.Name = Guid.NewGuid().ToString();
                        cmb.Size = new Size(165, 45);
                        cmb.Tag = s.hashid;
                        if (s.options != null)
                        {
                            string[] v = s.options.Split(',');
                            foreach (string d in v)
                            {
                                cmb.Items.Add(d.Trim());
                            }
                        }
                        cmb.Location = new Point(x, y + add);
                        pnlAdd.Controls.Add(lb);
                        pnlAdd.Controls.Add(cmb);
                        y += 42;
                        break;
                }
                f++;
                p = y;
            }
        }

        public string GetControlsValue()
        {
            string ret = "";
            string v = "";
            string h = "";
            string json = "";
            bool isEnter = false;

            foreach (Control c in pnlAdd.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    v = ((TextBox)c).Text == null ? "" : ((TextBox)c).Text;
                    h = ((TextBox)c).Tag.ToString();
                    isEnter = true;
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    v = ((ComboBox)c).Text;
                    h = ((ComboBox)c).Tag.ToString();
                    isEnter = true;
                }
                else if (c.GetType() == typeof(CheckBox))
                {
                    v = ((CheckBox)c).Checked.ToString();
                    h = ((CheckBox)c).Tag.ToString();
                    isEnter = true;
                }
                else if (c.GetType() == typeof(DateTimePicker))
                {
                    v = ((DateTimePicker)c).Value.ToString("yyyy-MM-dd");
                    h = ((DateTimePicker)c).Tag.ToString();
                    isEnter = true;
                }
                if (isEnter)
                {
                    json = String.Format("\"field_id\":\"{0}\",\"value\":\"{1}\"", h, v);
                    ret += "{" + json + "},";
                    isEnter = false;
                }
            }

            return ret;
        }

        #endregion

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(cmbLocation, "");

            if (cmbEmployee.SelectedIndex != -1)
            {
                cmbLocation.SelectedIndex = -1;
            }
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(cmbEmployee, "");
            if (cmbLocation.SelectedIndex != -1)
            {
                cmbEmployee.SelectedIndex = -1;
            }
        }


        private void MyThreadRoutine()
        {
            progressLoading.ShowDialog();
            progressLoading.Dispose();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmOptions frm = new frmOptions();
            frm.ShowDialog();
        }
    }
}
