using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Net;

namespace Netflix
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (!File.Exists(path+ "/AxInterop.WMPLib.dll"))
            {
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile("https://backend-server.18jchadwick.repl.co/download/1", path + "/AxInterop.WMPLib.dll");

            }
            if (!File.Exists(path + "/Interop.WMPLib.dll"))
            {
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile("https://backend-server.18jchadwick.repl.co/download/2", path + "/Interop.WMPLib.dll");
            }





            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
}
