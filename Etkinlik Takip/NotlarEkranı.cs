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

        public NotlarEkranı(string Pak)
        {
            this.Pak = Pak;
            InitializeComponent();
        }
        private void Notlar_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.Etkinlik_Takip;
            Text = "Mup " + Kendi.Adı() + " Notlar V" + Kendi.Sürümü_Dosya() + " ( çıkış - esc )";

            string PencereKonumu = "";
            if (File.Exists(Pak + "PencereKonumu.mup")) PencereKonumu = File.ReadAllText(Pak + "PencereKonumu.mup");
            if (!string.IsNullOrEmpty(PencereKonumu))
            {
                string[] dizi = PencereKonumu.Split(' ');
                if (dizi.Length == 4)
                {
                    if (int.TryParse(dizi[0], out int x))
                    {
                        if (int.TryParse(dizi[1], out int y))
                        {
                            if (int.TryParse(dizi[2], out int gen))
                            {
                                if (int.TryParse(dizi[3], out int uzu))
                                {
                                    Location = new System.Drawing.Point(x, y);
                                    Size = new System.Drawing.Size(gen, uzu);
                                }
                            }
                        }
                    }
                }
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

            string SonBaşlık = "";
            if (File.Exists(Pak + "SonBaşlık.mup")) SonBaşlık = File.ReadAllText(Pak + "SonBaşlık.mup");
            if (!string.IsNullOrEmpty(SonBaşlık))
            {
                Başlıklar.Text = SonBaşlık;
            }
            else if (Başlıklar.Items.Count > 0) Başlıklar.SelectedIndex = 0;
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
                DosyayaKaydet(String.IsNullOrEmpty(Başlıklar.Text) ? "Genel" : Başlıklar.Text); 
            }
        }
        private void NotlarEkranı_FormClosing(object sender, FormClosingEventArgs e)
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

                if (!string.IsNullOrEmpty(Başlıklar.Text)) File.WriteAllText(Pak + "SonBaşlık.mup", Başlıklar.Text);

                Show();
                if (WindowState == FormWindowState.Normal) File.WriteAllText(Pak + "PencereKonumu.mup", Location.X + " " + Location.Y + " " + Size.Width + " " + Size.Height);
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
