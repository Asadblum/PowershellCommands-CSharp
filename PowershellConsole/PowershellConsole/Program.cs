using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace PowershellConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Powershell Command:");
            string cmd = Console.ReadLine();
            string result = RunScript(cmd);
            Console.WriteLine("********************************");
            Console.WriteLine(result);
        }
        private static string RunScript(string script)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            Pipeline pipline = runspace.CreatePipeline();
            pipline.Commands.AddScript(script);
            pipline.Commands.Add("Out-String");
            Collection<PSObject> results = pipline.Invoke();
            runspace.Close();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject pSObject in results)
            {
                stringBuilder.AppendLine(pSObject.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
