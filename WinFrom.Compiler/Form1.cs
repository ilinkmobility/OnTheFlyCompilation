using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Storage;

namespace WinFrom.Compiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            txtLibraryLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            rtxtResult.Text = CompilerHelper.Instance.Compile(rtxtCode.Text, txtLibraryLocation.Text);
            //GetAllFiles();
        }

        public async void GetAllFiles()
        {
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var files = await appInstalledFolder.GetFilesAsync();

            string fileList = string.Empty;

            foreach (var file in files)
            {
                fileList += file.Name + "\n";
            }

            rtxtResult.Text = fileList;
        }
    }
}
