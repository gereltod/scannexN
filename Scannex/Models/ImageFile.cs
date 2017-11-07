using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scannex
{
    public class ImageFile
    {
        public string FileName { get; set; }
        public Image FileImage { get; set; }
        public Image ViewImage { get; set; }
        public PictureBox MyPicture { get; set; }

        public void SaveAll(string path)
        {
            this.FileImage.Save(path + this.FileName);
        }
    }
}
