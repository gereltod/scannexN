using System;
using System.Windows.Forms;
using System.Configuration;

namespace Scannex
{
    public partial class frmOptions : Form
    {
        public frmOptions()
        {
            InitializeComponent();
        }

        private void SaveConfig()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string configFile = System.IO.Path.Combine(appPath, "Scannex.exe.config");
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            config.AppSettings.Settings["DELETEDAY"].Value = txtDelete.Text;
            config.Save();
        }

        private void LoadConfig()
        {
            txtDelete.Text = ConfigurationManager.AppSettings["DELETEDAY"].ToString();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
