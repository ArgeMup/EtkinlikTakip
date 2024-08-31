// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/EtkinlikTakip>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.Data.SQLite;
using System.IO;

using ArgeMup.HazirKod;
using ArgeMup.HazirKod.ArkaPlan;
using ArgeMup.HazirKod.Dönüştürme;
using System.Threading;
using ArgeMup.HazirKod.Ekİşlemler;

namespace Etkinlik_Takip
{
    public partial class AnaEkran : Form
    {
        #region Tanımlar
        YeniYazılımKontrolü_ YeniYazılımKontrolü = new YeniYazılımKontrolü_();
        string pak = Directory.GetCurrentDirectory() + "\\EtkinlikTakipDosyalari\\"; //programanaklasör
        public enum Panel2Durumu { Görev, Boş_Aralık, Arama, Ayarlar, Hatırlatıcı };
        public enum EtkinlikDurumu { Hatırlatıcı, Üzerinde_Çalışılıyor, Düşük_Öncelikli, Yeni_Görev, Beklemede, Bitti_Geri_Bildirim_Bekleniyor, Diğer, Tamamlandı, İptal_Edildi, Güncellenen_Görev, Geçersiz };
        public string[] EtkinlikDurumu_OkunabilirListe =
        {
            "Hatırlatıcı",
            "Üzerinde çalışılıyor",
            "Düşük öncelikli",
            "Yeni görev",
            "Beklemede",
            "Bitti, geri bildirim bekleniyor",
            "Diğer",
            "Tamamlandı",
            "İptal edildi",
            "Güncellendi"
        };
        public enum EtkinlikÖzellikleri { OluşturulmaTarihi = -1, Tanımı = -2, Açıklaması = -3, SilinmedenÖncekiSahibi = -4, Durumu = -5, Sahibi = -1000 };
        public enum DalTürü { Görevler = 0, ÇöpKutusu = -1, Şablonlar = -2, Geçersiz = -3 };
        struct Genel_
        {
            public Panel2Durumu Panel2;
            public bool KaydedilmemişBilgiVar;
            public int Tik;

            public List<TreeNode> Ağaç_TamListe;

            public bool AğaçGüncelleniyor;
            public bool GözGezdirmeÇalışıyor;
            public bool GözGezdirmeKapatmaTalebi;

            public int[] AğacMenüFiltreleme;
            public int AğacMenüFiltrelemeSayacGizli;
            public List<int> AğacMenüHatırlatıcıSayacı;

            public bool[] _AğaçDallarıDurumu;
            public bool AğaçDallarıDurumu_Oku(int No)
            {
                if (_AğaçDallarıDurumu == null) _AğaçDallarıDurumu = new bool[35];

                if (No >= 0)
                {
                    if (No >= _AğaçDallarıDurumu.Length) Array.Resize(ref _AğaçDallarıDurumu, No + 35);
                    return _AğaçDallarıDurumu[No];
                }
                //else
                //{
                //    No *= -1;
                //    return _AğaçDallarıDurumu_Diğerleri[No];
                //}
                return true;
            }
            public void AğaçDallarıDurumu_Yaz(int No, bool Değer)
            {
                if (_AğaçDallarıDurumu == null) _AğaçDallarıDurumu = new bool[35];

                if (No >= 0)
                {
                    if (No >= _AğaçDallarıDurumu.Length) Array.Resize(ref _AğaçDallarıDurumu, No + 35);
                    _AğaçDallarıDurumu[No] = Değer;
                }
                //else
                //{
                //    No *= -1;
                //    _AğaçDallarıDurumu_Diğerleri[No] = Değer;
                //}
            }

            public int AnlıkFarePozisyonu_X, AnlıkFarePozisyonu_Y;
            public int AçıldığındakiFarePozisyonu_X, AçıldığındakiFarePozisyonu_Y;

            public TreeNode SonSeçilenDal;
        };
        Genel_ Genel = new Genel_();

        struct Sql_
        {
            public SQLiteConnectionStringBuilder BağlantıMeşajıÜreteci;
            public SQLiteConnection Bağlantı;
        };
        Sql_ Sql = new Sql_();

        UygulamaOncedenCalistirildiMi_ UygulamaOncedenCalistirildiMi;

        class HatırlatıcıSayfası_
        {
            public Hatırlatıcı_ Hatırlatıcı = null;
            public Hatırlatıcı_ Hatırlatıcı_Kaydedilmeyen = null;
            public Ayarlar_ Ayarlar = null;
        }
        HatırlatıcıSayfası_ HatırlatıcıSayfası = new HatırlatıcıSayfası_();

        class NotlarSayfası_
        {
            public KlavyeFareGozlemcisi_ KlavyeFareGozlemcisi = new KlavyeFareGozlemcisi_(false, false, true);
            public NotlarEkranı Pencere = null;
            public bool KısayolTuşunuDinliyor = false;
            public Ayarlar_ Ayarlar = null;
            public IDepo_Eleman Ayarlar_Notlar = null;
        }
        NotlarSayfası_ NotlarSayfası = new NotlarSayfası_();

        IDepo_Eleman Ayarlar_Genel = null;
        #endregion

        public AnaEkran()
        {
            string geci = ("Mup" + Application.ProductName + pak).Replace('\\', '.').Replace(':', '.').Replace(' ', '.');
            UygulamaOncedenCalistirildiMi = new UygulamaOncedenCalistirildiMi_();
            if (UygulamaOncedenCalistirildiMi.KontrolEt(geci))
            {
                UygulamaOncedenCalistirildiMi.DiğerUygulamayıÖneGetir();
                notifyIcon1.Dispose();
                Environment.Exit(1);
                return;
            }

            InitializeComponent();
        }
        private void AnaEkran_Load(object sender, EventArgs e)
        {
            try
            {
                Visible = false;
                Application.DoEvents();

                Sql_Başlat();
                Genel.AğacMenüFiltreleme = new int[EtkinlikDurumu_OkunabilirListe.Length];
                this.Text = "Mup " + Kendi.Adı + " V" + Kendi.Sürümü_Dosya;

                while (splitContainer1.Panel2.Controls.Count > 0) splitContainer1.Panel2.Controls.RemoveAt(0);
                splitContainer1.Panel2.Controls.Add(panel_Görev);
                splitContainer1.Panel2.Controls.Add(panel_Arama);
                splitContainer1.Panel2.Controls.Add(panel_Ayarlar);
                splitContainer1.Panel2.Controls.Add(panel_Hatırlatıcı);
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

                NotlarSayfası_İlkAçılış();

                numericUpDown1.Value = (decimal)Ayarlar_Genel.Oku_Sayı("Punto", 8);
                PuntoDeğişti(null, null);
                KullanıcıAdı.Text = Ayarlar_Genel.Oku("KullanıcıAdı", Kendi.KullanıcıAdı);

                if (!string.IsNullOrEmpty(Ayarlar_Genel.Oku("Pencere Konumu/x")))
                {
                    this.WindowState = (FormWindowState)Ayarlar_Genel.Oku_Sayı("Pencere", 0);
                    Location = new System.Drawing.Point((int)Ayarlar_Genel.Oku_Sayı("Pencere Konumu/x"), (int)Ayarlar_Genel.Oku_Sayı("Pencere Konumu/y"));
                    Size = new System.Drawing.Size((int)Ayarlar_Genel.Oku_Sayı("Pencere Konumu/genişlik"), (int)Ayarlar_Genel.Oku_Sayı("Pencere Konumu/uzunluk"));  
                }
                this.Tag = this.WindowState;

                if (!Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(new Rectangle(this.Left, this.Top, this.Width, this.Height))))
                {
                    this.Left = 0; this.Top = 0; this.Width = 400; this.Height = 300;
                }

                Ağaç.SelectedImageIndex = 10;
                Ağaç.TreeViewNodeSorter = new Ağaç_NodeSorter();
                //int adt = 0; try { adt = Convert.ToInt32(Sql_SorÖğren("select max(No) from Gorev")); } catch (Exception) { }

                try
                {
                    string dallar = File.ReadAllText(pak + "Banka\\" + Application.ProductName + ".agac");
                    for (int i = 0; i < dallar.Length; i++)
                    {
                        if (dallar[i] == '1') Genel.AğaçDallarıDurumu_Yaz(i, true);

                        if (Genel.Tik < Environment.TickCount) { Application.DoEvents(); Genel.Tik = Environment.TickCount + 1000; }
                    }
                }
                catch (Exception) { Genel.AğaçDallarıDurumu_Yaz(0, true); }

                Genel.AğaçGüncelleniyor = true;
                Filtreleme_D1.Checked = Ayarlar_Genel.Oku_Bit("Filtreleme_ÜzerindeÇalışılıyor", true);
                Filtreleme_D2.Checked = Ayarlar_Genel.Oku_Bit("Filtreleme_DüşükÖncelikli", true);
                Filtreleme_D3.Checked = Ayarlar_Genel.Oku_Bit("Filtreleme_YeniGörev", true);
                Filtreleme_D4.Checked = Ayarlar_Genel.Oku_Bit("Filtreleme_Beklemede", true);
                Filtreleme_D5.Checked = Ayarlar_Genel.Oku_Bit("Filtreleme_BittiGeriBildirimBekleniyor", true);
                Filtreleme_D6.Checked = Ayarlar_Genel.Oku_Bit("Filtreleme_Diğer", true);
                Filtreleme_D7.Checked = Ayarlar_Genel.Oku_Bit("Filtreleme_Tamamlandı", true);
                Filtreleme_D8.Checked = Ayarlar_Genel.Oku_Bit("Filtreleme_İptalEdildi", true);
                Grid_Listele_Tarihçe.Checked = Ayarlar_Genel.Oku_Bit("Grid_Listele_Tarihçe", true);

                splitContainer1.SplitterDistance = (int)Ayarlar_Genel.Oku_Sayı("Ayıraç1", Width / 2);
                splitContainer2.SplitterDistance = (int)Ayarlar_Genel.Oku_Sayı("Ayıraç2", Height / 2);
                Panel_Aç(Panel2Durumu.Arama);

                Üç_Değiştir.Checked = Ayarlar_Genel.Oku_Bit("ÜzerindeÇalışılıyorDurumu", true);
                OdaklanmışGörünüm_Kızart.Checked = Ayarlar_Genel.Oku_Bit("OdaklanmışGörünüm_Kızart", true);
                OdaklanmışGörünüm_Genişlet.Checked = Ayarlar_Genel.Oku_Bit("OdaklanmışGörünüm_Genişlet", true);
                OdaklanmışGörünüm_DiğerleriniDaralt.Checked = Ayarlar_Genel.Oku_Bit("OdaklanmışGörünüm_DiğerleriniDaralt", false);

                Hatırlatıcı_Hatırlat_Bugün_aa.Value = (decimal)Ayarlar_Genel.Oku_Sayı("Hatırlatıcı_Hatırlat_Bugün_aa", 9);
                Hatırlatıcı_Hatırlat_Bugün_bb.Value = (decimal)Ayarlar_Genel.Oku_Sayı("Hatırlatıcı_Hatırlat_Bugün_bb", 11);
                Hatırlatıcı_Hatırlat_Bugün_cc.Value = (decimal)Ayarlar_Genel.Oku_Sayı("Hatırlatıcı_Hatırlat_Bugün_cc", 13);
                Hatırlatıcı_Hatırlat_Bugün_dd.Value = (decimal)Ayarlar_Genel.Oku_Sayı("Hatırlatıcı_Hatırlat_Bugün_dd", 17);
                Hatırlatıcı_Hatırlat_Yarın_aa.Value = (decimal)Ayarlar_Genel.Oku_Sayı("Hatırlatıcı_Hatırlat_Yarın_aa", 9);
                Hatırlatıcı_Hatırlat_Yarın_bb.Value = (decimal)Ayarlar_Genel.Oku_Sayı("Hatırlatıcı_Hatırlat_Yarın_bb", 11);
                Hatırlatıcı_Hatırlat_Yarın_cc.Value = (decimal)Ayarlar_Genel.Oku_Sayı("Hatırlatıcı_Hatırlat_Yarın_cc", 13);
                Hatırlatıcı_Hatırlat_Yarın_dd.Value = (decimal)Ayarlar_Genel.Oku_Sayı("Hatırlatıcı_Hatırlat_Yarın_dd", 17);

                MenuItem_Grid_Etk_Sütünlar_Durum.Checked = Ayarlar_Genel.Oku_Bit("Sutunlar_Durum", true);
                MenuItem_Grid_Etk_Sütünlar_İçerik.Checked = Ayarlar_Genel.Oku_Bit("Sutunlar_İçerik", true);
                MenuItem_Grid_Etk_Sütünlar_An.Checked = Ayarlar_Genel.Oku_Bit("Sutunlar_An", true);
                MenuItem_Grid_Etk_Sütünlar_Tarih.Checked = Ayarlar_Genel.Oku_Bit("Sutunlar_Tarih", true);
                MenuItem_Grid_Etk_Sütünlar_Açıklama.Checked = Ayarlar_Genel.Oku_Bit("Sutunlar_Açıklama", true);
                MenuItem_Grid_Etk_AçıklamaTekSatırda.Checked = Ayarlar_Genel.Oku_Bit("Sutunlar_AçıklamaTekSatırda");
                MenuItem_Grid_Etk_Sıralama_YeniUstte.Checked = Ayarlar_Genel.Oku_Bit("Sıralama_Ustteki", true);
                MenuItem_Grid_Etk_AyarlarSayfasındakiFiltrelemeyiKullan.Checked = Ayarlar_Genel.Oku_Bit("AyarlarSayfasındakiFiltrelemeyiKullan");
                MenuItem_Grid_Etk_Sıralama_EskiUstte.Checked = !MenuItem_Grid_Etk_Sıralama_YeniUstte.Checked;
                MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Text = Ayarlar_Genel.Oku("GörüntelenecekEtkinlikSayısı");

                Sutun_Durum_G.Visible = MenuItem_Grid_Etk_Sütünlar_Durum.Checked;
                Sutun_Durum_M.Visible = MenuItem_Grid_Etk_Sütünlar_İçerik.Checked;
                Sutun_An.Visible = MenuItem_Grid_Etk_Sütünlar_An.Checked;
                Sutun_Tarih.Visible = MenuItem_Grid_Etk_Sütünlar_Tarih.Checked;
                Sutun_Açıklama.Visible = MenuItem_Grid_Etk_Sütünlar_Açıklama.Checked;
                if (MenuItem_Grid_Etk_AçıklamaTekSatırda.Checked) Grid_Etkinlikler.Columns["Sutun_Açıklama"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                else Grid_Etkinlikler.Columns["Sutun_Açıklama"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                HatırlatıcıSayfası.Ayarlar = new Ayarlar_(AyarlarDosyası: pak + "Banka\\Hatırlatıcı.Ayarlar", DeğişiklikleriKaydetmeAralığı_Sn: 0);
                HatırlatıcıSayfası.Hatırlatıcı = new Hatırlatıcı_(HatırlatıcıSayfası.Ayarlar.Bul("Hatırlatıcı_", true));
                Hatırlatıcı_Tekrarla_dönem.SelectedIndex = 0;
                
                Genel.AğacMenüHatırlatıcıSayacı = new List<int>();
                Genel.KaydedilmemişBilgiVar = false;
                Genel.AğaçGüncelleniyor = false;
                textBox_Arama.Focus();

                Ağaç.Nodes.AddRange(new TreeNode[] {
                new TreeNode("Görevler"),
                new TreeNode("Şablonlar"),
                new TreeNode("Çöp Kutusu")
                });
                Ağaç_Güncelle(true, true, true);
                Filtreleme_DurumDeğişikliği(null, null);

                Visible = true;
                while (Opacity < 1)
                {
                    Opacity += 0.05;
                    Application.DoEvents();
                    Thread.Sleep(25);
                }
                Opacity = 1;

                Sql_GeçersizÜyeleriSil();

                #if !DEBUG
                    YeniYazılımKontrolü.Başlat(new Uri("https://github.com/ArgeMup/EtkinlikTakip/blob/master/Etkinlik%20Takip/bin/Release/Etkinlik%20Takip.exe?raw=true"));
                #endif

                List<Hatırlatıcı_.Durum_> Hatırlatıcılar = HatırlatıcıSayfası.Hatırlatıcı.Bul();
                foreach (var b in Hatırlatıcılar)
                {
                    if (int.TryParse(b.TakmaAdı, out _)) HatırlatıcıSayfası.Hatırlatıcı.Düzenle(b.TakmaAdı, Hatırlatıcı_GeriBildirim_İşlemi);
                }
                HatırlatıcıSayfası.Hatırlatıcı_Kaydedilmeyen = new Hatırlatıcı_();
                HatırlatıcıSayfası.Hatırlatıcı_Kaydedilmeyen.Ekle("Hatırlatıcı_GeriBildirim_İşlemi_TümSüresiDolanlar", DateTime.Now.AddSeconds(15), GeriBildirim_Islemi: Hatırlatıcı_GeriBildirim_İşlemi_TümSüresiDolanlar);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); Application.Exit(); }
        }
        private void AnaEkran_FormClosing(object sender, FormClosingEventArgs e)
        {
            #if DEBUG
                e = new FormClosingEventArgs(CloseReason.None, false);
            #endif

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                if (WindowState != FormWindowState.Minimized) Tag = WindowState;
                Hide();
                WindowState = FormWindowState.Minimized;
                Show();
            }
            else
            {
                toolStripSil_Click(null, null);
                if (Genel.KaydedilmemişBilgiVar) e.Cancel = true;

                Göster();
            }
        }
        private void AnaEkran_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = false;

            HatırlatıcıSayfası.Hatırlatıcı.AyarlarıOku(true);

            Ayarlar_Genel.Yaz("KullanıcıAdı", KullanıcıAdı.Text);
            Ayarlar_Genel.Yaz("Ayıraç1", splitContainer1.SplitterDistance);
            Ayarlar_Genel.Yaz("Ayıraç2", splitContainer2.SplitterDistance);

            Ayarlar_Genel.Yaz("Punto", (double)numericUpDown1.Value);
            if (this.WindowState != FormWindowState.Minimized)
            {
                Ayarlar_Genel.Yaz("Pencere", Convert.ToInt32(this.WindowState));

                if (this.WindowState == FormWindowState.Normal)
                {
                    Ayarlar_Genel.Yaz("Pencere Konumu/x", this.Location.X);
                    Ayarlar_Genel.Yaz("Pencere Konumu/y", this.Location.Y);
                    Ayarlar_Genel.Yaz("Pencere Konumu/genişlik", this.Width);
                    Ayarlar_Genel.Yaz("Pencere Konumu/uzunluk", this.Height);
                }
            }
            Ayarlar_Genel.Yaz("ÜzerindeÇalışılıyorDurumu", Üç_Değiştir.Checked);
            Ayarlar_Genel.Yaz("OdaklanmışGörünüm_Kızart", OdaklanmışGörünüm_Kızart.Checked);
            Ayarlar_Genel.Yaz("OdaklanmışGörünüm_Genişlet", OdaklanmışGörünüm_Genişlet.Checked);
            Ayarlar_Genel.Yaz("OdaklanmışGörünüm_DiğerleriniDaralt", OdaklanmışGörünüm_DiğerleriniDaralt.Checked);
            Ayarlar_Genel.Yaz("Hatırlatıcı_Hatırlat_Bugün_aa", (double)Hatırlatıcı_Hatırlat_Bugün_aa.Value);
            Ayarlar_Genel.Yaz("Hatırlatıcı_Hatırlat_Bugün_bb", (double)Hatırlatıcı_Hatırlat_Bugün_bb.Value);
            Ayarlar_Genel.Yaz("Hatırlatıcı_Hatırlat_Bugün_cc", (double)Hatırlatıcı_Hatırlat_Bugün_cc.Value);
            Ayarlar_Genel.Yaz("Hatırlatıcı_Hatırlat_Bugün_dd", (double)Hatırlatıcı_Hatırlat_Bugün_dd.Value);
            Ayarlar_Genel.Yaz("Hatırlatıcı_Hatırlat_Yarın_aa", (double)Hatırlatıcı_Hatırlat_Yarın_aa.Value);
            Ayarlar_Genel.Yaz("Hatırlatıcı_Hatırlat_Yarın_bb", (double)Hatırlatıcı_Hatırlat_Yarın_bb.Value);
            Ayarlar_Genel.Yaz("Hatırlatıcı_Hatırlat_Yarın_cc", (double)Hatırlatıcı_Hatırlat_Yarın_cc.Value);
            Ayarlar_Genel.Yaz("Hatırlatıcı_Hatırlat_Yarın_dd", (double)Hatırlatıcı_Hatırlat_Yarın_dd.Value);

            Ayarlar_Genel.Yaz("Filtreleme_ÜzerindeÇalışılıyor", Filtreleme_D1.Checked);
            Ayarlar_Genel.Yaz("Filtreleme_DüşükÖncelikli", Filtreleme_D2.Checked);
            Ayarlar_Genel.Yaz("Filtreleme_YeniGörev", Filtreleme_D3.Checked);
            Ayarlar_Genel.Yaz("Filtreleme_Beklemede", Filtreleme_D4.Checked);
            Ayarlar_Genel.Yaz("Filtreleme_BittiGeriBildirimBekleniyor", Filtreleme_D5.Checked);
            Ayarlar_Genel.Yaz("Filtreleme_Diğer", Filtreleme_D6.Checked);
            Ayarlar_Genel.Yaz("Filtreleme_Tamamlandı", Filtreleme_D7.Checked);
            Ayarlar_Genel.Yaz("Filtreleme_İptalEdildi", Filtreleme_D8.Checked);
            Ayarlar_Genel.Yaz("Grid_Listele_Tarihçe", Grid_Listele_Tarihçe.Checked);

            Ayarlar_Genel.Yaz("Sutunlar_Durum", MenuItem_Grid_Etk_Sütünlar_Durum.Checked);
            Ayarlar_Genel.Yaz("Sutunlar_İçerik", MenuItem_Grid_Etk_Sütünlar_İçerik.Checked);
            Ayarlar_Genel.Yaz("Sutunlar_An", MenuItem_Grid_Etk_Sütünlar_An.Checked);
            Ayarlar_Genel.Yaz("Sutunlar_Tarih", MenuItem_Grid_Etk_Sütünlar_Tarih.Checked);
            Ayarlar_Genel.Yaz("Sutunlar_Açıklama", MenuItem_Grid_Etk_Sütünlar_Açıklama.Checked);
            Ayarlar_Genel.Yaz("Sutunlar_AçıklamaTekSatırda", MenuItem_Grid_Etk_AçıklamaTekSatırda.Checked);
            Ayarlar_Genel.Yaz("Sıralama_Ustteki", MenuItem_Grid_Etk_Sıralama_YeniUstte.Checked);
            Ayarlar_Genel.Yaz("AyarlarSayfasındakiFiltrelemeyiKullan", MenuItem_Grid_Etk_AyarlarSayfasındakiFiltrelemeyiKullan.Checked);
            Ayarlar_Genel.Yaz("GörüntelenecekEtkinlikSayısı", MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Text);

            NotlarSayfası_Kapanış();
            Sql_Durdur(true);

            try
            {
                string dallar = "";
                foreach (bool bit in Genel._AğaçDallarıDurumu) { dallar += (bit ? '1' : '0'); }
                File.WriteAllText(pak + "Banka\\" + Application.ProductName + ".agac", dallar);
            }
            catch (Exception) { }

            #region yedekleme
            Klasör_ ydk_ler = new Klasör_(pak + "Yedekler", Filtre_Dosya: new string[] { "*.zip" });
            ydk_ler.Dosya_Sil_SayısınaVeBoyutunaGöre(15, 50 * 1024 * 1024);
            ydk_ler.Güncelle();
            
            bool yedekle = false;
            if (ydk_ler.Dosyalar.Count == 0) yedekle = true;
            else
            {
                ydk_ler.Sırala_EskidenYeniye();

                Klasör_ son_ydk = SıkıştırılmışDosya.Listele(ydk_ler.Kök + "\\" + ydk_ler.Dosyalar.Last().Yolu);
                Klasör_ güncel = new Klasör_(pak + "Banka");
                Klasör_.Farklılık_ farklar = güncel.Karşılaştır(son_ydk);
                if (farklar.FarklılıkSayısı > 0)
                {
                    int içeriği_farklı_dosya_Sayısı = 0;
                    foreach (Klasör_.Fark_Dosya_ a in farklar.Dosyalar)
                    {
                        if (!a.Aynı_Doğrulama_Kodu)
                        {
                            içeriği_farklı_dosya_Sayısı++;
                            break;
                        }
                    }
                    if (içeriği_farklı_dosya_Sayısı > 0) yedekle = true;
                }
            }
            if (yedekle)
            {
                string k = pak + "Banka";
                string h = pak + "Yedekler\\" + D_TarihSaat.Yazıya(DateTime.Now, D_TarihSaat.Şablon_DosyaAdı) + ".zip";

                SıkıştırılmışDosya.Klasörden(k, h);
            }
            #endregion

            YeniYazılımKontrolü.Durdur();
            ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın = false;
        }
        private void AnaEkran_Resize(object sender, EventArgs e)
        {
            Genel.Panel2 = Panel2Durumu.Boş_Aralık;

            if (WindowState == FormWindowState.Minimized)
            {
                if (Genel.KaydedilmemişBilgiVar)
                {
                    DialogResult Dr = MessageBox.Show("Kaydetmeden devam etmek istediğinize emin misiniz? %1", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Dr == DialogResult.No)
                    {
                        Göster();
                        return;
                    }
                }

                Hide();
            }
            else Tag = WindowState;
        }
        private void AnaEkran_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case (Keys.F)://arama
                        toolStripSil_Click(null, null);
                        break;

                    case (Keys.S)://kaydet
                        toolStripEkle_Click(null, null);
                        break;
                }
            }
            else if (e.KeyCode == Keys.Escape) Menu_Ağaç.Close();
        }
        void Göster()
        {
            TopMost = true;
            Show();
            WindowState = (FormWindowState)Tag;
            BringToFront();

            Application.DoEvents();
            Thread.Sleep(1);

            TopMost = false;

            if (!Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(new Rectangle(this.Left, this.Top, this.Width, this.Height))))
            {
                Left = 0; Top = 0; Width = 400; Height = 300;
            }
        }
        private void Menu_İkon_Çıkış_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) Göster();
        }

        private bool Sql_Başlat()
        {
            try
            {
                Sql.BağlantıMeşajıÜreteci = new SQLiteConnectionStringBuilder();
                Sql.BağlantıMeşajıÜreteci.DataSource = pak + "Banka\\" + Kendi.Adı + ".mup";
                Sql.BağlantıMeşajıÜreteci.JournalMode = SQLiteJournalModeEnum.Wal;
                Sql.Bağlantı = new SQLiteConnection(Sql.BağlantıMeşajıÜreteci.ConnectionString);
                Sql.Bağlantı.Open();

                string dogrulama1 = (string)Sql_Ayarlar_Oku("DOGRULAMA", 0, "");
                string dogrulama2 = (string)Sql_Ayarlar_Oku("dogrulama", 0, "");

                if (dogrulama1 != "DOGrulama_" || dogrulama2 != "dogRULAMA_")
                {
                    if (!Sql_Sorgula("CREATE TABLE IF NOT EXISTS Ayarlar (Parametre TEXT PRIMARY KEY, Ayar TEXT)")) return false;
                    if (!Sql_Sorgula("CREATE TABLE IF NOT EXISTS Gorev  (No INTEGER PRIMARY KEY, Sahip INTEGER)")) return false;
                    if (!Sql_Sorgula("CREATE TABLE IF NOT EXISTS gecici (Zaman DATETIME, Durum INTEGER, Aciklama TEXT, Tanim TEXT)")) return false;

                    if (Sql_Sorgula("CREATE TABLE _t0 (Zaman DATETIME, Durum INTEGER, Aciklama TEXT)"))
                    {
                        Sql_Sorgula("insert into _t0 values (null, " + (int)EtkinlikÖzellikleri.OluşturulmaTarihi + ", '" + DateTime.Now.ToString() + "')");
                        Sql_Sorgula("insert into _t0 values (null, " + (int)EtkinlikÖzellikleri.Tanımı + ", '" + Kendi.Adı + "')");
                        Sql_Sorgula("insert into _t0 values (null, " + (int)EtkinlikÖzellikleri.Açıklaması + ", '" + Kendi.Adı + " oluşturuldu')");
                    }

                    Sql_Sorgula("insert into Gorev values (0, 0)");
                    Sql_Sorgula("insert into Ayarlar values ('ikiz', '')");

                    Sql_Sorgula("insert into Ayarlar values ('DOGRULAMA', 'DOGrulama_')");
                    Sql_Sorgula("insert into Ayarlar values ('dogrulama', 'dogRULAMA_')");
                    Sql_Ayarlar_Yaz("DOGRULAMA", "DOGrulama_");
                    Sql_Ayarlar_Yaz("dogrulama", "dogRULAMA_");
                }

                Sql_Zamanlayıcı.Start();

                return true;
            }
            catch (Exception ex) { MessageBox.Show(pak + "Banka\\" + Application.ProductName + ".mup dosyası hasarlı olabilir. Silinmesi gerekmektedir. (sqlite:" + ex.Message + ")"); Environment.Exit(1); }
            return false;

            Object Sql_Ayarlar_Oku(string Parametre, int Yazı_0_Rakam_1, object hatadurumunda = null)
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
                    if (SQLiteDataReader_.Read())
                    {
                        if (Yazı_0_Rakam_1 == 1) Çıktı = Convert.ToInt32(SQLiteDataReader_["Ayar"]);
                        else Çıktı = SQLiteDataReader_["Ayar"];
                    }
                    SQLiteDataReader_.Close();
                }
                catch (Exception) { }

                return Çıktı;
            }
            bool Sql_Ayarlar_Yaz(string Parametre, string Ayar)
            {
                return Sql_Sorgula("update Ayarlar set Ayar = '" + Ayar + "' where Parametre = '" + Parametre + "'");
            }
        }
        private bool Sql_Durdur(bool TamamenKapat = false)
        {
            try
            {
                Sql.Bağlantı.Close();

                if (TamamenKapat)
                {
                    Sql.Bağlantı.Dispose();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                return true;
            }
            catch (Exception) { }
            return false;
        }
        private bool Sql_Sorgula(string Sorgu)
        {
            Sql_Zamanlayıcı_YenidenBaşlat();

            try
            {
                SQLiteCommand SQLiteCommand_ = new SQLiteCommand(Sorgu, Sql.Bağlantı);
                SQLiteCommand_.ExecuteNonQuery();
                return true;
            }
            catch (Exception) { }
            return false;
        }
        private bool Sql_Sorgula(string Sorgu, out SQLiteDataReader SQLiteDataReader_)
        {
            Sql_Zamanlayıcı_YenidenBaşlat();

            SQLiteDataReader_ = null;

            try
            {
                SQLiteCommand SQLiteCommand_ = new SQLiteCommand(Sorgu, Sql.Bağlantı);
                SQLiteDataReader_ = SQLiteCommand_.ExecuteReader();
                return true;
            }
            catch (Exception) { }
            return false;
        }
        private Object Sql_SorÖğren(string Sorgu)
        {
            Sql_Zamanlayıcı_YenidenBaşlat();

            object çıktı = null;
            try
            {
                SQLiteCommand SQLiteCommand_ = new SQLiteCommand(Sorgu, Sql.Bağlantı);
                SQLiteDataReader SQLiteDataReader_ = SQLiteCommand_.ExecuteReader();
                if (SQLiteDataReader_.Read()) çıktı = SQLiteDataReader_[0];
                SQLiteDataReader_.Close();
            }
            catch (Exception) { }

            return çıktı;
        }  
        private List<TreeNode> Sql_ÜyeleriAl(int No)
        {
            List<TreeNode> Liste = new List<TreeNode>();
            SQLiteDataReader SQLiteDataReader_;
            int Nosu; int[] Dizi;
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
                        L_TrNo = Sql_ÜyeleriAl(Nosu);
                        TrNo = new TreeNode(Sql_Görev_TanımVeHatırlatıcıDetayı(Nosu), L_TrNo.ToArray());
                        TrNo.Tag = Nosu;

                        List<Hatırlatıcı_.Durum_> Durum_lar = HatırlatıcıSayfası.Hatırlatıcı.Bul(TakmaAdıKıstası: Nosu.ToString());
                        if (Durum_lar.Count > 0 && !Durum_lar[0].TetiklenmesiBekleniyor) TrNo.ImageIndex = (int)EtkinlikDurumu.Hatırlatıcı;
                        else
                        {
                            if (L_TrNo.Count > 0)
                            {
                                //alt dalların durumunu yansıt

                                ////kendini ekleyerek
                                //kendi durumunu ekleme iptal
                                //Dizi = new int[L_TrNo.Count + 1];
                                //for (int i = 0; i < L_TrNo.Count; i++) Dizi[i] = L_TrNo[i].ImageIndex;
                                //if (KendiDurumu == (int)EtkinlikDurumu.Yeni_Görev) KendiDurumu = Dizi[0];
                                //Dizi[Dizi.Length - 1] = KendiDurumu;
                                //TrNo.ImageIndex = Dizi.Min();

                                //kendini eklemeden
                                Dizi = new int[L_TrNo.Count];
                                for (int i = 0; i < L_TrNo.Count; i++) Dizi[i] = L_TrNo[i].ImageIndex;
                                TrNo.ImageIndex = Dizi.Min();
                            }
                            else
                            {
                                TrNo.ImageIndex = Convert.ToInt32(Sql_GörevÖzelliği(Nosu, EtkinlikÖzellikleri.Durumu));
                            }
                        }

                        Liste.Add(TrNo);
                    }

                    if (Genel.Tik < Environment.TickCount) { Application.DoEvents(); Genel.Tik = Environment.TickCount + 1000; }
                }
                SQLiteDataReader_.Close();
            }
            catch (Exception) { }
            return Liste;
        }
        private int Sql_GeçersizÜyeleriSil()
        {
            SQLiteDataReader SQLiteDataReader_;
            int Adet = 0;

            try
            {
                if (!Sql_Sorgula("SELECT * FROM Gorev ORDER BY No", out SQLiteDataReader_)) return 0;
                while (SQLiteDataReader_.Read())
                {
                    int Nosu = Convert.ToInt32(SQLiteDataReader_["No"]);
                    int Sahibi = Convert.ToInt32(SQLiteDataReader_["Sahip"]);
                    string tanımı_kendi = Sql_GörevÖzelliği(Nosu, EtkinlikÖzellikleri.Tanımı);
                    string tanımı_sahip = Sql_GörevÖzelliği(Sahibi, EtkinlikÖzellikleri.Tanımı);
                    Console.WriteLine("SQLite[" + Adet + "], Sahibi : " + Sahibi + " (" + tanımı_sahip + "), kendisi : " + Nosu + " (" + tanımı_kendi + ")");

                    if (Nosu > 0 && Sahibi > 0)
                    {
                        if (!string.IsNullOrEmpty(tanımı_kendi))
                        {
                            //kendisinin _t dosyası var
                            if (string.IsNullOrEmpty(tanımı_sahip))
                            {
                                //sahibinin _t dosyası yok, ilk sahibini 0 yap ve çöp kutusuna taşı
                                if (!Sql_Sorgula("insert into _t" + Nosu + " values (null, " + (int)EtkinlikÖzellikleri.SilinmedenÖncekiSahibi + ", '" + (int)DalTürü.Görevler + "')")) { Console.WriteLine("Görev -> No:" + Nosu + " _t tablosu güncellenemedi, _1_"); continue; }
                                if (!Sql_Sorgula("insert into _t" + Nosu + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + (int)EtkinlikDurumu.Güncellenen_Görev + ", 'Kurtarıldı')")) { Console.WriteLine("Görev -> No:" + Nosu + " _t tablosu güncellenemedi, _2_"); continue; }
                                if (!Sql_Sorgula("update Gorev set Sahip = " + (int)DalTürü.ÇöpKutusu + " where No = " + Nosu)) { Console.WriteLine("Görev -> No:" + Nosu + " _t tablosu güncellenemedi, _3_"); continue; }
                            }
                        }
                        else
                        {
                            //kendisinin _t dosyası yok, kaydı sil
                            if (Sql_Sorgula("delete from Gorev where No = " + Nosu))
                            {
                                Console.WriteLine("Görev -> No:" + Nosu + " _t tablosu bulunamadı, kayıt silindi");
                            }
                            else
                            {
                                Console.WriteLine("Görev -> No:" + Nosu + " _t tablosu bulunamadı, kayıt silinemedi");
                            }

                            HatırlatıcıSayfası.Hatırlatıcı.Sil(Nosu.ToString());
                        }
                    }
                    else if (Sahibi == (int)DalTürü.ÇöpKutusu)
                    {
                        string silinmeden_önceki_sahibi = Sql_GörevÖzelliği(Nosu, EtkinlikÖzellikleri.SilinmedenÖncekiSahibi);
                        string tanımı_sahibi = Sql_GörevÖzelliği(Convert.ToInt32(silinmeden_önceki_sahibi), EtkinlikÖzellikleri.Tanımı);
                        if (string.IsNullOrEmpty(tanımı_sahibi))
                        {
                            //sahibinin _t dosyası yok, ilk sahibini 0 yap ve çöp kutusuna taşı
                            if (!Sql_Sorgula("delete from _t" + Nosu + " where Durum = " + (int)EtkinlikÖzellikleri.SilinmedenÖncekiSahibi)) { Console.WriteLine("Görev -> No:" + Nosu + " _t tablosu güncellenemedi, _4_"); continue; }
                            if (!Sql_Sorgula("insert into _t" + Nosu + " values (null, " + (int)EtkinlikÖzellikleri.SilinmedenÖncekiSahibi + ", '" + (int)DalTürü.Görevler + "')")) { Console.WriteLine("Görev -> No:" + Nosu + " _t tablosu güncellenemedi, _5_"); continue; }
                            if (!Sql_Sorgula("insert into _t" + Nosu + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + (int)EtkinlikDurumu.Güncellenen_Görev + ", 'Kurtarıldı')")) { Console.WriteLine("Görev -> No:" + Nosu + " _t tablosu güncellenemedi, _6_"); continue; }
                            if (!Sql_Sorgula("update Gorev set Sahip = " + (int)DalTürü.ÇöpKutusu + " where No = " + Nosu)) { Console.WriteLine("Görev -> No:" + Nosu + " _t tablosu güncellenemedi, _7_"); continue; }
                        }
                    }

                    Adet++;

                    if (Genel.Tik < Environment.TickCount) { Application.DoEvents(); Genel.Tik = Environment.TickCount + 1000; }
                }
                SQLiteDataReader_.Close();
            }
            catch (Exception) { }
            return Adet;
        }
        private string Sql_GörevÖzelliği(int Görev, EtkinlikÖzellikleri Özellik)
        {
            if (Özellik == EtkinlikÖzellikleri.Durumu)
            {
                Object Alınan = Sql_SorÖğren("select Durum from _t" + Görev.ToString() + " where Durum < " + (int)EtkinlikDurumu.Güncellenen_Görev + " and Zaman is not null order by Zaman desc limit 1");
                if (Alınan != null) return Convert.ToInt32(Alınan).ToString();
                else return ((int)EtkinlikDurumu.Yeni_Görev).ToString();
            }

            string gecici;
            if (Özellik == EtkinlikÖzellikleri.Sahibi)
            {
                gecici = Sql_SorÖğren("select Sahip from Gorev where No = " + Görev).ToString();
            }
            else
            {
                gecici = (string)Sql_SorÖğren("select Aciklama from _t" + Görev + " where Durum = " + (int)Özellik + " and Zaman is null");
            }

            if (gecici == null) return "";
            else return gecici;
        }
        private string Sql_Görev_TanımVeHatırlatıcıDetayı(int Görev)
        {
            string Tanımı = Sql_GörevÖzelliği(Görev, EtkinlikÖzellikleri.Tanımı);

            List<Hatırlatıcı_.Durum_> D_ler = HatırlatıcıSayfası.Hatırlatıcı.Bul(TakmaAdıKıstası: Görev.ToString());
            if (D_ler.Count > 0) Tanımı += " (H:" + D_TarihSaat.Yazıya(D_ler[0].TetikleneceğiAn, "dd.MM.yyyy HH:mm") + (string.IsNullOrEmpty(D_ler[0].TekrarlayıcıKomutCümlesi) ? null : " " + D_ler[0].TekrarlayıcıKomutCümlesi) + ")";

            return Tanımı;
        }
        private void Sql_Zamanlayıcı_Tick(object sender, EventArgs e)
        {
            Sql_Zamanlayıcı.Stop();
            Sql.Bağlantı.Close();
        }
        private void Sql_Zamanlayıcı_YenidenBaşlat()
        {
            Sql_Zamanlayıcı.Stop();
            Sql_Zamanlayıcı.Start();

            if (Sql.Bağlantı.State != ConnectionState.Open)
            {
                Sql_Durdur();
                Sql.Bağlantı.Open();
            }
        }
        List<TreeNode> Ağaç_FilrelemeyiÇalıştır(List<TreeNode> Dallar)
        {
            List<TreeNode> yeni_liste = new List<TreeNode>();

            foreach (var biri in Dallar)
            {
                if (biri.ImageIndex == (int)EtkinlikDurumu.Hatırlatıcı) Genel.AğacMenüHatırlatıcıSayacı.Add((int)biri.Tag);

                if (Genel.AğacMenüFiltreleme[biri.ImageIndex] == 1)
                {
                    TreeNode kopyası = (TreeNode)biri.Clone();
                    yeni_liste.Add(kopyası);

                    if (kopyası.Nodes.Count > 0)
                    {
                        List<TreeNode> alttakiler = Ağaç_FilrelemeyiÇalıştır(kopyası.Nodes.Cast<TreeNode>().ToList());
                        kopyası.Nodes.Clear();
                        if (alttakiler.Count > 0)
                        {
                            kopyası.Nodes.AddRange(alttakiler.ToArray());

                            if (Genel.AğaçDallarıDurumu_Oku((int)kopyası.Tag)) kopyası.Expand();
                        }
                    }
                }
                else Genel.AğacMenüFiltrelemeSayacGizli++;
            }

            return yeni_liste;
        }
        private void Ağaç_Güncelle(bool Görevler, bool Şablonlar, bool ÇöpKutusu, bool SadeceFiltreleme = false)
        {
            if (Genel.AğaçGüncelleniyor) return;

            bool Şablonlar_Açık_Kalsın = false;
            if (Ağaç.Nodes.Count >= 2)
            {
                if (Ağaç.Nodes[1].IsExpanded) Şablonlar_Açık_Kalsın = true;
            }

            int SeçiliOlanDalınNumarası = Ağaç.SelectedNode == null || Ağaç.SelectedNode.Tag == null ? 0 : (int)Ağaç.SelectedNode.Tag;

            Genel.AğaçGüncelleniyor = true;

            Ağaç.BeginUpdate();

            TreeNode[] tümü = new TreeNode[Ağaç.Nodes.Count];
            Ağaç.Nodes.CopyTo(tümü, 0);

            Ağaç.Nodes.Clear();

            if (Görevler || SadeceFiltreleme)
            {
                if (!SadeceFiltreleme) Genel.Ağaç_TamListe = Sql_ÜyeleriAl((int)DalTürü.Görevler);

                Genel.AğacMenüFiltrelemeSayacGizli = 0;
                Genel.AğacMenüHatırlatıcıSayacı.Clear();
                TreeNode gecici = new TreeNode(KullanıcıAdı.Text, Ağaç_FilrelemeyiÇalıştır(Genel.Ağaç_TamListe).ToArray())
                {
                    Tag = 0,
                    ImageIndex = 11
                };

                Ağaç.Nodes.Add(gecici);

                toolStripEtiket.Text = "";
                if (Genel.AğacMenüFiltrelemeSayacGizli > 0) toolStripEtiket.Text += Genel.AğacMenüFiltrelemeSayacGizli.ToString() + " gizli ";
                if (Genel.AğacMenüHatırlatıcıSayacı.Count > 0)
                {
                    List<int> yeni_sayac = new List<int>();
                    foreach (var b in Genel.AğacMenüHatırlatıcıSayacı)
                    {
                        if (HatırlatıcıSayfası.Hatırlatıcı.Bul(TakmaAdıKıstası: b.ToString()).Count > 0) yeni_sayac.Add(b);
                    }
                    Genel.AğacMenüHatırlatıcıSayacı = yeni_sayac.Distinct().ToList();
                    toolStripEtiket.Text += Genel.AğacMenüHatırlatıcıSayacı.Count.ToString() + " hatırlatıcı ";
                    notifyIcon1.Icon = Properties.Resources.Etkinlik_Takip_Hatırlatıcı;
                }
                else
                {
                    notifyIcon1.Icon = Properties.Resources.Etkinlik_Takip;
                }

                gecici.Expand();
            }
            else Ağaç.Nodes.Add(tümü[0]);

            if (Şablonlar)
            {
                TreeNode gecici = new TreeNode("Şablonlar", Sql_ÜyeleriAl((int)DalTürü.Şablonlar).ToArray());

                gecici.Tag = DalTürü.Şablonlar;
                gecici.ImageIndex = 12;
                if (Şablonlar_Açık_Kalsın) gecici.Expand();

                Ağaç.Nodes.Add(gecici);
            }
            else Ağaç.Nodes.Add(tümü[1]);

            if (ÇöpKutusu)
            {
                TreeNode gecici = new TreeNode("Çöp Kutusu", Sql_ÜyeleriAl((int)DalTürü.ÇöpKutusu).ToArray());
                if (gecici.Nodes.Count > 0)
                {
                    gecici.Tag = DalTürü.ÇöpKutusu;
                    gecici.ImageIndex = 13;

                    Ağaç.Nodes.Add(gecici);
                }
            }
            else if (tümü.Length > 2 && tümü[2].Nodes.Count > 0) Ağaç.Nodes.Add(tümü[2]);

            Ağaç.Sort();
            Ağaç.EndUpdate();

            Genel.AğaçGüncelleniyor = false;

            //secili dalın tekrardan seçili duruma getirilmesi
            if (SeçiliOlanDalınNumarası > 0)
            {                         
                _Bul_Kontrol_Et_Ve_Seç_(Ağaç.Nodes[0]);

                bool _Bul_Kontrol_Et_Ve_Seç_(TreeNode _trn_)
                {
                    if ((int)_trn_.Tag == SeçiliOlanDalınNumarası)
                    {
                        Ağaç.SelectedNode = _trn_;
                        return true;
                    }

                    foreach (TreeNode _dal_ in _trn_.Nodes)
                    {
                        if (_Bul_Kontrol_Et_Ve_Seç_(_dal_)) return true;
                    }

                    return false;
                }
            }

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

                if (x == y)
                {
                    if (!int.TryParse(((TreeNode)x_).Text.Split('-')[0], out x)) return 0;
                    if (!int.TryParse(((TreeNode)y_).Text.Split('-')[0], out y)) return 0;

                    if (x == y) return 0;

                    if (x < y) return -1;
                    else return 1;
                }

                //if (x == (int)EtkinlikDurumu.Üzerinde_Çalışılıyor) return -1;
                //if (y == (int)EtkinlikDurumu.Üzerinde_Çalışılıyor) return 1;

                if (x < y) return -1;
                else return 1;
            }
        }
        #endregion

        private void label_Görev_Tarih_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Ağaç.SelectedNode = null;
            label_Görev_Tarih.Text = KullanıcıAdı.Text;
            label_Görev_Tarih.Enabled = false;
        }
        private void Filtreleme_DurumDeğişikliği(object sender, EventArgs e)
        {
            if (Genel.AğaçGüncelleniyor) return;
            Genel.AğacMenüFiltreleme[(int)EtkinlikDurumu.Hatırlatıcı] = 1;
            Genel.AğacMenüFiltreleme[(int)EtkinlikDurumu.Üzerinde_Çalışılıyor] = (int)Filtreleme_D1.CheckState;
            Genel.AğacMenüFiltreleme[(int)EtkinlikDurumu.Düşük_Öncelikli] = (int)Filtreleme_D2.CheckState;
            Genel.AğacMenüFiltreleme[(int)EtkinlikDurumu.Yeni_Görev] = (int)Filtreleme_D3.CheckState;
            Genel.AğacMenüFiltreleme[(int)EtkinlikDurumu.Beklemede] = (int)Filtreleme_D4.CheckState;
            Genel.AğacMenüFiltreleme[(int)EtkinlikDurumu.Bitti_Geri_Bildirim_Bekleniyor] = (int)Filtreleme_D5.CheckState;
            Genel.AğacMenüFiltreleme[(int)EtkinlikDurumu.Diğer] = (int)Filtreleme_D6.CheckState;
            Genel.AğacMenüFiltreleme[(int)EtkinlikDurumu.Tamamlandı] = (int)Filtreleme_D7.CheckState;
            Genel.AğacMenüFiltreleme[(int)EtkinlikDurumu.İptal_Edildi] = (int)Filtreleme_D8.CheckState;
            Ağaç_Güncelle(false, false, false, true);
        }
        private void FiltrelemeTumu_CheckedChanged(object sender, EventArgs e)
        {
            Genel.AğaçGüncelleniyor = true;
            Filtreleme_D1.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D2.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D3.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D4.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D5.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D6.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D7.CheckState = FiltrelemeTumu.CheckState;
            Filtreleme_D8.CheckState = FiltrelemeTumu.CheckState;
            Genel.AğaçGüncelleniyor = false;
        }
        private void textBox_Arama_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Arama.Text.Length < 2) return;
            if (Genel.GözGezdirmeÇalışıyor) { Genel.GözGezdirmeKapatmaTalebi = true; return; }
            Genel.GözGezdirmeÇalışıyor = true;
            Genel.GözGezdirmeKapatmaTalebi = false;
            Genel.Tik = Environment.TickCount + 100;

            Arama.RowCount = 0;
            TreeNodeCollection nodes = Ağaç.Nodes;
            foreach (TreeNode n in nodes) textBox_Arama_TextChanged_2(n);
            Genel.GözGezdirmeÇalışıyor = false;
        }
        private void textBox_Arama_TextChanged_2(TreeNode treeNode)
        {
            if (Genel.GözGezdirmeKapatmaTalebi) return;
            if (Genel.Tik < Environment.TickCount) { Application.DoEvents(); Genel.Tik = Environment.TickCount + 100; }

            SQLiteDataReader Dr; bool içeriktebulundu = false;
            string Sorgu = "select Aciklama from _t" + ((int)treeNode.Tag).ToString() + " where Zaman is not null";
            if (Sql_Sorgula(Sorgu, out Dr))
            {
                while (Dr.Read())
                {
                    if (Genel.Tik < Environment.TickCount) { Application.DoEvents(); Genel.Tik = Environment.TickCount + 100; }
                    if (((string)Dr["Aciklama"]).ToLower().Contains(textBox_Arama.Text.ToLower())) { içeriktebulundu = true; break; }
                }
                Dr.Close();
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

                Ağaç.SelectedNode = null;
                Ağaç.SelectedNode = (TreeNode)Arama.SelectedCells[0].Tag;
            }
        }
        private void Arama_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Genel.AğaçGüncelleniyor = true;
            Ağaç.SelectedNode = (TreeNode)(Arama[0, e.RowIndex].Tag);
            Genel.AğaçGüncelleniyor = false;

            Arama.Rows[e.RowIndex].Selected = true;
        }
        private void Arama_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Ağaç.SelectedNode = null;
            Ağaç.SelectedNode = (TreeNode)Arama[0, e.RowIndex].Tag;
        }

        private void Panel_Aç(Panel2Durumu Panel2)
        {
            if (Genel.Panel2 == Panel2) return;
            for (int i = 0; i < splitContainer1.Panel2.Controls.Count; i++) splitContainer1.Panel2.Controls[i].Visible = false;

            switch (Panel2)
            {
                case Panel2Durumu.Görev:
                    splitContainer1.Panel2.Controls["panel_Görev"].Visible = true;
                    break;

                case Panel2Durumu.Arama:
                    splitContainer1.Panel2.Controls["panel_Arama"].Visible = true;
                    break;

                case Panel2Durumu.Ayarlar:
                    splitContainer1.Panel2.Controls["panel_Ayarlar"].Visible = true;
                    break;

                case Panel2Durumu.Hatırlatıcı:
                    splitContainer1.Panel2.Controls["panel_Hatırlatıcı"].Visible = true;
                    break;
            }

            Genel.Panel2 = Panel2;
        }
        private void SayfaDüzeni_Normal()
        {
            statusStrip1.SuspendLayout();

            toolStripArama.Image = Properties.Resources.Arama;

            toolStripEkle.Visible = false;
            toolStripAyarlar.Visible = true;

            statusStrip1.ResumeLayout();
        }
        private void SayfaDüzeni_OnayRet()
        {
            statusStrip1.SuspendLayout();

            toolStripArama.Image = Properties.Resources.Ret;

            toolStripEkle.Visible = false;
            toolStripAyarlar.Visible = false;

            statusStrip1.ResumeLayout();
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
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.G_Durum_8);
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.Ayarlar);     //9     //G_Durum_8
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.Secili);      //10     //ağaçtan tıklanan
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.Etkinlik);    //11    //Kullanıcı kendi resmi
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.Sablon);      //12    //Şablonlar için görsel
            ımageList1.Images.Add(Etkinlik_Takip.Properties.Resources.Ret);         //13    //Çöp Kutusu
        }
        private void Uyarı(IWin32Window Pencere, string Mesaj, MessageBoxIcon ikon = MessageBoxIcon.Information, int Bekleme = 5000, string Başlık = "")
        {
            if (Başlık == "") Başlık = this.Text;
            MessageBox.Show(Pencere, Mesaj, Başlık, MessageBoxButtons.OK, ikon);
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

            if (Genel.Panel2 == Panel2Durumu.Görev && !Genel.GözGezdirmeKapatmaTalebi) SayfaDüzeni_OnayRet();

            Genel.KaydedilmemişBilgiVar = true;
            toolStripEkle.Visible = true;
        }
        private int YeniGörevOluştur(int Sahip, DateTime Tarih, string Tanım, string Açıklama = "", EtkinlikDurumu Durum = EtkinlikDurumu.Yeni_Görev)
        {
            if (Tanım == "") return 0;
            int adt = Convert.ToInt32(Sql_SorÖğren("select max(No) from Gorev"));
            string Kendi = (adt + 1).ToString();

            if (!Sql_Sorgula("CREATE TABLE _t" + Kendi + " (Zaman DATETIME, Durum INTEGER, Aciklama TEXT)"))
            {
                Sql_Sorgula("DROP TABLE _t" + Kendi);
                if (!Sql_Sorgula("CREATE TABLE _t" + Kendi + " (Zaman DATETIME, Durum INTEGER, Aciklama TEXT)")) return 0;
            }
            if (!Sql_Sorgula("insert into _t" + Kendi + " values (null, " + (int)EtkinlikÖzellikleri.OluşturulmaTarihi + ", '" + Tarih.ToString() + "')")) return 0;
            if (!Sql_Sorgula("insert into _t" + Kendi + " values (null, " + (int)EtkinlikÖzellikleri.Tanımı + ", '" + Tanım + "')")) return 0;
            if (!Sql_Sorgula("insert into _t" + Kendi + " values (null, " + (int)EtkinlikÖzellikleri.Açıklaması + ", '" + Açıklama + "')")) return 0;
            if (!Sql_Sorgula("insert into Gorev values (" + Kendi + ", " + Sahip.ToString() + ")")) return 0;

            if (Durum != EtkinlikDurumu.Yeni_Görev)
            {
                if (!Sql_Sorgula("insert into _t" + Kendi + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + (int)Durum + ", '" + Açıklama + "')")) return 0;
            }

            Genel.AğaçDallarıDurumu_Yaz(Sahip, true);
            return Convert.ToInt32(Kendi);
        }

        private void toolStripEkle_Click(object sender, EventArgs e)
        {
            if (Genel.KaydedilmemişBilgiVar)
            {
                int Sahip = 0;
                if (Genel.SonSeçilenDal != null) Sahip = (int)Genel.SonSeçilenDal.Tag;

                if (Genel.Panel2 == Panel2Durumu.Görev)
                {
                    //Görev, listeleme ekranında görev güncellendi, etkinlik olarak kaydet
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
                    if (!Sql_Sorgula("update _t" + Sahip.ToString() + " set Aciklama = '" + textBox_Görev_Tanım.Text + "' where Durum = " + (int)EtkinlikÖzellikleri.Tanımı)) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                    if (!Sql_Sorgula("update _t" + Sahip.ToString() + " set Aciklama = '" + textBox_Görev_Açıklama.Text + "' where Durum = " + (int)EtkinlikÖzellikleri.Açıklaması)) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }

                }
                else if (Genel.Panel2 == Panel2Durumu.Hatırlatıcı)
                {
                    DateTime d = DateTime.Now;
                    if (Hatırlatıcı_Hatırlat_Bugün_a.Checked)
                    {
                        d = new DateTime(d.Year, d.Month, d.Day, (int)Hatırlatıcı_Hatırlat_Bugün_aa.Value, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Bugün_b.Checked)
                    {
                        d = new DateTime(d.Year, d.Month, d.Day, (int)Hatırlatıcı_Hatırlat_Bugün_bb.Value, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Bugün_c.Checked)
                    {
                        d = new DateTime(d.Year, d.Month, d.Day, (int)Hatırlatıcı_Hatırlat_Bugün_cc.Value, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Bugün_d.Checked)
                    {
                        d = new DateTime(d.Year, d.Month, d.Day, (int)Hatırlatıcı_Hatırlat_Bugün_dd.Value, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Yarın_a.Checked)
                    {
                        d = d.AddDays(1);
                        d = new DateTime(d.Year, d.Month, d.Day, (int)Hatırlatıcı_Hatırlat_Yarın_aa.Value, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Yarın_b.Checked)
                    {
                        d = d.AddDays(1);
                        d = new DateTime(d.Year, d.Month, d.Day, (int)Hatırlatıcı_Hatırlat_Yarın_bb.Value, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Yarın_c.Checked)
                    {
                        d = d.AddDays(1);
                        d = new DateTime(d.Year, d.Month, d.Day, (int)Hatırlatıcı_Hatırlat_Yarın_cc.Value, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Yarın_d.Checked)
                    {
                        d = d.AddDays(1);
                        d = new DateTime(d.Year, d.Month, d.Day, (int)Hatırlatıcı_Hatırlat_Yarın_dd.Value, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Haftaya_Pzt.Checked)
                    {
                        d = d.AddDays((int)DayOfWeek.Monday - (int)d.DayOfWeek + 7);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Haftaya_Sa.Checked)
                    {
                        d = d.AddDays((int)DayOfWeek.Tuesday - (int)d.DayOfWeek + 7);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Haftaya_Çrş.Checked)
                    {
                        d = d.AddDays((int)DayOfWeek.Wednesday - (int)d.DayOfWeek + 7);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Haftaya_Per.Checked)
                    {
                        d = d.AddDays((int)DayOfWeek.Thursday - (int)d.DayOfWeek + 7);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Haftaya_Cu.Checked)
                    {
                        d = d.AddDays((int)DayOfWeek.Friday - (int)d.DayOfWeek + 7);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Haftaya_Cts.Checked)
                    {
                        d = d.AddDays((int)DayOfWeek.Saturday - (int)d.DayOfWeek + 7);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Haftaya_Pa.Checked)
                    {
                        d = d.AddDays(((int)DayOfWeek.Sunday - (int)d.DayOfWeek + 7) + 7);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_DahaSonra_2hafta.Checked)
                    {
                        d = d.AddDays(14);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_DahaSonra_3hafta.Checked)
                    {
                        d = d.AddDays(21);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_DahaSonra_1ay.Checked)
                    {
                        d = d.AddMonths(1);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_DahaSonra_2ay.Checked)
                    {
                        d = d.AddMonths(2);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_DahaSonra_3ay.Checked)
                    {
                        d = d.AddMonths(3);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_DahaSonra_6ay.Checked)
                    {
                        d = d.AddMonths(6);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_DahaSonra_1yıl.Checked)
                    {
                        d = d.AddYears(1);
                        d = new DateTime(d.Year, d.Month, d.Day, 9, 0, 0);
                    }
                    else if (Hatırlatıcı_Hatırlat_Diğer_Onay.Checked)
                    {
                        if (Hatırlatıcı_Hatırlat_Diğer.Tag == null)
                        {
                            MessageBox.Show("Hiçbir kıstas seçilmedi");
                            Hatırlatıcı_Hatırlat_Diğer_Yazı.Focus();
                            return;
                        }

                        d = (DateTime)Hatırlatıcı_Hatırlat_Diğer.Tag;
                    }

                    string tekrarlama = "";
                    if (Hatırlatıcı_Tekrarla_Onay.Checked)
                    {
                        switch (Hatırlatıcı_Tekrarla_dönem.Text)
                        {
                            case "Günde":
                                tekrarlama = "g+" + Hatırlatıcı_Tekrarla_adet.Value;
                                break;
                            case "Haftada":
                                tekrarlama = "h+" + Hatırlatıcı_Tekrarla_adet.Value;
                                break;
                            case "Ayda":
                                tekrarlama = "a+" + Hatırlatıcı_Tekrarla_adet.Value;
                                break;
                            case "Yılda":
                                tekrarlama = "y+" + Hatırlatıcı_Tekrarla_adet.Value;
                                break;
                            default: return;
                        }
                    }

                    HatırlatıcıSayfası.Hatırlatıcı.Sil(Sahip.ToString());
                    HatırlatıcıSayfası.Hatırlatıcı.Ekle(Sahip.ToString(), d, tekrarlama, Hatırlatıcı_GeriBildirim_İşlemi);
                }

                Ağaç_Güncelle(true, false, false);
                Panel_Aç(Panel2Durumu.Arama);
                SayfaDüzeni_Normal();
                Genel.KaydedilmemişBilgiVar = false;
            }
        }
        private void toolStripAyarlar_Click(object sender, EventArgs e)
        {
            Panel_Aç(Panel2Durumu.Ayarlar);
        }
        private void toolStripSil_Click(object sender, EventArgs e)
        {
            if (Genel.GözGezdirmeÇalışıyor)
            {
                Genel.GözGezdirmeKapatmaTalebi = true;
                return;
            }
            else if (Genel.KaydedilmemişBilgiVar)
            {
                DialogResult Dr = MessageBox.Show("Kaydetmeden devam etmek istediğinize emin misiniz? %2", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;
                Genel.KaydedilmemişBilgiVar = false;

                Ağaç_AfterSelect(null, null);
            }

            SayfaDüzeni_Normal();
            Panel_Aç(Panel2Durumu.Arama);
            textBox_Arama.SelectAll();
            textBox_Arama.Focus();
        }
        private void toolStripTamamlananlarıGösterGizle_Click(object sender, EventArgs e)
        {
            Filtreleme_D7.Checked = !Filtreleme_D7.Checked;
        }

        private void Ağaç_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Ağaç.BeginUpdate();
            if (OdaklanmışGörünüm_Kızart.Checked)
            {
                foreach (TreeNode t in Ağaç.Nodes) Sıfırla(t);
                e.Node.ForeColor = Color.Red;
                foreach (TreeNode n in e.Node.Nodes) n.ForeColor = e.Node.ForeColor;

                void Sıfırla(TreeNode Dal)
                {
                    Dal.ForeColor = SystemColors.WindowText;

                    foreach (TreeNode d in Dal.Nodes)
                    {
                        Sıfırla(d);
                    }
                }
            }
            if (OdaklanmışGörünüm_DiğerleriniDaralt.Checked)
            {
                Ağaç.CollapseAll();
            }
            if (OdaklanmışGörünüm_Genişlet.Checked)
            {
                TreeNode tn = e.Node;
                while (tn != null)
                {
                    tn.Expand();
                    tn = tn.Parent;
                }
            }
            Ağaç.EndUpdate();

            Ağaç.SelectedNode = e.Node;
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

            if (Hedef == null) return;

            if (Kaynak == null)
            {
                //panodaki yazıyı görev olarak kaydet
                MenuItem_Ağaç_PanodanAl_2((string)e.Data.GetData(typeof(string)), (int)Hedef.Tag);
            }
            else
            {
                if (Kaynak.Equals(Hedef)) return;

                TreeNode pare = Kaynak;
                DalTürü parentt_k = DalTürü.Geçersiz;
                while (pare != null)
                {
                    if (pare.Parent == null) parentt_k = (DalTürü)pare.Tag;
                    pare = pare.Parent;
                }
                if (parentt_k < DalTürü.Görevler)
                {
                    if (parentt_k == DalTürü.ÇöpKutusu) return;
                }

                pare = Hedef;
                DalTürü parentt_h = DalTürü.Geçersiz;
                while (pare != null)
                {
                    if (pare.Parent == null) parentt_h = (DalTürü)pare.Tag;
                    else if (pare.Tag == Kaynak.Tag) return;
                    pare = pare.Parent;
                }
                if (parentt_h < DalTürü.Görevler)
                {
                    if (parentt_h == DalTürü.ÇöpKutusu) return;
                }

                if ((Kaynak.Parent == Hedef) || ((DalTürü)Kaynak.Tag == DalTürü.Şablonlar)) return;

                if (parentt_k == DalTürü.Şablonlar || parentt_h == DalTürü.Şablonlar)
                {
                    Ağaç_Kopyala((int)Kaynak.Tag, (int)Hedef.Tag);
                }
                else
                {
                    string Kaynak_Adı = "Kök";
                    if (Kaynak.Parent != null) Kaynak_Adı = Sql_GörevÖzelliği((int)Kaynak.Parent.Tag, EtkinlikÖzellikleri.Tanımı);

                    if (!Sql_Sorgula("insert into _t" + ((int)Kaynak.Tag).ToString() + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + ((int)EtkinlikDurumu.Güncellenen_Görev).ToString() + ", 'Ağaç dal değişikliği, o anda : " + Environment.NewLine + Kaynak_Adı + "')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                    if (!Sql_Sorgula("update Gorev set Sahip = " + Convert.ToString((int)Hedef.Tag) + " where No = " + ((int)Kaynak.Tag).ToString())) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                    Genel.AğaçDallarıDurumu_Yaz((int)Hedef.Tag, true);
                }
            }

            Ağaç_Güncelle(true, true, true);
        }
        void Ağaç_Kopyala(int Kaynak, int Hedef)
        {
            Hedef = YeniGörevOluştur(Hedef, DateTime.Now, Sql_GörevÖzelliği(Kaynak, EtkinlikÖzellikleri.Tanımı), Sql_GörevÖzelliği(Kaynak, EtkinlikÖzellikleri.Açıklaması));

            var dizi = Sql_ÜyeleriAl(Kaynak);

            toolStripİlerleme.Value = 0;
            toolStripİlerleme.Maximum = dizi.Count;
            toolStripİlerleme.Visible = true;

            foreach (var biri in dizi)
            {
                Ağaç_Kopyala_2(biri, Hedef);

                toolStripİlerleme.Value++;
                if (Genel.Tik < Environment.TickCount) { Application.DoEvents(); Genel.Tik = Environment.TickCount + 1000; }
            }

            toolStripİlerleme.Visible = false;
        }
        void Ağaç_Kopyala_2(TreeNode Kaynak, int Hedef)
        {
            Hedef = YeniGörevOluştur(Hedef, DateTime.Now, Sql_GörevÖzelliği((int)Kaynak.Tag, EtkinlikÖzellikleri.Tanımı), Sql_GörevÖzelliği((int)Kaynak.Tag, EtkinlikÖzellikleri.Açıklaması));
            toolStripİlerleme.Maximum += Kaynak.Nodes.Count;

            foreach (TreeNode biri in Kaynak.Nodes)
            {
                toolStripİlerleme.Value++;
                if (Genel.Tik < Environment.TickCount) { Application.DoEvents(); Genel.Tik = Environment.TickCount + 1000; }

                Ağaç_Kopyala_2(biri, Hedef);
            }
        }
        private void Ağaç_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Göz Gezdirme
            if (Genel.KaydedilmemişBilgiVar)
            {
                if (Ağaç.SelectedNode == Genel.SonSeçilenDal) return;

                DialogResult Dr = MessageBox.Show("Kaydetmeden devam etmek istediğinize emin misiniz? %3", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                Genel.KaydedilmemişBilgiVar = false;
            }

            Genel.SonSeçilenDal = Ağaç.SelectedNode;

            if (!Genel.AğaçGüncelleniyor)
            {
                Sutun_Gorev.Visible = false;
                if ((int)Ağaç.SelectedNode.Tag > 0)
                {
                    //Görev
                    if (Genel.GözGezdirmeÇalışıyor) return;

                    Panel_Aç(Panel2Durumu.Görev);
                    label_Görev_Tarih.Text = "Bekleyiniz";
                    int Tablo = (int)Ağaç.SelectedNode.Tag;
                    textBox_Görev_Açıklama.Text = Sql_GörevÖzelliği(Tablo, EtkinlikÖzellikleri.Açıklaması);
                    textBox_Görev_Tanım.Text = Sql_GörevÖzelliği(Tablo, EtkinlikÖzellikleri.Tanımı);
                    label_Görev_Tarih.Text = Sql_GörevÖzelliği(Tablo, EtkinlikÖzellikleri.OluşturulmaTarihi);

                    if (Grid_Listele_Tarihçe.Checked)
                    {
                        if (!Sql_Sorgula("delete from gecici")) { label_Görev_Tarih.Text = "Beklenmeyen durum oluştu. Tekrar deneyiniz"; return; }
                        if (!Sql_Sorgula("insert into gecici (Zaman, Durum, Aciklama) select * from _t" + Tablo.ToString() + " where Zaman is not null")) { label_Görev_Tarih.Text = "Beklenmeyen durum oluştu. Tekrar deneyiniz"; return; }
                    }
                    MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
                }
            }
        }
        private void Ağaç_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            Genel.AğaçDallarıDurumu_Yaz((int)e.Node.Tag, false);
        }
        private void Ağaç_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Genel.AğaçDallarıDurumu_Yaz((int)e.Node.Tag, true);
        }

        private void Menu_Ağaç_Opening(object sender, CancelEventArgs e)
        {
            if (Genel.AğaçGüncelleniyor) return;
            if (Genel.KaydedilmemişBilgiVar) { e.Cancel = true; return; }

            Genel.AçıldığındakiFarePozisyonu_X = Genel.AnlıkFarePozisyonu_X;
            Genel.AçıldığındakiFarePozisyonu_Y = Genel.AnlıkFarePozisyonu_Y;

            TreeNode pare = Ağaç.SelectedNode;
            DalTürü parentt = DalTürü.Geçersiz;
            while (pare != null)
            {
                if (pare.Parent == null) parentt = (DalTürü)pare.Tag;
                pare = pare.Parent;
            }

            if (parentt < DalTürü.Görevler)
            {
                if (parentt == DalTürü.ÇöpKutusu)
                {
                    if (Ağaç.SelectedNode.Level != 1) { e.Cancel = true; return; }

                    Genel.AğaçGüncelleniyor = true;

                    for (int i = 0; i < Menu_Ağaç.Items.Count; i++) Menu_Ağaç.Items[i].Visible = false;
                    MenuItem_Ağaç_PanoyaKopyala.Visible = true;
                    MenuItem_Ağaç_GeriAl.Visible = true;
                    MenuItem_Ağaç_Sil.Visible = true;
                }
                else if (parentt == DalTürü.Şablonlar)
                {
                    if (Ağaç.SelectedNode.Level == 0) { e.Cancel = true; return; }

                    Genel.AğaçGüncelleniyor = true;

                    for (int i = 0; i < Menu_Ağaç.Items.Count; i++) Menu_Ağaç.Items[i].Visible = false;
                    MenuItem_Ağaç_PanoyaKopyala.Visible = true;
                    MenuItem_Ağaç_PanodanAl.Visible = true;
                    MenuItem_Ağaç_Sil.Visible = true;
                }
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
                MenuItem_Ağaç_PanoyaKopyala.Visible = true;
                MenuItem_Ağaç_PanodanAl.Visible = true;
                MenuItem_Ağaç_Sil.Visible = true;
                toolStripSeparator2.Visible = true;
                toolStripSeparator3.Visible = true;

                Genel.AğaçGüncelleniyor = false;
                if (Ağaç.SelectedNode.Nodes.Count > 0)
                {
                    //görev olduğunu varsay
                    MenuItem_Ağaç_Görev.Checked = true;
                }
                else
                {
                    //etkinlik olduğunu varsay
                    MenuItem_Ağaç_Etkinlik.Checked = true;
                }

                List<Hatırlatıcı_.Durum_> Durum_lar = HatırlatıcıSayfası.Hatırlatıcı.Bul(TakmaAdıKıstası: ((int)Ağaç.SelectedNode.Tag).ToString());
                MenuItem_Ağaç_Hatırlatıcı.Visible = true;
                if (Durum_lar.Count == 0)
                {
                    MenuItem_Ağaç_Hatırlatıcı.Text = "Hatırlatıcı kur";
                    MenuItem_Ağaç_Hatırlatıcı_TekrarKur.Visible = false;
                    MenuItem_Ağaç_Hatırlatıcı_İptalEt.Visible = false;
                }
                else
                {
                    MenuItem_Ağaç_Hatırlatıcı.Text = "Hatırlatıcı";
                    MenuItem_Ağaç_Hatırlatıcı_TekrarKur.Visible = true;
                    MenuItem_Ağaç_Hatırlatıcı_İptalEt.Visible = true;

                    MenuItem_Ağaç_Hatırlatıcı_TekrarKur.Tag = Durum_lar[0];
                }
            }

            Genel.AğaçGüncelleniyor = false;
        }
        private void Menu_Ağaç_Opened(object sender, EventArgs e)
        {
            if (MenuItem_Ağaç_Görev.Checked) MenuItem_Ağaç_Tanım.Focus();
            else MenuItem_Ağaç_Açıklama.Focus();
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

                    Genel.Tik = Environment.TickCount + 1000;
                    while (Dr.Read())
                    {
                        string tablo = Dr["name"].ToString();
                        string durum = Sql_GörevÖzelliği(Convert.ToInt32(tablo.Remove(0, 2)), EtkinlikÖzellikleri.Durumu);

                        if (durum == ((int)EtkinlikDurumu.Üzerinde_Çalışılıyor).ToString())
                        {
                            if (!Sql_Sorgula("insert into " + tablo + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + ((int)EtkinlikDurumu.Düşük_Öncelikli).ToString() + ", '" + st + "')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); goto HatalıÇıkış; }
                        }

                        if (Genel.Tik < Environment.TickCount) { Application.DoEvents(); Genel.Tik = Environment.TickCount + 1000; }
                    }
                    Dr.Close();
                }
            }

            if (Durum == EtkinlikDurumu.Tamamlandı || Durum == EtkinlikDurumu.İptal_Edildi)
            {
		        List<Hatırlatıcı_.Durum_> D_ler = HatırlatıcıSayfası.Hatırlatıcı.Bul(TakmaAdıKıstası: Sahip.ToString());
		        if (D_ler.Count > 0)
		        {
		            DialogResult Dr = MessageBox.Show("Hatırlatıcıyı iptal etmek istiyor musunuz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
		            if (Dr == DialogResult.Yes)
		            {
		                HatırlatıcıSayfası.Hatırlatıcı.Sil(Sahip.ToString());
		            }
		    	}
		    }

            MenuItem_Ağaç_Tanım.Text = "";
            MenuItem_Ağaç_Açıklama.Text = "";

        HatalıÇıkış:
            Menu_Ağaç.Close();
            Ağaç_Güncelle(true, false, false);
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
            DalTürü parentt_k = DalTürü.Geçersiz;
            while (pare != null)
            {
                if (pare.Parent == null) parentt_k = (DalTürü)pare.Tag;
                pare = pare.Parent;
            }

            if (parentt_k == DalTürü.ÇöpKutusu)
            {
                //tamamiyle sil
                DialogResult Dr = MessageBox.Show("Geri alınamıyacak şekilde silmek istediğinize emin misiniz?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                Ağaç_TamamenSil((int)Ağaç.SelectedNode.Tag);
                Ağaç_Güncelle(false, false, true);
            }
            else
            {
                ///çöp kutusuna taşı
                string sahip = Convert.ToString(Sql_SorÖğren("select Sahip from Gorev where No = " + (int)Ağaç.SelectedNode.Tag));
                if (!Sql_Sorgula("insert into _t" + (int)Ağaç.SelectedNode.Tag + " values (null, " + (int)EtkinlikÖzellikleri.SilinmedenÖncekiSahibi + ", '" + sahip + "')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                if (!Sql_Sorgula("insert into _t" + (int)Ağaç.SelectedNode.Tag + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + (int)EtkinlikDurumu.Güncellenen_Görev + ", 'Silindi')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                if (!Sql_Sorgula("update Gorev set Sahip = " + (int)DalTürü.ÇöpKutusu + " where No = " + (int)Ağaç.SelectedNode.Tag)) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
                Ağaç_Güncelle(true, true, true);
            }
        }
        void Ağaç_TamamenSil(int Kaynak)
        {
            var dizi = Sql_ÜyeleriAl(Kaynak);
            foreach (var biri in dizi)
            {
                Ağaç_TamamenSil_2(biri);
            }

            if (!Sql_Sorgula("drop table _t" + Kaynak)) { MessageBox.Show("Beklenmeyen durum oluştu. _t" + Kaynak + " silinemedi"); return; }
            if (!Sql_Sorgula("delete from Gorev where No = " + Kaynak)) { MessageBox.Show("Beklenmeyen durum oluştu. _t" + Kaynak + " Gorev den silinemedi"); return; }

            HatırlatıcıSayfası.Hatırlatıcı.Sil(Kaynak.ToString());
        }
        void Ağaç_TamamenSil_2(TreeNode Kaynak)
        {
            foreach (TreeNode biri in Kaynak.Nodes)
            {
                Ağaç_TamamenSil_2(biri);
            }

            if (!Sql_Sorgula("drop table _t" + (int)Kaynak.Tag)) { MessageBox.Show("Beklenmeyen durum oluştu. _t" + (int)Kaynak.Tag + " silinemedi"); return; }
            if (!Sql_Sorgula("delete from Gorev where No = " + (int)Kaynak.Tag)) { MessageBox.Show("Beklenmeyen durum oluştu. _t" + (int)Kaynak.Tag + " Gorev den silinemedi"); return; }

            HatırlatıcıSayfası.Hatırlatıcı.Sil(((int)Kaynak.Tag).ToString());
        }
        private void MenuItem_Ağaç_GeriAl_Click(object sender, EventArgs e)
        {
            if (Ağaç.SelectedNode == null) { Menu_Ağaç.Close(); Uyarı(Ağaç, "Ağaçtan görev seçiniz.", MessageBoxIcon.Error); return; }

            if (!Sql_Sorgula("insert into _t" + (int)Ağaç.SelectedNode.Tag + " values (DATETIME('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'), " + (int)EtkinlikDurumu.Güncellenen_Görev + ", 'Geri alındı')")) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
            string sahip = Sql_GörevÖzelliği((int)Ağaç.SelectedNode.Tag, EtkinlikÖzellikleri.SilinmedenÖncekiSahibi);
            if (!Sql_Sorgula("delete from _t" + (int)Ağaç.SelectedNode.Tag + " where Durum = " + (int)EtkinlikÖzellikleri.SilinmedenÖncekiSahibi)) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
            if (!Sql_Sorgula("update Gorev set Sahip = " + sahip + " where No = " + ((int)Ağaç.SelectedNode.Tag).ToString())) { MessageBox.Show("Beklenmeyen durum oluştu. Tekrar deneyiniz"); return; }
            Ağaç_Güncelle(true, true, true);
        }
        private void MenuItem_Ağaç_PanodanAl_Click(object sender, EventArgs e)
        {
            if (Ağaç.SelectedNode == null || Ağaç.SelectedNode.Tag == null) return;

            MenuItem_Ağaç_PanodanAl_2(Clipboard.GetText(), (int)Ağaç.SelectedNode.Tag);

            Ağaç_Güncelle(true, true, false);
        }
        private void MenuItem_Ağaç_PanodanAl_2(string Girdi, int Hedef)
        {
            if (string.IsNullOrEmpty(Girdi)) return;
            Girdi = Girdi.Trim(' ', '\r', '\n');

            string[] kkk = Girdi.Split(new char[] { '\r', '\n' });
            if (kkk == null || kkk.Length == 0) return;
            
            List<string> satırlar = new List<string>();
            foreach (string b in kkk)
            {
                if (string.IsNullOrEmpty(b)) continue;
                string c = b.Trim(' ', '\r', '\n');

                if (c.StartsWith(">")) satırlar.Add(c);
                else satırlar.Add(">;Yeni görev;" + c + ";");
            }
            if (satırlar.Count == 0) return;

            int Seviye = 1;
            List<int> Hedefler = new List<int>(); 
            Hedefler.Add(0);
            Hedefler.Add(Hedef); //ikinci kez eklenmesinin sebebi indeksin 1 den başlayacak şekilde olması 

            toolStripİlerleme.Value = 0;
            toolStripİlerleme.Maximum = satırlar.Count;
            toolStripİlerleme.Visible = true;

            bool Hatırlatıcı_EklensinMi = true;
            bool Hatırlatıcı_EklensinMi_SorulduMu = false;
            while (satırlar.Count > 0)
            {
                string[] bölümler = satırlar[0].Replace("|", Environment.NewLine).Split(';');
                satırlar.RemoveAt(0);

                if (bölümler.Length <= 3) continue;
                if (!bölümler[0].StartsWith(">") || !bölümler[0].EndsWith(">")) continue;

                int Durum = (int)EtkinlikDurumu.Geçersiz;
                for (int i = 0; i < EtkinlikDurumu_OkunabilirListe.Length; i++)
                {
                    if (EtkinlikDurumu_OkunabilirListe[i] == bölümler[1]) { Durum = i; break; }
                }
                if (Durum == (int)EtkinlikDurumu.Geçersiz) continue;

                int okunan_seviye = bölümler[0].Length;
            YenidenDene:
                string tanım = bölümler[2];
                int başla = tanım.LastIndexOf(" (H:");
                int bitir = -1;
                if (başla >= 0) bitir = tanım.IndexOf(")", başla);
                string tekrar_cümlesi = "";
                DateTime hatırlatıcı_tarih = default;
                if (bitir > başla)
                {
                    tanım = bölümler[2].Remove(başla);
                    string hatırlatıcı = bölümler[2].Substring(başla, bitir - başla + 1).Trim('(', ')', ' ', 'H', ':');
                    string tarih = hatırlatıcı.Substring(0, 16);
                    tekrar_cümlesi = hatırlatıcı.Remove(0, tarih.Length).Trim();

                    if (DateTime.TryParseExact(tarih, "dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out hatırlatıcı_tarih))
                    {
                        if (!Hatırlatıcı_EklensinMi_SorulduMu)
                        {
                            Hatırlatıcı_EklensinMi_SorulduMu = true;

                            DialogResult Dr = MessageBox.Show("Hatırlatıcıları da eklemek istiyor musunuz?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (Dr == DialogResult.No) Hatırlatıcı_EklensinMi = false;
                        }
                    }
                    else hatırlatıcı_tarih = default;
                }

                if (Seviye == okunan_seviye)
                {
                    int yeni_oluşturulan_görev = YeniGörevOluştur(Hedefler[okunan_seviye], DateTime.Now, tanım, bölümler[3], (EtkinlikDurumu)Durum);
                    if (yeni_oluşturulan_görev == 0) continue;

                    Hedefler.Insert(Seviye + 1, yeni_oluşturulan_görev);
                    while (Hedefler.Count > Seviye + 2) Hedefler.RemoveAt(Hedefler.Count - 1);

                    if (Hatırlatıcı_EklensinMi && hatırlatıcı_tarih != default)
                    {
                        HatırlatıcıSayfası.Hatırlatıcı.Ekle(yeni_oluşturulan_görev.ToString(), hatırlatıcı_tarih, tekrar_cümlesi, Hatırlatıcı_GeriBildirim_İşlemi);
                    }
                }
                else if (Seviye < okunan_seviye)
                {
                    Seviye++;

                    int yeni_oluşturulan_görev = YeniGörevOluştur(Hedefler[okunan_seviye], DateTime.Now, tanım, bölümler[3], (EtkinlikDurumu)Durum);
                    if (yeni_oluşturulan_görev == 0) continue;

                    Hedefler.Insert(Seviye + 1, yeni_oluşturulan_görev);
                    while (Hedefler.Count > Seviye + 2) Hedefler.RemoveAt(Hedefler.Count - 1);

                    if (Hatırlatıcı_EklensinMi && hatırlatıcı_tarih != default)
                    {
                        HatırlatıcıSayfası.Hatırlatıcı.Ekle(yeni_oluşturulan_görev.ToString(), hatırlatıcı_tarih, tekrar_cümlesi, Hatırlatıcı_GeriBildirim_İşlemi);
                    }
                }
                else
                {
                    Seviye -= Seviye - okunan_seviye;
                    while (Hedefler.Count > Seviye + 2) Hedefler.RemoveAt(Hedefler.Count - 1);
                    goto YenidenDene;
                }

                toolStripİlerleme.Value++;
                if (Genel.Tik < Environment.TickCount) { Application.DoEvents(); Genel.Tik = Environment.TickCount + 1000; }
            }

            toolStripİlerleme.Visible = false;
        }
        private void MenuItem_Ağaç_PanoyaKopyala_Click(object sender, EventArgs e)
        {
            string Çıktı = "";
            Ağaç_PanoyaKopyala((int)Ağaç.SelectedNode.Tag, 1, ref Çıktı);

            if (!string.IsNullOrEmpty(Çıktı)) Clipboard.SetText(Çıktı);
        }
        void Ağaç_PanoyaKopyala(int Kaynak, int Seviye, ref string Çıktı)
        {
            string Durum = EtkinlikDurumu_OkunabilirListe[Convert.ToInt32(Sql_GörevÖzelliği(Kaynak, EtkinlikÖzellikleri.Durumu))];
            string Tanım = Sql_Görev_TanımVeHatırlatıcıDetayı(Kaynak);
            string Açıklama = Sql_GörevÖzelliği(Kaynak, EtkinlikÖzellikleri.Açıklaması);

            for (int i = 0; i < Seviye; i++) Çıktı += '>';
            Çıktı += ";" + Durum + ";" + (Tanım + ";" + Açıklama).Replace(Environment.NewLine, "|") + Environment.NewLine;

            var dizi = Sql_ÜyeleriAl(Kaynak);
            foreach (var biri in dizi)
            {
                Ağaç_PanoyaKopyala_2(biri, Seviye + 1, ref Çıktı);
            }
        }
        void Ağaç_PanoyaKopyala_2(TreeNode Kaynak, int Seviye, ref string Çıktı)
        {
            string Durum = EtkinlikDurumu_OkunabilirListe[Convert.ToInt32(Sql_GörevÖzelliği((int)Kaynak.Tag, EtkinlikÖzellikleri.Durumu))];
            string Tanım = Sql_Görev_TanımVeHatırlatıcıDetayı((int)Kaynak.Tag);
            string Açıklama = Sql_GörevÖzelliği((int)Kaynak.Tag, EtkinlikÖzellikleri.Açıklaması);

            for (int i = 0; i < Seviye; i++) Çıktı += '>';
            Çıktı += ";" + Durum + ";" + (Tanım + ";" + Açıklama).Replace(Environment.NewLine, "|") + Environment.NewLine;

            foreach (TreeNode biri in Kaynak.Nodes)
            {
                Ağaç_PanoyaKopyala_2(biri, Seviye + 1, ref Çıktı);
            }
        }
        private void MenuItem_Ağaç_TümEtkinliklerinListele_Click(object sender, EventArgs e)
        {
            try
            {
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

                Genel.Tik = Environment.TickCount + 1000;
                MenuItem_Ağaç_TümEtkinliklerinListele_2(Ağaç.SelectedNode, textBox_Görev_Tanım.Text);
                Sutun_Gorev.Visible = true;
                Grid_Listele_Tarihçe.Checked = true;
                MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
            }
            catch (Exception) { }
        }
        private void MenuItem_Ağaç_TümEtkinliklerinListele_2(TreeNode Dallar, string Tanım)
        {
            DateTime dt;
            DateTime.TryParse(Sql_GörevÖzelliği((int)Dallar.Tag, EtkinlikÖzellikleri.OluşturulmaTarihi), out dt);

            Sql_Sorgula("insert into gecici (Zaman, Durum, Aciklama) select * from _t" + ((int)Dallar.Tag).ToString() + " where Zaman is not null");
            Sql_Sorgula("insert into gecici (Zaman, Durum, Aciklama) values ('" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "', " + ((int)EtkinlikDurumu.Yeni_Görev).ToString() + ", 'Oluşturuldu')");
            Sql_Sorgula("update gecici set Tanim = '" + Tanım + "' where Tanim is null");

            if (Genel.Tik < Environment.TickCount)
            {
                Grid_Etkinlikler.TopLeftHeaderCell.Value = Sql_SorÖğren("select count(Durum) from gecici").ToString();
                Application.DoEvents();
                Genel.Tik = Environment.TickCount + 1000;
            }

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
        private void MenuItem_Ağaç_Tanım_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (MenuItem_Ağaç_Görev.Checked) MenuItem_Ağaç_Çalış(EtkinlikDurumu.Yeni_Görev);
                else MenuItem_Ağaç_Çalış(EtkinlikDurumu.Üzerinde_Çalışılıyor);
            }
        }
        private void MenuItem_Ağaç_Hatırlatıcı_Kur_İptalEt_Click(object sender, EventArgs e)
        {
            if (Ağaç.SelectedNode == null) return;

            ToolStripMenuItem ts = sender as ToolStripMenuItem;
            Hatırlatıcı_.Durum_ Durum = ts.Tag as Hatırlatıcı_.Durum_;
            switch (ts.Text)
            {
                case "Hatırlatıcı kur":
                    _Hatırlatıcı_Görünümüne_Geç_();
                    break;

                case "Hatırlatıcıyı İptal et":
                    if (DialogResult.No == MessageBox.Show("Hatırlatıcıyı iptal etmek istiyor musunuz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return;
                    HatırlatıcıSayfası.Hatırlatıcı.Sil(((int)Ağaç.SelectedNode.Tag).ToString());
                    Ağaç_Güncelle(true, false, false);
                    break;

                case "Hatırlatıcıyı tekrar kur":
                    if (Durum.TetiklenmesiBekleniyor) _Hatırlatıcı_Görünümüne_Geç_();
                    else
                    {
                        //geçmiş
                        if (string.IsNullOrWhiteSpace(Durum.TekrarlayıcıKomutCümlesi)) _Hatırlatıcı_Görünümüne_Geç_();
                        else
                        {
                            if (DialogResult.No == MessageBox.Show("Hatırlatıcıyı tekrar kurmak istiyor musunuz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return;
                            HatırlatıcıSayfası.Hatırlatıcı.SonrakiTetikleme_Kur(((int)Ağaç.SelectedNode.Tag).ToString(), false);
                            Ağaç_Güncelle(true, false, false);
                        }
                    }
                    break;
            }

            void _Hatırlatıcı_Görünümüne_Geç_()
            {
                if (Durum == null)
                {
                    Hatırlatıcı_Hatırlat_Bugün_a.Checked = true;
                    Hatırlatıcı_Hatırlat_Bugün_a.Checked = false; //tüm seçili alanların sıfırlanması için
                    Hatırlatıcı_Tekrarla_Onay.Checked = false;
                }
                else
                {
                    Hatırlatıcı_Hatırlat_Diğer_Onay.Checked = true;
                    Hatırlatıcı_Hatırlat_Diğer_Yazı.Text = Durum.TetikleneceğiAn.Yazıya("dd MM yyyy HH mm");

                    if (!string.IsNullOrWhiteSpace(Durum.TekrarlayıcıKomutCümlesi))
                    {
                        Hatırlatıcı_Tekrarla_Onay.Checked = true;
                        Hatırlatıcı_Tekrarla_adet.Value = int.Parse(Durum.TekrarlayıcıKomutCümlesi.Substring(2));

                        switch (Durum.TekrarlayıcıKomutCümlesi[0])
                        {
                            case 'g': Hatırlatıcı_Tekrarla_dönem.SelectedIndex = 0; break;
                            case 'h': Hatırlatıcı_Tekrarla_dönem.SelectedIndex = 1; break;
                            case 'a': Hatırlatıcı_Tekrarla_dönem.SelectedIndex = 2; break;
                            case 'y': Hatırlatıcı_Tekrarla_dönem.SelectedIndex = 3; break;
                        }
                    }
                    else Hatırlatıcı_Tekrarla_Onay.Checked = false;
                }

                splitContainer2.Panel2Collapsed = true;
                Panel_Aç(Panel2Durumu.Hatırlatıcı);
                SayfaDüzeni_OnayRet();
                Genel.KaydedilmemişBilgiVar = false;
            }
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
            SayfaDüzeni_OnayRet();

            if (Grid_Listele_Tarihçe.Checked)
            {
                try
                {
                    if (Genel.GözGezdirmeÇalışıyor) { Genel.GözGezdirmeKapatmaTalebi = true; return; }
                    Genel.GözGezdirmeÇalışıyor = true;
                    Genel.GözGezdirmeKapatmaTalebi = false;
                    Genel.KaydedilmemişBilgiVar = true;
                    splitContainer2.Panel2Collapsed = false;

                    if (!int.TryParse(MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Text, out int İstenenÇıktıAdedi)) İstenenÇıktıAdedi = int.MaxValue;

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
                        toolStripİlerleme.Visible = true;

                        SQLiteDataReader Dr;
                        string Sorgu = "select * from gecici where Zaman between " + baslangıc + " and " + bitiş + " order by Zaman";
                        if (MenuItem_Grid_Etk_Sıralama_YeniUstte.Checked) Sorgu += " desc";
                        if (!Sql_Sorgula(Sorgu, out Dr)) { label_Görev_Tarih.Text = "Beklenmeyen durum oluştu. Tekrar deneyiniz"; return; }
                        Genel.Tik = Environment.TickCount + 1000;
                        int filrelendi = 0;
                        while (Dr.Read())
                        {
                            int durum_no = Convert.ToInt32(Dr["Durum"]);
                            if (MenuItem_Grid_Etk_AyarlarSayfasındakiFiltrelemeyiKullan.Checked)
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
                            if (Genel.Tik < Environment.TickCount)
                            {
                                Grid_Etkinlikler.TopLeftHeaderCell.Value = Grid_Etkinlikler.RowCount.ToString();
                                Application.DoEvents();
                                Genel.Tik = Environment.TickCount + 1000;
                            }
                            if (Genel.GözGezdirmeKapatmaTalebi) break;
                            if (Grid_Etkinlikler.RowCount == İstenenÇıktıAdedi) break;
                        }
                        Dr.Close();
                        Grid_Etkinlikler.TopLeftHeaderCell.Value = Grid_Etkinlikler.RowCount.ToString();
                        if (filrelendi > 0) Grid_Etkinlikler.TopLeftHeaderCell.Value += "-" + filrelendi.ToString();
                        if (Genel.GözGezdirmeKapatmaTalebi) Ağaç.SelectedNode = null;
                    }
                }
                catch (Exception) { label_Görev_Tarih.Text = "Beklenmeyen durum oluştu. Tekrar deneyiniz"; }
            }

            label_Görev_Tarih.Enabled = false;
            Genel.KaydedilmemişBilgiVar = false;
            toolStripİlerleme.Visible = false;
            toolStripEtiket.Visible = true;
            SayfaDüzeni_Normal();
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
        private void MenuItem_Grid_Etk_AyarlarSayfasındakiFiltrelemeyiKullan_Click(object sender, EventArgs e)
        {
            MenuItem_Grid_Etk_ZaAr_Filtrele_Click(null, null);
        }

        private void Grid_Listele_Tarihçe_CheckedChanged(object sender, EventArgs e)
        {
            if (Grid_Listele_Tarihçe.Checked)
            {
                TreeNode tn = Ağaç.SelectedNode;
                Ağaç.SelectedNode = null;
                Ağaç.SelectedNode = tn;
            }
            else splitContainer2.Panel2Collapsed = true;
        }
        private void Hatırlatıcı_Hatırlat_Diğer_Yazı_TextChanged(object sender, EventArgs e)
        {
            //gg aa yyyy ss dd zaman aralıgı girişi
            string girilen = Hatırlatıcı_Hatırlat_Diğer_Yazı.Text;
            girilen = girilen.Replace(',', ' ');
            string[] dizi = girilen.Split(' ');
            if (dizi.Length >= 1 && dizi[0].Length < 2) dizi[0] = "0" + dizi[0];    //gün 1 ise 01
            if (dizi.Length >= 2 && dizi[1].Length < 2) dizi[1] = "0" + dizi[1];    //ay  1 ise 01
            if (dizi.Length >= 3 && dizi[2].Length == 2) dizi[2] = "20" + dizi[2];  //yıl 20 ise 2020
            if (dizi.Length >= 4 && dizi[3].Length < 2) dizi[3] = "0" + dizi[3];    //saat 1 ise 01
            if (dizi.Length >= 5 && dizi[4].Length < 2) dizi[4] = "0" + dizi[4];    //dk   1 ise 01
            girilen = "";
            foreach (var b in dizi) girilen += b + " ";
            girilen = girilen.TrimEnd();

            string ş = "dd MM yyyy HH mm";
            while (ş.Length >= 5)
            {
                if (DateTime.TryParseExact(girilen, ş, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out DateTime hedeflenen_süre))
                {
                    Hatırlatıcı_Hatırlat_Diğer.Text = "Diğer -> " + ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Yazıya(hedeflenen_süre, "dd.MM.yyyy HH:mm") + " <- ( gg aa | gg aa yy | gg aa yy ss | gg aa yy ss dd | g/gg a/aa yy/yyyy s/ss d/dd boşluk virgül )";
                    Hatırlatıcı_Hatırlat_Diğer.Tag = hedeflenen_süre;

                    Genel_KaydedilmemişBilgiVar(null, null);
                    return;
                }
                else if (ş.EndsWith("yyyy")) ş = ş.Remove(ş.Length - 5);
                else ş = ş.Remove(ş.Length - 3);
            }
            Hatırlatıcı_Hatırlat_Diğer.Text = "Diğer ( gg aa | gg aa yy | gg aa yy ss | gg aa yy ss dd | g/gg a/aa yy/yyyy s/ss d/dd boşluk virgül )";
            Hatırlatıcı_Hatırlat_Diğer.Tag = null;
        }
        private void Hatırlatıcı_Hatırlat_Diğer_Yazı_KeyPress(object sender, KeyPressEventArgs e)
        {
            Hatırlatıcı_Hatırlat_Diğer_Onay.Checked = true;
        }
        int Hatırlatıcı_GeriBildirim_İşlemi(string TakmaAdı, object Hatırlatıcı)
        {
            Console.WriteLine("tetiklenen " + TakmaAdı);

            int kendisi = int.Parse(TakmaAdı);
            int sahibi = kendisi;
            while (sahibi > 0) sahibi = int.Parse(Sql_GörevÖzelliği(sahibi, EtkinlikÖzellikleri.Sahibi));
            if (sahibi == (int)DalTürü.Görevler)
            {
                //çöp kutusunda olan bir görev değil

                Ağaç.Invoke((Action)(() =>
                {
                    Ağaç_Güncelle(true, false, false);
                }));

                string tanımı = Sql_GörevÖzelliği(kendisi, EtkinlikÖzellikleri.Tanımı);
                notifyIcon1.ShowBalloonTip(15000, Kendi.Adı + " Hatırlatıcı", tanımı + " için kurduğunuz hatırlatıcının zamanı geçti", ToolTipIcon.Warning);
            }

            return 0;
        }
        int Hatırlatıcı_GeriBildirim_İşlemi_TümSüresiDolanlar(string TakmaAdı, object Hatırlatıcısı)
        {
            string SüresiDolanlar = "";
            List<Hatırlatıcı_.Durum_> Hatırlatıcılar = HatırlatıcıSayfası.Hatırlatıcı.Bul();
            int adet = 0;
            foreach (var b in Hatırlatıcılar)
            {
                if (int.TryParse(b.TakmaAdı, out int sahibi))
                {
                    while (sahibi > 0) sahibi = int.Parse(Sql_GörevÖzelliği(sahibi, EtkinlikÖzellikleri.Sahibi));
                    if (sahibi == (int)DalTürü.Görevler && !b.TetiklenmesiBekleniyor)
                    {
                        adet++;
                        SüresiDolanlar += Environment.NewLine + Sql_Görev_TanımVeHatırlatıcıDetayı(int.Parse(b.TakmaAdı));
                    }
                }
            }
            if (!string.IsNullOrEmpty(SüresiDolanlar))
            {
                notifyIcon1.ShowBalloonTip(15000, "Süresi geçen hatırlatıcılar (" + adet + ")", SüresiDolanlar, ToolTipIcon.Warning);
            }

            return (int)new TimeSpan(1, 0, 0).TotalMilliseconds;
        }

        void NotlarSayfası_İlkAçılış()
        {
            NotlarSayfası.Ayarlar = new Ayarlar_(AyarlarDosyası: pak + "Notlar\\Notlar.Ayarlar");
            NotlarSayfası.Ayarlar_Notlar = NotlarSayfası.Ayarlar.Bul("Notlar Sayfası", true);
            Ayarlar_Genel = NotlarSayfası.Ayarlar.Bul("Ana Ekran", true);

            NotlarSayfası_KısayolTuşu.Text = NotlarSayfası.Ayarlar_Notlar.Oku("Kısayol Tuşu", "");
            if (string.IsNullOrEmpty(NotlarSayfası_KısayolTuşu.Text)) return;

            try
            {
                NotlarSayfası.KlavyeFareGozlemcisi.KısayolTuşu_Ekle(NotlarSayfası_KısayolTuşu.Text, NotlarSayfası_KısayolTuşu_Basıldı, 0);
            }
            catch (Exception)
            {
                NotlarSayfası_KısayolTuşu.Text = "";
            }
        }
        void NotlarSayfası_Kapanış()
        {
            if (NotlarSayfası.Pencere != null) NotlarSayfası.Pencere.NotlarEkranı_FormClosing(null, new FormClosingEventArgs(CloseReason.None, false));

            NotlarSayfası.Ayarlar.DeğişiklikleriKaydet(true);
        }
        private void NotlarSayfası_Dinle_Click(object sender, EventArgs e)
        {
            if (NotlarSayfası.KısayolTuşunuDinliyor)
            {
                NotlarSayfası_Dinle.Text = "Dinle";
                NotlarSayfası.KısayolTuşunuDinliyor = false;
                NotlarSayfası.KlavyeFareGozlemcisi.KısayolTuşu_Tanıt_Bitir();

                if (string.IsNullOrEmpty(NotlarSayfası_KısayolTuşu.Text)) return;
                try
                {
                    NotlarSayfası.KlavyeFareGozlemcisi.KısayolTuşu_Ekle(NotlarSayfası_KısayolTuşu.Text, NotlarSayfası_KısayolTuşu_Basıldı, 0);
                    NotlarSayfası.Ayarlar_Notlar.Yaz("Kısayol Tuşu", NotlarSayfası_KısayolTuşu.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    NotlarSayfası_KısayolTuşu.Text = "";
                }
            }
            else
            {
                NotlarSayfası_Dinle.Text = "Kaydet";
                NotlarSayfası.KısayolTuşunuDinliyor = true;
                NotlarSayfası.KlavyeFareGozlemcisi.KısayolTuşu_Tanıt_Başlat(NotlarSayfası_KısayolTuşu_Tanıt_YeniTuşaBasıldı_Islemi_);
            }
        }
        public void NotlarSayfası_KısayolTuşu_Tanıt_YeniTuşaBasıldı_Islemi_(string BasılanTuşlar)
        {
            NotlarSayfası_KısayolTuşu.Text = BasılanTuşlar;
        }
        public void NotlarSayfası_KısayolTuşu_Basıldı(int Hatırlatıcı)
        {
            if (NotlarSayfası.Pencere == null)
            {
                NotlarSayfası.Pencere = new NotlarEkranı(pak + "Notlar\\", NotlarSayfası.Ayarlar_Notlar);
                NotlarSayfası.Pencere.Font = Font;
            }
            
            NotlarSayfası.Pencere.Show();
        }
        private void Menu_İkon_Notlar_Click(object sender, EventArgs e)
        {
            NotlarSayfası_KısayolTuşu_Basıldı(0);
        }
    }
}

