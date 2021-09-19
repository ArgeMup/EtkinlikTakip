// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/HazirKod>

using System;
using System.IO;
using System.Security.Cryptography;
using ArgeMup.HazirKod.Dönüştürme;

namespace ArgeMup.HazirKod
{
    public class DahaCokKarmasiklastirma_ : IDisposable
    {
        public const string Sürüm = "V1.2";
        public string Karıştır(string Girdi, string Parola)
        {
            return D_HexMetin.BaytDizisinden(Karıştır(D_Metin.BaytDizisine(Girdi), D_Metin.BaytDizisine(Parola)));
        }
        public byte[] Karıştır(byte[] Girdi, byte[] Parola)
        {
            try
            {
                Aes aes = new AesManaged();
                aes.IV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(Parola, aes.IV);
                MemoryStream ms = new MemoryStream();

                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = pdb.GetBytes(aes.KeySize / 8);
                aes.IV = pdb.GetBytes(aes.BlockSize / 8);

                CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(Girdi, 0, Girdi.Length);
                cs.Close();
                return ms.ToArray();
            }
            catch (Exception) { }
            return null;
        }

        public string Düzelt(string Girdi, string Parola)
        {
            return D_Metin.BaytDizisinden(Düzelt(D_HexMetin.BaytDizisine(Girdi), D_Metin.BaytDizisine(Parola)));
        }
        public byte[] Düzelt(byte[] Girdi, byte[] Parola)
        {
            try
            {
                Aes aes = new AesManaged();
                aes.IV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(Parola, aes.IV);
                MemoryStream ms = new MemoryStream();

                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = pdb.GetBytes(aes.KeySize / 8);
                aes.IV = pdb.GetBytes(aes.BlockSize / 8);

                CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(Girdi, 0, Girdi.Length);
                cs.Close();
                return ms.ToArray();
            }
            catch (Exception) { }
            return null;
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
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                //disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Karmasiklastirma_() {
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

namespace ArgeMup.HazirKod.Dönüştürme
{
    public static class D_GeriDönülemezKarmaşıklaştırmaMetodu
    {
        public const string Sürüm = "V1.0";

        /// <summary>
        /// Sha256 Oluşturucu
        /// </summary>
        /// <param name="ÇıktıKarakterSayısı">Geçersiz, Sadece uyumluluk için</param>
        /// <returns></returns>
        public static byte[] BaytDizisinden(byte[] Dizi, int ÇıktıKarakterSayısı = 32)
        {
            return new SHA256Managed().ComputeHash(Dizi);
        }

        public static string Metinden(string Girdi, int ÇıktıKarakterSayısı = 32)
        {
            byte[] Dizi1 = D_Metin.BaytDizisine(Girdi);
            byte[] Dizi2 = BaytDizisinden(Dizi1, ÇıktıKarakterSayısı);
            return D_HexMetin.BaytDizisinden(Dizi2);
        }
    }
}

