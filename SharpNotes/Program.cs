using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace SharpNotes
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool acilmamis = true;
            string myFile = null;
            Mutex mutex = new Mutex(false, "SharpNotes", out acilmamis);
            if (acilmamis)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (args.Length > 0)
                {
                    myFile = Convert.ToString(args[0]);
                }
                using (Form1 form1 = new Form1())
                {
                    form1.SelectedFile = myFile;
                    Application.Run(form1);
                }
            }
            else
            {
                if (args.Length > 0)
                {
                    myFile = Convert.ToString(args[0]);
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            NativeMethods.sendWindowsStringMessage((int)process.MainWindowHandle, 0, myFile);
                            break;
                        }
                    }
                    return;
                }
            }
            GC.KeepAlive(mutex);
        }
    }
}
