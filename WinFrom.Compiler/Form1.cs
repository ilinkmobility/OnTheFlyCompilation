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
        }

        private async void btnCompile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtxtCode.Text))
            {
                rtxtResult.Text = CompilerHelper.Instance.Compile(rtxtCode.Text);

                var files = await CompilerHelper.Instance.GetAllFiles();

                MessageBox.Show("Assembly Count : " + files.Count, "Info", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Please enter valid source code.", "Error", MessageBoxButtons.OK);
            }
        }

        private async void btnInvokeUWP_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri("open.ontheflycompilation://");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
