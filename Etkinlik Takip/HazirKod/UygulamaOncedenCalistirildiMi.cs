// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/HazirKod>

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ArgeMup.HazirKod.Dönüştürme;

namespace ArgeMup.HazirKod
{
    public class UygulamaOncedenCalistirildiMi_ : IDisposable
    {
        public const string Sürüm = "V1.4";
        Mutex OrtakNesne = null;

        public bool KontrolEt(string OrtakNesneAdı = "")
        {
            if (OrtakNesneAdı == "") OrtakNesneAdı = Application.ProductName;
            if (OrtakNesne != null) { OrtakNesne.Dispose(); OrtakNesne = null; }

            bool Evet = true;
            OrtakNesneAdı = "UygulamaOncedenCalistirildiMi_" + D_HexMetin.BaytDizisinden(D_GeriDönülemezKarmaşıklaştırmaMetodu.BaytDizisinden(D_Metin.BaytDizisine(OrtakNesneAdı)));
            OrtakNesne = new Mutex(false, OrtakNesneAdı, out Evet);

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

                W32_6.EnumWindows(delegate (IntPtr hWnd, int lParam)
                {
                    uint windowPid;
                    W32_5.GetWindowThreadProcessId(hWnd, out windowPid);
                    if (windowPid == Process.GetCurrentProcess().Id) return true;

                    int length = W32_4.GetWindowTextLength(hWnd);
                    if (length == 0) return true;

                    StringBuilder stringBuilder = new StringBuilder(length);
                    W32_4.GetWindowText(hWnd, stringBuilder, length + 1);
                    if (stringBuilder.ToString().Contains(Adı))
                    {
                        W32_7.WINDOWPLACEMENT wp = new W32_7.WINDOWPLACEMENT();
                        W32_7.GetWindowPlacement(hWnd, ref wp);
                        if (wp.showCmd == 0 || wp.showCmd == 2 || EkranıKapla)
                        {
                            if (EkranıKapla) wp.showCmd = 3;
                            else wp.showCmd = 1;

                            W32_7.SetWindowPlacement(hWnd, ref wp);
                        }
                        W32_3.SetForegroundWindow(hWnd);
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

                W32_6.EnumWindows(delegate (IntPtr hWnd, int lParam)
                {
                    uint windowPid;
                    W32_5.GetWindowThreadProcessId(hWnd, out windowPid);
                    if (windowPid == Process.GetCurrentProcess().Id) return true;

                    int length = W32_4.GetWindowTextLength(hWnd);
                    if (length == 0) return true;

                    StringBuilder stringBuilder = new StringBuilder(length);
                    W32_4.GetWindowText(hWnd, stringBuilder, length + 1);
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                    if (OrtakNesne != null) { OrtakNesne.Dispose(); OrtakNesne = null; }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                //disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UygulamaOncedenCalistirildiMi_() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

