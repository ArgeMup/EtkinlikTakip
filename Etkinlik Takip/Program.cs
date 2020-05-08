// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/EtkinlikTakip>

using System;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Etkinlik_Takip
{
    static class Program
    {
        static string pak = Directory.GetCurrentDirectory() + "\\EtkinlikTakipDosyalari\\"; //programanaklasör

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
            
            Directory.CreateDirectory(pak + "Banka");
            Directory.CreateDirectory(pak + "Yedekler");
            Directory.CreateDirectory(pak + "Kutuphane");

            AnaEkran_GerekliDosyaKontrolü(Etkinlik_Takip.Properties.Resources.System_Data_SQLite, "System.Data.SQLite.dll");
            if ( Environment.Is64BitOperatingSystem &&  //.net > 4.0
                 IntPtr.Size == 8                       //.net < 4.0
               ) AnaEkran_GerekliDosyaKontrolü(Etkinlik_Takip.Properties.Resources.SQLite_Interop_x64, "SQLite.Interop.dll");
            else AnaEkran_GerekliDosyaKontrolü(Etkinlik_Takip.Properties.Resources.SQLite_Interop_x86, "SQLite.Interop.dll");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AnaEkran());
        }
        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            string strTempAssmbPath = pak + "Kutuphane\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
            if (!File.Exists(strTempAssmbPath)) return null;
            return Assembly.LoadFrom(strTempAssmbPath);
        }
        static int AnaEkran_GerekliDosyaKontrolü(byte[] Kaynak, string AsılDosyaAdı)
        {
            try
            {
                if (!File.Exists(pak + "Kutuphane\\" + AsılDosyaAdı)) { File.WriteAllBytes(pak + "Kutuphane\\" + AsılDosyaAdı, Kaynak); return 1; }

                File.WriteAllBytes("GerekliDosya.Gecici", Kaynak);
                if (System.Diagnostics.FileVersionInfo.GetVersionInfo("GerekliDosya.Gecici").FileVersion != System.Diagnostics.FileVersionInfo.GetVersionInfo(pak + "Kutuphane\\" + AsılDosyaAdı).FileVersion ||
                    new FileInfo("GerekliDosya.Gecici").Length != new FileInfo(pak + "Kutuphane\\" + AsılDosyaAdı).Length ||
                    new FileInfo("GerekliDosya.Gecici").LastWriteTime != new FileInfo(pak + "Kutuphane\\" + AsılDosyaAdı).LastWriteTime)
                {
                    File.Delete(pak + "Kutuphane\\" + AsılDosyaAdı);
                    File.Move("GerekliDosya.Gecici", pak + "Kutuphane\\" + AsılDosyaAdı);
                    return 2;
                }

                File.Delete("GerekliDosya.Gecici");
            }
            catch (Exception) { }

            return 0;
        }
    }
}
