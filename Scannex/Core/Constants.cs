using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

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

        public static string USERNAME = "";
        public static bool ISLOGIN;


        public static List<Locations> ST_LOCATIONS;
        public static List<Employees> ST_EMPLOYEES;
        public static List<DocTypes> ST_DOCTYPES;


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

    }
}
