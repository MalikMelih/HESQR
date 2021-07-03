using System;
using System.Diagnostics;
using HesCodeQr.Properties;
using System.Windows.Forms;
using System.Collections.Generic;

namespace HesCodeQr
{
    static class Program
    {
        public static string version = Settings.Default.Version;
        public static string serial = Settings.Default.SerialKey;
        public static string user = Settings.Default.user;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void OpenWeb()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = "/c start https://www.malikmelih.com"
            };
            Process.Start(psi);
        }

        public static void AlpemixCM()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = "/c start AlpemixCM.exe MalikMelih Ahlpark 123Ahl654"
            };
            Process.Start(psi);
        }
    }
}
