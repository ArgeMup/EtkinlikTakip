// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/HazirKod>

using System;
using System.IO;
using System.Linq;
using System.Text;

#if !UUNNIITTYY
using System.Drawing;
#endif

namespace ArgeMup.HazirKod.Dönüştürme
{
    public static class D_Metin
    {
        public const string Sürüm = "V1.1";

        public static byte[] BaytDizisine(string Girdi)
        {
            if (string.IsNullOrEmpty(Girdi)) return null;
            return Encoding.UTF8.GetBytes(Girdi);
        }
        public static string BaytDizisinden(byte[] Girdi, int Boyut = int.MinValue)
        {
            if (Girdi == null) return "";
            if (Boyut == int.MinValue) Boyut = Girdi.Length;
            if (Boyut > Girdi.Length) Boyut = Girdi.Length;

            return Encoding.UTF8.GetString(Girdi, 0, Boyut).TrimEnd('\0');
        }
    }

    public static class D_HexMetin
    {
        public const string Sürüm = "V1.1";

        public static byte[] BaytDizisine(string Girdi)
        {
            if (string.IsNullOrEmpty(Girdi)) return null;
            return Enumerable.Range(0, Girdi.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(Girdi.Substring(x, 2), 16))
                     .ToArray();
        }
        public static string BaytDizisinden(byte[] Girdi, int Boyut = int.MinValue)
        {
            if (Girdi == null) return "";
            if (Boyut == int.MinValue) Boyut = Girdi.Length;
            if (Boyut > Girdi.Length) Boyut = Girdi.Length;

            string Çıktı = "";
            for (int w = 0; w < Boyut; w++) Çıktı += Girdi[w].ToString("X2");
            return Çıktı;
        }
    }

    public static class D_Akış
    {
        public const string Sürüm = "V1.1";

        public static byte[] BaytDizisine(MemoryStream Girdi, int Boyut = int.MinValue)
        {
            if (Girdi == null) return null;
            if (Boyut == int.MinValue) Boyut = (int)Girdi.Length;
            if (Boyut > Girdi.Length) Boyut = (int)Girdi.Length;

            byte[] Çıktı = new byte[Boyut];
            Girdi.Read(Çıktı, 0, Boyut);
            return Çıktı;
        }
        public static void BaytDizisinden(byte[] Girdi, ref MemoryStream Çıktı, int Boyut = int.MinValue)
        {
            if (Girdi == null) return;
            if (Boyut == int.MinValue) Boyut = Girdi.Length;
            if (Boyut > Girdi.Length) Boyut = Girdi.Length;

            Çıktı.Write(Girdi, 0, Boyut);
        }
    }

    public static class D_Nesne
    {
        public const string Sürüm = "V1.0";

        public static byte[] BaytDizisine(object Girdi)
        {
            if (Girdi == null) return null;

            byte[] Çıktı;
            using (var mS = new MemoryStream())
            {
                var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(mS, Girdi);
                Çıktı = mS.ToArray();
            }
            return Çıktı;
        }
        public static object BaytDizisinden(byte[] Girdi)
        {
            if (Girdi == null) return null;

            object Çıktı;
            using (var mS = new MemoryStream())
            {
                var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                mS.Write(Girdi, 0, Girdi.Length);
                mS.Seek(0, SeekOrigin.Begin);
                Çıktı = bf.Deserialize(mS);
            }
            return Çıktı;
        }
    }

    public static class D_WwwAdresi
    {
        public const string Sürüm = "V1.0";

        public static string IpAdresine(string Girdi)
        {
            return System.Net.Dns.GetHostAddresses(Girdi)[0].ToString();
        }
    }
		
	#if !UUNNIITTYY
    public static class D_İkon
    {
        public const string Sürüm = "V1.0";

        public static Icon Metinden(string Metin, Icon ikon, Font font, Color Renk, Point Konum, Color ArkaPlan)
        {
            Brush brush = new SolidBrush(Renk);
            Bitmap bitmap = new Bitmap(ikon.Width, ikon.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(ArkaPlan);
            graphics.DrawString(Metin, font, brush, Konum.X, Konum.Y);
            Icon createdIcon = Icon.FromHandle(bitmap.GetHicon());

            brush.Dispose();
            graphics.Dispose();
            bitmap.Dispose();

            return createdIcon;
        }

        public static void Yoket(Icon ikon)
        {
            W32_8.DestroyIcon(ikon.Handle);
            ikon.Dispose();
            ikon = null;
        }
    }
	#endif

    public static class D_DosyaBoyutu
    {
        public const string Sürüm = "V1.1";

        public static string Metne(decimal d, int NoktadanSonrakiKarakterSayısı = 2)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (d < 1) return "0B";

            int place = Convert.ToInt32(Math.Floor(Math.Log((Double)d, 1024)));
            double num = Math.Round((Double)d / Math.Pow(1024, place), NoktadanSonrakiKarakterSayısı);
            return (num).ToString() + " " + suf[place];
        }
    }

    public static class D_Süre
    {
        public const string Sürüm = "V1.2";

        public static class Metne
        {
            public static string Saatten(int Saat)
            {
                TimeSpan bbb = new TimeSpan(Saat, 0, 0);

                int gün = bbb.Days;
                int yıl = gün / 365;
                gün -= yıl * 365;

                int ay = gün / 30;
                gün -= ay * 30;

                string Çıktı = "";
                if (yıl > 0) Çıktı = Çıktı.Trim() + yıl + " yıl ";
                if (ay > 0) Çıktı = Çıktı.Trim() + " " + ay + " ay ";
                if (gün > 0) Çıktı = Çıktı.Trim() + " " + gün + " gün ";
                if (bbb.Hours > 0) Çıktı = Çıktı.Trim() + " " + bbb.Hours + " sa. ";
                if (bbb.Minutes > 0) Çıktı = Çıktı.Trim() + " " + bbb.Minutes + " dk. ";

                if (Çıktı == "") Çıktı = "1 dk. dan az";
                return Çıktı.Trim();
            }
            public static string Saniyeden(UInt64 Saniye)
            {
                decimal sn = Saniye, dk = 0, sa = 0, gün = 0, ay = 0, yıl = 0;

                while (sn >= 60)
                {
                    dk++;
                    sn -= 60;
                }

                while (dk >= 60)
                {
                    sa++;
                    dk -= 60;
                }

                while (sa >= 24)
                {
                    gün++;
                    sa -= 24;
                }

                while (gün >= 30)
                {
                    ay++;
                    gün -= 30;
                }

                while (ay >= 12)
                {
                    yıl++;
                    ay -= 12;
                }

                string Çıktı = "";
                if (yıl > 0) Çıktı = Çıktı.Trim() +       yıl + " yıl ";
                if (ay > 0)  Çıktı = Çıktı.Trim() + " " + ay  + " ay ";
                if (gün > 0) Çıktı = Çıktı.Trim() + " " + gün + " gün ";
                if (sa > 0)  Çıktı = Çıktı.Trim() + " " + sa  + " sa. ";
                if (dk > 0)  Çıktı = Çıktı.Trim() + " " + dk  + " dk. ";
                if (sn > 0)  Çıktı = Çıktı.Trim() + " " + sn  + " sn. ";

                if (Çıktı == "") Çıktı = "1 sn. den az";
                return Çıktı.Trim();
            }
            public static string MiliSaniyeden(UInt64 MiliSaniye)
            {
                decimal msn = MiliSaniye, sn = 0, dk = 0, sa = 0, gün = 0, ay = 0, yıl = 0;

                while (msn >= 1000)
                {
                    sn++;
                    msn -= 1000;
                }

                while (sn >= 60)
                {
                    dk++;
                    sn -= 60;
                }

                while (dk >= 60)
                {
                    sa++;
                    dk -= 60;
                }

                while (sa >= 24)
                {
                    gün++;
                    sa -= 24;
                }

                while (gün >= 30)
                {
                    ay++;
                    gün -= 30;
                }

                while (ay >= 12)
                {
                    yıl++;
                    ay -= 12;
                }

                string Çıktı = "";
                if (yıl > 0) Çıktı = Çıktı.Trim() + yıl + " yıl ";
                if (ay > 0) Çıktı = Çıktı.Trim() + " " + ay + " ay ";
                if (gün > 0) Çıktı = Çıktı.Trim() + " " + gün + " gün ";
                if (sa > 0) Çıktı = Çıktı.Trim() + " " + sa + " sa. ";
                if (dk > 0) Çıktı = Çıktı.Trim() + " " + dk + " dk. ";
                if (sn > 0) Çıktı = Çıktı.Trim() + " " + sn + " sn. ";
                if (msn > 0) Çıktı = Çıktı.Trim() + " " + msn + " msn. ";

                if (Çıktı == "") Çıktı = "1 msn. den az";
                return Çıktı.Trim();
            }
        }
    }
	
//#if !UUNNIITTYY
 //   public static class D_Parmakİzi
 //   {
 //       public const string Sürüm = "V1.0";

 //       public static string Metne()
 //       {
 //           /* 
 //            * Kullanılacak ise  
 //            * Solution Explorer -> Proje -> References -> Add Reference
 //            * Assemblies -> Framework -> System.Management
 //            */

 //           string Çıktı = "";
 //           System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_DiskDrive");
 //           System.Management.ManagementObjectCollection moc = mc.GetInstances();
 //           foreach (System.Management.ManagementBaseObject mo in moc)
 //           {
 //               var gecici = mo["InterfaceType"];
 //               if (gecici == null || gecici.ToString() == "USB") continue;

 //               gecici = mo["Model"];
 //               if (gecici != null) Çıktı += gecici.ToString() + ", ";

 //               gecici = mo["SerialNumber"];
 //               if (gecici != null) Çıktı += gecici.ToString() + ", ";

 //               gecici = mo["Signature"];
 //               if (gecici != null) Çıktı += gecici.ToString();

 //               if (Çıktı != "") break;
 //           }

 //           moc.Dispose();
 //           mc.Dispose();

 //           return Çıktı;
 //       }
 //   }
 //#endif
}