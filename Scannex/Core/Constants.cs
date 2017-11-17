using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Scannex
{
    public static class Constants
    {
        public static string FILE_NAME = "scannex.log";
        public static string SERVER_URL = "http://smartdrawers.net/";
        public static string ERROR_PATH = "";
        public static string FILE_PATH = "";
        public static string FILE_PATH_TODAY = "";
        public static double DELETE_DAY = 30;
        public static int EXPIRE_TIME = 2000;
        public static int IMAGE_WIDTH = 218;
        public static int IMAGETHUMB_WIDTH = 540;
        public static string USERNAME = "";
        public static bool ISLOGIN;

        public static int PADDING_SIZE = 30;
        public static int PIC_SIZEX = 220;
        public static int PIC_SIZEY = 330;
        public static Int16 PAGE_SIZE = 4;

        public static List<Locations> ST_LOCATIONS;
        public static List<Employees> ST_EMPLOYEES;
        public static List<DocTypes> ST_DOCTYPES;
        public static Info USER;


        public enum CORNER
        {
            EMPTY,
            TOP_LEFT = 0,
            TOP_RIGHT = 1,
            BOTTOM_RIGHT = 2,
            BOTTOM_LEFT = 3
        }
        public static T Deserialize<T>(this string toDeserialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringReader textReader = new StringReader(toDeserialize);
            return (T)xmlSerializer.Deserialize(textReader);
        }

        public static string Serialize<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();
        }

        public static Image ResizeImageFixedWidth(Image imgToResize, int width)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = ((float)width / (float)sourceWidth);

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

    }
}
