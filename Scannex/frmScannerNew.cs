using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scannex
{
    public partial class frmScannerNew : Form
    {
        Dictionary<int, ImageFile> imageList = new Dictionary<int, ImageFile>();

        public frmScannerNew()
        {
            InitializeComponent();
            (pnlPictures as Control).KeyDown += new KeyEventHandler(FrmScannerNew_KeyDown);
        }

        private void FrmScannerNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control && e.KeyCode == Keys.A)
            {
                MessageBox.Show("ctrl +a ");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.Refresh();
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.Refresh();
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|"
                                            + "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff";
            openFileDialog1.Title = "Please select an image file";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int maxId = imageList.Count();

                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        maxId++;
                        string ext = Path.GetExtension(file);
                       
                        Image loadedImage = Image.FromFile(file);
                        ImageFile f = new ImageFile();
                        f.FileImage = loadedImage;
                        f.ViewImage = Constants.ResizeImageFixedWidth(loadedImage, Constants.IMAGE_WIDTH);
                       
                        f.FileName = String.Format("{0}{1}", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), ext);
                        imageList.Add(maxId, f);

                        PictureBox com = new PictureBox();
                        com.Location = new Point(20, 20);
                        com.BackColor = Color.White;
                        com.Size = new Size(265, 350);
                        com.SizeMode = PictureBoxSizeMode.CenterImage;
                        com.MouseHover += Com_MouseHover;
                        com.MouseLeave += Com_MouseLeave;
                        com.Image = f.ViewImage;

                        //pnlPictures.Controls.Add(com);
                        //PictureBox com1 = new PictureBox();
                        //com1.Location = new Point(305, 20);
                        //com1.BackColor = Color.White;
                        //com1.Size = new Size(265, 350);
                        //com1.SizeMode = PictureBoxSizeMode.CenterImage;
                        //com1.MouseHover += Com_MouseHover;
                        //com1.MouseLeave += Com_MouseLeave;
                        //com1.Image = f.ViewImage;
                        //pnlPictures.Controls.Add(com1);

                        //PictureBox com2 = new PictureBox();
                        //com2.Location = new Point(590, 20);
                        //com2.BackColor = Color.White;
                        //com2.Size = new Size(265, 350);
                        //com2.SizeMode = PictureBoxSizeMode.CenterImage;
                        //com2.MouseHover += Com_MouseHover;
                        //com2.MouseLeave += Com_MouseLeave;
                        //com2.Image = f.ViewImage;
                        //pnlPictures.Controls.Add(com2);

                        //PictureBox com3 = new PictureBox();
                        //com3.Location = new Point(875, 20);
                        //com3.BackColor = Color.White;
                        //com3.Size = new Size(265, 350);
                        //com3.SizeMode = PictureBoxSizeMode.CenterImage;
                        //com3.MouseHover += Com_MouseHover;
                        //com3.MouseLeave += Com_MouseLeave;
                        //com3.Image = f.ViewImage;
                        //pnlPictures.Controls.Add(com3);


                        pnlPictures.Controls.Add(com);
                        PictureBox com1 = new PictureBox();
                        com1.Location = new Point(20, 390);
                        com1.BackColor = Color.White;
                        com1.Size = new Size(265, 350);
                        com1.SizeMode = PictureBoxSizeMode.CenterImage;
                        com1.MouseHover += Com_MouseHover;
                        com1.MouseLeave += Com_MouseLeave;
                        com1.Image = f.ViewImage;
                        pnlPictures.Controls.Add(com1);

                        PictureBox com2 = new PictureBox();
                        com2.Location = new Point(20, 760);
                        com2.BackColor = Color.White;
                        com2.Size = new Size(265, 350);
                        com2.SizeMode = PictureBoxSizeMode.CenterImage;
                        com2.MouseHover += Com_MouseHover;
                        com2.MouseLeave += Com_MouseLeave;
                        com2.Image = f.ViewImage;
                        pnlPictures.Controls.Add(com2);

                        PictureBox com3 = new PictureBox();
                        com3.Location = new Point(20, 1130);
                        com3.BackColor = Color.White;
                        com3.Size = new Size(265, 350);
                        com3.SizeMode = PictureBoxSizeMode.CenterImage;
                        com3.MouseHover += Com_MouseHover;
                        com3.MouseLeave += Com_MouseLeave;
                        com3.Image = f.ViewImage;
                        pnlPictures.Controls.Add(com3);

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
    }
}
