// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/HazirKod>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace ArgeMup.HazirKod
{
    public class YeniYazılımKontrolü_ : IDisposable
    {
    	public string Sürüm = "V1.0";
        public delegate void YeniYazılımKontrolü_GeriBildirim_(bool Sonuç, string Açıklama);

        WebClient İstemci = null;
        YeniYazılımKontrolü_GeriBildirim_ GeriBildirim_İşlemi = null;

        public void Başlat(Uri DosyaKonumu, YeniYazılımKontrolü_GeriBildirim_ GeriBildirim = null)
        {
            İstemci = new WebClient();

            GeriBildirim_İşlemi = GeriBildirim;

            İstemci.DownloadFileAsync(DosyaKonumu, "ArgeMuP.HazirKod.YeniYazılımKontrolü");
            İstemci.DownloadFileCompleted += new AsyncCompletedEventHandler(İstemci_DownloadFileCompleted);
        }
        public void Durdur()
        {
            if (İstemci == null) return;

            İstemci.CancelAsync();
            İstemci.Dispose();

            GeriBildirim_İşlemi?.Invoke(false, "Durduruldu");
        }
        void İstemci_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string şimdiki_dosya_yolu = System.Reflection.Assembly.GetExecutingAssembly().Location;
                FileVersionInfo gelen = FileVersionInfo.GetVersionInfo("ArgeMuP.HazirKod.YeniYazılımKontrolü");
                FileVersionInfo şimdiki = FileVersionInfo.GetVersionInfo(şimdiki_dosya_yolu);

                bool gelen_daha_yeni = false;
                if (gelen.FileMajorPart > şimdiki.FileMajorPart) gelen_daha_yeni = true;
                else if (gelen.FileMajorPart == şimdiki.FileMajorPart)
                {
                    if (gelen.FileMinorPart > şimdiki.FileMinorPart) gelen_daha_yeni = true;
                }

                if (gelen_daha_yeni)
                {
                    try
                    {
                        File.Move(şimdiki_dosya_yolu, şimdiki_dosya_yolu + ".V" + şimdiki.FileVersion);
                        File.Move("ArgeMuP.HazirKod.YeniYazılımKontrolü", şimdiki_dosya_yolu);

                        GeriBildirim_İşlemi?.Invoke(true, "Eski:V" + şimdiki.FileVersion + " Yeni:V" + gelen.FileVersion);
                    }
                    catch (Exception ex)
                    {
                        GeriBildirim_İşlemi?.Invoke(false, ex.Message);
                    }
                }
                else GeriBildirim_İşlemi?.Invoke(true, "Güncel V" + şimdiki.FileVersion);
            }
            else GeriBildirim_İşlemi?.Invoke(false, e.Error.Message);

            if (File.Exists("ArgeMuP.HazirKod.YeniYazılımKontrolü")) File.Delete("ArgeMuP.HazirKod.YeniYazılımKontrolü");
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

                    Durdur();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~YeniYazılımKontrolü_()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

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