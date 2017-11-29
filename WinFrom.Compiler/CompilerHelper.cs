using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Storage;

namespace WinFrom.Compiler
{
    public class CompilerHelper
    {
        const string FilePrefix = "OnTheFlyAssembly";

        private static CompilerHelper instance;

        private CodeDomProvider codeProvider;

        private CompilerHelper()
        {
            codeProvider = CodeDomProvider.CreateProvider("CSharp");
        }

        public static CompilerHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CompilerHelper();
                }
                return instance;
            }
        }
        public string Compile(string source)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = false,
                OutputAssembly = localFolder.Path + @"\" + FilePrefix + "-" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".dll"
            };

            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, source);

            string result = string.Empty;

            if (results.Errors.Count > 0)
            {
                foreach (CompilerError CompErr in results.Errors)
                {
                    result += "Line number " + CompErr.Line +
                        ", Error Number: " + CompErr.ErrorNumber +
                        ", '" + CompErr.ErrorText + ";" +
                        Environment.NewLine + Environment.NewLine;
                };

                return result;
            }
            else
            {
                return results.CompiledAssembly.CodeBase;
            }
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
                if(file.Name.StartsWith(FilePrefix))
                    fileList.Add(file.Name);
            }

            return fileList;
        }
    }
}
