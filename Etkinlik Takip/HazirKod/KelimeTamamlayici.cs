// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/HazirKod>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ArgeMup.HazirKod
{
    public class KelimeTamamlayici_ : IDisposable
    {
        public const string Sürüm = "V1.7";
        #region Tanımlamalar
        public List<string> ÖnerilenKelimeler;

        Form AnaForm;
        ListBox Tavsiyeler;
        System.Timers.Timer Zamanlayıcı = new System.Timers.Timer(); int KaydetmeAnı;
        TextBox _TextBox;
        ContextMenuStrip _AnaMenü_ToolStripTextBox; ToolStripTextBox _ToolStripTextBox;
        Button _TavsiyeSilmeTuşu;
        string KelimeListesiDosyaAdı;
        int BirKeredeTavsiyeEdilecekKelimeSatırAdedi;
        int SonÖnerilenKelimeAdedi;

        enum MalzemeTipi { Boşta, TextBox, ToolStripTextBox };
        MalzemeTipi Tip = MalzemeTipi.Boşta;

        struct _Girdi
        {
            public bool GözGezdirmeÇalışıyor;
            public bool GözGezdirmeKapatmaTalebi;
        }
        _Girdi Girdi = new _Girdi();

        public struct _İmlaKuralları
        {
            public bool Anaİzin;
            public bool ilkHarfleriBüyüt;
            public bool NoktaVirgülVeAltSatıraGeç_ArdındanBoşlukEkle;
            public bool FazlaBoşluklarıSil;
            public bool CümleSonlarınaNoktaEkle;
            public char[] BaşındanSonundanSilinecekKarakterler;
        }
        public _İmlaKuralları İmlaKuralları = new _İmlaKuralları();
        #endregion

        public KelimeTamamlayici_(Form Sahibi, string ÖnerilecekKelimelerListesiDosyaYolu, List<string> KelimeListesi, int TavsiyeSatırSayısı = 5)
        {
            if (Tip != MalzemeTipi.Boşta) return;

            AnaForm = Sahibi;
            KelimeListesiDosyaAdı = ÖnerilecekKelimelerListesiDosyaYolu;
            ÖnerilenKelimeler = KelimeListesi;
            BirKeredeTavsiyeEdilecekKelimeSatırAdedi = TavsiyeSatırSayısı;

            İmlaKuralları.Anaİzin = true;
            İmlaKuralları.CümleSonlarınaNoktaEkle = true;
            İmlaKuralları.FazlaBoşluklarıSil = true;
            İmlaKuralları.ilkHarfleriBüyüt = true;
            İmlaKuralları.NoktaVirgülVeAltSatıraGeç_ArdındanBoşlukEkle = true;
            İmlaKuralları.BaşındanSonundanSilinecekKarakterler = new char[0];
        }
        public bool Başlat(TextBox Kutucuk)
        {
            try
            {
                if (Kutucuk == null) return false;
                if (Kutucuk == _TextBox) return true;
                Başlat_Ortak(Kutucuk.Font);

                _TextBox = Kutucuk;
                _TextBox.KeyDown += _TuşaBasıldı;
                _TextBox.KeyPress += _TuşaBasıldıÇekildi;
                _TextBox.TextChanged += _İçerikDeğişti;
                _TextBox.Leave += _OdağınıKaybetti;

                Tip = MalzemeTipi.TextBox;
                return true;
            }
            catch (Exception) { }
            return false;
        }
        public bool Başlat(ContextMenuStrip SağTıklamaMenüsü, ToolStripTextBox Kutucuk)
        {
            try
            {
                if (SağTıklamaMenüsü == null || Kutucuk == null) return false;
                if (_AnaMenü_ToolStripTextBox == SağTıklamaMenüsü && Kutucuk == _ToolStripTextBox) return true;
                Başlat_Ortak(Kutucuk.Font);

                _AnaMenü_ToolStripTextBox = SağTıklamaMenüsü;
                _ToolStripTextBox = Kutucuk;
                _ToolStripTextBox.KeyDown += _TuşaBasıldı;
                _ToolStripTextBox.KeyPress += _TuşaBasıldıÇekildi;
                _ToolStripTextBox.TextChanged += _İçerikDeğişti;
                _AnaMenü_ToolStripTextBox.Closed += _OdağınıKaybetti;

                Tip = MalzemeTipi.ToolStripTextBox;
                return true;
            }
            catch (Exception) { }
            return false;
        }
        public void İmlaKurallarınıKopyala(_İmlaKuralları kurallar)
        {
            İmlaKuralları.Anaİzin = kurallar.Anaİzin;
            İmlaKuralları.CümleSonlarınaNoktaEkle = kurallar.CümleSonlarınaNoktaEkle;
            İmlaKuralları.FazlaBoşluklarıSil = kurallar.FazlaBoşluklarıSil;
            İmlaKuralları.ilkHarfleriBüyüt = kurallar.ilkHarfleriBüyüt;
            İmlaKuralları.NoktaVirgülVeAltSatıraGeç_ArdındanBoşlukEkle = kurallar.NoktaVirgülVeAltSatıraGeç_ArdındanBoşlukEkle;
            İmlaKuralları.BaşındanSonundanSilinecekKarakterler = kurallar.BaşındanSonundanSilinecekKarakterler;
        }
        public bool ListeGörünüyorMu()
        {
            if (Tavsiyeler == null) return false;
            if (!Tavsiyeler.Visible) return false;
            return true;
        }
        public void Durdur()
        {
            try
            {
                if (Tip == MalzemeTipi.TextBox)
                {
                    _TextBox.KeyDown -= _TuşaBasıldı;
                    _TextBox.KeyPress -= _TuşaBasıldıÇekildi;
                    _TextBox.TextChanged -= _İçerikDeğişti;
                    _TextBox.Leave -= _OdağınıKaybetti;
                    _TextBox = null;
                }
                else if (Tip == MalzemeTipi.ToolStripTextBox)
                {
                    _ToolStripTextBox.KeyDown -= _TuşaBasıldı;
                    _ToolStripTextBox.KeyPress -= _TuşaBasıldıÇekildi;
                    _ToolStripTextBox.TextChanged -= _İçerikDeğişti;
                    _AnaMenü_ToolStripTextBox.Closed -= _OdağınıKaybetti;

                    _ToolStripTextBox = null;
                }
                else return;

                if (AnaForm.Controls.IndexOf(Tavsiyeler) >= 0) AnaForm.Controls.Remove(Tavsiyeler);
                if (AnaForm.Controls.IndexOf(_TavsiyeSilmeTuşu) >= 0) AnaForm.Controls.Remove(_TavsiyeSilmeTuşu);

                if (Tavsiyeler != null)
                {
                    _TavsiyeSilmeTuşu.Click -= TavsiyeSilmeTuşunaBasıldı;
                    _TavsiyeSilmeTuşu.Dispose();

                    Tavsiyeler.MouseEnter -= Tavsiyeler_Fareİçerde;
                    Tavsiyeler.MouseDoubleClick -= Tavsiyeler_FareÇiftTıklandı;
                    Tavsiyeler.Dispose();
                    Tavsiyeler = null;
                }
                Tip = MalzemeTipi.Boşta;

                if (Zamanlayıcı != null) { Zamanlayıcı.Dispose(); Zamanlayıcı = null; }
            }
            catch (Exception) { }

            DosyayaKaydet();
        }

        void Tavsiyeler_Fareİçerde(object sender, EventArgs e)
        {
            _TavsiyeSilmeTuşu.Visible = true;
        }
        void Tavsiyeler_FareÇiftTıklandı(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            KeyEventArgs eee = new KeyEventArgs(Keys.Enter);
            _TuşaBasıldı(null, eee);
        }
        void TavsiyeSilmeTuşunaBasıldı(object sender, EventArgs e)
        {
            if (Tavsiyeler.SelectedIndex == -1) return;
            ÖnerilenKelimeler.RemoveAll(q => q == Tavsiyeler.SelectedItem.ToString());
            Tavsiyeler.Items.RemoveAt(Tavsiyeler.SelectedIndex);
            if (Tavsiyeler.Items.Count == 0) { Tavsiyeler.Visible = false; _TavsiyeSilmeTuşu.Visible = false; }
        }
        void _OdağınıKaybetti(object sender, EventArgs e)
        {
            try
            {
                if (_TavsiyeSilmeTuşu.Visible) return;

                if (Tavsiyeler != null) { Tavsiyeler.Visible = false; _TavsiyeSilmeTuşu.Visible = false; }

                string çıktı;
                if (Tip == MalzemeTipi.TextBox)
                {
                    çıktı = MetniDüzenle(_TextBox.Text, _TextBox.Multiline);
                    if (çıktı != _TextBox.Text) _TextBox.Text = çıktı;
                }
                else
                {
                    çıktı = MetniDüzenle(_ToolStripTextBox.Text);
                    if (çıktı != _ToolStripTextBox.Text) _ToolStripTextBox.Text = çıktı;
                }
            }
            catch (Exception) { }
        }
        void _TuşaBasıldı(object sender, KeyEventArgs e)
        {
            try
            {
                //önce starts with ve sonrasında conttains
                e.SuppressKeyPress = true;

                if (e.KeyCode == Keys.Up)
                {
                    if (Tavsiyeler.Visible)
                    {
                        if (Tavsiyeler.SelectedIndex > 0) Tavsiyeler.SelectedIndex--;
                    }
                    else e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (Tavsiyeler.Visible)
                    {
                        if (Tavsiyeler.SelectedIndex < Tavsiyeler.Items.Count - 1) Tavsiyeler.SelectedIndex++;
                    }
                    else e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Tab) e.SuppressKeyPress = false;
                else if (e.KeyCode == Keys.Escape) { Tavsiyeler.Visible = false; _TavsiyeSilmeTuşu.Visible = false; }
                else if (e.KeyCode == Keys.Left)
                {
                    if (Tip == MalzemeTipi.TextBox)
                    {
                        if (_TextBox.SelectionStart > 0) _TextBox.SelectionStart -= 1;
                    }
                    else
                    {
                        if (_ToolStripTextBox.SelectionStart > 0) _ToolStripTextBox.SelectionStart -= 1;
                    }
                    _İçerikDeğişti(null, null);
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (Tip == MalzemeTipi.TextBox)
                    {
                        if (_TextBox.SelectionStart < _TextBox.TextLength) _TextBox.SelectionStart += 1;
                    }
                    else
                    {
                        if (_ToolStripTextBox.SelectionStart < _ToolStripTextBox.TextLength) _ToolStripTextBox.SelectionStart += 1;
                    }
                    _İçerikDeğişti(null, null);
                }
                else if (e.KeyCode == Keys.Enter && Tavsiyeler.Visible && Tavsiyeler.SelectedIndex > -1)
                {
                    int Kursör;
                    string içerik;

                    if (Tip == MalzemeTipi.TextBox)
                    {
                        Kursör = _TextBox.SelectionStart;
                        içerik = _TextBox.Text;
                    }
                    else
                    {
                        Kursör = _ToolStripTextBox.SelectionStart;
                        içerik = _ToolStripTextBox.Text;
                    }

                    int KursörünİlkHali = Kursör;
                    if (Kursör > 0 && Kursör >= içerik.Length) Kursör--;

                    while (Kursör > 0)
                    {
                        if (içerik[Kursör] != ' ' &&
                            içerik[Kursör] != '.' &&
                            içerik[Kursör] != ',' &&
                            içerik[Kursör] != '\r' &&
                            içerik[Kursör] != '\n') Kursör--;
                        else break;
                    }
                    Kursör++;

                    içerik = KursördekiMetniDeğiştir(içerik, KursörünİlkHali, Tavsiyeler.SelectedItem.ToString());

                    while (Kursör < içerik.Length)
                    {
                        if (içerik[Kursör] != ' ' &&
                            içerik[Kursör] != '.' &&
                            içerik[Kursör] != ',' &&
                            içerik[Kursör] != '\r' &&
                            içerik[Kursör] != '\n') Kursör++;
                        else break;
                    }

                    if (Tip == MalzemeTipi.TextBox)
                    {
                        _TextBox.Text = içerik;
                        _TextBox.SelectionStart = Kursör;
                    }
                    else
                    {
                        _ToolStripTextBox.Text = içerik;
                        _ToolStripTextBox.SelectionStart = Kursör;
                    }

                    Tavsiyeler.Visible = false;
                    _TavsiyeSilmeTuşu.Visible = false;
                }
                else e.SuppressKeyPress = false;
            }
            catch (Exception) { }
        }
        void _TuşaBasıldıÇekildi(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' ||
                e.KeyChar == ',' ||
                e.KeyChar == '.' ||
                e.KeyChar == '\r' ||
                e.KeyChar == '\n')
            {
                int Kursör = 0;
                string içerik = "";

                if (Tip == MalzemeTipi.TextBox)
                {
                    Kursör = _TextBox.SelectionStart;
                    içerik = _TextBox.Text;
                }
                else
                {
                    Kursör = _ToolStripTextBox.SelectionStart;
                    içerik = _ToolStripTextBox.Text;
                }

                if (Kursör > 0 && Kursör >= içerik.Length) Kursör--;
                while (Kursör > 0)
                {
                    if (içerik[Kursör] == ' ' ||
                        içerik[Kursör] == '.' ||
                        içerik[Kursör] == ',' ||
                        içerik[Kursör] == '\r' ||
                        içerik[Kursör] == '\n') Kursör--;
                    else break;
                }
                int KursörBitiş = Kursör;

                while (Kursör > 0)
                {
                    if (içerik[Kursör] != ' ' &&
                        içerik[Kursör] != '.' &&
                        içerik[Kursör] != ',' &&
                        içerik[Kursör] != '\r' &&
                        içerik[Kursör] != '\n') Kursör--;
                    else break;
                }
                if (Kursör > 0) Kursör++;
                if (!string.IsNullOrEmpty(içerik))
                {
                    string EklenecekOlan = içerik.Substring(Kursör, (KursörBitiş - Kursör) + 1).Trim(' ', ',', '.', '\r', '\n');
                    if (İmlaKuralları.BaşındanSonundanSilinecekKarakterler != null) EklenecekOlan = EklenecekOlan.Trim(İmlaKuralları.BaşındanSonundanSilinecekKarakterler);
                    ÖnerilenKelimeler.Add(EklenecekOlan);
                }
            }
        }
        void _İçerikDeğişti(object sender, EventArgs e)
        {
            try
            {
                if (Tip == MalzemeTipi.Boşta) return;
                if (Girdi.GözGezdirmeÇalışıyor) { Girdi.GözGezdirmeKapatmaTalebi = true; return; }
                Girdi.GözGezdirmeÇalışıyor = true;
                Girdi.GözGezdirmeKapatmaTalebi = false;

                string İçerik = "";
                int tick = Environment.TickCount + 100;

                if (Tip == MalzemeTipi.TextBox) İçerik = KursördekiMetniAl(_TextBox.Text, _TextBox.SelectionStart);
                else İçerik = KursördekiMetniAl(_ToolStripTextBox.Text, _ToolStripTextBox.SelectionStart);
                İçerik = İçerik.ToLower().Trim(' ', ',', '.', '\r', '\n');
                if (İçerik == "") goto HatalıÇıkış;

                Tavsiyeler.BeginUpdate();
                Tavsiyeler.Items.Clear();
                Tavsiyeler.Height = 0;
                Tavsiyeler.Width = 0;

                List<string> gecici_starts = new List<string>();
                List<string> gecici_contains = new List<string>();

                foreach (var sıradaki in ÖnerilenKelimeler)
                {
                    if (sıradaki.ToLower().StartsWith(İçerik)) gecici_starts.Add(sıradaki);
                    if (sıradaki.ToLower().Contains(İçerik)) gecici_contains.Add(sıradaki);

                    if (Girdi.GözGezdirmeKapatmaTalebi) break;
                    if (tick < Environment.TickCount) { tick = Environment.TickCount + 100; Application.DoEvents(); }
                }

                gecici_starts.Sort();
                gecici_contains.Sort();

                foreach (var sıradaki in gecici_starts)
                {
                    if (!Tavsiyeler.Items.Contains(sıradaki)) Tavsiyeler.Items.Add(sıradaki);

                    if (Girdi.GözGezdirmeKapatmaTalebi) break;
                    if (tick < Environment.TickCount) { tick = Environment.TickCount + 100; Application.DoEvents(); }
                }

                foreach (var sıradaki in gecici_contains)
                {
                    if (!Tavsiyeler.Items.Contains(sıradaki)) Tavsiyeler.Items.Add(sıradaki);

                    if (Girdi.GözGezdirmeKapatmaTalebi) break;
                    if (tick < Environment.TickCount) { tick = Environment.TickCount + 100; Application.DoEvents(); }
                }

                if (Tavsiyeler.Items.Count == 0) { Tavsiyeler.EndUpdate(); goto HatalıÇıkış; }

                using (Graphics graphics = Tavsiyeler.CreateGraphics())
                {
                    for (int i = 0; i < Tavsiyeler.Items.Count; i++)
                    {
                        if (i < BirKeredeTavsiyeEdilecekKelimeSatırAdedi) Tavsiyeler.Height += Tavsiyeler.GetItemHeight(i);

                        int itemWidth = (int)graphics.MeasureString(((string)Tavsiyeler.Items[i]) + "___", Tavsiyeler.Font).Width;
                        if (Tavsiyeler.Width < itemWidth) Tavsiyeler.Width = itemWidth;

                        if (Girdi.GözGezdirmeKapatmaTalebi) break;
                        if (tick < Environment.TickCount) { tick = Environment.TickCount + 100; Application.DoEvents(); }
                    }
                    _TavsiyeSilmeTuşu.Width = (int)graphics.MeasureString("Sil", Tavsiyeler.Font).Width;
                }

                if (Tip == MalzemeTipi.TextBox)
                {
                    int konum = _TextBox.SelectionStart;
                    if (konum == _TextBox.Text.Length) { if (konum > 0) konum--; }

                    Point locationOnForm = _TextBox.FindForm().PointToClient(_TextBox.Parent.PointToScreen(_TextBox.Location));

                    Tavsiyeler.Left = locationOnForm.X + _TextBox.GetPositionFromCharIndex(konum).X;
                    Tavsiyeler.Top = locationOnForm.Y + _TextBox.GetPositionFromCharIndex(konum).Y + _TextBox.Font.Height + 6;
                }
                else
                {
                    Tavsiyeler.Left = _ToolStripTextBox.Owner.Location.X - AnaForm.Location.X + _ToolStripTextBox.Owner.Size.Width - 6;
                    Tavsiyeler.Top = _ToolStripTextBox.Owner.Location.Y - AnaForm.Location.Y - (30);
                }

                /*if (Tavsiyeler.Items.Count > BirKeredeTavsiyeEdilecekKelimeSatırAdedi)*/
                Tavsiyeler.Width += 20;
                _TavsiyeSilmeTuşu.Location = new Point(Tavsiyeler.Left + Tavsiyeler.Width - _TavsiyeSilmeTuşu.Width - 1, Tavsiyeler.Top + Tavsiyeler.Height - _TavsiyeSilmeTuşu.Height - 1);
                _TavsiyeSilmeTuşu.Visible = false;

                Tavsiyeler.SelectedIndex = 0;
                Tavsiyeler.Visible = true;
                Tavsiyeler.BringToFront();
                _TavsiyeSilmeTuşu.BringToFront();
                Tavsiyeler.EndUpdate();
                Girdi.GözGezdirmeÇalışıyor = false;
                return;
            }
            catch (Exception) { }

        HatalıÇıkış:
            Girdi.GözGezdirmeÇalışıyor = false;
            try { if (Tavsiyeler != null) { Tavsiyeler.Visible = false; _TavsiyeSilmeTuşu.Visible = false; } } catch (Exception) { }
        }

        void Başlat_Ortak(Font Ölçek)
        {
            Durdur();

            YenidenDene:
            if (!File.Exists(KelimeListesiDosyaAdı))
            {
                if (ÖnerilenKelimeler == null)
                {
                    //istenilen dosya adı ile dosya oluşturulabiliyormu kontrolü 
                    File.WriteAllText(KelimeListesiDosyaAdı, "}][{");
                    goto YenidenDene;
                }
            }
            else
            {
                ÖnerilenKelimeler = new List<string>();
                foreach (string satır in File.ReadAllLines(KelimeListesiDosyaAdı)) ÖnerilenKelimeler.Add(satır);
            }
            SonÖnerilenKelimeAdedi = ÖnerilenKelimeler.Count();

            if (Tavsiyeler == null)
            {
                Tavsiyeler = new ListBox();
                Tavsiyeler.Font = Ölçek;
                Tavsiyeler.Visible = false;
                Tavsiyeler.TabStop = false;
                Tavsiyeler.MouseEnter += Tavsiyeler_Fareİçerde;
                Tavsiyeler.MouseDoubleClick += Tavsiyeler_FareÇiftTıklandı;
                AnaForm.Controls.Add(Tavsiyeler);

                _TavsiyeSilmeTuşu = new Button();
                _TavsiyeSilmeTuşu.Font = Ölçek;
                _TavsiyeSilmeTuşu.Visible = false;
                _TavsiyeSilmeTuşu.AutoSize = true;
                _TavsiyeSilmeTuşu.TabStop = false;
                _TavsiyeSilmeTuşu.Text = "Sil";
                _TavsiyeSilmeTuşu.UseVisualStyleBackColor = true;
                _TavsiyeSilmeTuşu.Click += TavsiyeSilmeTuşunaBasıldı;
                AnaForm.Controls.Add(_TavsiyeSilmeTuşu);
            }

            if (Zamanlayıcı == null)
            {
                Zamanlayıcı = new System.Timers.Timer();
                Zamanlayıcı.Elapsed += Zamanlayıcıİşlemleri;
                Zamanlayıcı.Interval = 5 * 1000;
                Zamanlayıcı.AutoReset = false;
                Zamanlayıcı.Enabled = true;
                Zamanlayıcı.Start();
            }

            if (BirKeredeTavsiyeEdilecekKelimeSatırAdedi < 1) BirKeredeTavsiyeEdilecekKelimeSatırAdedi = 1;
        }
        void Zamanlayıcıİşlemleri(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Environment.TickCount > KaydetmeAnı)
            {
                if (SonÖnerilenKelimeAdedi != ÖnerilenKelimeler.Count())
                {
                    DosyayaKaydet();
                    SonÖnerilenKelimeAdedi = ÖnerilenKelimeler.Count();
                }
                KaydetmeAnı = Environment.TickCount + (5 * 60 * 1000);
            }

            _TavsiyeSilmeTuşu.Visible = false;

            Zamanlayıcı.Enabled = true;
        }
        void DosyayaKaydet()
        {
            try
            {
                if (string.IsNullOrEmpty(KelimeListesiDosyaAdı)) return;

                ÖnerilenKelimeler = ÖnerilenKelimeler.Distinct().ToList();

                using (StreamWriter st = new StreamWriter(KelimeListesiDosyaAdı, false))
                {
                    foreach (string str in ÖnerilenKelimeler) st.WriteLine(str);
                }
            }
            catch (Exception) { }
        }
        string KursördekiMetniAl(string Metin, int Kursör)
        {
            int KonumBaşlangıç = Kursör;
            while (KonumBaşlangıç > 0)
            {
                char şimdi = Metin[--KonumBaşlangıç];
                if (şimdi == ' ' || şimdi == '.' || şimdi == '\r' || şimdi == '\n') break;
            }

            int ek = 0;
            if (KonumBaşlangıç > 0) ek = 1;
            Metin = Metin.Substring(KonumBaşlangıç + ek, (Kursör - KonumBaşlangıç) - ek).ToLower();

            return Metin;
        }
        string KursördekiMetniDeğiştir(string Metin, int Kursör, string YeniMetin)
        {
            int KonumBaşlangıç = Kursör;
            while (KonumBaşlangıç > 0)
            {
                char şimdi = Metin[--KonumBaşlangıç];
                if (şimdi == ' ' || şimdi == '.' || şimdi == '\r' || şimdi == '\n') break;
            }
            int EkBaşlangıç = 0;
            if (KonumBaşlangıç > 0) EkBaşlangıç = 1;

            int KonumBitiş = Kursör;
            while (KonumBitiş < Metin.Length)
            {
                char şimdi = Metin[KonumBitiş];
                if (şimdi == ' ' || şimdi == '.' || şimdi == '\r' || şimdi == '\n') break;
                KonumBitiş++;
            }

            Metin = Metin.Remove(KonumBaşlangıç + EkBaşlangıç, (KonumBitiş - KonumBaşlangıç) - EkBaşlangıç);
            Metin = Metin.Insert(KonumBaşlangıç + EkBaşlangıç, YeniMetin);

            return Metin;
        }
        string MetniDüzenle(string Metin, bool ÇokSatırlı = false)
        {
            if (!İmlaKuralları.Anaİzin) return Metin;

            string çıktı = Metin.Trim(' ', ',', '.', '\r', '\n');
            if (çıktı == "") return Metin;

            if (İmlaKuralları.ilkHarfleriBüyüt) çıktı = çıktı.First().ToString().ToUpper() + çıktı.Substring(1);

            for (int i = 1; i < çıktı.Length; i++)
            {
                if (çıktı[i] == ',' || çıktı[i] == '.' || çıktı[i] == '\r')
                {
                    bool enter = false;
                    if (çıktı[i] == '\r')
                    {
                        enter = true;
                        if (i + 1 < çıktı.Length) if (çıktı[i + 1] == '\n') i++;
                    }
                    if (İmlaKuralları.NoktaVirgülVeAltSatıraGeç_ArdındanBoşlukEkle)
                    {
                        int i2 = i + 1;
                        while (i2 < çıktı.Length)
                        {
                            if (çıktı[i2] == ' ') çıktı = çıktı.Remove(i2, 1);
                            else break;
                        }
                        if (enter && ÇokSatırlı) { Thread.Sleep(0); }
                        else { çıktı = çıktı.Insert(i2, " "); }
                    }

                    if (İmlaKuralları.ilkHarfleriBüyüt && çıktı[i] != ',' && i + 2 < çıktı.Length)
                    {
                        int aaa = i + 2;
                        if (enter && ÇokSatırlı) aaa--;

                        string büyük = çıktı[aaa].ToString().ToUpper();
                        çıktı = çıktı.Remove(aaa, 1);
                        çıktı = çıktı.Insert(aaa, büyük);
                    }
                }
                else if (İmlaKuralları.FazlaBoşluklarıSil && çıktı[i] == ' ' && i + 1 < çıktı.Length) while (çıktı[i + 1] == ' ') çıktı = çıktı.Remove(i + 1, 1);
            }

            if (İmlaKuralları.CümleSonlarınaNoktaEkle && !çıktı.EndsWith(".")) çıktı += ".";
            return çıktı;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                DosyayaKaydet();

                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                    Durdur();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                //disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~KelimeTamamlayici_()
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

