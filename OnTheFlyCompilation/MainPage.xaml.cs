using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OnTheFlyCompilation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const string FilePrefix = "OnTheFlyAssembly";

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += async (s, e) =>
            {
                var list = await GetAllFiles();

                var md = new MessageDialog("File Count : " + list.Count);
                await md.ShowAsync();
            };
        }

        public async Task<List<string>> GetAllFiles()
        {
            var fileList = new List<string>();

            //StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            //var files = await appInstalledFolder.GetFilesAsync();

            StorageFolder assemblyFolder = ApplicationData.Current.LocalFolder;
            var files = await assemblyFolder.GetFilesAsync();

            foreach (var file in files)
            {
                if (file.Name.StartsWith(FilePrefix))
                    fileList.Add(file.Name);
            }

            return fileList;
        }
    }
}
