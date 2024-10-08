﻿using ArgeMup.HazirKod;
using System;
using System.IO;
using System.Windows.Forms;
using ArgeMup.HazirKod.Dönüştürme;
using System.Drawing;
using System.Collections.Generic;
using ArgeMup.HazirKod.Ekİşlemler;

namespace Etkinlik_Takip
{
    public partial class NotlarEkranı : Form
    {
        string Pak;
        string AçıkOlanBaşlık = "";
        bool Çalışsın = true;
        bool YazıDeğişti
        {
            get
            {
                return _Sayac_YazıDeğişti > 0;
            }
            set
            {
                if (value)
                {
                    _Sayac_YazıDeğişti++;
                    Text = _text_YazıDeğişti + " (*" + _Sayac_YazıDeğişti + "*)";
                }
                else
                {
                    _Sayac_YazıDeğişti = 0;
                    Text = _text_YazıDeğişti;
                }
            }
        }
        int _Sayac_YazıDeğişti = 0;
        string _text_YazıDeğişti = "Mup " + Kendi.Adı + " Notlar V" + Kendi.Sürümü_Dosya + " ( çıkış - esc )";
        IDepo_Eleman Ayarlar = null;
        ArgeMup.HazirKod.Ekranlar.ListeKutusu Başlıklar = new ArgeMup.HazirKod.Ekranlar.ListeKutusu();

        public NotlarEkranı(string Pak, IDepo_Eleman Ayarlar)
        {
            InitializeComponent();

            Ayraç.Panel1.Controls.Add(Başlıklar);
            Başlıklar.Dock = DockStyle.Fill;
            Başlıklar.GeriBildirim_İşlemi += Başlıklar_GeriBildirim_İşlemi;

            this.Ayarlar = Ayarlar;
            this.Pak = Pak;
        }
        private void Notlar_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.Etkinlik_Takip;
            Text = _text_YazıDeğişti;

            if (!string.IsNullOrEmpty(Ayarlar.Oku("Pencere Konumu/x")))
            {
                Location = new System.Drawing.Point((int)Ayarlar.Oku_Sayı("Pencere Konumu/x"), (int)Ayarlar.Oku_Sayı("Pencere Konumu/y"));
                Size = new System.Drawing.Size((int)Ayarlar.Oku_Sayı("Pencere Konumu/genişlik"), (int)Ayarlar.Oku_Sayı("Pencere Konumu/uzunluk"));
            }

            List<string> Başlık_lar = new List<string>(Ayarlar["Tüm Başlıklar"].İçeriği);
            foreach (string b in Klasör.Listele_Klasör(Pak, "*", SearchOption.TopDirectoryOnly))
            {
                string kesilmiş = b.Substring(Pak.Length);

                if (!Başlık_lar.Contains(kesilmiş)) Başlık_lar.Add(kesilmiş);

                Dosya.Sil_SayısınaGöre(b, 15, new string[] { "*.mup" });
            }
            
            Başlıklar.Başlat(null, Başlık_lar, "Konu başlıkları", new ArgeMup.HazirKod.Ekranlar.ListeKutusu.Ayarlar_(Gizlenebilir: false));

            Başlıklar.SeçilenEleman_Adı = Ayarlar.Oku("Son Başlık");
            if (string.IsNullOrEmpty(Başlıklar.SeçilenEleman_Adı) && Başlıklar.Tüm_Elemanlar.Count > 0) Başlıklar.SeçilenEleman_Adı = Başlıklar.Tüm_Elemanlar[0];

            NotlarınKendisi.ZoomFactor = (float)Ayarlar.Oku_Sayı("Yakınlaştırma Oranı", 1);
        }
        private void NotlarEkranı_Activated(object sender, EventArgs e)
        {
            NotlarınKendisi.Focus();
        }
        private void Notlar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Hide();
                DosyayaKaydet(string.IsNullOrEmpty(Başlıklar.SeçilenEleman_Adı) ? "Genel" : Başlıklar.SeçilenEleman_Adı); 
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                DosyayaKaydet(string.IsNullOrEmpty(Başlıklar.SeçilenEleman_Adı) ? "Genel" : Başlıklar.SeçilenEleman_Adı);
            }
        }
        public void NotlarEkranı_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                Notlar_KeyDown(null, new KeyEventArgs(Keys.Escape));
            }
            else
            {
                Çalışsın = false;
                Notlar_KeyDown(null, new KeyEventArgs(Keys.Escape));

                Ayarlar["Tüm Başlıklar"].İçeriği = Başlıklar.Tüm_Elemanlar.ToArray();
                Ayarlar.Yaz("Son Başlık", Başlıklar.SeçilenEleman_Adı);
                Ayarlar.Yaz("Yakınlaştırma Oranı", NotlarınKendisi.ZoomFactor);
                Ayarlar.Yaz("Pencere Konumu/x", Location.X);
                Ayarlar.Yaz("Pencere Konumu/y", Location.Y);
                Ayarlar.Yaz("Pencere Konumu/genişlik", Size.Width);
                Ayarlar.Yaz("Pencere Konumu/uzunluk", Size.Height);
            }
        }

        bool DosyayaKaydet(string Başlık)
        {
            if (YazıDeğişti && !string.IsNullOrEmpty(Başlık))
            {
                try
                {
                    string kls = Pak + Başlık + "\\";
                    Directory.CreateDirectory(kls);

                    File.WriteAllText(kls + D_TarihSaat.Yazıya(DateTime.Now, D_TarihSaat.Şablon_DosyaAdı) + ".mup", NotlarınKendisi.Text);

                    NotlarınKendisi.SaveFile(kls + Başlık + ".rtf");
                    YazıDeğişti = false;
                    return true;
                }
                catch (Exception ex)
                {
                    if (Çalışsın) MessageBox.Show(ex.Message);
                }
            }

            return false;
        }

        private void Belirginleştir_X_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            switch (tsmi.Text)
            {
                case "Kızart":
                    NotlarınKendisi.SelectionFont = new Font(NotlarınKendisi.Font, FontStyle.Bold);
                    NotlarınKendisi.SelectionBackColor = Color.Red;
                    break;
                case "Sarart":
                    NotlarınKendisi.SelectionFont = new Font(NotlarınKendisi.Font, FontStyle.Bold);
                    NotlarınKendisi.SelectionBackColor = Color.Yellow;
                    break;
                case "Beyazlat":
                    NotlarınKendisi.SelectionFont = new Font(NotlarınKendisi.Font, FontStyle.Regular);
                    NotlarınKendisi.SelectionColor = SystemColors.WindowText;
                    NotlarınKendisi.SelectionBackColor = SystemColors.Window;
                    break;
            }
        }

        private void NotlarınKendisi_TextChanged(object sender, EventArgs e)
        {
            YazıDeğişti = true;
        }

        private bool Başlıklar_GeriBildirim_İşlemi(string Adı, ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü Türü, string YeniAdı)
        {
            if (Adı.BoşMu()) return false;

            switch (Türü)
            {
                case ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.YeniEklendi:
                    YazıDeğişti = true;
                    return DosyayaKaydet(Adı);

                case ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.ElemanSeçildi:
                    DosyayaKaydet(AçıkOlanBaşlık);

                    AçıkOlanBaşlık = "";
                    float YakınlaştırmaDeğeri = NotlarınKendisi.ZoomFactor;
                    NotlarınKendisi.Clear();

                    if (!string.IsNullOrEmpty(Başlıklar.SeçilenEleman_Adı))
                    {
                        string dsy = Pak + Başlıklar.SeçilenEleman_Adı + "\\" + Başlıklar.SeçilenEleman_Adı + ".rtf";
                        if (File.Exists(dsy))
                        {
                            NotlarınKendisi.LoadFile(dsy);
                        }

                        AçıkOlanBaşlık = Başlıklar.SeçilenEleman_Adı;
                    }

                    NotlarınKendisi.ClearUndo();
                    NotlarınKendisi.ZoomFactor = 1.0f;
                    NotlarınKendisi.ZoomFactor = YakınlaştırmaDeğeri;
                    YazıDeğişti = false;
                    return true;

                case ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.AdıDeğiştirildi:
                    string kaynak = Pak + Adı + "\\";
                    string hedef = Pak + YeniAdı + "\\";
                    Directory.Move(kaynak, hedef);
                    File.Move(hedef + Adı + ".rtf", hedef + YeniAdı + ".rtf");
                    return true;

                case ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.KonumDeğişikliğiKaydedildi:
                    return true;

                case ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.Silindi:
                    string kls = Pak + Adı + "\\";
                    YazıDeğişti = false;
                    return Temkinli.Klasör.Sil(kls);
            }

            return false;
        }
    }
}
