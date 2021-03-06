﻿using System;
using System.Collections.Concurrent;
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
        private static AreaSettings AreaSettings = new AreaSettings(TwainDotNet.TwainNative.Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);

        Twain _twain;
        ScanSettings _settings;

        frmTwainLoading progressDialog = new frmTwainLoading();
        frmMessage progressLoading = new frmMessage();
        private System.Windows.Forms.Timer mTimer;
        private int mDialogCount;
        private bool mouseDown;
        private Point lastLocation;


        public void Init()
        {
            try
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
            catch (Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
            }
        }

        int selected = 0;
        int unselected = 0;

        public frmScannerNew()
        {
            try
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
            catch (Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
            }
        }
             
        private void FrmScannerNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control && e.KeyCode == Keys.A)
            {
                MessageBox.Show("ctrl +a ");
            }
        }

        #region Zoom

        private void lblZoomIn_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Black;
        }

        private void lblZoomIn_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.DimGray;
        }

        private void lblZoomOut_Click(object sender, EventArgs e)
        {
            if (lblAll.ForeColor == Color.Black)
            {
                int sizex = Constants.PIC_SIZEX + 30;
                int sizey = Constants.PIC_SIZEY + 30;
                if (Constants.PIC_MAXSIZEX >= sizex)
                {
                    Constants.PIC_SIZEX = sizex;
                    Constants.PIC_SIZEY = sizey;
                    Constants.IMAGE_WIDTH = Constants.IMAGE_WIDTH + 30;
                    int t = (pnlPictures.Width) / (Constants.PIC_SIZEX + Constants.PADDING_SIZE);
                    Constants.PAGE_SIZE = (short)t;

                    RefreshPnl(true);
                }
            }
        }

        private void lblZoomIn_Click(object sender, EventArgs e)
        {
            if (lblAll.ForeColor == Color.Black)
            {

                int sizex = Constants.PIC_SIZEX - 30;
                int sizey = Constants.PIC_SIZEY - 30;
                if (Constants.PIC_MINSIZEX <= sizex)
                {

                    if (sizex > 0)
                    {
                        Constants.PIC_SIZEX = sizex;
                        Constants.PIC_SIZEY = sizey;
                        Constants.IMAGE_WIDTH = Constants.IMAGE_WIDTH - 30;
                        int t = (pnlPictures.Width) / (Constants.PIC_SIZEX + Constants.PADDING_SIZE);
                        Constants.PAGE_SIZE = (short)t;
                    }
                    RefreshPnl(true);
                }
            }
        }

        #endregion

        #region PicCreate
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
            com.MouseDown += pnlPictures_MouseDown;
            com.Image = f.ViewImage;
            com.Name = f.FileName;

            PictureBox check = new PictureBox();
            
            check.Location = new Point(x, y);
            check.BackColor = Color.Transparent;
            check.Padding = new Padding(2);
            check.Size = new Size(42, 42);
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
            
            check.Image = Scannex.Properties.Resources.zov3;
            

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
            check.Size = new Size(32, 32);
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
            check.Size = new Size(32, 32);
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

        private void Com_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        private void Com_MouseHover(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }

        #endregion
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

                    for (int i = 0; i < imageListSelected.Count(); i++)
                    {
                        if (imageListSelected[i].MyPicture.Name == name)
                        {
                            imageListSelected.RemoveAt(i);
                            break;
                        }
                    }

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
            errorProvider1.SetError(lblOnly, "");
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
      
        
        #region FormMove
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
            //this.Close();
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        bool pnlRight = false;
        bool pnlLeft = false;
        bool pnlTop = false;
        bool pnlResize = false;
        Constants.CORNER CORNER;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!pnlRight && !pnlLeft && !pnlTop && !pnlResize)
            {
                this.Height = new Size(MousePosition).Height - this.Top;
            }
            else if (pnlRight)
            {
                if (this.MinimumSize.Width < (new Size(MousePosition)).Width - this.Left)
                {
                    int width = Screen.PrimaryScreen.WorkingArea.Width;
                    this.Width = (new Size(MousePosition)).Width - this.Left;
                }
            }
            else if (pnlLeft)
            {
                int width = Screen.PrimaryScreen.WorkingArea.Width;
                if (this.MinimumSize.Width < width - (new Size(MousePosition)).Width)
                {
                    this.Width = width - (new Size(MousePosition)).Width;
                    this.Left = MousePosition.X;
                }
            }
            else if (pnlTop)
            {
                int heigh = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.MinimumSize.Height < heigh - (new Size(MousePosition)).Height)
                {                    
                    this.Height = heigh - (new Size(MousePosition)).Height;
                    this.Top = MousePosition.Y;
                }
            }
            else if (pnlResize)
            {
                if (CORNER == Constants.CORNER.BOTTOM_RIGHT)
                {
                    if (this.MinimumSize.Width < MousePosition.X - this.Left && this.MinimumSize.Height < MousePosition.Y - this.Top)
                    {
                        this.Size = new Size(MousePosition.X - this.Left, MousePosition.Y - this.Top);
                    }
                }
                else if (CORNER == Constants.CORNER.BOTTOM_LEFT)
                {
                    int width = Screen.PrimaryScreen.WorkingArea.Width;
                    int heigth = Screen.PrimaryScreen.WorkingArea.Height;
                    if (this.MinimumSize.Width < width - MousePosition.X && this.MinimumSize.Height < MousePosition.Y)
                    {
                        this.Size = new Size(width - MousePosition.X, MousePosition.Y);
                        this.Left = MousePosition.X;
                    }
                }
                else if (CORNER == Constants.CORNER.TOP_LEFT)
                {
                    int width = Screen.PrimaryScreen.WorkingArea.Width;
                    int heigth = Screen.PrimaryScreen.WorkingArea.Height;
                    if (this.MinimumSize.Width < width - MousePosition.X && this.MinimumSize.Height < heigth - MousePosition.Y)
                    {
                        this.Size = new Size(width - MousePosition.X, heigth - MousePosition.Y);
                        this.Left = MousePosition.X;
                        this.Top = MousePosition.Y;
                    }
                }
                else if (CORNER == Constants.CORNER.TOP_RIGHT)
                {
                    int width = Screen.PrimaryScreen.WorkingArea.Width;
                    int heigth = Screen.PrimaryScreen.WorkingArea.Height;
                    if (this.MinimumSize.Width < MousePosition.X && this.MinimumSize.Height < heigth - MousePosition.Y)
                    {
                        this.Size = new Size(MousePosition.X, heigth - MousePosition.Y);
                        this.Top = MousePosition.Y;
                    }
                }
            }
        }
        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (((Panel)sender).Name == "panel6")
            {
                pnlRight = true;
                pnlLeft = false;
                pnlTop = false;
                pnlResize = false;
            }
            else if (((Panel)sender).Name == "panel10")
            {
                pnlResize = true;
                pnlLeft = false;
                pnlRight = false;
                pnlTop = false;
                CORNER = Constants.CORNER.BOTTOM_RIGHT;
            }
            else if (((Panel)sender).Name == "panel11")
            {
                pnlRight = false;
                pnlLeft = false;
                pnlTop = false;
                pnlResize = true;
                CORNER = Constants.CORNER.TOP_LEFT;
            }
            else if (((Panel)sender).Name == "panel12")
            {
                pnlRight = false;
                pnlLeft = false;
                pnlTop = false;
                pnlResize = true;
                CORNER = Constants.CORNER.TOP_RIGHT;
            }
            else if (((Panel)sender).Name == "panel13")
            {
                pnlRight = false;
                pnlLeft = false;
                pnlTop = false;
                pnlResize = true;
                CORNER = Constants.CORNER.BOTTOM_LEFT;
            }
            else if (((Panel)sender).Name == "panel7")
            {
                pnlLeft = true;
                pnlRight = false;
                pnlTop = false;
                pnlResize = false;
            }
            else if (((Panel)sender).Name == "panel8")
            {
                pnlTop = true;
                pnlLeft = false;
                pnlRight = false;
                pnlResize = false;
            }          
            else
            {
                pnlRight = false;
                pnlLeft = false;
                pnlTop = false;
                pnlResize = false;
                CORNER = Constants.CORNER.EMPTY;
            }

            timer1.Enabled = true;
            timer1.Interval = 3;
        }


        #endregion
               
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
            this.Left = this.Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            int t = (pnlPictures.Width) / (Constants.PIC_SIZEX + Constants.PADDING_SIZE);
            Constants.PAGE_SIZE = (short)t;
            int c = (pnlPictures.Width) / 2;
            

            frmLogin frmshow = new frmLogin();
            if (frmshow.ShowDialog() == DialogResult.OK)
            {
                Constants.ST_LOCATIONS = ServerConnections.ServerGETData<List<Locations>>("api/scannex/v2/locations");
                Constants.ST_EMPLOYEES = ServerConnections.ServerGETData<List<Employees>>("api/scannex/v2/employees");
                Constants.ST_DOCTYPES = ServerConnections.ServerGETData<List<DocTypes>>("api/scannex/v2/doctypes");
                Constants.USER = ServerConnections.ServerGETData<Info>("api/scannex/v2/user");
                lblCompany.Text = Constants.USER.client_name;
                mTimer.Interval = Constants.EXPIRE_TIME * 1000;
                mTimer.Enabled = true;
                lblStatus.Text = String.Format("Logged in as {0}", Constants.USER.name);


                Comboload();
                LoadFolder();
                
                cmbDoctype.SelectedIndex = -1;
                cmbEmployee.SelectedIndex = -1;
                cmbLocation.SelectedIndex = -1;
                cmbEmployee.Text = "Employee";
                cmbLocation.Text = "Location";
                cmbDoctype.Text = "Document Type";
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
            lblOnly.ForeColor = Color.DarkGray;
            pnlOnly.BackColor = Color.DarkGray;
            lblUnselected.ForeColor = Color.DarkGray;
            pnlUnselected.BackColor = Color.DarkGray;
            lblAll.ForeColor = Color.Black;
            pnlAll.BackColor = Color.Red;
            if (imageList.Count > 0)
            {
                RefreshPnl(true);               
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
        
              
        private void Progress()
        {
            progressDialog.ShowDialog();
            progressDialog.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            progressDialog = new frmTwainLoading();
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
                    this.Activate();
                    progressDialog.CloseForm();                    
                }
                catch (TwainException ex)
                {
                    progressDialog.CloseForm();
                    this.Activate();
                    FileLogger.LogStringInFile(ex.Message);
                    MessageBox.Show(ex.Message);
                    Enabled = true;
                    
                    Init();
                }
                finally
                {

                }
                backgroundThread.Abort();
            }
        }

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        //CancellationToken token = tokenSource.Token;
        public bool isOk = false;

        private void button1_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            isOk = false;
            CancellationToken token = tokenSource.Token;

            try
            {
                string json = "{";

                if (cmbEmployee.SelectedIndex == -1)
                {
                    if (cmbLocation.SelectedIndex == -1)
                    {
                        json += "\"location\":\"\",";
                        errorProvider1.SetError(cmbLocation, "Enter Employee or Location.");
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
                    errorProvider1.SetError(cmbDoctype, "Enter Document Type.");
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
                    errorProvider1.SetError(lblOnly, "Select upload files.");
                    return;
                }
                string name = cmbEmployee.SelectedIndex == -1 ? "" : cmbEmployee.Text;
                string forn = cmbEmployee.SelectedIndex == -1 ? "" : cmbEmployee.Text;
                if (name == String.Empty)
                {
                    name = Constants.USERNAME;
                    forn = cmbLocation.Text;
                }

                string subjson = GetControlsValue();

                Task t;
                var tasks = new ConcurrentBag<Task>();
                               
                this.Enabled = false;
                Thread backgroundThread = new Thread(new ThreadStart(MyThreadRoutine));
                progressLoading = new frmMessage();
                progressLoading.tokenSource = this.tokenSource;
                progressLoading.Message(cmbDoctype.Text, forn);
                progressLoading.StartPosition = FormStartPosition.CenterParent;
                progressLoading.Title("Uploading");
                backgroundThread.Start();
                progressLoading.Activate();
                //Save(json);

                t = Task.Factory.StartNew(() => Save(json, name, subjson, token), token);                
                tasks.Add(t);
                string path = Constants.FILE_PATH_TODAY + "\\UPLOAD\\";
               
                try
                {
                    Task.WaitAll(tasks.ToArray());

                    if (isOk)
                    {
                        pnlPictures.Controls.Clear();
                        DeleteUploadFiles(path);
                        ResetControl();
                        LoadFolder();
                    }
                }               
                catch (AggregateException err)
                {
                    FileLogger.LogStringInFile(err.Message);
                    progressLoading.CloseForm();
                    this.Activate();
                    MessageBox.Show("Command cancelled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    tokenSource.Dispose();
                }

            }
            catch (Exception ex)
            {
                progressLoading.CloseForm();
                this.Activate();
                FileLogger.LogStringInFile(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //progressLoading.CloseForm();
            this.Enabled = true;
        }
        

        private void Save(object param, string name, string subjson, CancellationToken ct)
        {
            try
            {
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }
                string file = Convertpdf(name);
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }
               
                string json = param.ToString();

                if (subjson.Length > 0)
                    subjson = subjson.Substring(0, subjson.Length - 1);
                json += "\"comment\":\"" + txtComment.Text + "\",";
                json += "\"fields\":[" + subjson + "],";

                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }
                if (file != "")
                {
                    string path = Constants.FILE_PATH_TODAY + "\\UPLOAD\\";

                    byte[] bytes = System.IO.File.ReadAllBytes(path + file);
                    if (ct.IsCancellationRequested)
                    {
                        ct.ThrowIfCancellationRequested();
                    }
                    string ret = ServerConnections.ServerFile(file, bytes);
                    if (ct.IsCancellationRequested)
                    {
                        ct.ThrowIfCancellationRequested();
                    }
                    var postMessage = Constants.Deserialize<PostResponse>(ret);
                    if (ct.IsCancellationRequested)
                    {
                        ct.ThrowIfCancellationRequested();
                    }
                    if (postMessage != null)
                    {
                        string aws = Newtonsoft.Json.JsonConvert.SerializeObject(postMessage);

                        json += "\"s3-response\":" + aws + "}";

                        if (ct.IsCancellationRequested)
                        {
                            ct.ThrowIfCancellationRequested();
                        }
                        ret = ServerConnections.ServerPostData("/api/s3doc", json);
                        if (ct.IsCancellationRequested)
                        {
                            ct.ThrowIfCancellationRequested();
                        }
                        if (ret == "OK")
                        {
                            progressLoading.Done();
                            //this.Activate();
                            //MessageBox.Show("File saved successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            imageList = new Dictionary<int, ImageFile>();
                            isOk = true;
                        }
                        else
                        {
                            progressLoading.CloseForm();
                            this.Activate();
                            MessageBox.Show(ret, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (TaskCanceledException tce)
            {
                FileLogger.LogStringInFile(tce.Message);
            }
        }


        #region File
        private void ResetControl()
        {
            cmbEmployee.SelectedIndex = -1;
            cmbLocation.SelectedIndex = -1;
            cmbDoctype.SelectedIndex = -1;
            cmbEmployee.Text = "Employee";
            cmbLocation.Text = "Location";
            cmbDoctype.Text = "Document Type";
            txtComment.Text = "";
            pnlAdd.Controls.Clear();
            errorProvider1.SetError(cmbEmployee, "");
            errorProvider1.SetError(cmbLocation, "");
            errorProvider1.SetError(cmbDoctype, "");
            errorProvider1.SetError(lblOnly, "");
        }

        private string Convertpdf(string name)
        {
            string ret = "";
            if (imageListSelected.Count > 0)
            {
                string path = Constants.FILE_PATH_TODAY + "\\UPLOAD\\";
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                try
                {
                    //string name = cmbEmployee.Text;
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

            //this.Invoke(new Action(() =>
            //{
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

            //}));
            return ret;
        }

        #endregion

        #region ComboChange
        private void cmbDoctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDoctype.SelectedIndex != -1)
            {
                errorProvider1.SetError(cmbDoctype, "");
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

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(cmbEmployee, "");
            errorProvider1.SetError(cmbLocation, "");
            if (cmbEmployee.SelectedIndex != -1)
            {
                cmbLocation.SelectedIndex = -1;
            }
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(cmbEmployee, "");
            errorProvider1.SetError(cmbLocation, "");
            if (cmbLocation.SelectedIndex != -1)
            {
                cmbEmployee.SelectedIndex = -1;
            }
        }

        #endregion
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

        #region Mouseover

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Scannex.Properties.Resources.Scan_button__mouseover_;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Scannex.Properties.Resources.Scan_button__normal_;
        }

        private void btnImport_MouseHover(object sender, EventArgs e)
        {
            btnImport.Image = Scannex.Properties.Resources.Import_button__mouseover_;
        }

        private void btnImport_MouseLeave(object sender, EventArgs e)
        {
            btnImport.Image = Scannex.Properties.Resources.Import_button__normal_;
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Image = Scannex.Properties.Resources.Settings_button__mouseover_;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Scannex.Properties.Resources.Settings_button__normal_;
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            pictureBox5.Image = Scannex.Properties.Resources.Help_button__mouseover_;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = Scannex.Properties.Resources.Help_button__normal_;
        }     

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Image = Scannex.Properties.Resources.error_over;
        }
              
        private void pictureBox3_MouseLeave_1(object sender, EventArgs e)
        {
            pictureBox3.Image = Scannex.Properties.Resources.error50;
        }
        
        #endregion

        private void tStripMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem click = (ToolStripMenuItem)sender;
            if (click != null)
            {
                switch(click.Tag.ToString())
                {
                    case "rr": // rotate this page right
                        if (RightpictureBox != null)
                        {
                            foreach (ImageFile f in imageList.Values)
                            {
                                if (f.MyPicture == RightpictureBox)
                                {
                                    Image img = f.FileImage;
                                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                    f.ViewImage = Constants.ResizeImageFixedWidth(img, Constants.IMAGE_WIDTH);
                                    f.MyPicture.Image = f.ViewImage;

                                    pnlPictures.Controls.Add(f.MyCheck);
                                    pnlPictures.Controls.Add(f.MyPicture);
                                    pnlPictures.Controls.Add(f.MyLabel);
                                    break;
                                }
                            }
                        }
                        break;
                    case "rl":// rotate this page left
                        if (RightpictureBox != null)
                        {
                            foreach (ImageFile f in imageList.Values)
                            {
                                if (f.MyPicture == RightpictureBox)
                                {
                                    Image img = f.FileImage;
                                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    f.ViewImage = Constants.ResizeImageFixedWidth(img, Constants.IMAGE_WIDTH);
                                    f.MyPicture.Image = f.ViewImage;
                                    f.SaveAll(Constants.FILE_PATH_TODAY);

                                    pnlPictures.Controls.Add(f.MyCheck);
                                    pnlPictures.Controls.Add(f.MyPicture);
                                    pnlPictures.Controls.Add(f.MyLabel);
                                    break;
                                }
                            }
                        }
                        break;
                    case "rar":// rotate all  page left
                        foreach (ImageFile f in imageList.Values)
                        {
                            Image img = f.FileImage;
                            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            f.ViewImage = Constants.ResizeImageFixedWidth(img, Constants.IMAGE_WIDTH);
                            f.MyPicture.Image = f.ViewImage;
                            f.SaveAll(Constants.FILE_PATH_TODAY);

                            pnlPictures.Controls.Add(f.MyCheck);
                            pnlPictures.Controls.Add(f.MyPicture);
                            pnlPictures.Controls.Add(f.MyLabel);
                        }
                        break;
                    case "ral":// rotate all  page left
                        foreach (ImageFile f in imageList.Values)
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
                        break;
                    case "sa":// Select all
                        imageListSelected.Clear();
                        foreach (ImageFile f in imageList.Values)
                        {
                            if (!imageListSelected.Contains(f))
                                imageListSelected.Add(f);
                            f.MyCheck.Visible = true;

                            f.MyCheck.Location = new Point(f.MyPicture.Location.X, f.MyPicture.Location.Y);
                            pnlPictures.Controls.Add(f.MyCheck);
                            pnlPictures.Controls.Add(f.MyPicture);
                            pnlPictures.Controls.Add(f.MyLabel);
                        }
                        errorProvider1.SetError(lblOnly, "");                        
                        break;
                    case "de":// Deselect all
                        foreach (ImageFile f in imageList.Values)
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
                            f.MyCheck.Location = new Point(f.MyPicture.Location.X, f.MyPicture.Location.Y);
                            pnlPictures.Controls.Add(f.MyCheck);
                            pnlPictures.Controls.Add(f.MyPicture);
                            pnlPictures.Controls.Add(f.MyLabel);
                        }
                        imageListSelected.Clear();
                        break;
                    case "ret":// Remove this page
                        foreach (int f in imageList.Keys)
                        {
                            if (imageList[f].MyPicture == RightpictureBox)
                            {                                
                                imageList.Remove(f);
                                for (int i = 0; i < imageListSelected.Count(); i++)
                                {
                                    if (imageListSelected[i].MyPicture == RightpictureBox)
                                    {
                                        imageListSelected.RemoveAt(i);
                                        break;
                                    }
                                }
                                RefreshPnl(true);
                                break;
                            }
                        }
                        break;
                    case "res":// Remove selected pages
                        for (int i = 0; i < imageListSelected.Count(); i++)
                        {
                            foreach (int f in imageList.Keys)
                            {
                                if (imageList[f].MyPicture == imageListSelected[i].MyPicture)
                                {
                                    imageList.Remove(f);
                                    break;
                                }
                            }
                            imageListSelected.RemoveAt(i);
                        }
                        imageListSelected.Clear();
                        RefreshPnl(true);
                        break;
                    case "rea":// Remove ALL page
                        imageList.Clear();
                        imageListSelected.Clear();                       
                        RefreshPnl(true);
                        break;
                }
                lblSelected.Text = String.Format("Selected {0} of {1} pages", imageListSelected.Count, imageList.Count);
            }
        }

        PictureBox RightpictureBox;

        private void pnlPictures_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var relativeClickedPosition = e.Location;
                var screenClickedPosition = (sender as Control).PointToScreen(relativeClickedPosition);
                if (typeof(PictureBox) == sender.GetType())
                {
                    RightpictureBox = (PictureBox)sender;
                    tStripMenu.Enabled = true;
                    toolStripMenuItem2.Enabled = true;
                    toolStripMenuItem7.Enabled = true;
                }
                else
                {
                    RightpictureBox = null;
                    tStripMenu.Enabled = false;
                    toolStripMenuItem2.Enabled = false;
                    toolStripMenuItem7.Enabled = false;
                }
                contextMenuStrip1.Show(screenClickedPosition);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Left = this.Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Left = this.Top = 0;
            this.Width = this.MinimumSize.Width;
            this.Height = this.MinimumSize.Height;
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.Image = Scannex.Properties.Resources.minimize75;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = Scannex.Properties.Resources.minimize50;
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.Image = Scannex.Properties.Resources.expand_button75;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Image = Scannex.Properties.Resources.expand_button50;
        }
    }
}
