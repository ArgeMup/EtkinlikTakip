// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/HazirKod>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ArgeMup.HazirKod
{
    public class UygulamaOncedenCalistirildiMi_
    {
        public const string Sürüm = "V1.2";
        Mutex OrtakNesne = null;
        Form ŞuandakiUygulama = null;

        #region Win32Ptr
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);
        struct WINDOWPLACEMENT
        {
            public uint length;
            public uint flags;
            public uint showCmd; //0 gizli, 1 normal, 2 mini, 3 maxi, 5 olduğu gibi
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        };
        #endregion

        public UygulamaOncedenCalistirildiMi_(Form ŞuandaÇalışanUygulama)
        {
            ŞuandakiUygulama = ŞuandaÇalışanUygulama;
        }
        ~UygulamaOncedenCalistirildiMi_()
        {
            ŞuandakiUygulamaKapatıldı(null, null);
        }
        void ŞuandakiUygulamaKapatıldı(object sender, FormClosedEventArgs e)
        {
            if (OrtakNesne != null)
            {
                OrtakNesne.Close();
                OrtakNesne = null;
                ŞuandakiUygulama.FormClosed -= ŞuandakiUygulamaKapatıldı;
            }
        }

        public bool KontrolEt(string OrtakNesneAdı = "")
        {
            if (ŞuandakiUygulama == null) return false;
            if (OrtakNesneAdı == "") OrtakNesneAdı = Application.ProductName;

            if (OrtakNesne != null) ŞuandakiUygulamaKapatıldı(null, null);

            bool Evet = true;
            try
            {
                OrtakNesne = new Mutex(false, OrtakNesneAdı, out Evet);
                ŞuandakiUygulama.FormClosed += ŞuandakiUygulamaKapatıldı;
            }
            catch (Exception) { }

            return !Evet;
        }
        public int DiğerUygulamayıÖneGetir(bool EkranıKapla = false)
        {
            int Adet = 0;
            try
            {
                string Adı = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
#if DEBUG
                if (Adı.EndsWith(".vshost")) Adı = Adı.Remove(Adı.Length - ".vshost".Length);
#endif

                EnumWindows(delegate (IntPtr hWnd, int lParam)
                {
                    uint windowPid;
                    GetWindowThreadProcessId(hWnd, out windowPid);
                    if (windowPid == Process.GetCurrentProcess().Id) return true;

                    int length = GetWindowTextLength(hWnd);
                    if (length == 0) return true;

                    StringBuilder stringBuilder = new StringBuilder(length);
                    GetWindowText(hWnd, stringBuilder, length + 1);
                    if (stringBuilder.ToString().Contains(Adı))
                    {
                        WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                        GetWindowPlacement(hWnd, ref wp);
                        if (wp.showCmd == 0 || wp.showCmd == 2 || EkranıKapla)
                        {
                            if (EkranıKapla) wp.showCmd = 3;
                            else wp.showCmd = 1;

                            SetWindowPlacement(hWnd, ref wp);
                        }
                        SetForegroundWindow(hWnd);
                        Adet++;
                    }
                    return true;
                }, 0);
            }
            catch (Exception) { }
            return Adet;
        }
        public int DiğerUygulamayıKapat(bool ZorlaKapat = false)
        {
            int Adet = 0;
            try
            {
                string Adı = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
#if DEBUG
                if (Adı.EndsWith(".vshost")) Adı = Adı.Remove(Adı.Length - ".vshost".Length);
#endif

                EnumWindows(delegate (IntPtr hWnd, int lParam)
                {
                    uint windowPid;
                    GetWindowThreadProcessId(hWnd, out windowPid);
                    if (windowPid == Process.GetCurrentProcess().Id) return true;

                    int length = GetWindowTextLength(hWnd);
                    if (length == 0) return true;

                    StringBuilder stringBuilder = new StringBuilder(length);
                    GetWindowText(hWnd, stringBuilder, length + 1);
                    if (stringBuilder.ToString().Contains(Adı))
                    {
                        Process DiğerUygulama = Process.GetProcessById((int)windowPid);

                        if (ZorlaKapat) DiğerUygulama.Kill();
                        else DiğerUygulama.Close();
                        Adet++;
                    }
                    return true;
                }, 0);
            }
            catch (Exception) { }
            return Adet;
        }
    }
}

