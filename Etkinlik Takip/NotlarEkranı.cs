using ArgeMup.HazirKod;
using System;
using System.IO;
using System.Windows.Forms;
using ArgeMup.HazirKod.Dönüştürme;
using System.Drawing;

namespace Etkinlik_Takip
{
    public partial class NotlarEkranı : Form
    {
        string Pak;
        string AçıkOlanBaşlık = "";
        bool Çalışsın = true;
        bool YazıDeğişti = false;
        IDepo_Eleman Ayarlar = null;

        public NotlarEkranı(string Pak, IDepo_Eleman Ayarlar)
        {
            InitializeComponent();

            this.Ayarlar = Ayarlar;
            this.Pak = Pak;
        }
        private void Notlar_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.Etkinlik_Takip;
            Text = "Mup " + Kendi.Adı + " Notlar V" + Kendi.Sürümü_Dosya + " ( çıkış - esc )";

            if (!string.IsNullOrEmpty(Ayarlar.Oku("Pencere Konumu/x")))
            {
                Location = new System.Drawing.Point((int)Ayarlar.Oku_Sayı("Pencere Konumu/x"), (int)Ayarlar.Oku_Sayı("Pencere Konumu/y"));
                Size = new System.Drawing.Size((int)Ayarlar.Oku_Sayı("Pencere Konumu/genişlik"), (int)Ayarlar.Oku_Sayı("Pencere Konumu/uzunluk"));
            }

            string[] dizi_başlıklar = Directory.GetDirectories(Pak, "*", SearchOption.TopDirectoryOnly);
            if (dizi_başlıklar != null && dizi_başlıklar.Length > 0)
            {
                Başlıklar.BeginUpdate();
                foreach (string b in dizi_başlıklar)
                {
                    string kesilmiş = b.Substring(Pak.Length);

                    Başlıklar.Items.Add(kesilmiş);

                    Dosya.Sil_SayısınaGöre(b, 15, "*.mup");
                }
                Başlıklar.EndUpdate();
            }

            Başlıklar.Text = Ayarlar.Oku("Son Başlık");
            if (string.IsNullOrEmpty(Başlıklar.Text))
            {
                if (Başlıklar.Items.Count > 0) Başlıklar.SelectedIndex = 0;
            }
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
                DosyayaKaydet(string.IsNullOrEmpty(Başlıklar.Text) ? "Genel" : Başlıklar.Text); 
            }
        }
        private void NotlarEkranı_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            Notlar_KeyDown(null, new KeyEventArgs(Keys.Escape));
        }
        public void KapatSinyali()
        {
            Çalışsın = false;
            Notlar_KeyDown(null, new KeyEventArgs(Keys.Escape));

            Ayarlar.Yaz("Son Başlık", Başlıklar.Text);

            Show();
            if (WindowState == FormWindowState.Normal)
            {
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

        private void Ekle_Click(object sender, EventArgs e)
        {
            YazıDeğişti = true;
            if (!DosyayaKaydet(YeniBaşlık.Text)) return;
            
            if (!Başlıklar.Items.Contains(YeniBaşlık.Text)) Başlıklar.Items.Add(YeniBaşlık.Text);
        }

        private void Başlıklar_SelectedIndexChanged(object sender, EventArgs e)
        {
            DosyayaKaydet(AçıkOlanBaşlık);
            
            AçıkOlanBaşlık = "";
            float YakınlaştırmaDeğeri = NotlarınKendisi.ZoomFactor;
            NotlarınKendisi.Clear();

            if (!string.IsNullOrEmpty(Başlıklar.Text))
            {
                string dsy = Pak + Başlıklar.Text + "\\" + Başlıklar.Text + ".rtf";
                if (File.Exists(dsy))
                {
                    NotlarınKendisi.LoadFile(dsy);
                }

                AçıkOlanBaşlık = Başlıklar.Text;
            }

            NotlarınKendisi.ClearUndo();
            NotlarınKendisi.ZoomFactor = 1.0f;
            NotlarınKendisi.ZoomFactor = YakınlaştırmaDeğeri;
            YazıDeğişti = false;
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
    }
}
