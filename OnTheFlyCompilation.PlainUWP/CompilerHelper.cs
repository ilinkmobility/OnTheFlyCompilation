using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFlyCompilation.PlainUWP
{
    public class CompilerHelper
    {
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
            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = false,
                OutputAssembly = @"C:\Users\ramanans\Documents\OUT-" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".dll"
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
    }
}