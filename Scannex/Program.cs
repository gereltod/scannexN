using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Scannex
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Constants.ERROR_PATH = Application.StartupPath + "\\Logs\\";
            Constants.FILE_PATH = System.Configuration.ConfigurationManager.AppSettings["FILEPATH"].ToString();
            Constants.DELETE_DAY = double.Parse(System.Configuration.ConfigurationManager.AppSettings["DELETEDAY"].ToString());

            FileLogger.InitLog(Constants.ERROR_PATH, Constants.FILE_NAME);

            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
