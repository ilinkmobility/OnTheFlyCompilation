using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
                if (!string.IsNullOrEmpty(txtAssemblyName.Text))
                {
                    rtxtResult.Text = CompilerHelper.Instance.Compile(rtxtCode.Text, txtAssemblyName.Text);
                }
                else
                {
                    rtxtResult.Text = CompilerHelper.Instance.Compile(rtxtCode.Text);

                    var files = await CompilerHelper.Instance.GetAllAssemblies();

                    MessageBox.Show("Assembly Count : " + files.Count, "Info", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid source code.", "Error", MessageBoxButtons.OK);
            }
        }

        private async void btnInvokeUWP_Click(object sender, EventArgs e)
        {
            Type type = CompilerHelper.Instance.GetAssembly(txtAssemblyName.Text);
            object obj = Activator.CreateInstance(type);

            string json = JsonConvert.SerializeObject(obj);

            MessageBox.Show(json, "Json", MessageBoxButtons.OK);

            Uri uri = new Uri("open.ontheflycompilation://message?Hello=" + json);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private async void btnLoadAssemblies_Click(object sender, EventArgs e)
        {
            var assemblies = await CompilerHelper.Instance.GetAllAssemblies();

            var assembly = Assembly.LoadFile(assemblies[0]);
        }

        private void btnLoadAssembly_Click(object sender, EventArgs e)
        {
            Type type = CompilerHelper.Instance.GetAssembly(txtAssemblyName.Text);

            MethodInfo[] methodInfo = type.GetMethods();

            string methodList = string.Empty;

            foreach(var method in methodInfo)
            {
                methodList += method.Name + "\n";
            }

            MessageBox.Show(methodList, "Method List", MessageBoxButtons.OK);

            //Invoking SayHello method.
            object obj = Activator.CreateInstance(type);

            object[] mParam = new object[] { "User" };

            string result = (string) type.InvokeMember("SayHello", BindingFlags.InvokeMethod, null, obj, mParam);

            MessageBox.Show(result, "SayHello method result", MessageBoxButtons.OK);
        }
    }
}
