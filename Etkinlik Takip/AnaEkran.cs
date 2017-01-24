// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/EtkinlikTakip>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.ArgeMup;
using System.Windows.Forms;

using System.Data.SQLite;
using System.Threading;
using System.IO;

namespace Etkinlik_Takip
{
    public partial class AnaEkran : Form
    {
        #region Tanımlar
        string pak = Directory.GetCurrentDirectory() + "\\EtkinlikTakipDosyalari\\"; //programanaklasör
        public enum Panel2Durumu { Görev, Etkinlik, Boş_Aralık, Arama, Ayarlar };
        public enum EtkinlikDurumu { Yeni_Görev, Üzerinde_Çalışılıyor, Düşük_Öncelikli, Beklemede, Bitti_Geri_Bildirim_Bekleniyor, Diğer, Tamamlandı, İptal_Edildi, Güncellenen_Görev };
        public string[] EtkinlikDurumu_OkunabilirListe =
        {
            "Yeni görev",
            "Üzerinde çalışılıyor",
            "Düşük öncelikli",
            "Beklemede",
            "Bitti, geri bildirim bekleniyor",
            "Diğer",
            "Tamamlandı",
            "İptal edildi",
            "Güncellendi"
        };
        public enum EtkinlikÖzellikleri { OluşturulmaTarihi = -1, Tanımı = -2, Açıklaması = -3, SilinmedenÖncekiSahibi = -4, Durumu = -5 };
        struct Genel_
        {
            public Panel2Durumu Panel2;
            public bool KaydedilmemişBilgiVar;
            public string ex;
            public int Tick;

            public bool AğaçGüncelleniyor;
            public bool GözGezdirmeÇalışıyor;
            public bool GözGezdirmeKapatmaTalebi;

            public int[] AğacMenüFiltreleme;
            public int[] AğacMenüFiltrelemeSayac;
            public int AğacMenüFiltrelemeSayacGizli;

            public bool[] AğaçDallarıDurumu;

            public FormWindowState Pencere;

            public Mutex Mutex_;

            public bool Menu_Ağac_Açık;

            public int AnlıkFarePozisyonu_X, AnlıkFarePozisyonu_Y;
            public int AçıldığındakiFarePozisyonu_X, AçıldığındakiFarePozisyonu_Y;
        };
        Genel_ Genel = new Genel_();

        struct Sql_
        {
            public SQLiteConnectionStringBuilder BağlantıMeşajıÜreteci;
            public SQLiteConnection Bağlantı;
        };
        Sql_ Sql = new Sql_();

        KelimeTamamlayıcı[] KeTa = new KelimeTamamlayıcı[6];
        #endregion

        public AnaEkran()
        {
            InitializeComponent();
        }
        private void AnaEkran_Load(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = 0;
                pak = Directory.GetCurrentDirectory() + "\\EtkinlikTakipDosyalari\\";

                Sql_Başlat();
                Genel.AğacMenüFiltreleme = new int[EtkinlikDurumu_OkunabilirListe.Length];
                Genel.AğacMenüFiltrelemeSayac = new int[Genel.AğacMenüFiltreleme.Length];
                this.Text = "Mup " + Application.ProductName + " V" + Application.ProductVersion;

                string geci = Sql_Ayarlar_Oku("ikiz", 0).ToString();
                if (geci != "")
                {
                    bool yeni;
                    Genel.Mutex_ = new Mutex(false, geci, out yeni);
                    if (!yeni) { MessageBox.Show("Aynı veribankası ile ikinci uygulama çalıştırılamamaktadır."); Environment.Exit(1); return; }
                }
                else
                {
                    geci = "EtkinlikTakip_Acilis_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_";
                    Genel.Mutex_ = new Mutex(false, geci);
                    Sql_Ayarlar_Yaz("ikiz", geci);
                }

                while (splitContainer1.Panel2.Controls.Count > 0) splitContainer1.Panel2.Controls.RemoveAt(0);
                splitContainer1.Panel2.Controls.Add(panel_Görev);
                splitContainer1.Panel2.Controls.Add(panel_Etkinlik);
                splitContainer1.Panel2.Controls.Add(panel_Arama);
                splitContainer1.Panel2.Controls.Add(panel_Ayarlar);
                Genel.Panel2 = Panel2Durumu.Ayarlar;
                Panel_Aç(Panel2Durumu.Görev);
                SayfaDüzeni_Normal();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); } 
        }
        private void AnaEkran_Shown(object sender, EventArgs e)
        {
            try
            {
                //Ara düzeltme
                textBox_Görev_Tanım.Width = panel_Görev__.Width - textBox_Görev_Tanım.Left - 3;
                textBox_Görev_Açıklama.Width = textBox_Görev_Tanım.Width;
                comboBox_Etkinlik_Durum.Width = panel_Etkinlik.Width - comboBox_Etkinlik_Durum.Left - 3;
                textBox_Etkinlik_Açıklama.Width = comboBox_Etkinlik_Durum.Width;

                numericUpDown1.Value = (int)Sql_Ayarlar_Oku("Punto", 1, (object)8);
                PuntoDeğişti(null, null);
                Application.DoEvents();
                KullanıcıAdı.Text = (string)Sql_Ayarlar_Oku("KullanıcıAdı", 0, Environment.UserName);

                this.Location = new Point((int)Sql_Ayarlar_Oku("Form_Poz_X", 1, (object)20), (int)Sql_Ayarlar_Oku("Form_Poz_Y", 1, (object)20));
                this.Width = (int)Sql_Ayarlar_Oku("Form_Genişlik", 1, (object)400);
                this.Height = (int)Sql_Ayarlar_Oku("Form_Yükseklik", 1, (object)300);
                this.WindowState = (FormWindowState)Sql_Ayarlar_Oku("Pencere", 1, (object)0);

                if (!Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(new Rectangle(this.Left, this.Top, this.Width, this.Height))))
                {
                    this.Left = 0; this.Top = 0; this.Width = 400; this.Height = 300;
                }

                Ağaç.SelectedImageIndex = 9;
                Ağaç.TreeViewNodeSorter = new Ağaç_NodeSorter();
                Genel.AğaçDallarıDurumu = new bool[0];
                int adt = 0; try { adt = Convert.ToInt32(Sql_SorÖğren("select max(No) from Gorev")); } catch (Exception) { }
                Ağaç_DallarıDurumuÖnKontrol(adt);

                try
                {
                    using (Stream stream = new FileStream(pak + "Banka\\" + Application.ProductName + ".agac", FileMode.Open))
                    {
                        using (StreamReader sw = new StreamReader(stream))
                        {
                            int adet = 0;
                            while (!sw.EndOfStream)
                            {
                                if (sw.Read() == '1') Genel.AğaçDallarıDurumu[adet] = true;
                                adet++;
                            }
                        }
                    }
                }
                catch (Exception) { }

                Genel.AğaçGüncelleniyor = false;
                Filtreleme_DurumDeğişikliği(null, null);
                splitContainer1.SplitterDistance = (int)Sql_Ayarlar_Oku("Ayıraç1", 1);
                splitContainer2.SplitterDistance = (int)Sql_Ayarlar_Oku("Ayıraç2", 1);
                Panel_Aç(Panel2Durumu.Arama);
                Application.DoEvents();
                this.Opacity = 1;

                Üç_Değiştir.CheckState = (CheckState)Sql_Ayarlar_Oku("ÜzerindeÇalışılıyorDurumu", 1, (object)1);
                Genel.AğaçGüncelleniyor = true;
                Filtreleme_D0.CheckState = (CheckState)Sql_Ayarlar_Oku("Filtreleme_YeniGörev", 1);
                Filtreleme_D1.CheckState = (CheckState)Sql_Ayarlar_Oku("Filtreleme_ÜzerindeÇalışılıyor", 1);
                Filtreleme_D2.CheckState = (CheckState)Sql_Ayarlar_Oku("Filtreleme_DüşükÖncelikli", 1);
                Filtreleme_D3.CheckState = (CheckState)Sql_Ayarlar_Oku("Filtreleme_Beklemede", 1);
                Filtreleme_D4.CheckState = (CheckState)Sql_Ayarlar_Oku("Filtreleme_BittiGeriBildirimBekleniyor", 1);
                Filtreleme_D5.CheckState = (CheckState)Sql_Ayarlar_Oku("Filtreleme_Diğer", 1);
                Filtreleme_D6.CheckState = (CheckState)Sql_Ayarlar_Oku("Filtreleme_Tamamlandı", 1);
                Filtreleme_D7.CheckState = (CheckState)Sql_Ayarlar_Oku("Filtreleme_İptalEdildi", 1);
                Filtreleme_ÇöpKutusu.CheckState = (CheckState)Sql_Ayarlar_Oku("Filtreleme_ÇöpKutusu", 1);

                MenuItem_Grid_Etk_Sütünlar_Durum.CheckState = (CheckState)Sql_Ayarlar_Oku("Sutunlar_Durum", 1);
                MenuItem_Grid_Etk_Sütünlar_İçerik.CheckState = (CheckState)Sql_Ayarlar_Oku("Sutunlar_İçerik", 1);
                MenuItem_Grid_Etk_Sütünlar_An.CheckState = (CheckState)Sql_Ayarlar_Oku("Sutunlar_An", 1);
                MenuItem_Grid_Etk_Sütünlar_Tarih.CheckState = (CheckState)Sql_Ayarlar_Oku("Sutunlar_Tarih", 1);
                MenuItem_Grid_Etk_Sütünlar_Açıklama.CheckState = (CheckState)Sql_Ayarlar_Oku("Sutunlar_Açıklama", 1);
                MenuItem_Grid_Etk_AçıklamaTekSatırda.CheckState = (CheckState)Sql_Ayarlar_Oku("Sutunlar_AçıklamaTekSatırda", 1);
                MenuItem_Grid_Etk_Sıralama_YeniUstte.CheckState = (CheckState)Sql_Ayarlar_Oku("Sıralama_Ustteki", 1);
                MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan.CheckState = (CheckState)Sql_Ayarlar_Oku("AyarlarSayfaınakiFiltrelemeyiKullan", 1);
                MenuItem_Grid_Etk_Sıralama_EskiUstte.Checked = !MenuItem_Grid_Etk_Sıralama_YeniUstte.Checked;
                MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Text = (string)Sql_Ayarlar_Oku("GörüntelenecekEtkinlikSayısı", 0);

                Sutun_Durum_G.Visible = MenuItem_Grid_Etk_Sütünlar_Durum.Checked;
                Sutun_Durum_M.Visible = MenuItem_Grid_Etk_Sütünlar_İçerik.Checked;
                Sutun_An.Visible = MenuItem_Grid_Etk_Sütünlar_An.Checked;
                Sutun_Tarih.Visible = MenuItem_Grid_Etk_Sütünlar_Tarih.Checked;
                Sutun_Açıklama.Visible = MenuItem_Grid_Etk_Sütünlar_Açıklama.Checked;
                if (MenuItem_Grid_Etk_AçıklamaTekSatırda.Checked) Grid_Etkinlikler.Columns["Sutun_Açıklama"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                else Grid_Etkinlikler.Columns["Sutun_Açıklama"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                comboBox_Etkinlik_Durum.Items.AddRange(EtkinlikDurumu_OkunabilirListe);
                comboBox_Etkinlik_Durum.Items.RemoveAt((int)EtkinlikDurumu.Güncellenen_Görev);
                comboBox_Etkinlik_Durum.Items.RemoveAt((int)EtkinlikDurumu.Yeni_Görev);

                KeTa[0] = new KelimeTamamlayıcı(this, pak + "Banka\\" + Application.ProductName + ".KelimeTamamlayıcı", null);
                KeTa[0].Başlat(textBox_Görev_Tanım);

                KeTa[1] = new KelimeTamamlayıcı(this, "", KeTa[0].ÖnerilenKelimeler);
                KeTa[2] = new KelimeTamamlayıcı(this, "", KeTa[0].ÖnerilenKelimeler);
                KeTa[3] = new KelimeTamamlayıcı(this, "", KeTa[0].ÖnerilenKelimeler);
                KeTa[4] = new KelimeTamamlayıcı(this, "", KeTa[0].ÖnerilenKelimeler);
                KeTa[5] = new KelimeTamamlayıcı(this, "", KeTa[0].ÖnerilenKelimeler);

                KeTa[1].Başlat(textBox_Görev_Açıklama);               
                KeTa[2].Başlat(textBox_Etkinlik_Açıklama);                
                KeTa[3].Başlat(textBox_Arama); KeTa[3].İmlaKuralları.Anaİzin = false;
                KeTa[4].Başlat(Menu_Ağaç, MenuItem_Ağaç_Tanım);               
                KeTa[5].Başlat(Menu_Ağaç, MenuItem_Ağaç_Açıklama);

                Genel.KaydedilmemişBilgiVar = false;
                Genel.AğaçGüncelleniyor = false;
                Application.DoEvents();
                textBox_Arama.Focus();
            }
            catch (Exception) { }
        }
        private void AnaEkran_FormClosing(object sender, FormClosingEventArgs e)
        {
            toolStripSil_Click(null, null);
            if (Genel.KaydedilmemişBilgiVar) e.Cancel = true;
        }
        private void AnaEkran_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = false;
            
            Sql_Ayarlar_Yaz("KullanıcıAdı", KullanıcıAdı.Text);
            Sql_Ayarlar_Yaz("Ayıraç1", (int)splitContainer1.SplitterDistance);
            Sql_Ayarlar_Yaz("Ayıraç2", (int)splitContainer2.SplitterDistance);

            Sql_Ayarlar_Yaz("Punto", (int)numericUpDown1.Value);
            if (this.WindowState != FormWindowState.Minimized) Sql_Ayarlar_Yaz("Pencere", Convert.ToInt32(this.WindowState));
            if (this.WindowState == FormWindowState.Normal)
            {
                Sql_Ayarlar_Yaz("Form_Poz_X", this.Location.X);
                Sql_Ayarlar_Yaz("Form_Poz_Y", this.Location.Y);
                Sql_Ayarlar_Yaz("Form_Genişlik", this.Width);
                Sql_Ayarlar_Yaz("Form_Yükseklik", this.Height);
            }
            Sql_Ayarlar_Yaz("ÜzerindeÇalışılıyorDurumu", Convert.ToInt32(Üç_Değiştir.CheckState));

            Sql_Ayarlar_Yaz("Filtreleme_YeniGörev", (int)Filtreleme_D0.CheckState);
            Sql_Ayarlar_Yaz("Filtreleme_ÜzerindeÇalışılıyor", (int)Filtreleme_D1.CheckState);
            Sql_Ayarlar_Yaz("Filtreleme_DüşükÖncelikli", (int)Filtreleme_D2.CheckState);
            Sql_Ayarlar_Yaz("Filtreleme_Beklemede", (int)Filtreleme_D3.CheckState);
            Sql_Ayarlar_Yaz("Filtreleme_BittiGeriBildirimBekleniyor", (int)Filtreleme_D4.CheckState);
            Sql_Ayarlar_Yaz("Filtreleme_Diğer", (int)Filtreleme_D5.CheckState);
            Sql_Ayarlar_Yaz("Filtreleme_Tamamlandı", (int)Filtreleme_D6.CheckState);
            Sql_Ayarlar_Yaz("Filtreleme_İptalEdildi", (int)Filtreleme_D7.CheckState);
            Sql_Ayarlar_Yaz("Filtreleme_ÇöpKutusu", (int)Filtreleme_ÇöpKutusu.CheckState);

            Sql_Ayarlar_Yaz("Sutunlar_Durum", (int)MenuItem_Grid_Etk_Sütünlar_Durum.CheckState);
            Sql_Ayarlar_Yaz("Sutunlar_İçerik", (int)MenuItem_Grid_Etk_Sütünlar_İçerik.CheckState);
            Sql_Ayarlar_Yaz("Sutunlar_An", (int)MenuItem_Grid_Etk_Sütünlar_An.CheckState);
            Sql_Ayarlar_Yaz("Sutunlar_Tarih", (int)MenuItem_Grid_Etk_Sütünlar_Tarih.CheckState);
            Sql_Ayarlar_Yaz("Sutunlar_Açıklama", (int)MenuItem_Grid_Etk_Sütünlar_Açıklama.CheckState);
            Sql_Ayarlar_Yaz("Sutunlar_AçıklamaTekSatırda", (int)MenuItem_Grid_Etk_AçıklamaTekSatırda.CheckState);
            Sql_Ayarlar_Yaz("Sıralama_Ustteki", (int)MenuItem_Grid_Etk_Sıralama_YeniUstte.CheckState);
            Sql_Ayarlar_Yaz("AyarlarSayfaınakiFiltrelemeyiKullan", (int)MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan.CheckState);
            Sql_Ayarlar_Yaz("GörüntelenecekEtkinlikSayısı", (string)MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Text);

            Sql_Ayarlar_Yaz("ikiz", "");
            Sql_Durdur();

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(pak + "Yedekler\\");
                List<FileInfo> result = directoryInfo.GetFiles("*.mup", SearchOption.TopDirectoryOnly).OrderBy(t => t.CreationTime).ToList();
                decimal Boyut = 0;
                foreach (var item in result) Boyut += item.Length;
                while (Boyut > 100 * 1024 * 1024 && result.Count > 15) { Boyut -= result.First().Length; result.First().Delete(); result.RemoveAt(0); }//en eski
                if (result.Count == 0 || result.Last().LastWriteTime != File.GetLastWriteTime(pak + "Banka\\" + Application.ProductName + ".mup")) File.Copy(pak + "Banka\\" + Application.ProductName + ".mup", pak + "Yedekler\\" + DateTime.Now.ToString("_yyyyMMddhhmmss_") + Application.ProductName + ".mup", true);
            }
            catch (Exception) { }

            try
            {
                using (Stream stream = new FileStream(pak + "Banka\\" + Application.ProductName + ".agac", FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        foreach (bool bit in Genel.AğaçDallarıDurumu) { sw.Write(bit ? 1 : 0); }
                    }
                }
            }
            catch (Exception) { }

            foreach (var nesne in KeTa) nesne.Durdur();
        }
        private void AnaEkran_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) this.Hide();
            else Genel.Pencere = this.WindowState;
        }
        private void AnaEkran_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case (Keys.F):
                        toolStripSil_Click(null, null);
                        break;

                    case (Keys.N):
                        toolStripEkle.ShowDropDown();
                        break;

                    case (Keys.S):
                        toolStripEkle_Click(null, null);
                        break;
                }
            }
            else if (e.KeyCode == Keys.Escape) Menu_Ağaç.Close();
        }
        
        private bool Sql_Başlat()
        {
            try
            {
                Sql.BağlantıMeşajıÜreteci = new SQLiteConnectionStringBuilder();
                Sql.BağlantıMeşajıÜreteci.DataSource = pak + "Banka\\" + Application.ProductName  + ".mup";
                Sql.BağlantıMeşajıÜreteci.JournalMode = SQLiteJournalModeEnum.Wal;
                Sql.BağlantıMeşajıÜreteci.Password = "fa337d9e075366beda900d1f030397e930a53268";
                Sql.Bağlantı = new SQLiteConnection(Sql.BağlantıMeşajıÜreteci.ConnectionString);
                Sql.Bağlantı.Open();

                string dogrulama1 = (string)Sql_Ayarlar_Oku("DOGRULAMA", 0, "");
                string dogrulama2 = (string)Sql_Ayarlar_Oku("dogrulama", 0, "");
                
                if (dogrulama1 != "DOGrulama_" + Application.ProductVersion || dogrulama2 != "dogRULAMA_" + Application.ProductVersion)
                {
                    if (!dogrulama1.Contains("DOGrulama") || !dogrulama2.Contains("dogRULAMA")) Sql_Sorgula("drop table Ayarlar");
                    if (!Sql_Sorgula("CREATE TABLE IF NOT EXISTS Ayarlar (Parametre TEXT PRIMARY KEY, Ayar TEXT)")) return false;
                    if (!Sql_Sorgula("CREATE TABLE IF NOT EXISTS Gorev  (No INTEGER PRIMARY KEY, Sahip INTEGER)")) return false;
                    if (!Sql_Sorgula("CREATE TABLE IF NOT EXISTS gecici (Zaman DATETIME, Durum INTEGER, Aciklama TEXT, Tanim TEXT)")) return false;

                    if (Sql_Sorgula("CREATE TABLE _t0 (Zaman DATETIME, Durum INTEGER, Aciklama TEXT)"))
                    {
                        Sql_Sorgula("insert into _t0 values (null, -1, '" + DateTime.Now.ToString() + "')");
                        Sql_Sorgula("insert into _t0 values (null, -2, '" + Environment.UserName + "')");
                        Sql_Sorgula("insert into _t0 values (null, -3, '" + Environment.UserName + " oluşturuldu')");
                    }

                    Sql_Sorgula("insert into Gorev values (0, 0)");

                    Sql_Sorgula("insert into Ayarlar values ('KullanıcıAdı', '" + Environment.UserName + "')");
                    Sql_Sorgula("insert into Ayarlar values ('Form_Poz_X', '0')");
                    Sql_Sorgula("insert into Ayarlar values ('Form_Poz_Y', '0')");
                    Sql_Sorgula("insert into Ayarlar values ('Form_Genişlik', '400')");
                    Sql_Sorgula("insert into Ayarlar values ('Form_Yükseklik', '300')");
                    Sql_Sorgula("insert into Ayarlar values ('Ayıraç1', '200')");
                    Sql_Sorgula("insert into Ayarlar values ('Ayıraç2', '1000')");
                    Sql_Sorgula("insert into Ayarlar values ('Punto', '8')");
                    Sql_Sorgula("insert into Ayarlar values ('Pencere', '" + Convert.ToString((int)FormWindowState.Normal) + "')");
                    Sql_Sorgula("insert into Ayarlar values ('ÜzerindeÇalışılıyorDurumu', '1')");

                    Sql_Sorgula("insert into Ayarlar values ('Sutunlar_Durum', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Sutunlar_İçerik', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Sutunlar_An', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Sutunlar_Tarih', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Sutunlar_Açıklama', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Sutunlar_AçıklamaTekSatırda', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Sıralama_Ustteki', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('GörüntelenecekEtkinlikSayısı', '')");
                    Sql_Sorgula("insert into Ayarlar values ('AyarlarSayfaınakiFiltrelemeyiKullan', '1')");

                    Sql_Sorgula("insert into Ayarlar values ('Filtreleme_YeniGörev', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Filtreleme_ÜzerindeÇalışılıyor', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Filtreleme_DüşükÖncelikli', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Filtreleme_Beklemede', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Filtreleme_BittiGeriBildirimBekleniyor', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Filtreleme_Tamamlandı', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Filtreleme_İptalEdildi', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Filtreleme_Diğer', '1')");
                    Sql_Sorgula("insert into Ayarlar values ('Filtreleme_ÇöpKutusu', '1')");

                    Sql_Sorgula("insert into Ayarlar values ('ikiz', '')");

                    Sql_Sorgula("insert into Ayarlar values ('DOGRULAMA', 'DOGrulama_" + Application.ProductVersion + "')");
                    Sql_Sorgula("insert into Ayarlar values ('dogrulama', 'dogRULAMA_" + Application.ProductVersion + "')");
                    Sql_Ayarlar_Yaz("DOGRULAMA", "DOGrulama_" + Application.ProductVersion);
                    Sql_Ayarlar_Yaz("dogrulama", "dogRULAMA_" + Application.ProductVersion);
                }
                return true;
            }
            catch (Exception ex) { MessageBox.Show(pak + "Banka\\" + Application.ProductName + ".mup dosyası hasarlı olabilir. Silinmesi gerekmektedir. (sqlite:" + ex.Message + ")"); Environment.Exit(1); }
            return false;
        }
        private bool Sql_Durdur()
        {
            try
            {
                Sql.Bağlantı.Close();
                return true;
            }
            catch (Exception ex) { Genel.ex = ex.ToString(); }
            return false;
        }
        private bool Sql_Sorgula(string Sorgu)
        {
            try
            {
                SQLiteCommand SQLiteCommand_ = new SQLiteCommand(Sorgu, Sql.Bağlantı);
                SQLiteCommand_.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) { Genel.ex = ex.ToString(); }
            return false;
        }
        private bool Sql_Sorgula(string Sorgu, out SQLiteDataReader SQLiteDataReader_)
        {
            SQLiteDataReader_ = null;

            try
            {
                SQLiteCommand SQLiteCommand_ = new SQLiteCommand(Sorgu, Sql.Bağlantı);
                SQLiteDataReader_ = SQLiteCommand_.ExecuteReader();
                return true;
            }
            catch (Exception ex) { Genel.ex = ex.ToString(); }
            return false;
        }
        private Object Sql_SorÖğren(string Sorgu)
        {
            try
            {
                SQLiteCommand SQLiteCommand_ = new SQLiteCommand(Sorgu, Sql.Bağlantı);
                SQLiteDataReader SQLiteDataReader_ = SQLiteCommand_.ExecuteReader();
                if (!SQLiteDataReader_.Read()) return null;
                return SQLiteDataReader_[0];
            }
            catch (Exception ex) { Genel.ex = ex.ToString(); }
            return null;
        }
        private Object Sql_Ayarlar_Oku(string Parametre, int Yazı_0_Rakam_1, object hatadurumunda = null)
        {
            Object Çıktı;

            if (hatadurumunda != null) Çıktı = hatadurumunda;
            else
            { 
                if (Yazı_0_Rakam_1 == 1) Çıktı = 0;
                else Çıktı = "";
            }

            try
            {
                SQLiteDataReader SQLiteDataReader_ = null;
                if (!Sql_Sorgula("select Ayar from Ayarlar where Parametre = '" + Parametre + "'", out SQLiteDataReader_)) return Çıktı;
                if (!SQLiteDataReader_.Read()) return Çıktı;

                if (Yazı_0_Rakam_1 == 1) Çıktı = Convert.ToInt32(SQLiteDataReader_["Ayar"]);
                else Çıktı = SQLiteDataReader_["Ayar"];
            }
            catch (Exception) { }

            return Çıktı;
        }
        private bool Sql_Ayarlar_Yaz(string Parametre, string Ayar)
        {
            if (!Sql_Sorgula("update Ayarlar set Ayar = '" + Ayar + "' where Parametre = '" + Parametre + "'")) return false;
            return true;
        }
        private bool Sql_Ayarlar_Yaz(string Parametre, int Ayar)
        {
            if (!Sql_Sorgula("update Ayarlar set Ayar = '" + Ayar.ToString() + "' where Parametre = '" + Parametre + "'")) return false;
            return true;
        }
        private List<TreeNode> Sql_ÜyeleriAl(int No)
        {
            List<TreeNode> Liste = new List<TreeNode>();
            SQLiteDataReader SQLiteDataReader_;
            int Nosu; int[] Dizi;
            string Tanımı;
            List<TreeNode> L_TrNo;
            TreeNode TrNo;

            try
            {
                if (!Sql_Sorgula("SELECT No FROM Gorev WHERE Sahip = " + No + " ORDER BY No", out SQLiteDataReader_)) return Liste;
                while (SQLiteDataReader_.Read())
                {
                    Nosu = Convert.ToInt32(SQLiteDataReader_["No"]);
                    if (Nosu > 0)
                    {
                        Tanımı = Sql_GörevÖzelliği(Nosu,  EtkinlikÖzellikleri.Tanımı);
                        L_TrNo = Sql_ÜyeleriAl((int)Nosu);
                        TrNo = new TreeNode(Tanımı, L_TrNo.ToArray());
                        TrNo.Tag = Nosu;
                        if (Genel.AğaçDallarıDurumu[Nosu]) TrNo.Expand();

                        Dizi = new int[L_TrNo.Count + 1];
                        for (int i = 0; i < L_TrNo.Count; i++) Dizi[i] = L_TrNo[i].ImageIndex;
                        Dizi[Dizi.Length - 1] = Convert.ToInt32(Sql_GörevÖzelliği(Nosu, EtkinlikÖzellikleri.Durumu));
                        if (Dizi[Dizi.Length - 1] == (int)EtkinlikDurumu.Yeni_Görev) Dizi[Dizi.Length - 1] = Dizi[0];
                        TrNo.ImageIndex = Dizi.Min();

                        if (Genel.AğacMenüFiltreleme[TrNo.ImageIndex] == 1) Liste.Add(TrNo);
                        else Genel.AğacMenüFiltrelemeSayacGizli++;
                        Genel.AğacMenüFiltrelemeSayac[TrNo.ImageIndex]++;
                    }
                }
            }
            catch (Exception ex) { Genel.ex = ex.ToString(); }
            return Liste;
        }
        private string Sql_GörevÖzelliği(int Görev, EtkinlikÖzellikleri Özellik)
        {
            if (Özellik == EtkinlikÖzellikleri.Durumu)
            {
                Object Alınan = Sql_SorÖğren("select Durum from _t" + Görev.ToString() + " where Durum < " + ((int)EtkinlikDurumu.Güncellenen_Görev).ToString() + " and Zaman is not null order by Zaman desc limit 1");
                if (Alınan != null) return (Convert.ToInt32(Alınan)).ToString();
                else return ((int)EtkinlikDurumu.Yeni_Görev).ToString();
            }
            else
            {
                string gecici = (string)Sql_SorÖğren("select Aciklama from _t" + Görev.ToString() + " where Durum = " + (int)Özellik + " and Zaman is null");
                if (gecici == null) return "";
                else return gecici;
            }
        }
        private void Ağaç_Güncelle()
        {
            for (int i = 0; i < Genel.AğacMenüFiltrelemeSayac.Length; i++) Genel.AğacMenüFiltrelemeSayac[i] = 0;
            Genel.AğacMenüFiltrelemeSayacGizli = 0;

            Genel.AğaçGüncelleniyor = true;
            Ağaç.Nodes.Clear();

            TreeNode gecici = new TreeNode(KullanıcıAdı.Text, Sql_ÜyeleriAl(0).ToArray());
            gecici.Tag = 0;
            gecici.ImageIndex = 10;            
            Ağaç.Nodes.Add(gecici);

            toolStripEtiket.Text = "";
            if (Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Yeni_Görev] > 0) toolStripEtiket.Text += Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Yeni_Görev].ToString() + " yeni ";
            if (Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Üzerinde_Çalışılıyor] > 0) toolStripEtiket.Text += Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Üzerinde_Çalışılıyor].ToString() + " çalışılan ";
            if (Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Düşük_Öncelikli] > 0) toolStripEtiket.Text += Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Düşük_Öncelikli].ToString() + " düşük önc. ";
            Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Beklemede] += Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Bitti_Geri_Bildirim_Bekleniyor];
            if (Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Beklemede] > 0) toolStripEtiket.Text += Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Beklemede].ToString() + " beklenen ";
            if (Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Diğer] > 0) toolStripEtiket.Text += Genel.AğacMenüFiltrelemeSayac[(int)EtkinlikDurumu.Diğer].ToString() + " diğer ";
            if (Genel.AğacMenüFiltrelemeSayacGizli > 0) toolStripEtiket.Text += Genel.AğacMenüFiltrelemeSayacGizli.ToString() + " gizli ";

            if (Filtreleme_ÇöpKutusu.Checked)
            {
                gecici = new TreeNode("Çöp Kutusu", Sql_ÜyeleriAl(-1).ToArray());
                if (gecici.Nodes.Count > 0)
                {
                    gecici.Tag = -1;
                    gecici.ImageIndex = 11;
                    Ağaç.Nodes.Add(gecici);
                }
            }

            Ağaç.Sort();
            Ağaç.Nodes[0].Expand();
            Genel.AğaçGüncelleniyor = false;

            string not = this.Text + Environment.NewLine + toolStripEtiket.Text;
            if (not.Length > 63) notifyIcon1.Text = not.Substring(0, 63);
            else notifyIcon1.Text = not;
        }
        #region Ağaç_NodeSorter
        private class Ağaç_NodeSorter : System.Collections.IComparer
        {   // Your sorting logic here... return -1 if tx < ty, 1 if tx > ty, 0 otherwise
            public int Compare(object x_, object y_)
            {
                int x = ((TreeNode)x_).ImageIndex;
                int y = ((TreeNode)y_).ImageIndex;

                if (x == y) return 0;

                if (x == (int)EtkinlikDurumu.Yeni_Görev) return -1;
                if (y == (int)EtkinlikDurumu.Yeni_Görev) return 1;

                if (x < y) return -1;
                else return 1;
            }
        }
        #endregion
        
        private void comboBox_Etkinlik_Durum_SelectedIndexChanged(object sender, EventArgs e)
        {
            Genel_KaydedilmemişBilgiVar(null, null);

            if (comboBox_Etkinlik_Durum.SelectedIndex == -1) pictureBox_Etkinlik_Durum.Image = Etkinlik_Takip.Properties.Resources.Ret;
            else pictureBox_Etkinlik_Durum.Image = ımageList1.Images[comboBox_Etkinlik_Durum.SelectedIndex+1];
        }
        private void label_Görev_Tarih_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Ağaç.SelectedNode = null;
            label_Görev_Tarih.Text = KullanıcıAdı.Text;
            label_Görev_Tarih.Enabled = false;
        }
        private void Filtreleme_DurumDeğişikliği(object sender, EventArgs e)
        {
            if (Genel.AğaçGüncelleniyor) return;
            Genel.AğacMenüFiltreleme[0] = (int)Filtreleme_D0.CheckState;
            Genel.AğacMenüFiltreleme[1] = (int)Filtreleme_D1.CheckState;
            Genel.AğacMenüFiltreleme[2] = (int)Filtreleme_D2.CheckState;
            Genel.AğacMenüFiltreleme[3] = (int)Filtreleme_D3.CheckState;
            Genel.AğacMenüFiltreleme[4] = (int)Filtreleme_D4.CheckState;
            Genel.AğacMenüFiltreleme[5] = (int)Filtreleme_D5.CheckState;
            Genel.AğacMenüFiltreleme[6] = (int)Filtreleme_D6.CheckState;
            Genel.AğacMenüFiltreleme[7] = (int)Filtreleme_D7.CheckState;
            Ağaç_Güncelle();
        }
        private void FiltrelemeTumu_CheckedChanged(object sender, EventArgs e)
        {
            Genel.AğaçGüncelleniyor = true;
            Filtreleme_D0.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D1.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D2.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D3.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D4.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D5.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D6.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D7.CheckState = FiltrelemeTumu.CheckState;
            Genel.AğaçGüncelleniyor = false;
            Filtreleme_ÇöpKutusu.CheckState = FiltrelemeTumu.CheckState;
        }
        private void textBox_Arama_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Arama.Text.Length < 2) return;
            if (Genel.GözGezdirmeÇalışıyor) { Genel.GözGezdirmeKapatmaTalebi = true; return; }
            Genel.GözGezdirmeÇalışıyor = true;
            Genel.GözGezdirmeKapatmaTalebi = false;
            Genel.Tick = Environment.TickCount + 100;

            Arama.RowCount = 0;
            TreeNodeCollection nodes = Ağaç.Nodes;
            foreach (TreeNode n in nodes) textBox_Arama_TextChanged_2(n);
            Genel.GözGezdirmeÇalışıyor = false;
        }
        private void textBox_Arama_TextChanged_2(TreeNode treeNode)
        {
            if (Genel.GözGezdirmeKapatmaTalebi) return;
            if (Genel.Tick < Environment.TickCount) { Genel.Tick = Environment.TickCount + 100; Application.DoEvents(); }

            SQLiteDataReader Dr; bool içeriktebulundu = false;
            string Sorgu = "select Aciklama from _t" + ((int)treeNode.Tag).ToString() + " where Zaman is not null";
            if (Sql_Sorgula(Sorgu, out Dr))
            {
                while (Dr.Read())
                {
                    if (Genel.Tick < Environment.TickCount) { Genel.Tick = Environment.TickCount + 100; Application.DoEvents(); }
                    if (((string)Dr["Aciklama"]).ToLower().Contains(textBox_Arama.Text.ToLower())) { içeriktebulundu = true; break; }
                }
            }

            string Açıklaması = Sql_GörevÖzelliği((int)treeNode.Tag, EtkinlikÖzellikleri.Açıklaması) + "";

            if (treeNode.Text.ToLower().Contains(textBox_Arama.Text.ToLower()) || 
                Açıklaması.ToLower().Contains(textBox_Arama.Text.ToLower()) ||
                içeriktebulundu)
            {
                Arama.RowCount++;
                Arama[0, Arama.RowCount - 1].Value = treeNode.Text;
                Arama[0, Arama.RowCount - 1].Tag = treeNode; 
            }

            foreach (TreeNode tn in treeNode.Nodes) textBox_Arama_TextChanged_2(tn);
        }
        private void textBox_Arama_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewCellEventArgs gecici;

            if (e.KeyCode == Keys.Up)
            {
                if (Arama.Rows.Count == 0) return;
                int seçilen = -1;
                for (int i = 0; i < Arama.Rows.Count; i++)
                {
                    if (Arama.Rows[i].Selected) { seçilen = i; Arama.Rows[i].Selected = false; break; }
                }
                if (seçilen < 0) { Arama.Rows[0].Selected = true; seçilen = 0; }
                else if (seçilen > 0)
                {
                    seçilen--;
                    Arama.Rows[seçilen].Selected = true;
                    gecici = new DataGridViewCellEventArgs(0, seçilen);
                    Arama_CellClick(null, gecici);
                }
                else if (seçilen == 0) Arama.Rows[0].Selected = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (Arama.Rows.Count == 0) return;
                int seçilen = -1;
                for (int i = 0; i < Arama.Rows.Count; i++)
                {
                    if (Arama.Rows[i].Selected) { seçilen = i; Arama.Rows[i].Selected = false; break; }
                }
                if (seçilen < 0) { Arama.Rows[0].Selected = true; seçilen = 0; }
                else if (seçilen < Arama.Rows.Count - 1)
                {
                    seçilen++;
                    Arama.Rows[seçilen].Selected = true;
                    gecici = new DataGridViewCellEventArgs(0, seçilen);
                    Arama_CellClick(null, gecici);
                }
                else if (seçilen == Arama.Rows.Count - 1) Arama.Rows[seçilen].Selected = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (Arama.SelectedCells.Count == 0) return;
                if (KeTa[3].ListeGörünüyorMu()) return;

                Ağaç.SelectedNode = null;
                Ağaç.SelectedNode = (TreeNode)Arama.SelectedCells[0].Tag;
            }
        }
        private void Arama_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Genel.AğaçGüncelleniyor = true;
            Ağaç.SelectedNode = (TreeNode)Arama[0, e.RowIndex].Tag;
            Genel.AğaçGüncelleniyor = false;

            Arama.Rows[e.RowIndex].Selected = true;
        }
        private void Arama_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Ağaç.SelectedNode = null;
            Ağaç.SelectedNode = (TreeNode)Arama[0, e.RowIndex].Tag;
        }
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = Genel.Pencere;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        private void Panel_Aç(Panel2Durumu Panel2)
        {
            if (Genel.Panel2 == Panel2) return;
            for (int i = 0; i < splitContainer1.Panel2.Controls.Count; i++) splitContainer1.Panel2.Controls[i].Visible = false;

            switch (Panel2)
            {
                case Panel2Durumu.Görev:
                    splitContainer1.Panel2.Controls["panel_Görev"].Visible = true;
                    Genel.Panel2 = Panel2Durumu.Görev;
                    break;

                case Panel2Durumu.Etkinlik:
                    splitContainer1.Panel2.Controls["panel_Etkinlik"].Visible = true;
                    Genel.Panel2 = Panel2Durumu.Etkinlik;
                    break;

                case Panel2Durumu.Arama:
                    splitContainer1.Panel2.Controls["panel_Arama"].Visible = true;
                    Genel.Panel2 = Panel2Durumu.Arama;
                    break;

                case Panel2Durumu.Ayarlar:
                    splitContainer1.Panel2.Controls["panel_Ayarlar"].Visible = true;
                    Genel.Panel2 = Panel2Durumu.Ayarlar;
                    break;
            }
        }
        private void SayfaDüzeni_Normal()
        {
            toolStripEkle.Image = Etkinlik_Takip.Properties.Resources.Ekle;
            toolStripArama.Image = Etkinlik_Takip.Properties.Resources.Arama;

            for (int i = 0; i < toolStripEkle.DropDownItems.Count; i++) toolStripEkle.DropDownItems[i].Visible = true;
            toolStripEkle.Visible = true;
            toolStripAyarlar.Visible = true;
        }
        private void SayfaDüzeni_OnayRet()
        {
            toolStripEkle.Image = Etkinlik_Takip.Properties.Resources.Onay;
            toolStripArama.Image = Etkinlik_Takip.Properties.Resources.Ret;

            for (int i = 0; i < toolStripEkle.DropDownItems.Count; i++) toolStripEkle.DropDownItems[i].Visible = false;
            toolStripEkle.Visible = false;
            toolStripAyarlar.Visible = false;
        }
        private void PuntoDeğişti(object sender, EventArgs e)
        {
            statusStrip1.ImageScalingSize = new Size((int)numericUpDown1.Value * 2, (int)numericUpDown1.Value * 2);
            statusStrip1.Font = new Font(statusStrip1.Font.FontFamily, (float)numericUpDown1.Value);
            splitContainer1.Font = new Font(splitContainer1.Font.FontFamily, (float)numericUpDown1.Value);
            Menu_Grid_Etkinlikler.Font = new Font(Menu_Grid_Etkinlikler.Font.FontFamily, (float)numericUpDown1.Value);
            MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Font = new Font(MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Font.FontFamily, (float)numericUpDown1.Value);
            Menu_Ağaç.ImageScalingSize = new Size((int)numericUpDown1.Value * 2, (int)numericUpDown1.Value * 2);
            MenuItem_Ağaç_Tanım.Font = new Font(MenuItem_Ağaç_Tanım.Font.FontFamily, (float)numericUpDown1.Value);
            MenuItem_Ağaç_Açıklama.Font = new Font(MenuItem_Ağaç_Açıklama.Font.FontFamily, (float)numericUpDown1.Value);
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Font = new Font(MenuItem_Grid_Etk_ZaAr_Baslangıç.Font.FontFamily, (float)numericUpDown1.Value);
            MenuItem_Grid_Etk_ZaAr_Bitiş.Font = new Font(MenuItem_Grid_Etk_ZaAr_Bitiş.Font.FontFamily, (float)numericUpDown1.Value);
            Menu_Ağaç.Font = new Font(Menu_Ağaç.Font.FontFamily, (float)numericUpDown1.Value);
            this.Font = new Font(this.Font.FontFamily, (float)numericUpDown1.Value);

            MenuItem_Ağaç_Tanım.Width = MenuItem_Ağaç_BittiGeriBildirimBekleniyor.Width / 2 + 50;
            MenuItem_Ağaç_Açıklama.Width = MenuItem_Ağaç_Tanım.Width;
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Width = MenuItem_Ağaç_Tanım.Width;
            MenuItem_Grid_Etk_ZaAr_Bitiş.Width = MenuItem_Ağaç_Tanım.Width;
            MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Width = MenuItem_Ağaç_Tanım.Width / 2;

            ımageList1.ImageSize = new Size((int)(numericUpDown1.Value * new decimal(1.75)), (int)(numericUpDown1.Value * new decimal(1.75)));
            ımageList1.Images.Clear();
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.G_Durum_0);   
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.G_Durum_1);   
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.G_Durum_2);   
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.G_Durum_3);   
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.G_Durum_4);   
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.G_Durum_5);       
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.G_Durum_6);   
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.G_Durum_7);
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.Ayarlar);     //8     //G_Durum_8
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.Secili);      //9     //ağaçtan tıklanan
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.Etkinlik);    //10    //Kullanıcı kendi resmi
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.Ret);         //11    //Çöp Kutusu
        }
        private void Uyarı(IWin32Window Pencere, string Mesaj, MessageBoxIcon ikon = MessageBoxIcon.Information, int Bekleme = 5000, string Başlık = "")
        {
            if (Başlık == "") Başlık = this.Text;
            MessageBox.Show(Pencere, Mesaj,Başlık,MessageBoxButtons.OK,ikon);
        }
        private string TarihDönüştürme(DateTime tarih)
        {
            int konum = 0;
            int fark = DateTime.Now.Year - tarih.Year;
            if (tarih.Year == DateTime.Now.Year)
            {
                konum++;
                fark = DateTime.Now.Month - tarih.Month;
                if (tarih.Month == DateTime.Now.Month)
                {
                    konum++;
                    fark = DateTime.Now.Day - tarih.Day;
                    if (tarih.Day == DateTime.Now.Day)
                    {
                        konum++;
                        fark = DateTime.Now.Hour - tarih.Hour;
                        if (tarih.Hour == DateTime.Now.Hour)
                        {
                            konum++;
                            fark = DateTime.Now.Minute - tarih.Minute;
                            if (tarih.Minute == DateTime.Now.Minute)
                            {
                                konum++;
                            }
                        }
                    }
                }
            }

            switch (konum)
            {
                default: return Convert.ToString(fark) + " yıl";

                case (1):
                    if (fark < 12) return tarih.ToString("MMMM");
                    else return Convert.ToString(fark) + " ay";

                case (2):
                    if (fark < 7) return tarih.ToString("dddd");
                    else return Convert.ToString(fark) + " gün";

                case (3): 
                    return Convert.ToString(fark) + " saat";

                case (4): 
                    return Convert.ToString(fark) + " dakika";

                case (5): 
                    return "az önce";
            }
        }
        private void Genel_KaydedilmemişBilgiVar(object sender, EventArgs e)
        {
            if (Genel.KaydedilmemişBilgiVar) return;

            if (Genel.Panel2 == Panel2Durumu.Görev && !splitContainer2.Panel2Collapsed && !Genel.GözGezdirmeKapatmaTalebi) SayfaDüzeni_OnayRet();

            Genel.KaydedilmemişBilgiVar = true;
            toolStripEkle.Visible = true;
        }
        private int YeniGörevOluştur(int Sahip, DateTime Tarih, string Tanım, string Açıklama = "")
        {
            if (Tanım == "") return 0;
            int adt = Convert.ToInt32(Sql_SorÖğren("select max(No) from Gorev"));
            Ağaç_DallarıDurumuÖnKontrol(adt);
            string Kendi = (adt + 1).ToString();
            
            if (!Sql_Sorgula("CREATE TABLE _t" + Kendi + " (Zaman DATETIME, Durum INTEGER, Aciklama TEXT)"))
            {
                Sql_Sorgula("DROP TABLE " + Kendi);
                if (!Sql_Sorgula("CREATE TABLE _t" + Kendi + " (Zaman DATETIME, Durum INTEGER, Aciklama TEXT)")) return 0;
            }
            if (!Sql_Sorgula("insert into _t" + Kendi + " values (null, -1, '" + Tarih.ToString() + "')")) return 0;
            if (!Sql_Sorgula("insert into _t" + Kendi + " values (null, -2, '" + Tanım + "')")) return 0;
            if (!Sql_Sorgula("insert into _t" + Kendi + " values (null, -3, '" + Açıklama + "')")) return 0;
            if (!Sql_Sorgula("insert into Gorev values (" + Kendi + ", " + Sahip.ToString() + ")")) return 0;

            Genel.AğaçDallarıDurumu[Sahip] = true;
            return Convert.ToInt32(Kendi);
        }

        private void toolStripEkle_Click(object sender, EventArgs e)
        {
            if (!toolStripAyarlar.Visible && Genel.KaydedilmemişBilgiVar)
            {
                int Sahip = 0;
                if (Ağaç.SelectedNode != null) Sahip = (int)Ağaç.SelectedNode.Tag;

                if (Genel.Panel2 == Panel2Durumu.Görev)
                {
                    //Görev
                    if (splitContainer2.Panel2Collapsed)
                    {
                        //Görev Ekleme Ekranından ekleme
                        if (textBox_Görev_Tanım.Text == "") { Uyarı(textBox_Görev_Tanım, "Tanımı doldurunuz.", MessageBoxIcon.Error); return; }
                        DateTime tarih = Görev_TarihSeçici.Value;
                        List<string> gecici = textBox_Görev_Açıklama.Lines.ToList();
                       
                        int Kendi = YeniGörevOluştur(Sahip, tarih, textBox_Görev_Tanım.Text);
                        if (Kendi == 0) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                        
                        for (int i = 0; i < gecici.Count();i++)
                        {
                            if (gecici[i].StartsWith("%"))
                            {
                                if (YeniGörevOluştur(Sahip, tarih, gecici[i].Remove(0,1))==0) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                                gecici[i] = "";
                            }
                            else if (gecici[i].StartsWith("?"))
                            {
                                if (YeniGörevOluştur(Kendi, tarih, gecici[i].Remove(0, 1)) == 0) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                                gecici[i] = "";
                            }
                        }

                        textBox_Görev_Açıklama.Text ="";
                        for (int i = 0; i < gecici.Count(); i++) if (gecici[i] != "") textBox_Görev_Açıklama.Text += gecici[i] + Environment.NewLine;
                        textBox_Görev_Açıklama.Text = textBox_Görev_Açıklama.Text.Trim();

                        if (textBox_Görev_Açıklama.Text != "") if (!Sql_Sorgula("insert into _t" + Kendi + " values (null, -3, '" + textBox_Görev_Açıklama.Text + "')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
 
                    }
                    else
                    {
                        //görev listeleme ekranında görev güncellendi, etkinlik olarak kaydet
                        if (Sahip == 0) return;
                        string ek = "";

                        string gecici = Sql_GörevÖzelliği(Sahip, EtkinlikÖzellikleri.Tanımı);
                        if (gecici != textBox_Görev_Tanım.Text) ek += "Görev Tanım Değişikliği, o anda : " + Environment.NewLine + gecici + " " + Environment.NewLine;

                        gecici = Sql_GörevÖzelliği(Sahip, EtkinlikÖzellikleri.Açıklaması);
                        if (gecici != textBox_Görev_Açıklama.Text) ek += "Görev Açıklama Değişikliği, o anda : " + Environment.NewLine + gecici;
                        
                        string sorgu = "insert into _t" + Sahip.ToString() + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), ";
                        sorgu += ((int)(EtkinlikDurumu.Güncellenen_Görev)).ToString() + ", '";
                        sorgu += ek + "')";
                        if (!Sql_Sorgula(sorgu)) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                        if (!Sql_Sorgula("update _t" + Sahip.ToString() + " set Aciklama = '" + textBox_Görev_Tanım.Text + "' where Durum = -2")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                        if (!Sql_Sorgula("update _t" + Sahip.ToString() + " set Aciklama = '" + textBox_Görev_Açıklama.Text + "' where Durum = -3")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                    }
                }
                else
                {
                    //Etkinlik  
                    if (Sahip == 0) { Uyarı(Ağaç, "Ağaçtan görev seçiniz.", MessageBoxIcon.Warning); return; }
                    if (comboBox_Etkinlik_Durum.SelectedIndex == -1) { Uyarı(comboBox_Etkinlik_Durum, "Durum seçiniz.", MessageBoxIcon.Warning); comboBox_Etkinlik_Durum.DroppedDown = true; return; }

                    DateTime tarih = Etkinlik_TarihSeçici.Value;
                    string sorgu = "insert into _t" + Sahip.ToString() + " values (DATETIME('" + tarih.ToString("yyyy-MM-dd HH:mm:ss") + "'), ";
                    sorgu += (comboBox_Etkinlik_Durum.SelectedIndex+1).ToString() + ", '";
                    sorgu += textBox_Etkinlik_Açıklama.Text + "')";
                    if (!Sql_Sorgula(sorgu)) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }

                    if (Üç_Değiştir.Checked)
                    {
                        SQLiteDataReader Dr;
                        string Sorgu = "SELECT name FROM sqlite_master where name like '_t%' and name is not '_t" + Sahip.ToString() + "' UNION SELECT name FROM sqlite_temp_master where name like '_t%' and name is not '_t" + Sahip.ToString() + "'";
                        if (!Sql_Sorgula(Sorgu, out Dr)) { label_Görev_Tarih.Text = "Beklenmeyen durum oluştu. Tekrar deneyiniz"; return; }

                        TreeNode tr = Ağaç.SelectedNode;
                        string st = "";
                        string sl = " -> " + Ağaç.Nodes[0].Text + " -> ";
                        while (tr != null)
                        {
                            st = " -> " + tr.Text + st;
                            tr = tr.Parent;
                        }
                        st = st.Remove(0, sl.Length);

                        while (Dr.Read())
                        {
                            string tablo = Dr["name"].ToString();
                            string durum = Sql_GörevÖzelliği(Convert.ToInt32(tablo.Remove(0, 2)), EtkinlikÖzellikleri.Durumu);

                            if (durum == ((int)EtkinlikDurumu.Üzerinde_Çalışılıyor).ToString())
                            {
                                if (!Sql_Sorgula("insert into " + tablo + " values (DATETIME('" + tarih.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + ((int)EtkinlikDurumu.Düşük_Öncelikli).ToString() + ", '" + st + "')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                            }
                        }
                    }
                }

                Ağaç_Güncelle();
                Panel_Aç(Panel2Durumu.Arama);
                SayfaDüzeni_Normal();
                Genel.KaydedilmemişBilgiVar = false;
            }
        }
        private void toolStripEkle_Görev_Click(object sender, EventArgs e)
        {
            Görev_TarihSeçici.Visible = true;
            Görev_TarihSeçici.Value = DateTime.Now;
            textBox_Görev_Tanım.Text = "";
            textBox_Görev_Açıklama.Text = "";
            splitContainer2.Panel2Collapsed = true;
            Panel_Aç(Panel2Durumu.Görev);
            textBox_Görev_Tanım.Focus();

            SayfaDüzeni_OnayRet();
            label_Görev_Tarih_LinkClicked(null, null);
            Ağaç.SelectedNode = null;
            Genel.KaydedilmemişBilgiVar = false;
        }
        private void toolStripEkle_Etkinlik_Click(object sender, EventArgs e)
        {
            Etkinlik_TarihSeçici.Value = DateTime.Now;
            textBox_Etkinlik_Açıklama.Text = "";
            Panel_Aç(Panel2Durumu.Etkinlik);
            splitContainer2.Panel2Collapsed = true;
            comboBox_Etkinlik_Durum.SelectedIndex = -1;
            comboBox_Etkinlik_Durum_SelectedIndexChanged(null, null);
            comboBox_Etkinlik_Durum.DroppedDown = true;
            comboBox_Etkinlik_Durum.Focus();

            SayfaDüzeni_OnayRet();
            Ağaç.SelectedNode = null;
            Genel.KaydedilmemişBilgiVar = false;
        }    
        private void toolStripAyarlar_Click(object sender, EventArgs e)
        {
            Panel_Aç(Panel2Durumu.Ayarlar);
        }
        private void toolStripSil_Click(object sender, EventArgs e)
        {
            if (!splitContainer2.Panel2Collapsed)
            {
                if (Genel.GözGezdirmeÇalışıyor)
                {
                    Genel.GözGezdirmeKapatmaTalebi = true;
                    return;
                }
                else if (Genel.KaydedilmemişBilgiVar)
                {
                    DialogResult Dr = MessageBox.Show("Kaydetmeden kapatmak istediğinize emin misiniz?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Dr == DialogResult.No) return;
                    Ağaç_AfterSelect(null, null);
                }
            }
            else
            {
                if (Genel.KaydedilmemişBilgiVar)
                {
                    DialogResult Dr = MessageBox.Show("Kaydetmeden kapatmak istediğinize emin misiniz?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Dr == DialogResult.No) return;           
                }
            }

            SayfaDüzeni_Normal();
            Panel_Aç(Panel2Durumu.Arama);
            textBox_Arama.SelectAll();
            textBox_Arama.Focus();
            Genel.KaydedilmemişBilgiVar = false;
        }

        private void Ağaç_MouseMove(object sender, MouseEventArgs e)
        {
            Genel.AnlıkFarePozisyonu_X = e.X;
            Genel.AnlıkFarePozisyonu_Y = e.Y;
        }
        private void Ağaç_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void Ağaç_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void Ağaç_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = Ağaç.PointToClient(new Point(e.X, e.Y));
            TreeNode Hedef = Ağaç.GetNodeAt(targetPoint);
            TreeNode Kaynak = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (Kaynak.Equals(Hedef)) return;
            if (Hedef == null) return;
            if (Kaynak == null) return;

            TreeNode pare = Kaynak;
            int parentt_k = -2;
            while (pare != null)
            {
                if (pare.Parent == null) parentt_k = (int)pare.Tag;
                pare = pare.Parent;
            }
            if (parentt_k == -1) return;

            pare = Hedef;
            int parentt_h = -2;
            while (pare != null)
            {
                if (pare.Parent == null) parentt_h = (int)pare.Tag;
                else if (pare.Tag == Kaynak.Tag) return;
                pare = pare.Parent;
            }
            if (parentt_h == -1) return;

            if (Kaynak.Parent == Hedef) return;

            string Kaynak_Adı = "Kök";
            if (Kaynak.Parent != null) Kaynak_Adı = Sql_GörevÖzelliği((int)Kaynak.Parent.Tag, EtkinlikÖzellikleri.Tanımı);

            if (!Sql_Sorgula("insert into _t" + ((int)Kaynak.Tag).ToString() + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + ((int)EtkinlikDurumu.Güncellenen_Görev).ToString() + ", 'Ağaç dal değişikliği, o anda : " + Environment.NewLine + Kaynak_Adı + "')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
            if (!Sql_Sorgula("update Gorev set Sahip = " + Convert.ToString((int)Hedef.Tag) + " where No = " + ((int)Kaynak.Tag).ToString())) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
            Genel.AğaçDallarıDurumu[(int)Hedef.Tag] = true;

            Ağaç_Güncelle();
        }
        private void Ağaç_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if ((toolStripAyarlar.Visible || !splitContainer2.Panel2Collapsed) && !Genel.AğaçGüncelleniyor)
                {
                    //Göz Gezdirme
                    if (Genel.KaydedilmemişBilgiVar)
                    {
                        DialogResult Dr = MessageBox.Show("Kaydetmeden kapatmak istediğinize emin misiniz?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (Dr == DialogResult.No) return;
                    }

                    Sutun_Gorev.Visible = false;
                    if ((int)Ağaç.SelectedNode.Tag > 0)
                    {
                        //Görev
                        if (Genel.GözGezdirmeÇalışıyor) return;

                        Görev_TarihSeçici.Visible = false;
                        splitContainer2.Panel2Collapsed = false;
                        Panel_Aç(Panel2Durumu.Görev);
                        label_Görev_Tarih.Text = "Bekleyiniz";
                        int Tablo = (int)Ağaç.SelectedNode.Tag;
                        textBox_Görev_Açıklama.Text = Sql_GörevÖzelliği(Tablo, EtkinlikÖzellikleri.Açıklaması);
                        textBox_Görev_Tanım.Text = Sql_GörevÖzelliği(Tablo, EtkinlikÖzellikleri.Tanımı);
                        label_Görev_Tarih.Text = Sql_GörevÖzelliği(Tablo, EtkinlikÖzellikleri.OluşturulmaTarihi);

                        if (!Sql_Sorgula("delete from gecici")) { label_Görev_Tarih.Text = "Beklenmeyen durum oluştu. Tekrar deneyiniz"; return; }
                        if (!Sql_Sorgula("insert into gecici (Zaman, Durum, Aciklama) select * from _t" + Tablo.ToString() + " where Zaman is not null")) { label_Görev_Tarih.Text = "Beklenmeyen durum oluştu. Tekrar deneyiniz"; return; }
                        MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
                    }
                    else
                    {
                        //Etkinlik
                    }
                }
                else
                {
                    //Görev veya Etkinlik Ekleme Düzenleme
                    TreeNode pare = Ağaç.SelectedNode;
                    int parentt = -2;
                    while (pare != null)
                    {
                        if (pare.Parent == null) parentt = (int)pare.Tag;
                        pare = pare.Parent;
                    }
                    if (parentt == -1) { Ağaç.SelectedNode = null; return; }

                    label_Görev_Tarih.Text = Ağaç.SelectedNode.Text;
                    label_Görev_Tarih.Enabled = true;

                    label_Etkinlik_Tarih.Text = Ağaç.SelectedNode.Text;
                }
            }
            catch (Exception ex) { label_Görev_Tarih.Text = ex.Message; }
        }
        private void Ağaç_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if ((int)e.Node.Tag < 1) return;
            Ağaç_DallarıDurumuÖnKontrol((int)e.Node.Tag);
            Genel.AğaçDallarıDurumu[(int)e.Node.Tag] = false;
        }
        private void Ağaç_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if ((int)e.Node.Tag < 1) return;
            Ağaç_DallarıDurumuÖnKontrol((int)e.Node.Tag);
            Genel.AğaçDallarıDurumu[(int)e.Node.Tag] = true;
        }
        private void Ağaç_DallarıDurumuÖnKontrol(int boyut)
        {
            if (boyut >= Genel.AğaçDallarıDurumu.Length)
            {
                bool[] gecici = new bool[Genel.AğaçDallarıDurumu.Length];
                Genel.AğaçDallarıDurumu.CopyTo(gecici, 0);
                Genel.AğaçDallarıDurumu = new bool[boyut + 35];
                gecici.CopyTo(Genel.AğaçDallarıDurumu, 0);
            }
        }

        private void Menu_Ağaç_Opening(object sender, CancelEventArgs e)
        {
            //-2 null
            //-1 çöp kutusu
            // diğer normal

            if (Genel.AğaçGüncelleniyor) { return; }
            
            TreeNode pare = Ağaç.SelectedNode;
            int parentt = -2;
            while (pare != null)
            {
                if (pare.Parent == null) parentt = (int)pare.Tag;
                pare = pare.Parent;
            }

            if (parentt == -1)
            {
                if (Ağaç.SelectedNode.Level != 1) { e.Cancel = true; return; }

                Genel.AğaçGüncelleniyor = true;

                for (int i = 0; i < Menu_Ağaç.Items.Count; i++) Menu_Ağaç.Items[i].Visible = false;
                MenuItem_Ağaç_GeriAl.Visible = true;
                MenuItem_Ağaç_Sil.Visible = true;
            }
            else
            {
                Genel.AğaçGüncelleniyor = true;

                for (int i = 0; i < Menu_Ağaç.Items.Count; i++) Menu_Ağaç.Items[i].Visible = false;
                MenuItem_Ağaç_Görev.Checked = false;
                MenuItem_Ağaç_Etkinlik.Checked = false;

                MenuItem_Ağaç_Görev.Visible = true;
                MenuItem_Ağaç_Etkinlik.Visible = true;
                MenuItem_Ağaç_TümEtkinliklerinListele.Visible = true;
                MenuItem_Ağaç_TümünüAç.Visible = true;
                MenuItem_Ağaç_TümünüDaralt.Visible = true;
                MenuItem_Ağaç_Sil.Visible = true;
                toolStripSeparator2.Visible = true;
                toolStripSeparator3.Visible = true;
            }

            Genel.AçıldığındakiFarePozisyonu_X = Genel.AnlıkFarePozisyonu_X;
            Genel.AçıldığındakiFarePozisyonu_Y = Genel.AnlıkFarePozisyonu_Y;

            Genel.AğaçGüncelleniyor = false;
        }
        private void Menu_Ağaç_Opened(object sender, EventArgs e)
        {
            Genel.Menu_Ağac_Açık = true;
        }
        private void Menu_Ağaç_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            Genel.Menu_Ağac_Açık = false;
        }

        private void MenuItem_Ağaç_Yeni_Click(object sender, EventArgs e)
        {
            MenuItem_Ağaç_Çalış(EtkinlikDurumu.Yeni_Görev);
        }
        private void MenuItem_Ağaç_ÜzerindeÇalışılıyor_Click(object sender, EventArgs e)
        {
            MenuItem_Ağaç_Çalış(EtkinlikDurumu.Üzerinde_Çalışılıyor);
        }
        private void MenuItem_Ağaç_DüşükÖncelikli_Click(object sender, EventArgs e)
        {
            MenuItem_Ağaç_Çalış(EtkinlikDurumu.Düşük_Öncelikli);
        }
        private void MenuItem_Ağaç_Beklemede_Click(object sender, EventArgs e)
        {
            MenuItem_Ağaç_Çalış(EtkinlikDurumu.Beklemede);
        }
        private void MenuItem_Ağaç_BittiGeriBildirimBekleniyor_Click(object sender, EventArgs e)
        {
            MenuItem_Ağaç_Çalış(EtkinlikDurumu.Bitti_Geri_Bildirim_Bekleniyor);
        }
        private void MenuItem_Ağaç_Diğer_Click(object sender, EventArgs e)
        {
            MenuItem_Ağaç_Çalış(EtkinlikDurumu.Diğer);
        }
        private void MenuItem_Ağaç_Tamamlandı_Click(object sender, EventArgs e)
        {
            MenuItem_Ağaç_Çalış(EtkinlikDurumu.Tamamlandı);
        }
        private void MenuItem_Ağaç_İptalEdildi_Click(object sender, EventArgs e)
        {
            MenuItem_Ağaç_Çalış(EtkinlikDurumu.İptal_Edildi);
        }
        private void MenuItem_Ağaç_Çalış(EtkinlikDurumu Durum)
        {
            int Sahip = 0;
            string st = "";
            bool konum = MenuItem_Ağaç_Görev.Checked;
            if (Ağaç.SelectedNode != null) Sahip = (int)Ağaç.SelectedNode.Tag;

            if (Durum == EtkinlikDurumu.Üzerinde_Çalışılıyor && Ağaç.SelectedNode != null && Ağaç.Nodes[0] != Ağaç.SelectedNode)
            {
                TreeNode tr = Ağaç.SelectedNode;
                string sl = " -> " + Ağaç.Nodes[0].Text + " -> ";
                while (tr != null)
                {
                    st = " -> " + tr.Text + st;
                    tr = tr.Parent;
                }
                st = st.Remove(0, sl.Length);
            }

            MenuItem_Ağaç_Çalış_Devam:
            if (konum)
            {
                //Görev
                if (MenuItem_Ağaç_Tanım.Text == "") { Uyarı(Ağaç, "Tanımı doldurunuz.", MessageBoxIcon.Error); MenuItem_Ağaç_Tanım.Focus(); return; }

                Sahip = YeniGörevOluştur(Sahip, DateTime.Now, MenuItem_Ağaç_Tanım.Text, MenuItem_Ağaç_Açıklama.Text);
                if (Sahip == 0) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz."); goto HatalıÇıkış; }

                if (Durum != EtkinlikDurumu.Yeni_Görev)
                {
                    if (Durum == EtkinlikDurumu.Üzerinde_Çalışılıyor) st += " -> " + MenuItem_Ağaç_Tanım.Text;
                    konum = false;
                    goto MenuItem_Ağaç_Çalış_Devam;
                }
            }
            else
            {
                //Etkinlik
                if (Sahip < 1) { Uyarı(Ağaç, "Ağaçtan görev seçiniz.", MessageBoxIcon.Error); Menu_Ağaç.Close(); return; }
                if (!Sql_Sorgula("insert into _t" + (Sahip.ToString() + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + ((int)Durum).ToString() + ", '" + MenuItem_Ağaç_Açıklama.Text + "')"))) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); goto HatalıÇıkış; }

                //Üzerinde çalışılıyor
                if (Durum == EtkinlikDurumu.Üzerinde_Çalışılıyor && Üç_Değiştir.Checked)
                {
                    SQLiteDataReader Dr;
                    string Sorgu = "SELECT name FROM sqlite_master where name like '_t%' and name is not '_t" + Sahip.ToString() + "' UNION SELECT name FROM sqlite_temp_master where name like '_t%' and name is not '_t" + Sahip.ToString() + "'";
                    if (!Sql_Sorgula(Sorgu, out Dr)) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); goto HatalıÇıkış; }

                    while (Dr.Read())
                    {
                        string tablo = Dr["name"].ToString();
                        string durum = Sql_GörevÖzelliği(Convert.ToInt32(tablo.Remove(0, 2)), EtkinlikÖzellikleri.Durumu);

                        if (durum == ((int)EtkinlikDurumu.Üzerinde_Çalışılıyor).ToString())
                        {
                            if (!Sql_Sorgula("insert into " + tablo + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + ((int)EtkinlikDurumu.Düşük_Öncelikli).ToString() + ", '" + st + "')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); goto HatalıÇıkış; }
                        }
                    }
                }
            }

            MenuItem_Ağaç_Tanım.Text = "";
            MenuItem_Ağaç_Açıklama.Text = "";
            
            HatalıÇıkış:
            Menu_Ağaç.Close();
            Ağaç_Güncelle();
        }
        private void MenuItem_Ağaç_TümünüAç_Click(object sender, EventArgs e)
        {
            if (Ağaç.SelectedNode == null) Ağaç.ExpandAll();
            else Ağaç.SelectedNode.ExpandAll();
        }
        private void MenuItem_Ağaç_TümünüDaralt_Click(object sender, EventArgs e)
        {
            if (Ağaç.SelectedNode == null) Ağaç.CollapseAll();
            else Ağaç.SelectedNode.Collapse();
        }
        private void MenuItem_Ağaç_Sil_Click(object sender, EventArgs e)
        {
            if (Ağaç.SelectedNode == null) { Menu_Ağaç.Close(); Uyarı(Ağaç, "Ağaçtan görev seçiniz.", MessageBoxIcon.Error); return; }

            TreeNode pare = Ağaç.SelectedNode;
            int parentt_k = -2;
            while (pare != null)
            {
                if (pare.Parent == null) parentt_k = (int)pare.Tag;
                pare = pare.Parent;
            }

            if (parentt_k == -1)
            {
                //tamamiyle sil
                DialogResult Dr = MessageBox.Show("Geri alınamıyacak şekilde silmek istediğinize emin misiniz?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                if (!Sql_Sorgula("drop table _t" + ((int)Ağaç.SelectedNode.Tag).ToString())) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                if (!Sql_Sorgula("delete from Gorev where No = " + ((int)Ağaç.SelectedNode.Tag).ToString())) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
            }
            else 
            {
                ///çöp kutusuna taşı
                string sahip = Convert.ToString(Sql_SorÖğren("select Sahip from Gorev where No = " + ((int)Ağaç.SelectedNode.Tag).ToString()));
                if (!Sql_Sorgula("insert into _t" + ((int)Ağaç.SelectedNode.Tag).ToString() + " values (null, -4, '" + sahip + "')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                if (!Sql_Sorgula("insert into _t" + ((int)Ağaç.SelectedNode.Tag).ToString() + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + ((int)EtkinlikDurumu.Güncellenen_Görev).ToString() + ", 'Silindi')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                if (!Sql_Sorgula("update Gorev set Sahip = -1 where No = " + ((int)Ağaç.SelectedNode.Tag).ToString())) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
            }
            
            Ağaç_Güncelle();
        }
        private void MenuItem_Ağaç_GeriAl_Click(object sender, EventArgs e)
        {
            if (Ağaç.SelectedNode == null) { Menu_Ağaç.Close(); Uyarı(Ağaç, "Ağaçtan görev seçiniz.", MessageBoxIcon.Error); return; }

            if (!Sql_Sorgula("insert into _t" + ((int)Ağaç.SelectedNode.Tag).ToString() + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + ((int)EtkinlikDurumu.Güncellenen_Görev).ToString() + ", 'Geri alındı')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
            string sahip = Sql_GörevÖzelliği((int)Ağaç.SelectedNode.Tag, EtkinlikÖzellikleri.SilinmedenÖncekiSahibi);
            if (!Sql_Sorgula("delete from _t" + ((int)Ağaç.SelectedNode.Tag).ToString() + " where Durum = -4")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
            if (!Sql_Sorgula("update Gorev set Sahip = " + sahip + " where No = " + ((int)Ağaç.SelectedNode.Tag).ToString())) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
            Ağaç_Güncelle();
        }
        private void MenuItem_Ağaç_TümEtkinliklerinListele_Click(object sender, EventArgs e)
        {
            try
            {
                if (!toolStripAyarlar.Visible) return;
                if (Genel.GözGezdirmeÇalışıyor) return;
                if ((int)Ağaç.SelectedNode.Tag < 0) return;

                splitContainer2.Panel2Collapsed = false;
                Panel_Aç(Panel2Durumu.Görev);
                label_Görev_Tarih.Text = "Bekleyiniz";
                if (!Sql_Sorgula("delete from gecici")) { label_Görev_Tarih.Text = "Beklenmeyen durum oluştu. Tekrar deneyiniz"; return; }

                if ((int)Ağaç.SelectedNode.Tag > 0)
                {
                    int Tablo = (int)Ağaç.SelectedNode.Tag;
                    textBox_Görev_Açıklama.Text = Sql_GörevÖzelliği(Tablo, EtkinlikÖzellikleri.Açıklaması);
                    textBox_Görev_Tanım.Text = Sql_GörevÖzelliği(Tablo, EtkinlikÖzellikleri.Tanımı);
                    label_Görev_Tarih.Text = Sql_GörevÖzelliği(Tablo, EtkinlikÖzellikleri.OluşturulmaTarihi);
                }
                else
                {
                    textBox_Görev_Açıklama.Text = "";
                    textBox_Görev_Tanım.Text = KullanıcıAdı.Text;
                    label_Görev_Tarih.Text = "";
                }

                MenuItem_Ağaç_TümEtkinliklerinListele_2(Ağaç.SelectedNode, textBox_Görev_Tanım.Text);
                Sutun_Gorev.Visible = true;
                MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
            }
            catch (Exception) { }
        }
        private void MenuItem_Ağaç_TümEtkinliklerinListele_2(TreeNode Dallar, string Tanım)
        {
            DateTime dt;
            DateTime.TryParse(Sql_GörevÖzelliği((int)Dallar.Tag, EtkinlikÖzellikleri.OluşturulmaTarihi), out dt); 

            Sql_Sorgula("insert into gecici (Zaman, Durum, Aciklama) select * from _t" + ((int)Dallar.Tag).ToString() + " where Zaman is not null");            //0:YeniGörev    
            Sql_Sorgula("insert into gecici (Zaman, Durum, Aciklama) values ('" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "', " + ((int)EtkinlikDurumu.Yeni_Görev).ToString() + ", 'Oluşturuldu')");
            Sql_Sorgula("update gecici set Tanim = '" + Tanım + "' where Tanim is null");

            foreach (TreeNode Dal in Dallar.Nodes) MenuItem_Ağaç_TümEtkinliklerinListele_2(Dal, Tanım + " -> " + Sql_GörevÖzelliği((int)Dal.Tag, EtkinlikÖzellikleri.Tanımı));
        }
        private void MenuItem_Ağaç_Görev_CheckedChanged(object sender, EventArgs e)
        {
            if (Genel.AğaçGüncelleniyor) return;

            if (MenuItem_Ağaç_Görev.Checked)
            {
                MenuItem_Ağaç_Etkinlik.Checked = false;
                MenuItem_Ağaç_Tanım.Visible = true;
                MenuItem_Ağaç_Yeni.Visible = true;
            }
            else if (!MenuItem_Ağaç_Etkinlik.Checked) MenuItem_Ağaç_Etkinlik.Checked = true;

            MenuItem_Ağaç_GörevEtkinlik_CheckedChanged();
        }
        private void MenuItem_Ağaç_Etkinlik_CheckedChanged(object sender, EventArgs e)
        {
            if (Genel.AğaçGüncelleniyor) return;

            if (MenuItem_Ağaç_Etkinlik.Checked)
            {
                MenuItem_Ağaç_Görev.Checked = false;
                MenuItem_Ağaç_Tanım.Visible = false;
                MenuItem_Ağaç_Yeni.Visible = false;
            }
            else if (!MenuItem_Ağaç_Görev.Checked) MenuItem_Ağaç_Görev.Checked = true;

            MenuItem_Ağaç_GörevEtkinlik_CheckedChanged();
        }       
        private void MenuItem_Ağaç_GörevEtkinlik_CheckedChanged()
        {
            MenuItem_Ağaç_Açıklama.Visible = true;
            MenuItem_Ağaç_ÜzerindeÇalışılıyor.Visible = true;
            MenuItem_Ağaç_DüşükÖncelikli.Visible = true;
            MenuItem_Ağaç_Beklemede.Visible = true;
            MenuItem_Ağaç_BittiGeriBildirimBekleniyor.Visible = true;
            MenuItem_Ağaç_Diğer.Visible = true;
            MenuItem_Ağaç_Tamamlandı.Visible = true;
            MenuItem_Ağaç_İptalEdildi.Visible = true;
            toolStripSeparator1.Visible = true;

            Genel.AğaçGüncelleniyor = true;
            Menu_Ağaç.Show(Ağaç, Genel.AçıldığındakiFarePozisyonu_X, Genel.AçıldığındakiFarePozisyonu_Y);
            Genel.AğaçGüncelleniyor = false;
        }

        private void MenuItem_Grid_Etk_AçıklamaTekSatırda_Click(object sender, EventArgs e)
        {
            MenuItem_Grid_Etk_AçıklamaTekSatırda.Checked = !MenuItem_Grid_Etk_AçıklamaTekSatırda.Checked;

            if (MenuItem_Grid_Etk_AçıklamaTekSatırda.Checked) Grid_Etkinlikler.Columns["Sutun_Açıklama"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            else Grid_Etkinlikler.Columns["Sutun_Açıklama"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
        private void MenuItem_Grid_Etk_Sütünlar_Durum_Click(object sender, EventArgs e)
        {
            MenuItem_Grid_Etk_Sütünlar_Durum.Checked = !MenuItem_Grid_Etk_Sütünlar_Durum.Checked;
            Sutun_Durum_G.Visible = MenuItem_Grid_Etk_Sütünlar_Durum.Checked;
        }
        private void MenuItem_Grid_Etk_Sütünlar_İçerik_Click(object sender, EventArgs e)
        {
            MenuItem_Grid_Etk_Sütünlar_İçerik.Checked = !MenuItem_Grid_Etk_Sütünlar_İçerik.Checked;
            Sutun_Durum_M.Visible = MenuItem_Grid_Etk_Sütünlar_İçerik.Checked;
        }
        private void MenuItem_Grid_Etk_Sütünlar_Tarih_Click(object sender, EventArgs e)
        {
            MenuItem_Grid_Etk_Sütünlar_Tarih.Checked = !MenuItem_Grid_Etk_Sütünlar_Tarih.Checked;
            Sutun_Tarih.Visible = MenuItem_Grid_Etk_Sütünlar_Tarih.Checked;
        }
        private void MenuItem_Grid_Etk_Sütünlar_Açıklama_Click(object sender, EventArgs e)
        {
            MenuItem_Grid_Etk_Sütünlar_Açıklama.Checked = !MenuItem_Grid_Etk_Sütünlar_Açıklama.Checked;
            Sutun_Açıklama.Visible = MenuItem_Grid_Etk_Sütünlar_Açıklama.Checked;
        }
        private void MenuItem_Grid_Etk_Sıralama_YeniUstte_Click(object sender, EventArgs e)
        {
            if (!MenuItem_Grid_Etk_Sıralama_YeniUstte.Checked)
            {
                MenuItem_Grid_Etk_Sıralama_YeniUstte.Checked = true;
                MenuItem_Grid_Etk_Sıralama_EskiUstte.Checked = false;
                MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
            }
        }
        private void MenuItem_Grid_Etk_Sıralama_EskiUstte_Click(object sender, EventArgs e)
        {
            if (!MenuItem_Grid_Etk_Sıralama_EskiUstte.Checked)
            {
                MenuItem_Grid_Etk_Sıralama_EskiUstte.Checked = true;
                MenuItem_Grid_Etk_Sıralama_YeniUstte.Checked = false;
                MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
            }
        }
        private void MenuItem_Grid_Etk_Sütünlar_An_Click(object sender, EventArgs e)
        {
            MenuItem_Grid_Etk_Sütünlar_An.Checked = !MenuItem_Grid_Etk_Sütünlar_An.Checked;
            Sutun_An.Visible = MenuItem_Grid_Etk_Sütünlar_An.Checked;
        }
        
        private void MenuItem_Grid_Etk_ZaAr_Filtrele_Click(object sender, EventArgs e)
        {
            try
            {
                if (Genel.GözGezdirmeÇalışıyor) { Genel.GözGezdirmeKapatmaTalebi = true; return; }
                Genel.GözGezdirmeÇalışıyor = true;
                Genel.GözGezdirmeKapatmaTalebi = false;
                Genel.KaydedilmemişBilgiVar = true;

                int İstenenÇıktıAdedi = 0;
                int.TryParse(MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Text, out İstenenÇıktıAdedi);
                if (İstenenÇıktıAdedi == 0) İstenenÇıktıAdedi = int.MaxValue;

                DateTime dt;
                string baslangıc, bitiş;
                DateTime.TryParse(MenuItem_Grid_Etk_ZaAr_Baslangıç.Text, out dt);
                baslangıc = "'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                if (DateTime.TryParse(MenuItem_Grid_Etk_ZaAr_Bitiş.Text, out dt)) bitiş = "'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                else bitiş = "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                toolStripİlerleme.Value = 0;
                toolStripİlerleme.Maximum = Convert.ToInt16(Sql_SorÖğren("select count(Durum) from gecici"));
                Grid_Etkinlikler.TopLeftHeaderCell.Value = toolStripİlerleme.Maximum.ToString();
                Grid_Etkinlikler.RowCount = 0;
                
                if (toolStripİlerleme.Maximum > 0)
                {
                    toolStripEtiket.Visible = false;
                    toolStripİlerleme.Width = toolStripEtiket.Width + 100;
                    toolStripİlerleme.Visible = true;
                    SayfaDüzeni_OnayRet();

                    SQLiteDataReader Dr;
                    string Sorgu = "select * from gecici where Zaman between " + baslangıc + " and " + bitiş + " order by Zaman";
                    if (MenuItem_Grid_Etk_Sıralama_YeniUstte.Checked) Sorgu += " desc";
                    if (!Sql_Sorgula(Sorgu, out Dr)) { label_Görev_Tarih.Text = "Beklenmeyen durum oluştu. Tekrar deneyiniz"; return; }
                    int tick = Environment.TickCount + 1000;
                    int filrelendi = 0;
                    while (Dr.Read())
                    {
                        int durum_no = Convert.ToInt32(Dr["Durum"]); 
                        if (MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan.Checked)
                        {
                            if (durum_no != (int)EtkinlikDurumu.Güncellenen_Görev)
                            {
                                if (Genel.AğacMenüFiltreleme[durum_no] != 1) { filrelendi++; goto devam; }
                            }
                        }

                        Grid_Etkinlikler.RowCount++;
                        int satır = Grid_Etkinlikler.RowCount - 1;
                        Grid_Etkinlikler["Sutun_Durum_G", satır].Value = ımageList1.Images[durum_no];
                        Grid_Etkinlikler["Sutun_Durum_G", satır].ToolTipText = EtkinlikDurumu_OkunabilirListe[durum_no];
                        Grid_Etkinlikler["Sutun_Durum_M", satır].Value = EtkinlikDurumu_OkunabilirListe[durum_no];
                        Grid_Etkinlikler["Sutun_An", satır].Value = TarihDönüştürme((DateTime)Dr["Zaman"]);
                        Grid_Etkinlikler["Sutun_An", satır].ToolTipText = ((DateTime)Dr["Zaman"]).ToString("dd/MMMM/yyyy HH:mm:ss");
                        Grid_Etkinlikler["Sutun_Tarih", satır].Value = ((DateTime)Dr["Zaman"]).ToString("dd/MMMM/yyyy HH:mm:ss");
                        Grid_Etkinlikler["Sutun_Açıklama", satır].Value = (string)Dr["Aciklama"];
                        if (Sutun_Gorev.Visible) Grid_Etkinlikler["Sutun_Gorev", satır].Value = (string)Dr["Tanim"];

                        devam:
                        toolStripİlerleme.Value++;
                        if (tick < Environment.TickCount) { tick = Environment.TickCount + 1000; Application.DoEvents(); }
                        if (Genel.GözGezdirmeKapatmaTalebi) break;
                        if (Grid_Etkinlikler.RowCount == İstenenÇıktıAdedi) break;
                    }
                    if (filrelendi > 0) Grid_Etkinlikler.TopLeftHeaderCell.Value += "-" + filrelendi.ToString();
                    if (Genel.GözGezdirmeKapatmaTalebi) Ağaç.SelectedNode = null;
                }
            }
            catch (Exception) { label_Görev_Tarih.Text = "Beklenmeyen durum oluştu. Tekrar deneyiniz"; }

            label_Görev_Tarih.Enabled = false;
            Genel.KaydedilmemişBilgiVar = false;
            toolStripİlerleme.Visible = false;
            toolStripEtiket.Visible = true;
            if (!toolStripAyarlar.Visible) SayfaDüzeni_Normal();
            Genel.GözGezdirmeÇalışıyor = false;
        }
        private void MenuItem_Grid_Etk_ZaAr_Bugün_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Text = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Bitiş.Text = dt.ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_ZaAr_Dün_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddDays(-1);
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Text = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Bitiş.Text = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_ZaAr_BuHafta_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddDays(-7);
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Text = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Bitiş.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_ZaAr_Son15Gün_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddDays(-15);
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Text = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Bitiş.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_ZaAr_BuAy_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Text = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Bitiş.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_ZaAr_Son3Ay_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddMonths(-3);
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Text = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Bitiş.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_ZaAr_Son6Ay_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddMonths(-6);
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Text = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Bitiş.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_ZaAr_BuYıl_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Text = new DateTime(dt.Year, 1, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Bitiş.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_ZaAr_GeçenYıl_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddYears(-1);
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Text = new DateTime(dt.Year, 1, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Bitiş.Text = new DateTime(dt.Year, 12, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm");
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_ZaAr_TümZamanlar_Click(object sender, EventArgs e)
        {
            MenuItem_Grid_Etk_ZaAr_Baslangıç.Text = "Büyük Patlama";
            MenuItem_Grid_Etk_ZaAr_Bitiş.Text = "Büyük Patlama";
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_ZaAr_Baslangıç_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter) MenuItem_Grid_Etk_ZaAr_Bitiş.Focus();
        }
        private void MenuItem_Grid_Etk_ZaAr_Bitiş_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter) MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
        private void MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan_Click(object sender, EventArgs e)
        {
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }
    }
}