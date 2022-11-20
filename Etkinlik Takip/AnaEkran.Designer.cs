namespace Etkinlik_Takip
{
    partial class AnaEkran
    {
        class ÇiftTamponluTreeView : System.Windows.Forms.TreeView
        {
            private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
            private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
            private const int TVS_EX_DOUBLEBUFFER = 0x0004;
            protected override void OnHandleCreated(System.EventArgs e)
            {
                ArgeMup.HazirKod.W32_9.SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (System.IntPtr)TVS_EX_DOUBLEBUFFER, (System.IntPtr)TVS_EX_DOUBLEBUFFER);
                base.OnHandleCreated(e);
            }
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaEkran));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Ağaç = new Etkinlik_Takip.AnaEkran.ÇiftTamponluTreeView();
            this.Menu_Ağaç = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_Ağaç_Görev = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_Etkinlik = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_Tanım = new System.Windows.Forms.ToolStripTextBox();
            this.MenuItem_Ağaç_Açıklama = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Ağaç_Yeni = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_ÜzerindeÇalışılıyor = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_DüşükÖncelikli = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_Beklemede = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_BittiGeriBildirimBekleniyor = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_Diğer = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_Tamamlandı = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_İptalEdildi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Ağaç_Hatırlatıcı = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_Hatırlatıcı_Kur = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_Hatırlatıcı_İptalEt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_TümEtkinliklerinListele = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_TümünüAç = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_TümünüDaralt = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Ağaç_PanoyaKopyala = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_PanodanAl = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_GeriAl = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Ağaç_Sil = new System.Windows.Forms.ToolStripMenuItem();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel_Görev = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel_Görev__ = new System.Windows.Forms.Panel();
            this.Grid_Listele_Tarihçe = new System.Windows.Forms.CheckBox();
            this.label_Görev_Tarih = new System.Windows.Forms.LinkLabel();
            this.textBox_Görev_Açıklama = new System.Windows.Forms.TextBox();
            this.textBox_Görev_Tanım = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Grid_Etkinlikler = new System.Windows.Forms.DataGridView();
            this.Sutun_Gorev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sutun_An = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sutun_Tarih = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sutun_Durum_G = new System.Windows.Forms.DataGridViewImageColumn();
            this.Sutun_Durum_M = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sutun_Açıklama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Menu_Grid_Etkinlikler = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_Grid_Etk_Sütünlar = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_Sütünlar_An = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_Sütünlar_Tarih = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_Sütünlar_Durum = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_Sütünlar_İçerik = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_Sütünlar_Açıklama = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_Sıralama = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_Sıralama_YeniUstte = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_Sıralama_EskiUstte = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet = new System.Windows.Forms.ToolStripTextBox();
            this.MenuItem_Grid_Etk_ZaAr = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_ZaAr_Bugün = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_ZaAr_Dün = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_ZaAr_BuHafta = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_ZaAr_Son15Gün = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_ZaAr_BuAy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_ZaAr_Son3Ay = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_ZaAr_Son6Ay = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_ZaAr_BuYıl = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_ZaAr_GeçenYıl = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_ZaAr_TümZamanlar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Grid_Etk_ZaAr_Baslangıç = new System.Windows.Forms.ToolStripTextBox();
            this.MenuItem_Grid_Etk_ZaAr_Bitiş = new System.Windows.Forms.ToolStripTextBox();
            this.MenuItem_Grid_Etk_ZaAr_Filtrele = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_AçıklamaTekSatırda = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel_Arama = new System.Windows.Forms.Panel();
            this.Arama = new System.Windows.Forms.DataGridView();
            this.Arama_Bulunan_adı = new System.Windows.Forms.DataGridViewLinkColumn();
            this.textBox_Arama = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel_Ayarlar = new System.Windows.Forms.Panel();
            this.OdaklanmışGörünüm_DiğerleriniDaralt = new System.Windows.Forms.CheckBox();
            this.KullanıcıAdı = new System.Windows.Forms.TextBox();
            this.Üç_Değiştir = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FiltrelemeTumu = new System.Windows.Forms.CheckBox();
            this.Filtreleme_D6 = new System.Windows.Forms.CheckBox();
            this.Filtreleme_D8 = new System.Windows.Forms.CheckBox();
            this.Filtreleme_D7 = new System.Windows.Forms.CheckBox();
            this.Filtreleme_D3 = new System.Windows.Forms.CheckBox();
            this.Filtreleme_D4 = new System.Windows.Forms.CheckBox();
            this.Filtreleme_D5 = new System.Windows.Forms.CheckBox();
            this.Filtreleme_D2 = new System.Windows.Forms.CheckBox();
            this.Filtreleme_D1 = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel_Hatırlatıcı = new System.Windows.Forms.Panel();
            this.Hatırlatıcı_Hatırlat_Yarın_dd = new System.Windows.Forms.NumericUpDown();
            this.Hatırlatıcı_Hatırlat_Bugün_dd = new System.Windows.Forms.NumericUpDown();
            this.Hatırlatıcı_Hatırlat_Yarın_cc = new System.Windows.Forms.NumericUpDown();
            this.Hatırlatıcı_Hatırlat_Bugün_cc = new System.Windows.Forms.NumericUpDown();
            this.Hatırlatıcı_Hatırlat_Yarın_bb = new System.Windows.Forms.NumericUpDown();
            this.Hatırlatıcı_Hatırlat_Bugün_bb = new System.Windows.Forms.NumericUpDown();
            this.Hatırlatıcı_Hatırlat_Yarın_aa = new System.Windows.Forms.NumericUpDown();
            this.Hatırlatıcı_Hatırlat_Bugün_aa = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.Hatırlatıcı_Tekrarla_Onay = new System.Windows.Forms.CheckBox();
            this.Hatırlatıcı_Hatırlat_Diğer_Onay = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Tekrarla_dönem = new System.Windows.Forms.ComboBox();
            this.Hatırlatıcı_Hatırlat_Diğer_Yazı = new System.Windows.Forms.TextBox();
            this.Hatırlatıcı_Tekrarla_adet = new System.Windows.Forms.NumericUpDown();
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Yarın_d = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Yarın_c = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Yarın_b = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Diğer = new System.Windows.Forms.Label();
            this.Hatırlatıcı_Hatırlat_Haftaya_Per = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Yarın_a = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Hatırlatıcı_Hatırlat_Bugün_d = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Bugün_c = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Bugün_b = new System.Windows.Forms.RadioButton();
            this.Hatırlatıcı_Hatırlat_Bugün_a = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripEkle = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripArama = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripİlerleme = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripEtiket = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripAyarlar = new System.Windows.Forms.ToolStripDropDownButton();
            this.BaloncukluUyari = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.Menu_İkon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menu_İkon_Çıkış = new System.Windows.Forms.ToolStripMenuItem();
            this.Sql_Zamanlayıcı = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OdaklanmışGörünüm_Kızart = new System.Windows.Forms.CheckBox();
            this.OdaklanmışGörünüm_Genişlet = new System.Windows.Forms.CheckBox();
            this.Menu_Ağaç.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel_Görev.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel_Görev__.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Etkinlikler)).BeginInit();
            this.Menu_Grid_Etkinlikler.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel_Arama.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Arama)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.panel_Ayarlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel_Hatırlatıcı.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Yarın_dd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Bugün_dd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Yarın_cc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Bugün_cc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Yarın_bb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Bugün_bb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Yarın_aa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Bugün_aa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Tekrarla_adet)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.Menu_İkon.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ağaç
            // 
            this.Ağaç.AllowDrop = true;
            this.Ağaç.ContextMenuStrip = this.Menu_Ağaç;
            this.Ağaç.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Ağaç.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ağaç.ImageIndex = 0;
            this.Ağaç.ImageList = this.ımageList1;
            this.Ağaç.Location = new System.Drawing.Point(0, 0);
            this.Ağaç.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Ağaç.Name = "Ağaç";
            this.Ağaç.SelectedImageIndex = 0;
            this.Ağaç.Size = new System.Drawing.Size(84, 428);
            this.Ağaç.TabIndex = 1;
            this.Ağaç.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.Ağaç_AfterCollapse);
            this.Ağaç.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.Ağaç_AfterExpand);
            this.Ağaç.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.Ağaç_ItemDrag);
            this.Ağaç.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Ağaç_AfterSelect);
            this.Ağaç.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Ağaç_NodeMouseClick);
            this.Ağaç.DragDrop += new System.Windows.Forms.DragEventHandler(this.Ağaç_DragDrop);
            this.Ağaç.DragEnter += new System.Windows.Forms.DragEventHandler(this.Ağaç_DragEnter);
            this.Ağaç.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Ağaç_MouseMove);
            // 
            // Menu_Ağaç
            // 
            this.Menu_Ağaç.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu_Ağaç.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Ağaç_Görev,
            this.MenuItem_Ağaç_Etkinlik,
            this.MenuItem_Ağaç_Tanım,
            this.MenuItem_Ağaç_Açıklama,
            this.toolStripSeparator1,
            this.MenuItem_Ağaç_Yeni,
            this.MenuItem_Ağaç_ÜzerindeÇalışılıyor,
            this.MenuItem_Ağaç_DüşükÖncelikli,
            this.MenuItem_Ağaç_Beklemede,
            this.MenuItem_Ağaç_BittiGeriBildirimBekleniyor,
            this.MenuItem_Ağaç_Diğer,
            this.MenuItem_Ağaç_Tamamlandı,
            this.MenuItem_Ağaç_İptalEdildi,
            this.toolStripSeparator2,
            this.MenuItem_Ağaç_Hatırlatıcı,
            this.MenuItem_Ağaç_TümEtkinliklerinListele,
            this.MenuItem_Ağaç_TümünüAç,
            this.MenuItem_Ağaç_TümünüDaralt,
            this.toolStripSeparator3,
            this.MenuItem_Ağaç_PanoyaKopyala,
            this.MenuItem_Ağaç_PanodanAl,
            this.MenuItem_Ağaç_GeriAl,
            this.MenuItem_Ağaç_Sil});
            this.Menu_Ağaç.Name = "Menu_Ağaç";
            this.Menu_Ağaç.Size = new System.Drawing.Size(285, 548);
            this.Menu_Ağaç.Opening += new System.ComponentModel.CancelEventHandler(this.Menu_Ağaç_Opening);
            this.Menu_Ağaç.Opened += new System.EventHandler(this.Menu_Ağaç_Opened);
            // 
            // MenuItem_Ağaç_Görev
            // 
            this.MenuItem_Ağaç_Görev.CheckOnClick = true;
            this.MenuItem_Ağaç_Görev.Name = "MenuItem_Ağaç_Görev";
            this.MenuItem_Ağaç_Görev.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_Görev.Text = "Yeni görev";
            this.MenuItem_Ağaç_Görev.CheckedChanged += new System.EventHandler(this.MenuItem_Ağaç_Görev_CheckedChanged);
            // 
            // MenuItem_Ağaç_Etkinlik
            // 
            this.MenuItem_Ağaç_Etkinlik.CheckOnClick = true;
            this.MenuItem_Ağaç_Etkinlik.Name = "MenuItem_Ağaç_Etkinlik";
            this.MenuItem_Ağaç_Etkinlik.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_Etkinlik.Text = "Yeni etkinlik";
            this.MenuItem_Ağaç_Etkinlik.CheckedChanged += new System.EventHandler(this.MenuItem_Ağaç_Etkinlik_CheckedChanged);
            // 
            // MenuItem_Ağaç_Tanım
            // 
            this.MenuItem_Ağaç_Tanım.AutoSize = false;
            this.MenuItem_Ağaç_Tanım.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.MenuItem_Ağaç_Tanım.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MenuItem_Ağaç_Tanım.Name = "MenuItem_Ağaç_Tanım";
            this.MenuItem_Ağaç_Tanım.Size = new System.Drawing.Size(100, 27);
            this.MenuItem_Ağaç_Tanım.ToolTipText = "Yeni görevin tanımı *Mecburi.";
            this.MenuItem_Ağaç_Tanım.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MenuItem_Ağaç_Tanım_KeyPress);
            // 
            // MenuItem_Ağaç_Açıklama
            // 
            this.MenuItem_Ağaç_Açıklama.AutoSize = false;
            this.MenuItem_Ağaç_Açıklama.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.MenuItem_Ağaç_Açıklama.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MenuItem_Ağaç_Açıklama.Name = "MenuItem_Ağaç_Açıklama";
            this.MenuItem_Ağaç_Açıklama.Size = new System.Drawing.Size(100, 27);
            this.MenuItem_Ağaç_Açıklama.ToolTipText = "Yeni oluşumun açıklaması *İsteğe bağlı.";
            this.MenuItem_Ağaç_Açıklama.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MenuItem_Ağaç_Tanım_KeyPress);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(281, 6);
            // 
            // MenuItem_Ağaç_Yeni
            // 
            this.MenuItem_Ağaç_Yeni.Image = ((System.Drawing.Image)(resources.GetObject("MenuItem_Ağaç_Yeni.Image")));
            this.MenuItem_Ağaç_Yeni.Name = "MenuItem_Ağaç_Yeni";
            this.MenuItem_Ağaç_Yeni.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_Yeni.Text = "Yeni";
            this.MenuItem_Ağaç_Yeni.Click += new System.EventHandler(this.MenuItem_Ağaç_Yeni_Click);
            // 
            // MenuItem_Ağaç_ÜzerindeÇalışılıyor
            // 
            this.MenuItem_Ağaç_ÜzerindeÇalışılıyor.Image = global::Etkinlik_Takip.Properties.Resources.G_Durum_1;
            this.MenuItem_Ağaç_ÜzerindeÇalışılıyor.Name = "MenuItem_Ağaç_ÜzerindeÇalışılıyor";
            this.MenuItem_Ağaç_ÜzerindeÇalışılıyor.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_ÜzerindeÇalışılıyor.Text = "Üzerinde çalışılıyor";
            this.MenuItem_Ağaç_ÜzerindeÇalışılıyor.Click += new System.EventHandler(this.MenuItem_Ağaç_ÜzerindeÇalışılıyor_Click);
            // 
            // MenuItem_Ağaç_DüşükÖncelikli
            // 
            this.MenuItem_Ağaç_DüşükÖncelikli.Image = global::Etkinlik_Takip.Properties.Resources.G_Durum_2;
            this.MenuItem_Ağaç_DüşükÖncelikli.Name = "MenuItem_Ağaç_DüşükÖncelikli";
            this.MenuItem_Ağaç_DüşükÖncelikli.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_DüşükÖncelikli.Text = "Düşük öncelikli";
            this.MenuItem_Ağaç_DüşükÖncelikli.Click += new System.EventHandler(this.MenuItem_Ağaç_DüşükÖncelikli_Click);
            // 
            // MenuItem_Ağaç_Beklemede
            // 
            this.MenuItem_Ağaç_Beklemede.Image = global::Etkinlik_Takip.Properties.Resources.G_Durum_4;
            this.MenuItem_Ağaç_Beklemede.Name = "MenuItem_Ağaç_Beklemede";
            this.MenuItem_Ağaç_Beklemede.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_Beklemede.Text = "Beklemede";
            this.MenuItem_Ağaç_Beklemede.Click += new System.EventHandler(this.MenuItem_Ağaç_Beklemede_Click);
            // 
            // MenuItem_Ağaç_BittiGeriBildirimBekleniyor
            // 
            this.MenuItem_Ağaç_BittiGeriBildirimBekleniyor.Image = global::Etkinlik_Takip.Properties.Resources.G_Durum_5;
            this.MenuItem_Ağaç_BittiGeriBildirimBekleniyor.Name = "MenuItem_Ağaç_BittiGeriBildirimBekleniyor";
            this.MenuItem_Ağaç_BittiGeriBildirimBekleniyor.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_BittiGeriBildirimBekleniyor.Text = "Bitti, geri bildirim bekleniyor";
            this.MenuItem_Ağaç_BittiGeriBildirimBekleniyor.Click += new System.EventHandler(this.MenuItem_Ağaç_BittiGeriBildirimBekleniyor_Click);
            // 
            // MenuItem_Ağaç_Diğer
            // 
            this.MenuItem_Ağaç_Diğer.Image = global::Etkinlik_Takip.Properties.Resources.G_Durum_6;
            this.MenuItem_Ağaç_Diğer.Name = "MenuItem_Ağaç_Diğer";
            this.MenuItem_Ağaç_Diğer.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_Diğer.Text = "Diğer";
            this.MenuItem_Ağaç_Diğer.Click += new System.EventHandler(this.MenuItem_Ağaç_Diğer_Click);
            // 
            // MenuItem_Ağaç_Tamamlandı
            // 
            this.MenuItem_Ağaç_Tamamlandı.Image = global::Etkinlik_Takip.Properties.Resources.G_Durum_7;
            this.MenuItem_Ağaç_Tamamlandı.Name = "MenuItem_Ağaç_Tamamlandı";
            this.MenuItem_Ağaç_Tamamlandı.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_Tamamlandı.Text = "Tamamlandı";
            this.MenuItem_Ağaç_Tamamlandı.Click += new System.EventHandler(this.MenuItem_Ağaç_Tamamlandı_Click);
            // 
            // MenuItem_Ağaç_İptalEdildi
            // 
            this.MenuItem_Ağaç_İptalEdildi.Image = global::Etkinlik_Takip.Properties.Resources.G_Durum_8;
            this.MenuItem_Ağaç_İptalEdildi.Name = "MenuItem_Ağaç_İptalEdildi";
            this.MenuItem_Ağaç_İptalEdildi.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_İptalEdildi.Text = "İptal edildi";
            this.MenuItem_Ağaç_İptalEdildi.Click += new System.EventHandler(this.MenuItem_Ağaç_İptalEdildi_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(281, 6);
            // 
            // MenuItem_Ağaç_Hatırlatıcı
            // 
            this.MenuItem_Ağaç_Hatırlatıcı.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Ağaç_Hatırlatıcı_Kur,
            this.MenuItem_Ağaç_Hatırlatıcı_İptalEt});
            this.MenuItem_Ağaç_Hatırlatıcı.Name = "MenuItem_Ağaç_Hatırlatıcı";
            this.MenuItem_Ağaç_Hatırlatıcı.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_Hatırlatıcı.Text = " ... Hatırlatıcı ... Kur ... İptal et ... ";
            this.MenuItem_Ağaç_Hatırlatıcı.Click += new System.EventHandler(this.MenuItem_Ağaç_Hatırlatıcı_Kur_İptalEt_Click);
            // 
            // MenuItem_Ağaç_Hatırlatıcı_Kur
            // 
            this.MenuItem_Ağaç_Hatırlatıcı_Kur.Name = "MenuItem_Ağaç_Hatırlatıcı_Kur";
            this.MenuItem_Ağaç_Hatırlatıcı_Kur.Size = new System.Drawing.Size(234, 26);
            this.MenuItem_Ağaç_Hatırlatıcı_Kur.Text = "Hatırlatıcıyı tekrar kur";
            this.MenuItem_Ağaç_Hatırlatıcı_Kur.Click += new System.EventHandler(this.MenuItem_Ağaç_Hatırlatıcı_Kur_İptalEt_Click);
            // 
            // MenuItem_Ağaç_Hatırlatıcı_İptalEt
            // 
            this.MenuItem_Ağaç_Hatırlatıcı_İptalEt.Name = "MenuItem_Ağaç_Hatırlatıcı_İptalEt";
            this.MenuItem_Ağaç_Hatırlatıcı_İptalEt.Size = new System.Drawing.Size(234, 26);
            this.MenuItem_Ağaç_Hatırlatıcı_İptalEt.Text = "Hatırlatıcıyı İptal et";
            this.MenuItem_Ağaç_Hatırlatıcı_İptalEt.Click += new System.EventHandler(this.MenuItem_Ağaç_Hatırlatıcı_Kur_İptalEt_Click);
            // 
            // MenuItem_Ağaç_TümEtkinliklerinListele
            // 
            this.MenuItem_Ağaç_TümEtkinliklerinListele.Name = "MenuItem_Ağaç_TümEtkinliklerinListele";
            this.MenuItem_Ağaç_TümEtkinliklerinListele.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_TümEtkinliklerinListele.Text = "Tüm etkinliklerini listele";
            this.MenuItem_Ağaç_TümEtkinliklerinListele.Click += new System.EventHandler(this.MenuItem_Ağaç_TümEtkinliklerinListele_Click);
            // 
            // MenuItem_Ağaç_TümünüAç
            // 
            this.MenuItem_Ağaç_TümünüAç.Name = "MenuItem_Ağaç_TümünüAç";
            this.MenuItem_Ağaç_TümünüAç.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_TümünüAç.Text = "Genişlet";
            this.MenuItem_Ağaç_TümünüAç.Click += new System.EventHandler(this.MenuItem_Ağaç_TümünüAç_Click);
            // 
            // MenuItem_Ağaç_TümünüDaralt
            // 
            this.MenuItem_Ağaç_TümünüDaralt.Name = "MenuItem_Ağaç_TümünüDaralt";
            this.MenuItem_Ağaç_TümünüDaralt.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_TümünüDaralt.Text = "Daralt";
            this.MenuItem_Ağaç_TümünüDaralt.Click += new System.EventHandler(this.MenuItem_Ağaç_TümünüDaralt_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(281, 6);
            // 
            // MenuItem_Ağaç_PanoyaKopyala
            // 
            this.MenuItem_Ağaç_PanoyaKopyala.Name = "MenuItem_Ağaç_PanoyaKopyala";
            this.MenuItem_Ağaç_PanoyaKopyala.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_PanoyaKopyala.Text = "Panoya Kopyala";
            this.MenuItem_Ağaç_PanoyaKopyala.Click += new System.EventHandler(this.MenuItem_Ağaç_PanoyaKopyala_Click);
            // 
            // MenuItem_Ağaç_PanodanAl
            // 
            this.MenuItem_Ağaç_PanodanAl.Name = "MenuItem_Ağaç_PanodanAl";
            this.MenuItem_Ağaç_PanodanAl.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_PanodanAl.Text = "Panodan Al";
            this.MenuItem_Ağaç_PanodanAl.Click += new System.EventHandler(this.MenuItem_Ağaç_PanodanAl_Click);
            // 
            // MenuItem_Ağaç_GeriAl
            // 
            this.MenuItem_Ağaç_GeriAl.Image = global::Etkinlik_Takip.Properties.Resources.Onay;
            this.MenuItem_Ağaç_GeriAl.Name = "MenuItem_Ağaç_GeriAl";
            this.MenuItem_Ağaç_GeriAl.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_GeriAl.Text = "Geri Al";
            this.MenuItem_Ağaç_GeriAl.Click += new System.EventHandler(this.MenuItem_Ağaç_GeriAl_Click);
            // 
            // MenuItem_Ağaç_Sil
            // 
            this.MenuItem_Ağaç_Sil.Image = global::Etkinlik_Takip.Properties.Resources.Ret;
            this.MenuItem_Ağaç_Sil.Name = "MenuItem_Ağaç_Sil";
            this.MenuItem_Ağaç_Sil.Size = new System.Drawing.Size(284, 26);
            this.MenuItem_Ağaç_Sil.Text = "Sil";
            this.MenuItem_Ağaç_Sil.Click += new System.EventHandler(this.MenuItem_Ağaç_Sil_Click);
            // 
            // ımageList1
            // 
            this.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ımageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Ağaç);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(659, 432);
            this.splitContainer1.SplitterDistance = 88;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(562, 428);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel_Görev);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(554, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Görev";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel_Görev
            // 
            this.panel_Görev.AutoScroll = true;
            this.panel_Görev.Controls.Add(this.splitContainer2);
            this.panel_Görev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Görev.Location = new System.Drawing.Point(3, 2);
            this.panel_Görev.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Görev.Name = "panel_Görev";
            this.panel_Görev.Size = new System.Drawing.Size(548, 395);
            this.panel_Görev.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel_Görev__);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Grid_Etkinlikler);
            this.splitContainer2.Size = new System.Drawing.Size(548, 395);
            this.splitContainer2.SplitterDistance = 269;
            this.splitContainer2.TabIndex = 3;
            // 
            // panel_Görev__
            // 
            this.panel_Görev__.AutoScroll = true;
            this.panel_Görev__.Controls.Add(this.Grid_Listele_Tarihçe);
            this.panel_Görev__.Controls.Add(this.label_Görev_Tarih);
            this.panel_Görev__.Controls.Add(this.textBox_Görev_Açıklama);
            this.panel_Görev__.Controls.Add(this.textBox_Görev_Tanım);
            this.panel_Görev__.Controls.Add(this.label3);
            this.panel_Görev__.Controls.Add(this.label2);
            this.panel_Görev__.Controls.Add(this.label1);
            this.panel_Görev__.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Görev__.Location = new System.Drawing.Point(0, 0);
            this.panel_Görev__.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_Görev__.Name = "panel_Görev__";
            this.panel_Görev__.Size = new System.Drawing.Size(544, 265);
            this.panel_Görev__.TabIndex = 2;
            // 
            // Grid_Listele_Tarihçe
            // 
            this.Grid_Listele_Tarihçe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Grid_Listele_Tarihçe.Appearance = System.Windows.Forms.Appearance.Button;
            this.Grid_Listele_Tarihçe.AutoSize = true;
            this.Grid_Listele_Tarihçe.Checked = true;
            this.Grid_Listele_Tarihçe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Grid_Listele_Tarihçe.Location = new System.Drawing.Point(1, 237);
            this.Grid_Listele_Tarihçe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Grid_Listele_Tarihçe.Name = "Grid_Listele_Tarihçe";
            this.Grid_Listele_Tarihçe.Size = new System.Drawing.Size(66, 27);
            this.Grid_Listele_Tarihçe.TabIndex = 13;
            this.Grid_Listele_Tarihçe.Text = "Tarihçe";
            this.BaloncukluUyari.SetToolTip(this.Grid_Listele_Tarihçe, "Listele");
            this.Grid_Listele_Tarihçe.UseVisualStyleBackColor = true;
            this.Grid_Listele_Tarihçe.CheckedChanged += new System.EventHandler(this.Grid_Listele_Tarihçe_CheckedChanged);
            // 
            // label_Görev_Tarih
            // 
            this.label_Görev_Tarih.AutoSize = true;
            this.label_Görev_Tarih.Location = new System.Drawing.Point(93, 11);
            this.label_Görev_Tarih.Name = "label_Görev_Tarih";
            this.label_Görev_Tarih.Size = new System.Drawing.Size(36, 17);
            this.label_Görev_Tarih.TabIndex = 13;
            this.label_Görev_Tarih.TabStop = true;
            this.label_Görev_Tarih.Text = ".......";
            this.label_Görev_Tarih.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label_Görev_Tarih_LinkClicked);
            // 
            // textBox_Görev_Açıklama
            // 
            this.textBox_Görev_Açıklama.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Görev_Açıklama.Location = new System.Drawing.Point(96, 68);
            this.textBox_Görev_Açıklama.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Görev_Açıklama.Multiline = true;
            this.textBox_Görev_Açıklama.Name = "textBox_Görev_Açıklama";
            this.textBox_Görev_Açıklama.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Görev_Açıklama.Size = new System.Drawing.Size(444, 192);
            this.textBox_Görev_Açıklama.TabIndex = 11;
            this.textBox_Görev_Açıklama.TextChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // textBox_Görev_Tanım
            // 
            this.textBox_Görev_Tanım.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Görev_Tanım.Location = new System.Drawing.Point(96, 37);
            this.textBox_Görev_Tanım.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Görev_Tanım.Name = "textBox_Görev_Tanım";
            this.textBox_Görev_Tanım.Size = new System.Drawing.Size(444, 23);
            this.textBox_Görev_Tanım.TabIndex = 10;
            this.textBox_Görev_Tanım.TextChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Açıklama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tanım";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Oluşturulma";
            // 
            // Grid_Etkinlikler
            // 
            this.Grid_Etkinlikler.AllowUserToAddRows = false;
            this.Grid_Etkinlikler.AllowUserToDeleteRows = false;
            this.Grid_Etkinlikler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Grid_Etkinlikler.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_Etkinlikler.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Grid_Etkinlikler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid_Etkinlikler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sutun_Gorev,
            this.Sutun_An,
            this.Sutun_Tarih,
            this.Sutun_Durum_G,
            this.Sutun_Durum_M,
            this.Sutun_Açıklama});
            this.Grid_Etkinlikler.ContextMenuStrip = this.Menu_Grid_Etkinlikler;
            this.Grid_Etkinlikler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_Etkinlikler.Location = new System.Drawing.Point(0, 0);
            this.Grid_Etkinlikler.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Grid_Etkinlikler.Name = "Grid_Etkinlikler";
            this.Grid_Etkinlikler.ReadOnly = true;
            this.Grid_Etkinlikler.RowHeadersWidth = 62;
            this.Grid_Etkinlikler.RowTemplate.Height = 24;
            this.Grid_Etkinlikler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Grid_Etkinlikler.Size = new System.Drawing.Size(544, 118);
            this.Grid_Etkinlikler.TabIndex = 12;
            // 
            // Sutun_Gorev
            // 
            this.Sutun_Gorev.HeaderText = "Görev";
            this.Sutun_Gorev.MinimumWidth = 8;
            this.Sutun_Gorev.Name = "Sutun_Gorev";
            this.Sutun_Gorev.ReadOnly = true;
            this.Sutun_Gorev.Visible = false;
            this.Sutun_Gorev.Width = 61;
            // 
            // Sutun_An
            // 
            this.Sutun_An.HeaderText = "An";
            this.Sutun_An.MinimumWidth = 8;
            this.Sutun_An.Name = "Sutun_An";
            this.Sutun_An.ReadOnly = true;
            this.Sutun_An.Width = 54;
            // 
            // Sutun_Tarih
            // 
            this.Sutun_Tarih.HeaderText = "Tarih";
            this.Sutun_Tarih.MinimumWidth = 8;
            this.Sutun_Tarih.Name = "Sutun_Tarih";
            this.Sutun_Tarih.ReadOnly = true;
            this.Sutun_Tarih.Width = 70;
            // 
            // Sutun_Durum_G
            // 
            this.Sutun_Durum_G.HeaderText = "    ";
            this.Sutun_Durum_G.MinimumWidth = 8;
            this.Sutun_Durum_G.Name = "Sutun_Durum_G";
            this.Sutun_Durum_G.ReadOnly = true;
            this.Sutun_Durum_G.Width = 27;
            // 
            // Sutun_Durum_M
            // 
            this.Sutun_Durum_M.HeaderText = "Durum";
            this.Sutun_Durum_M.MinimumWidth = 8;
            this.Sutun_Durum_M.Name = "Sutun_Durum_M";
            this.Sutun_Durum_M.ReadOnly = true;
            this.Sutun_Durum_M.Width = 79;
            // 
            // Sutun_Açıklama
            // 
            this.Sutun_Açıklama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Sutun_Açıklama.DefaultCellStyle = dataGridViewCellStyle4;
            this.Sutun_Açıklama.HeaderText = "Açıklama";
            this.Sutun_Açıklama.MinimumWidth = 8;
            this.Sutun_Açıklama.Name = "Sutun_Açıklama";
            this.Sutun_Açıklama.ReadOnly = true;
            this.Sutun_Açıklama.Width = 93;
            // 
            // Menu_Grid_Etkinlikler
            // 
            this.Menu_Grid_Etkinlikler.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu_Grid_Etkinlikler.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Grid_Etk_Sütünlar,
            this.MenuItem_Grid_Etk_Sıralama,
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı,
            this.MenuItem_Grid_Etk_ZaAr,
            this.MenuItem_Grid_Etk_AçıklamaTekSatırda,
            this.MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan});
            this.Menu_Grid_Etkinlikler.Name = "Menu_Grid_Etkinlikler";
            this.Menu_Grid_Etkinlikler.ShowCheckMargin = true;
            this.Menu_Grid_Etkinlikler.ShowImageMargin = false;
            this.Menu_Grid_Etkinlikler.Size = new System.Drawing.Size(332, 160);
            // 
            // MenuItem_Grid_Etk_Sütünlar
            // 
            this.MenuItem_Grid_Etk_Sütünlar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Grid_Etk_Sütünlar_An,
            this.MenuItem_Grid_Etk_Sütünlar_Tarih,
            this.MenuItem_Grid_Etk_Sütünlar_Durum,
            this.MenuItem_Grid_Etk_Sütünlar_İçerik,
            this.MenuItem_Grid_Etk_Sütünlar_Açıklama});
            this.MenuItem_Grid_Etk_Sütünlar.Name = "MenuItem_Grid_Etk_Sütünlar";
            this.MenuItem_Grid_Etk_Sütünlar.Size = new System.Drawing.Size(331, 26);
            this.MenuItem_Grid_Etk_Sütünlar.Text = "Sütünlar";
            // 
            // MenuItem_Grid_Etk_Sütünlar_An
            // 
            this.MenuItem_Grid_Etk_Sütünlar_An.Checked = true;
            this.MenuItem_Grid_Etk_Sütünlar_An.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItem_Grid_Etk_Sütünlar_An.Name = "MenuItem_Grid_Etk_Sütünlar_An";
            this.MenuItem_Grid_Etk_Sütünlar_An.Size = new System.Drawing.Size(153, 26);
            this.MenuItem_Grid_Etk_Sütünlar_An.Text = "An";
            this.MenuItem_Grid_Etk_Sütünlar_An.Click += new System.EventHandler(this.MenuItem_Grid_Etk_Sütünlar_An_Click);
            // 
            // MenuItem_Grid_Etk_Sütünlar_Tarih
            // 
            this.MenuItem_Grid_Etk_Sütünlar_Tarih.Checked = true;
            this.MenuItem_Grid_Etk_Sütünlar_Tarih.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItem_Grid_Etk_Sütünlar_Tarih.Name = "MenuItem_Grid_Etk_Sütünlar_Tarih";
            this.MenuItem_Grid_Etk_Sütünlar_Tarih.Size = new System.Drawing.Size(153, 26);
            this.MenuItem_Grid_Etk_Sütünlar_Tarih.Text = "Tarih";
            this.MenuItem_Grid_Etk_Sütünlar_Tarih.Click += new System.EventHandler(this.MenuItem_Grid_Etk_Sütünlar_Tarih_Click);
            // 
            // MenuItem_Grid_Etk_Sütünlar_Durum
            // 
            this.MenuItem_Grid_Etk_Sütünlar_Durum.Checked = true;
            this.MenuItem_Grid_Etk_Sütünlar_Durum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItem_Grid_Etk_Sütünlar_Durum.Name = "MenuItem_Grid_Etk_Sütünlar_Durum";
            this.MenuItem_Grid_Etk_Sütünlar_Durum.Size = new System.Drawing.Size(153, 26);
            this.MenuItem_Grid_Etk_Sütünlar_Durum.Text = "Resim";
            this.MenuItem_Grid_Etk_Sütünlar_Durum.Click += new System.EventHandler(this.MenuItem_Grid_Etk_Sütünlar_Durum_Click);
            // 
            // MenuItem_Grid_Etk_Sütünlar_İçerik
            // 
            this.MenuItem_Grid_Etk_Sütünlar_İçerik.Checked = true;
            this.MenuItem_Grid_Etk_Sütünlar_İçerik.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItem_Grid_Etk_Sütünlar_İçerik.Name = "MenuItem_Grid_Etk_Sütünlar_İçerik";
            this.MenuItem_Grid_Etk_Sütünlar_İçerik.Size = new System.Drawing.Size(153, 26);
            this.MenuItem_Grid_Etk_Sütünlar_İçerik.Text = "Durum";
            this.MenuItem_Grid_Etk_Sütünlar_İçerik.Click += new System.EventHandler(this.MenuItem_Grid_Etk_Sütünlar_İçerik_Click);
            // 
            // MenuItem_Grid_Etk_Sütünlar_Açıklama
            // 
            this.MenuItem_Grid_Etk_Sütünlar_Açıklama.Checked = true;
            this.MenuItem_Grid_Etk_Sütünlar_Açıklama.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItem_Grid_Etk_Sütünlar_Açıklama.Name = "MenuItem_Grid_Etk_Sütünlar_Açıklama";
            this.MenuItem_Grid_Etk_Sütünlar_Açıklama.Size = new System.Drawing.Size(153, 26);
            this.MenuItem_Grid_Etk_Sütünlar_Açıklama.Text = "Açıklama";
            this.MenuItem_Grid_Etk_Sütünlar_Açıklama.Click += new System.EventHandler(this.MenuItem_Grid_Etk_Sütünlar_Açıklama_Click);
            // 
            // MenuItem_Grid_Etk_Sıralama
            // 
            this.MenuItem_Grid_Etk_Sıralama.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Grid_Etk_Sıralama_YeniUstte,
            this.MenuItem_Grid_Etk_Sıralama_EskiUstte});
            this.MenuItem_Grid_Etk_Sıralama.Name = "MenuItem_Grid_Etk_Sıralama";
            this.MenuItem_Grid_Etk_Sıralama.Size = new System.Drawing.Size(331, 26);
            this.MenuItem_Grid_Etk_Sıralama.Text = "Sıralama";
            // 
            // MenuItem_Grid_Etk_Sıralama_YeniUstte
            // 
            this.MenuItem_Grid_Etk_Sıralama_YeniUstte.Checked = true;
            this.MenuItem_Grid_Etk_Sıralama_YeniUstte.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItem_Grid_Etk_Sıralama_YeniUstte.Name = "MenuItem_Grid_Etk_Sıralama_YeniUstte";
            this.MenuItem_Grid_Etk_Sıralama_YeniUstte.Size = new System.Drawing.Size(195, 26);
            this.MenuItem_Grid_Etk_Sıralama_YeniUstte.Text = "En yeni en üstte";
            this.MenuItem_Grid_Etk_Sıralama_YeniUstte.Click += new System.EventHandler(this.MenuItem_Grid_Etk_Sıralama_YeniUstte_Click);
            // 
            // MenuItem_Grid_Etk_Sıralama_EskiUstte
            // 
            this.MenuItem_Grid_Etk_Sıralama_EskiUstte.Name = "MenuItem_Grid_Etk_Sıralama_EskiUstte";
            this.MenuItem_Grid_Etk_Sıralama_EskiUstte.Size = new System.Drawing.Size(195, 26);
            this.MenuItem_Grid_Etk_Sıralama_EskiUstte.Text = "En eski en üstte";
            this.MenuItem_Grid_Etk_Sıralama_EskiUstte.Click += new System.EventHandler(this.MenuItem_Grid_Etk_Sıralama_EskiUstte_Click);
            // 
            // MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı
            // 
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet});
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı.Name = "MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı";
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı.Size = new System.Drawing.Size(331, 26);
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı.Text = "Görüntülenecek etkinlik sayısı";
            // 
            // MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet
            // 
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.AutoSize = false;
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Name = "MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet";
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Size = new System.Drawing.Size(100, 27);
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.Text = "0";
            this.MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet.ToolTipText = "Rakamları kullanınız";
            // 
            // MenuItem_Grid_Etk_ZaAr
            // 
            this.MenuItem_Grid_Etk_ZaAr.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Grid_Etk_ZaAr_Bugün,
            this.MenuItem_Grid_Etk_ZaAr_Dün,
            this.MenuItem_Grid_Etk_ZaAr_BuHafta,
            this.MenuItem_Grid_Etk_ZaAr_Son15Gün,
            this.MenuItem_Grid_Etk_ZaAr_BuAy,
            this.MenuItem_Grid_Etk_ZaAr_Son3Ay,
            this.MenuItem_Grid_Etk_ZaAr_Son6Ay,
            this.MenuItem_Grid_Etk_ZaAr_BuYıl,
            this.MenuItem_Grid_Etk_ZaAr_GeçenYıl,
            this.MenuItem_Grid_Etk_ZaAr_TümZamanlar,
            this.toolStripSeparator4,
            this.MenuItem_Grid_Etk_ZaAr_Baslangıç,
            this.MenuItem_Grid_Etk_ZaAr_Bitiş,
            this.MenuItem_Grid_Etk_ZaAr_Filtrele});
            this.MenuItem_Grid_Etk_ZaAr.Name = "MenuItem_Grid_Etk_ZaAr";
            this.MenuItem_Grid_Etk_ZaAr.Size = new System.Drawing.Size(331, 26);
            this.MenuItem_Grid_Etk_ZaAr.Text = "Zaman Aralığı";
            // 
            // MenuItem_Grid_Etk_ZaAr_Bugün
            // 
            this.MenuItem_Grid_Etk_ZaAr_Bugün.Name = "MenuItem_Grid_Etk_ZaAr_Bugün";
            this.MenuItem_Grid_Etk_ZaAr_Bugün.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_Bugün.Text = "Bugün";
            this.MenuItem_Grid_Etk_ZaAr_Bugün.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_Bugün_Click);
            // 
            // MenuItem_Grid_Etk_ZaAr_Dün
            // 
            this.MenuItem_Grid_Etk_ZaAr_Dün.Name = "MenuItem_Grid_Etk_ZaAr_Dün";
            this.MenuItem_Grid_Etk_ZaAr_Dün.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_Dün.Text = "Dün";
            this.MenuItem_Grid_Etk_ZaAr_Dün.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_Dün_Click);
            // 
            // MenuItem_Grid_Etk_ZaAr_BuHafta
            // 
            this.MenuItem_Grid_Etk_ZaAr_BuHafta.Name = "MenuItem_Grid_Etk_ZaAr_BuHafta";
            this.MenuItem_Grid_Etk_ZaAr_BuHafta.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_BuHafta.Text = "Son 1 hafta";
            this.MenuItem_Grid_Etk_ZaAr_BuHafta.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_BuHafta_Click);
            // 
            // MenuItem_Grid_Etk_ZaAr_Son15Gün
            // 
            this.MenuItem_Grid_Etk_ZaAr_Son15Gün.Name = "MenuItem_Grid_Etk_ZaAr_Son15Gün";
            this.MenuItem_Grid_Etk_ZaAr_Son15Gün.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_Son15Gün.Text = "Son 2 hafta";
            this.MenuItem_Grid_Etk_ZaAr_Son15Gün.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_Son15Gün_Click);
            // 
            // MenuItem_Grid_Etk_ZaAr_BuAy
            // 
            this.MenuItem_Grid_Etk_ZaAr_BuAy.Name = "MenuItem_Grid_Etk_ZaAr_BuAy";
            this.MenuItem_Grid_Etk_ZaAr_BuAy.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_BuAy.Text = "Bu ay";
            this.MenuItem_Grid_Etk_ZaAr_BuAy.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_BuAy_Click);
            // 
            // MenuItem_Grid_Etk_ZaAr_Son3Ay
            // 
            this.MenuItem_Grid_Etk_ZaAr_Son3Ay.Name = "MenuItem_Grid_Etk_ZaAr_Son3Ay";
            this.MenuItem_Grid_Etk_ZaAr_Son3Ay.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_Son3Ay.Text = "Son 3 ay";
            this.MenuItem_Grid_Etk_ZaAr_Son3Ay.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_Son3Ay_Click);
            // 
            // MenuItem_Grid_Etk_ZaAr_Son6Ay
            // 
            this.MenuItem_Grid_Etk_ZaAr_Son6Ay.Name = "MenuItem_Grid_Etk_ZaAr_Son6Ay";
            this.MenuItem_Grid_Etk_ZaAr_Son6Ay.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_Son6Ay.Text = "Son 6 ay";
            this.MenuItem_Grid_Etk_ZaAr_Son6Ay.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_Son6Ay_Click);
            // 
            // MenuItem_Grid_Etk_ZaAr_BuYıl
            // 
            this.MenuItem_Grid_Etk_ZaAr_BuYıl.Name = "MenuItem_Grid_Etk_ZaAr_BuYıl";
            this.MenuItem_Grid_Etk_ZaAr_BuYıl.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_BuYıl.Text = "Bu yıl";
            this.MenuItem_Grid_Etk_ZaAr_BuYıl.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_BuYıl_Click);
            // 
            // MenuItem_Grid_Etk_ZaAr_GeçenYıl
            // 
            this.MenuItem_Grid_Etk_ZaAr_GeçenYıl.Name = "MenuItem_Grid_Etk_ZaAr_GeçenYıl";
            this.MenuItem_Grid_Etk_ZaAr_GeçenYıl.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_GeçenYıl.Text = "Geçen yıl";
            this.MenuItem_Grid_Etk_ZaAr_GeçenYıl.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_GeçenYıl_Click);
            // 
            // MenuItem_Grid_Etk_ZaAr_TümZamanlar
            // 
            this.MenuItem_Grid_Etk_ZaAr_TümZamanlar.Name = "MenuItem_Grid_Etk_ZaAr_TümZamanlar";
            this.MenuItem_Grid_Etk_ZaAr_TümZamanlar.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_TümZamanlar.Text = "Tüm zamanlar";
            this.MenuItem_Grid_Etk_ZaAr_TümZamanlar.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_TümZamanlar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(318, 6);
            // 
            // MenuItem_Grid_Etk_ZaAr_Baslangıç
            // 
            this.MenuItem_Grid_Etk_ZaAr_Baslangıç.AutoSize = false;
            this.MenuItem_Grid_Etk_ZaAr_Baslangıç.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.MenuItem_Grid_Etk_ZaAr_Baslangıç.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MenuItem_Grid_Etk_ZaAr_Baslangıç.Name = "MenuItem_Grid_Etk_ZaAr_Baslangıç";
            this.MenuItem_Grid_Etk_ZaAr_Baslangıç.Size = new System.Drawing.Size(181, 27);
            this.MenuItem_Grid_Etk_ZaAr_Baslangıç.ToolTipText = "İlk Tarih\r\ngg/aa/yyyy ss:dd:ss";
            this.MenuItem_Grid_Etk_ZaAr_Baslangıç.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MenuItem_Grid_Etk_ZaAr_Baslangıç_KeyPress);
            // 
            // MenuItem_Grid_Etk_ZaAr_Bitiş
            // 
            this.MenuItem_Grid_Etk_ZaAr_Bitiş.AutoSize = false;
            this.MenuItem_Grid_Etk_ZaAr_Bitiş.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.MenuItem_Grid_Etk_ZaAr_Bitiş.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MenuItem_Grid_Etk_ZaAr_Bitiş.Name = "MenuItem_Grid_Etk_ZaAr_Bitiş";
            this.MenuItem_Grid_Etk_ZaAr_Bitiş.Size = new System.Drawing.Size(247, 27);
            this.MenuItem_Grid_Etk_ZaAr_Bitiş.ToolTipText = "Son Tarih\r\ndd/MMMM/yyyy HH:mm:ss";
            this.MenuItem_Grid_Etk_ZaAr_Bitiş.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MenuItem_Grid_Etk_ZaAr_Bitiş_KeyPress);
            // 
            // MenuItem_Grid_Etk_ZaAr_Filtrele
            // 
            this.MenuItem_Grid_Etk_ZaAr_Filtrele.Name = "MenuItem_Grid_Etk_ZaAr_Filtrele";
            this.MenuItem_Grid_Etk_ZaAr_Filtrele.Size = new System.Drawing.Size(321, 26);
            this.MenuItem_Grid_Etk_ZaAr_Filtrele.Text = "Filtrele";
            this.MenuItem_Grid_Etk_ZaAr_Filtrele.Click += new System.EventHandler(this.MenuItem_Grid_Etk_ZaAr_Filtrele_Click);
            // 
            // MenuItem_Grid_Etk_AçıklamaTekSatırda
            // 
            this.MenuItem_Grid_Etk_AçıklamaTekSatırda.Checked = true;
            this.MenuItem_Grid_Etk_AçıklamaTekSatırda.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItem_Grid_Etk_AçıklamaTekSatırda.Name = "MenuItem_Grid_Etk_AçıklamaTekSatırda";
            this.MenuItem_Grid_Etk_AçıklamaTekSatırda.Size = new System.Drawing.Size(331, 26);
            this.MenuItem_Grid_Etk_AçıklamaTekSatırda.Text = "Açıklama tek satırda gösterilsin";
            this.MenuItem_Grid_Etk_AçıklamaTekSatırda.Click += new System.EventHandler(this.MenuItem_Grid_Etk_AçıklamaTekSatırda_Click);
            // 
            // MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan
            // 
            this.MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan.CheckOnClick = true;
            this.MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan.Name = "MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan";
            this.MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan.Size = new System.Drawing.Size(331, 26);
            this.MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan.Text = "Ayarlar sayfasındaki filtrelemeyi kullan";
            this.MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan.Click += new System.EventHandler(this.MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel_Arama);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(554, 399);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Arama";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel_Arama
            // 
            this.panel_Arama.Controls.Add(this.Arama);
            this.panel_Arama.Controls.Add(this.textBox_Arama);
            this.panel_Arama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Arama.Location = new System.Drawing.Point(3, 2);
            this.panel_Arama.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_Arama.Name = "panel_Arama";
            this.panel_Arama.Size = new System.Drawing.Size(548, 395);
            this.panel_Arama.TabIndex = 3;
            // 
            // Arama
            // 
            this.Arama.AllowUserToAddRows = false;
            this.Arama.AllowUserToDeleteRows = false;
            this.Arama.AllowUserToOrderColumns = true;
            this.Arama.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Arama.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Arama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Arama.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Arama_Bulunan_adı});
            this.Arama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Arama.Location = new System.Drawing.Point(0, 23);
            this.Arama.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Arama.Name = "Arama";
            this.Arama.ReadOnly = true;
            this.Arama.RowHeadersWidth = 62;
            this.Arama.RowTemplate.Height = 24;
            this.Arama.Size = new System.Drawing.Size(548, 372);
            this.Arama.TabIndex = 11;
            this.Arama.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Arama_CellClick);
            this.Arama.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Arama_CellDoubleClick);
            // 
            // Arama_Bulunan_adı
            // 
            this.Arama_Bulunan_adı.HeaderText = "Tanım";
            this.Arama_Bulunan_adı.MinimumWidth = 8;
            this.Arama_Bulunan_adı.Name = "Arama_Bulunan_adı";
            this.Arama_Bulunan_adı.ReadOnly = true;
            // 
            // textBox_Arama
            // 
            this.textBox_Arama.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_Arama.Location = new System.Drawing.Point(0, 0);
            this.textBox_Arama.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Arama.Name = "textBox_Arama";
            this.textBox_Arama.Size = new System.Drawing.Size(548, 23);
            this.textBox_Arama.TabIndex = 10;
            this.BaloncukluUyari.SetToolTip(this.textBox_Arama, "En az 2 harf içermelidir");
            this.textBox_Arama.TextChanged += new System.EventHandler(this.textBox_Arama_TextChanged);
            this.textBox_Arama.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Arama_KeyDown);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel_Ayarlar);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Size = new System.Drawing.Size(554, 399);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Ayarlar";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel_Ayarlar
            // 
            this.panel_Ayarlar.AutoScroll = true;
            this.panel_Ayarlar.Controls.Add(this.groupBox2);
            this.panel_Ayarlar.Controls.Add(this.KullanıcıAdı);
            this.panel_Ayarlar.Controls.Add(this.Üç_Değiştir);
            this.panel_Ayarlar.Controls.Add(this.numericUpDown1);
            this.panel_Ayarlar.Controls.Add(this.label4);
            this.panel_Ayarlar.Controls.Add(this.label5);
            this.panel_Ayarlar.Controls.Add(this.groupBox1);
            this.panel_Ayarlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Ayarlar.Location = new System.Drawing.Point(3, 2);
            this.panel_Ayarlar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Ayarlar.Name = "panel_Ayarlar";
            this.panel_Ayarlar.Size = new System.Drawing.Size(548, 395);
            this.panel_Ayarlar.TabIndex = 0;
            // 
            // OdaklanmışGörünüm_DiğerleriniDaralt
            // 
            this.OdaklanmışGörünüm_DiğerleriniDaralt.AutoSize = true;
            this.OdaklanmışGörünüm_DiğerleriniDaralt.Location = new System.Drawing.Point(221, 22);
            this.OdaklanmışGörünüm_DiğerleriniDaralt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OdaklanmışGörünüm_DiğerleriniDaralt.Name = "OdaklanmışGörünüm_DiğerleriniDaralt";
            this.OdaklanmışGörünüm_DiğerleriniDaralt.Size = new System.Drawing.Size(134, 21);
            this.OdaklanmışGörünüm_DiğerleriniDaralt.TabIndex = 21;
            this.OdaklanmışGörünüm_DiğerleriniDaralt.Text = "Diğerlerini daralt";
            this.BaloncukluUyari.SetToolTip(this.OdaklanmışGörünüm_DiğerleriniDaralt, "Tüm açık dalları kapatarak gezinmeyi kolaylaştırır");
            this.OdaklanmışGörünüm_DiğerleriniDaralt.UseVisualStyleBackColor = true;
            // 
            // KullanıcıAdı
            // 
            this.KullanıcıAdı.Location = new System.Drawing.Point(224, 26);
            this.KullanıcıAdı.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.KullanıcıAdı.Name = "KullanıcıAdı";
            this.KullanıcıAdı.Size = new System.Drawing.Size(175, 23);
            this.KullanıcıAdı.TabIndex = 20;
            // 
            // Üç_Değiştir
            // 
            this.Üç_Değiştir.AutoSize = true;
            this.Üç_Değiştir.Checked = true;
            this.Üç_Değiştir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Üç_Değiştir.Location = new System.Drawing.Point(11, 107);
            this.Üç_Değiştir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Üç_Değiştir.Name = "Üç_Değiştir";
            this.Üç_Değiştir.Size = new System.Drawing.Size(348, 55);
            this.Üç_Değiştir.TabIndex = 19;
            this.Üç_Değiştir.Text = "Yeni etkinlik oluştururken ÜZERİNDE ÇALIŞILIYOR\r\nseçildiğinde diğer ÜZERİNDE ÇALI" +
    "ŞILIYOR\r\netkinliklerini DÜŞÜK ÖNCELİKLİ ile değiştir";
            this.BaloncukluUyari.SetToolTip(this.Üç_Değiştir, "Tüm girişler için ortak kontrol");
            this.Üç_Değiştir.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(11, 26);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(80, 23);
            this.numericUpDown1.TabIndex = 10;
            this.numericUpDown1.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.PuntoDeğişti);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Kullanıcı Adı";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Ekran Karakter Büyüklüğü";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FiltrelemeTumu);
            this.groupBox1.Controls.Add(this.Filtreleme_D6);
            this.groupBox1.Controls.Add(this.Filtreleme_D8);
            this.groupBox1.Controls.Add(this.Filtreleme_D7);
            this.groupBox1.Controls.Add(this.Filtreleme_D3);
            this.groupBox1.Controls.Add(this.Filtreleme_D4);
            this.groupBox1.Controls.Add(this.Filtreleme_D5);
            this.groupBox1.Controls.Add(this.Filtreleme_D2);
            this.groupBox1.Controls.Add(this.Filtreleme_D1);
            this.groupBox1.Location = new System.Drawing.Point(3, 166);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(396, 167);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // FiltrelemeTumu
            // 
            this.FiltrelemeTumu.AutoSize = true;
            this.FiltrelemeTumu.Checked = true;
            this.FiltrelemeTumu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FiltrelemeTumu.Location = new System.Drawing.Point(8, 0);
            this.FiltrelemeTumu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FiltrelemeTumu.Name = "FiltrelemeTumu";
            this.FiltrelemeTumu.Size = new System.Drawing.Size(91, 21);
            this.FiltrelemeTumu.TabIndex = 21;
            this.FiltrelemeTumu.Text = "Filtreleme";
            this.FiltrelemeTumu.UseVisualStyleBackColor = true;
            this.FiltrelemeTumu.CheckedChanged += new System.EventHandler(this.FiltrelemeTumu_CheckedChanged);
            // 
            // Filtreleme_D6
            // 
            this.Filtreleme_D6.AutoSize = true;
            this.Filtreleme_D6.Checked = true;
            this.Filtreleme_D6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Filtreleme_D6.Location = new System.Drawing.Point(221, 26);
            this.Filtreleme_D6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filtreleme_D6.Name = "Filtreleme_D6";
            this.Filtreleme_D6.Size = new System.Drawing.Size(64, 21);
            this.Filtreleme_D6.TabIndex = 21;
            this.Filtreleme_D6.Text = "Diğer";
            this.Filtreleme_D6.UseVisualStyleBackColor = true;
            this.Filtreleme_D6.CheckedChanged += new System.EventHandler(this.Filtreleme_DurumDeğişikliği);
            // 
            // Filtreleme_D8
            // 
            this.Filtreleme_D8.AutoSize = true;
            this.Filtreleme_D8.Checked = true;
            this.Filtreleme_D8.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Filtreleme_D8.Location = new System.Drawing.Point(221, 80);
            this.Filtreleme_D8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filtreleme_D8.Name = "Filtreleme_D8";
            this.Filtreleme_D8.Size = new System.Drawing.Size(93, 21);
            this.Filtreleme_D8.TabIndex = 23;
            this.Filtreleme_D8.Text = "İptal edildi";
            this.Filtreleme_D8.UseVisualStyleBackColor = true;
            this.Filtreleme_D8.CheckedChanged += new System.EventHandler(this.Filtreleme_DurumDeğişikliği);
            // 
            // Filtreleme_D7
            // 
            this.Filtreleme_D7.AutoSize = true;
            this.Filtreleme_D7.Checked = true;
            this.Filtreleme_D7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Filtreleme_D7.Location = new System.Drawing.Point(221, 53);
            this.Filtreleme_D7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filtreleme_D7.Name = "Filtreleme_D7";
            this.Filtreleme_D7.Size = new System.Drawing.Size(107, 21);
            this.Filtreleme_D7.TabIndex = 22;
            this.Filtreleme_D7.Text = "Tamamlandı";
            this.Filtreleme_D7.UseVisualStyleBackColor = true;
            this.Filtreleme_D7.CheckedChanged += new System.EventHandler(this.Filtreleme_DurumDeğişikliği);
            // 
            // Filtreleme_D3
            // 
            this.Filtreleme_D3.AutoSize = true;
            this.Filtreleme_D3.Checked = true;
            this.Filtreleme_D3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Filtreleme_D3.Location = new System.Drawing.Point(8, 80);
            this.Filtreleme_D3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filtreleme_D3.Name = "Filtreleme_D3";
            this.Filtreleme_D3.Size = new System.Drawing.Size(98, 21);
            this.Filtreleme_D3.TabIndex = 17;
            this.Filtreleme_D3.Text = "Yeni görev";
            this.Filtreleme_D3.UseVisualStyleBackColor = true;
            this.Filtreleme_D3.CheckedChanged += new System.EventHandler(this.Filtreleme_DurumDeğişikliği);
            // 
            // Filtreleme_D4
            // 
            this.Filtreleme_D4.AutoSize = true;
            this.Filtreleme_D4.Checked = true;
            this.Filtreleme_D4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Filtreleme_D4.Location = new System.Drawing.Point(8, 107);
            this.Filtreleme_D4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filtreleme_D4.Name = "Filtreleme_D4";
            this.Filtreleme_D4.Size = new System.Drawing.Size(100, 21);
            this.Filtreleme_D4.TabIndex = 19;
            this.Filtreleme_D4.Text = "Beklemede";
            this.Filtreleme_D4.UseVisualStyleBackColor = true;
            this.Filtreleme_D4.CheckedChanged += new System.EventHandler(this.Filtreleme_DurumDeğişikliği);
            // 
            // Filtreleme_D5
            // 
            this.Filtreleme_D5.AutoSize = true;
            this.Filtreleme_D5.Checked = true;
            this.Filtreleme_D5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Filtreleme_D5.Location = new System.Drawing.Point(8, 134);
            this.Filtreleme_D5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filtreleme_D5.Name = "Filtreleme_D5";
            this.Filtreleme_D5.Size = new System.Drawing.Size(202, 21);
            this.Filtreleme_D5.TabIndex = 20;
            this.Filtreleme_D5.Text = "Bitti, geri bildirim bekleniyor";
            this.Filtreleme_D5.UseVisualStyleBackColor = true;
            this.Filtreleme_D5.CheckedChanged += new System.EventHandler(this.Filtreleme_DurumDeğişikliği);
            // 
            // Filtreleme_D2
            // 
            this.Filtreleme_D2.AutoSize = true;
            this.Filtreleme_D2.Checked = true;
            this.Filtreleme_D2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Filtreleme_D2.Location = new System.Drawing.Point(8, 53);
            this.Filtreleme_D2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filtreleme_D2.Name = "Filtreleme_D2";
            this.Filtreleme_D2.Size = new System.Drawing.Size(124, 21);
            this.Filtreleme_D2.TabIndex = 18;
            this.Filtreleme_D2.Text = "Düşük öncelikli";
            this.Filtreleme_D2.UseVisualStyleBackColor = true;
            this.Filtreleme_D2.CheckedChanged += new System.EventHandler(this.Filtreleme_DurumDeğişikliği);
            // 
            // Filtreleme_D1
            // 
            this.Filtreleme_D1.AutoSize = true;
            this.Filtreleme_D1.Checked = true;
            this.Filtreleme_D1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Filtreleme_D1.Location = new System.Drawing.Point(8, 26);
            this.Filtreleme_D1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filtreleme_D1.Name = "Filtreleme_D1";
            this.Filtreleme_D1.Size = new System.Drawing.Size(148, 21);
            this.Filtreleme_D1.TabIndex = 18;
            this.Filtreleme_D1.Text = "Üzerinde çalışılıyor";
            this.Filtreleme_D1.UseVisualStyleBackColor = true;
            this.Filtreleme_D1.CheckedChanged += new System.EventHandler(this.Filtreleme_DurumDeğişikliği);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.panel_Hatırlatıcı);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage5.Size = new System.Drawing.Size(554, 399);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Hatırlatıcı";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel_Hatırlatıcı
            // 
            this.panel_Hatırlatıcı.AutoScroll = true;
            this.panel_Hatırlatıcı.AutoScrollMargin = new System.Drawing.Size(50, 0);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Yarın_dd);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Bugün_dd);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Yarın_cc);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Bugün_cc);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Yarın_bb);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Bugün_bb);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Yarın_aa);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Bugün_aa);
            this.panel_Hatırlatıcı.Controls.Add(this.label14);
            this.panel_Hatırlatıcı.Controls.Add(this.label15);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Tekrarla_Onay);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Diğer_Onay);
            this.panel_Hatırlatıcı.Controls.Add(this.label6);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Tekrarla_dönem);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Diğer_Yazı);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Tekrarla_adet);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_DahaSonra_6ay);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Haftaya_Pa);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_DahaSonra_3ay);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_DahaSonra_2ay);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_DahaSonra_1ay);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Yarın_d);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Yarın_c);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Haftaya_Cts);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Yarın_b);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Haftaya_Cu);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Diğer);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Haftaya_Per);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Yarın_a);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Haftaya_Çrş);
            this.panel_Hatırlatıcı.Controls.Add(this.label13);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Haftaya_Sa);
            this.panel_Hatırlatıcı.Controls.Add(this.label12);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Haftaya_Pzt);
            this.panel_Hatırlatıcı.Controls.Add(this.label11);
            this.panel_Hatırlatıcı.Controls.Add(this.label7);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Bugün_d);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Bugün_c);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Bugün_b);
            this.panel_Hatırlatıcı.Controls.Add(this.Hatırlatıcı_Hatırlat_Bugün_a);
            this.panel_Hatırlatıcı.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Hatırlatıcı.Location = new System.Drawing.Point(4, 4);
            this.panel_Hatırlatıcı.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_Hatırlatıcı.Name = "panel_Hatırlatıcı";
            this.panel_Hatırlatıcı.Size = new System.Drawing.Size(546, 391);
            this.panel_Hatırlatıcı.TabIndex = 2;
            // 
            // Hatırlatıcı_Hatırlat_Yarın_dd
            // 
            this.Hatırlatıcı_Hatırlat_Yarın_dd.Location = new System.Drawing.Point(320, 83);
            this.Hatırlatıcı_Hatırlat_Yarın_dd.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.Hatırlatıcı_Hatırlat_Yarın_dd.Name = "Hatırlatıcı_Hatırlat_Yarın_dd";
            this.Hatırlatıcı_Hatırlat_Yarın_dd.Size = new System.Drawing.Size(50, 23);
            this.Hatırlatıcı_Hatırlat_Yarın_dd.TabIndex = 34;
            this.Hatırlatıcı_Hatırlat_Yarın_dd.TabStop = false;
            this.Hatırlatıcı_Hatırlat_Yarın_dd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Hatırlatıcı_Hatırlat_Yarın_dd.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
            // 
            // Hatırlatıcı_Hatırlat_Bugün_dd
            // 
            this.Hatırlatıcı_Hatırlat_Bugün_dd.Location = new System.Drawing.Point(320, 29);
            this.Hatırlatıcı_Hatırlat_Bugün_dd.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.Hatırlatıcı_Hatırlat_Bugün_dd.Name = "Hatırlatıcı_Hatırlat_Bugün_dd";
            this.Hatırlatıcı_Hatırlat_Bugün_dd.Size = new System.Drawing.Size(50, 23);
            this.Hatırlatıcı_Hatırlat_Bugün_dd.TabIndex = 34;
            this.Hatırlatıcı_Hatırlat_Bugün_dd.TabStop = false;
            this.Hatırlatıcı_Hatırlat_Bugün_dd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Hatırlatıcı_Hatırlat_Bugün_dd.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
            // 
            // Hatırlatıcı_Hatırlat_Yarın_cc
            // 
            this.Hatırlatıcı_Hatırlat_Yarın_cc.Location = new System.Drawing.Point(225, 83);
            this.Hatırlatıcı_Hatırlat_Yarın_cc.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.Hatırlatıcı_Hatırlat_Yarın_cc.Name = "Hatırlatıcı_Hatırlat_Yarın_cc";
            this.Hatırlatıcı_Hatırlat_Yarın_cc.Size = new System.Drawing.Size(50, 23);
            this.Hatırlatıcı_Hatırlat_Yarın_cc.TabIndex = 34;
            this.Hatırlatıcı_Hatırlat_Yarın_cc.TabStop = false;
            this.Hatırlatıcı_Hatırlat_Yarın_cc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Hatırlatıcı_Hatırlat_Yarın_cc.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
            // 
            // Hatırlatıcı_Hatırlat_Bugün_cc
            // 
            this.Hatırlatıcı_Hatırlat_Bugün_cc.Location = new System.Drawing.Point(225, 29);
            this.Hatırlatıcı_Hatırlat_Bugün_cc.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.Hatırlatıcı_Hatırlat_Bugün_cc.Name = "Hatırlatıcı_Hatırlat_Bugün_cc";
            this.Hatırlatıcı_Hatırlat_Bugün_cc.Size = new System.Drawing.Size(50, 23);
            this.Hatırlatıcı_Hatırlat_Bugün_cc.TabIndex = 34;
            this.Hatırlatıcı_Hatırlat_Bugün_cc.TabStop = false;
            this.Hatırlatıcı_Hatırlat_Bugün_cc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Hatırlatıcı_Hatırlat_Bugün_cc.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
            // 
            // Hatırlatıcı_Hatırlat_Yarın_bb
            // 
            this.Hatırlatıcı_Hatırlat_Yarın_bb.Location = new System.Drawing.Point(131, 83);
            this.Hatırlatıcı_Hatırlat_Yarın_bb.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.Hatırlatıcı_Hatırlat_Yarın_bb.Name = "Hatırlatıcı_Hatırlat_Yarın_bb";
            this.Hatırlatıcı_Hatırlat_Yarın_bb.Size = new System.Drawing.Size(50, 23);
            this.Hatırlatıcı_Hatırlat_Yarın_bb.TabIndex = 34;
            this.Hatırlatıcı_Hatırlat_Yarın_bb.TabStop = false;
            this.Hatırlatıcı_Hatırlat_Yarın_bb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Hatırlatıcı_Hatırlat_Yarın_bb.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // Hatırlatıcı_Hatırlat_Bugün_bb
            // 
            this.Hatırlatıcı_Hatırlat_Bugün_bb.Location = new System.Drawing.Point(131, 29);
            this.Hatırlatıcı_Hatırlat_Bugün_bb.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.Hatırlatıcı_Hatırlat_Bugün_bb.Name = "Hatırlatıcı_Hatırlat_Bugün_bb";
            this.Hatırlatıcı_Hatırlat_Bugün_bb.Size = new System.Drawing.Size(50, 23);
            this.Hatırlatıcı_Hatırlat_Bugün_bb.TabIndex = 34;
            this.Hatırlatıcı_Hatırlat_Bugün_bb.TabStop = false;
            this.Hatırlatıcı_Hatırlat_Bugün_bb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Hatırlatıcı_Hatırlat_Bugün_bb.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // Hatırlatıcı_Hatırlat_Yarın_aa
            // 
            this.Hatırlatıcı_Hatırlat_Yarın_aa.Location = new System.Drawing.Point(36, 83);
            this.Hatırlatıcı_Hatırlat_Yarın_aa.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.Hatırlatıcı_Hatırlat_Yarın_aa.Name = "Hatırlatıcı_Hatırlat_Yarın_aa";
            this.Hatırlatıcı_Hatırlat_Yarın_aa.Size = new System.Drawing.Size(50, 23);
            this.Hatırlatıcı_Hatırlat_Yarın_aa.TabIndex = 34;
            this.Hatırlatıcı_Hatırlat_Yarın_aa.TabStop = false;
            this.Hatırlatıcı_Hatırlat_Yarın_aa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Hatırlatıcı_Hatırlat_Yarın_aa.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // Hatırlatıcı_Hatırlat_Bugün_aa
            // 
            this.Hatırlatıcı_Hatırlat_Bugün_aa.Location = new System.Drawing.Point(36, 29);
            this.Hatırlatıcı_Hatırlat_Bugün_aa.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.Hatırlatıcı_Hatırlat_Bugün_aa.Name = "Hatırlatıcı_Hatırlat_Bugün_aa";
            this.Hatırlatıcı_Hatırlat_Bugün_aa.Size = new System.Drawing.Size(50, 23);
            this.Hatırlatıcı_Hatırlat_Bugün_aa.TabIndex = 1;
            this.Hatırlatıcı_Hatırlat_Bugün_aa.TabStop = false;
            this.Hatırlatıcı_Hatırlat_Bugün_aa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Hatırlatıcı_Hatırlat_Bugün_aa.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(45, 329);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 17);
            this.label14.TabIndex = 29;
            this.label14.Text = "Her";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 304);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 17);
            this.label15.TabIndex = 33;
            this.label15.Text = "Tekrarla";
            // 
            // Hatırlatıcı_Tekrarla_Onay
            // 
            this.Hatırlatıcı_Tekrarla_Onay.AutoSize = true;
            this.Hatırlatıcı_Tekrarla_Onay.Location = new System.Drawing.Point(13, 329);
            this.Hatırlatıcı_Tekrarla_Onay.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Tekrarla_Onay.Name = "Hatırlatıcı_Tekrarla_Onay";
            this.Hatırlatıcı_Tekrarla_Onay.Size = new System.Drawing.Size(18, 17);
            this.Hatırlatıcı_Tekrarla_Onay.TabIndex = 24;
            this.Hatırlatıcı_Tekrarla_Onay.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Tekrarla_Onay.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Diğer_Onay
            // 
            this.Hatırlatıcı_Hatırlat_Diğer_Onay.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Diğer_Onay.Location = new System.Drawing.Point(13, 263);
            this.Hatırlatıcı_Hatırlat_Diğer_Onay.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Diğer_Onay.Name = "Hatırlatıcı_Hatırlat_Diğer_Onay";
            this.Hatırlatıcı_Hatırlat_Diğer_Onay.Size = new System.Drawing.Size(17, 16);
            this.Hatırlatıcı_Hatırlat_Diğer_Onay.TabIndex = 22;
            this.Hatırlatıcı_Hatırlat_Diğer_Onay.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Diğer_Onay.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Diğer_Onay.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(313, 329);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "bir sefer";
            // 
            // Hatırlatıcı_Hatırlat_DahaSonra_1yıl
            // 
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl.Location = new System.Drawing.Point(427, 193);
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl.Name = "Hatırlatıcı_Hatırlat_DahaSonra_1yıl";
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl.Size = new System.Drawing.Size(56, 21);
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl.TabIndex = 21;
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl.TabStop = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl.Text = "1 Yıl";
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_1yıl.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Tekrarla_dönem
            // 
            this.Hatırlatıcı_Tekrarla_dönem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Hatırlatıcı_Tekrarla_dönem.FormattingEnabled = true;
            this.Hatırlatıcı_Tekrarla_dönem.Items.AddRange(new object[] {
            "Günde",
            "Haftada",
            "Ayda",
            "Yılda"});
            this.Hatırlatıcı_Tekrarla_dönem.Location = new System.Drawing.Point(191, 325);
            this.Hatırlatıcı_Tekrarla_dönem.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Tekrarla_dönem.Name = "Hatırlatıcı_Tekrarla_dönem";
            this.Hatırlatıcı_Tekrarla_dönem.Size = new System.Drawing.Size(113, 24);
            this.Hatırlatıcı_Tekrarla_dönem.TabIndex = 26;
            // 
            // Hatırlatıcı_Hatırlat_Diğer_Yazı
            // 
            this.Hatırlatıcı_Hatırlat_Diğer_Yazı.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Hatırlatıcı_Hatırlat_Diğer_Yazı.Location = new System.Drawing.Point(40, 260);
            this.Hatırlatıcı_Hatırlat_Diğer_Yazı.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Diğer_Yazı.Name = "Hatırlatıcı_Hatırlat_Diğer_Yazı";
            this.Hatırlatıcı_Hatırlat_Diğer_Yazı.Size = new System.Drawing.Size(576, 23);
            this.Hatırlatıcı_Hatırlat_Diğer_Yazı.TabIndex = 23;
            this.Hatırlatıcı_Hatırlat_Diğer_Yazı.TextChanged += new System.EventHandler(this.Hatırlatıcı_Hatırlat_Diğer_Yazı_TextChanged);
            this.Hatırlatıcı_Hatırlat_Diğer_Yazı.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Hatırlatıcı_Hatırlat_Diğer_Yazı_KeyPress);
            // 
            // Hatırlatıcı_Tekrarla_adet
            // 
            this.Hatırlatıcı_Tekrarla_adet.Location = new System.Drawing.Point(93, 325);
            this.Hatırlatıcı_Tekrarla_adet.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Tekrarla_adet.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.Hatırlatıcı_Tekrarla_adet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Hatırlatıcı_Tekrarla_adet.Name = "Hatırlatıcı_Tekrarla_adet";
            this.Hatırlatıcı_Tekrarla_adet.Size = new System.Drawing.Size(85, 23);
            this.Hatırlatıcı_Tekrarla_adet.TabIndex = 25;
            this.Hatırlatıcı_Tekrarla_adet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Hatırlatıcı_Tekrarla_adet.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Hatırlatıcı_Hatırlat_DahaSonra_6ay
            // 
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay.Location = new System.Drawing.Point(366, 193);
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay.Name = "Hatırlatıcı_Hatırlat_DahaSonra_6ay";
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay.Size = new System.Drawing.Size(53, 21);
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay.TabIndex = 20;
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay.TabStop = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay.Text = "6Ay";
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_6ay.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Haftaya_Pa
            // 
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa.Location = new System.Drawing.Point(437, 139);
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa.Name = "Hatırlatıcı_Hatırlat_Haftaya_Pa";
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa.Size = new System.Drawing.Size(46, 21);
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa.TabIndex = 14;
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa.Text = "Pa";
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Pa.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_DahaSonra_3ay
            // 
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay.Location = new System.Drawing.Point(305, 193);
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay.Name = "Hatırlatıcı_Hatırlat_DahaSonra_3ay";
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay.Size = new System.Drawing.Size(53, 21);
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay.TabIndex = 19;
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay.TabStop = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay.Text = "3Ay";
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_3ay.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_DahaSonra_2ay
            // 
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay.Location = new System.Drawing.Point(243, 193);
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay.Name = "Hatırlatıcı_Hatırlat_DahaSonra_2ay";
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay.Size = new System.Drawing.Size(53, 21);
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay.TabIndex = 18;
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay.TabStop = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay.Text = "2Ay";
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_2ay.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_DahaSonra_1ay
            // 
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay.Location = new System.Drawing.Point(178, 193);
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay.Name = "Hatırlatıcı_Hatırlat_DahaSonra_1ay";
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay.Size = new System.Drawing.Size(57, 21);
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay.TabIndex = 17;
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay.TabStop = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay.Text = "1 Ay";
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_1ay.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Yarın_d
            // 
            this.Hatırlatıcı_Hatırlat_Yarın_d.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Yarın_d.Location = new System.Drawing.Point(296, 85);
            this.Hatırlatıcı_Hatırlat_Yarın_d.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Yarın_d.Name = "Hatırlatıcı_Hatırlat_Yarın_d";
            this.Hatırlatıcı_Hatırlat_Yarın_d.Size = new System.Drawing.Size(17, 16);
            this.Hatırlatıcı_Hatırlat_Yarın_d.TabIndex = 7;
            this.Hatırlatıcı_Hatırlat_Yarın_d.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Yarın_d.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Yarın_d.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Yarın_c
            // 
            this.Hatırlatıcı_Hatırlat_Yarın_c.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Yarın_c.Location = new System.Drawing.Point(201, 85);
            this.Hatırlatıcı_Hatırlat_Yarın_c.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Yarın_c.Name = "Hatırlatıcı_Hatırlat_Yarın_c";
            this.Hatırlatıcı_Hatırlat_Yarın_c.Size = new System.Drawing.Size(17, 16);
            this.Hatırlatıcı_Hatırlat_Yarın_c.TabIndex = 6;
            this.Hatırlatıcı_Hatırlat_Yarın_c.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Yarın_c.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Yarın_c.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_DahaSonra_3hafta
            // 
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta.Location = new System.Drawing.Point(95, 193);
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta.Name = "Hatırlatıcı_Hatırlat_DahaSonra_3hafta";
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta.Size = new System.Drawing.Size(75, 21);
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta.TabIndex = 16;
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta.TabStop = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta.Text = "3 Hafta";
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_3hafta.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Haftaya_Cts
            // 
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts.Location = new System.Drawing.Point(368, 139);
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts.Name = "Hatırlatıcı_Hatırlat_Haftaya_Cts";
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts.Size = new System.Drawing.Size(49, 21);
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts.TabIndex = 13;
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts.Text = "Cts";
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Cts.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_DahaSonra_2hafta
            // 
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta.Location = new System.Drawing.Point(12, 193);
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta.Name = "Hatırlatıcı_Hatırlat_DahaSonra_2hafta";
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta.Size = new System.Drawing.Size(75, 21);
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta.TabIndex = 15;
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta.TabStop = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta.Text = "2 Hafta";
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_DahaSonra_2hafta.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Yarın_b
            // 
            this.Hatırlatıcı_Hatırlat_Yarın_b.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Yarın_b.Location = new System.Drawing.Point(107, 85);
            this.Hatırlatıcı_Hatırlat_Yarın_b.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Yarın_b.Name = "Hatırlatıcı_Hatırlat_Yarın_b";
            this.Hatırlatıcı_Hatırlat_Yarın_b.Size = new System.Drawing.Size(17, 16);
            this.Hatırlatıcı_Hatırlat_Yarın_b.TabIndex = 5;
            this.Hatırlatıcı_Hatırlat_Yarın_b.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Yarın_b.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Yarın_b.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Haftaya_Cu
            // 
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu.Location = new System.Drawing.Point(305, 139);
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu.Name = "Hatırlatıcı_Hatırlat_Haftaya_Cu";
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu.Size = new System.Drawing.Size(46, 21);
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu.TabIndex = 12;
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu.Text = "Cu";
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Cu.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Diğer
            // 
            this.Hatırlatıcı_Hatırlat_Diğer.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Diğer.Location = new System.Drawing.Point(9, 235);
            this.Hatırlatıcı_Hatırlat_Diğer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Hatırlatıcı_Hatırlat_Diğer.Name = "Hatırlatıcı_Hatırlat_Diğer";
            this.Hatırlatıcı_Hatırlat_Diğer.Size = new System.Drawing.Size(616, 17);
            this.Hatırlatıcı_Hatırlat_Diğer.TabIndex = 32;
            this.Hatırlatıcı_Hatırlat_Diğer.Text = "Diğer ( gg aa | gg aa yy | gg aa yy ss | gg aa yy ss dd | g/gg a/aa yy/yyyy s/ss " +
    "d/dd boşluk virgül )";
            // 
            // Hatırlatıcı_Hatırlat_Haftaya_Per
            // 
            this.Hatırlatıcı_Hatırlat_Haftaya_Per.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Per.Location = new System.Drawing.Point(229, 139);
            this.Hatırlatıcı_Hatırlat_Haftaya_Per.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Haftaya_Per.Name = "Hatırlatıcı_Hatırlat_Haftaya_Per";
            this.Hatırlatıcı_Hatırlat_Haftaya_Per.Size = new System.Drawing.Size(51, 21);
            this.Hatırlatıcı_Hatırlat_Haftaya_Per.TabIndex = 11;
            this.Hatırlatıcı_Hatırlat_Haftaya_Per.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Per.Text = "Per";
            this.Hatırlatıcı_Hatırlat_Haftaya_Per.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Per.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Yarın_a
            // 
            this.Hatırlatıcı_Hatırlat_Yarın_a.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Yarın_a.Location = new System.Drawing.Point(12, 85);
            this.Hatırlatıcı_Hatırlat_Yarın_a.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Yarın_a.Name = "Hatırlatıcı_Hatırlat_Yarın_a";
            this.Hatırlatıcı_Hatırlat_Yarın_a.Size = new System.Drawing.Size(17, 16);
            this.Hatırlatıcı_Hatırlat_Yarın_a.TabIndex = 4;
            this.Hatırlatıcı_Hatırlat_Yarın_a.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Yarın_a.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Yarın_a.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Haftaya_Çrş
            // 
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş.Location = new System.Drawing.Point(155, 139);
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş.Name = "Hatırlatıcı_Hatırlat_Haftaya_Çrş";
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş.Size = new System.Drawing.Size(50, 21);
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş.TabIndex = 10;
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş.Text = "Çrş";
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Çrş.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 169);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 17);
            this.label13.TabIndex = 31;
            this.label13.Text = "Daha sonra";
            // 
            // Hatırlatıcı_Hatırlat_Haftaya_Sa
            // 
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa.Location = new System.Drawing.Point(85, 139);
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa.Name = "Hatırlatıcı_Hatırlat_Haftaya_Sa";
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa.Size = new System.Drawing.Size(46, 21);
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa.TabIndex = 9;
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa.Text = "Sa";
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Sa.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 114);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 17);
            this.label12.TabIndex = 30;
            this.label12.Text = "Haftaya";
            // 
            // Hatırlatıcı_Hatırlat_Haftaya_Pzt
            // 
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt.Location = new System.Drawing.Point(12, 139);
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt.Name = "Hatırlatıcı_Hatırlat_Haftaya_Pzt";
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt.Size = new System.Drawing.Size(49, 21);
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt.TabIndex = 8;
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt.Text = "Pzt";
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Haftaya_Pzt.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 60);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 17);
            this.label11.TabIndex = 29;
            this.label11.Text = "Yarın";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 17);
            this.label7.TabIndex = 28;
            this.label7.Text = "Bugün";
            // 
            // Hatırlatıcı_Hatırlat_Bugün_d
            // 
            this.Hatırlatıcı_Hatırlat_Bugün_d.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Bugün_d.Location = new System.Drawing.Point(296, 31);
            this.Hatırlatıcı_Hatırlat_Bugün_d.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Bugün_d.Name = "Hatırlatıcı_Hatırlat_Bugün_d";
            this.Hatırlatıcı_Hatırlat_Bugün_d.Size = new System.Drawing.Size(17, 16);
            this.Hatırlatıcı_Hatırlat_Bugün_d.TabIndex = 3;
            this.Hatırlatıcı_Hatırlat_Bugün_d.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Bugün_d.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Bugün_d.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Bugün_c
            // 
            this.Hatırlatıcı_Hatırlat_Bugün_c.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Bugün_c.Location = new System.Drawing.Point(201, 31);
            this.Hatırlatıcı_Hatırlat_Bugün_c.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Bugün_c.Name = "Hatırlatıcı_Hatırlat_Bugün_c";
            this.Hatırlatıcı_Hatırlat_Bugün_c.Size = new System.Drawing.Size(17, 16);
            this.Hatırlatıcı_Hatırlat_Bugün_c.TabIndex = 2;
            this.Hatırlatıcı_Hatırlat_Bugün_c.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Bugün_c.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Bugün_c.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Bugün_b
            // 
            this.Hatırlatıcı_Hatırlat_Bugün_b.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Bugün_b.Location = new System.Drawing.Point(107, 31);
            this.Hatırlatıcı_Hatırlat_Bugün_b.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Bugün_b.Name = "Hatırlatıcı_Hatırlat_Bugün_b";
            this.Hatırlatıcı_Hatırlat_Bugün_b.Size = new System.Drawing.Size(17, 16);
            this.Hatırlatıcı_Hatırlat_Bugün_b.TabIndex = 1;
            this.Hatırlatıcı_Hatırlat_Bugün_b.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Bugün_b.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Bugün_b.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // Hatırlatıcı_Hatırlat_Bugün_a
            // 
            this.Hatırlatıcı_Hatırlat_Bugün_a.AutoSize = true;
            this.Hatırlatıcı_Hatırlat_Bugün_a.Location = new System.Drawing.Point(12, 31);
            this.Hatırlatıcı_Hatırlat_Bugün_a.Margin = new System.Windows.Forms.Padding(4);
            this.Hatırlatıcı_Hatırlat_Bugün_a.Name = "Hatırlatıcı_Hatırlat_Bugün_a";
            this.Hatırlatıcı_Hatırlat_Bugün_a.Size = new System.Drawing.Size(17, 16);
            this.Hatırlatıcı_Hatırlat_Bugün_a.TabIndex = 0;
            this.Hatırlatıcı_Hatırlat_Bugün_a.TabStop = true;
            this.Hatırlatıcı_Hatırlat_Bugün_a.UseVisualStyleBackColor = true;
            this.Hatırlatıcı_Hatırlat_Bugün_a.CheckedChanged += new System.EventHandler(this.Genel_KaydedilmemişBilgiVar);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEkle,
            this.toolStripArama,
            this.toolStripİlerleme,
            this.toolStripEtiket,
            this.toolStripAyarlar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(3, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(659, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripEkle
            // 
            this.toolStripEkle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEkle.Image = global::Etkinlik_Takip.Properties.Resources.Onay;
            this.toolStripEkle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEkle.Name = "toolStripEkle";
            this.toolStripEkle.Size = new System.Drawing.Size(34, 24);
            this.toolStripEkle.Text = "toolStripDropDownButton1";
            this.toolStripEkle.Click += new System.EventHandler(this.toolStripEkle_Click);
            // 
            // toolStripArama
            // 
            this.toolStripArama.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripArama.Image = ((System.Drawing.Image)(resources.GetObject("toolStripArama.Image")));
            this.toolStripArama.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripArama.Name = "toolStripArama";
            this.toolStripArama.ShowDropDownArrow = false;
            this.toolStripArama.Size = new System.Drawing.Size(24, 24);
            this.toolStripArama.Text = "toolStripDropDownButton2";
            this.toolStripArama.Click += new System.EventHandler(this.toolStripSil_Click);
            // 
            // toolStripİlerleme
            // 
            this.toolStripİlerleme.AutoSize = false;
            this.toolStripİlerleme.Name = "toolStripİlerleme";
            this.toolStripİlerleme.Size = new System.Drawing.Size(100, 18);
            this.toolStripİlerleme.Visible = false;
            // 
            // toolStripEtiket
            // 
            this.toolStripEtiket.Name = "toolStripEtiket";
            this.toolStripEtiket.Size = new System.Drawing.Size(555, 20);
            this.toolStripEtiket.Spring = true;
            this.toolStripEtiket.Text = "sdf";
            // 
            // toolStripAyarlar
            // 
            this.toolStripAyarlar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAyarlar.Image = global::Etkinlik_Takip.Properties.Resources.Ayarlar;
            this.toolStripAyarlar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAyarlar.Name = "toolStripAyarlar";
            this.toolStripAyarlar.ShowDropDownArrow = false;
            this.toolStripAyarlar.Size = new System.Drawing.Size(24, 24);
            this.toolStripAyarlar.Text = "toolStripDropDownButton1";
            this.toolStripAyarlar.Click += new System.EventHandler(this.toolStripAyarlar_Click);
            // 
            // BaloncukluUyari
            // 
            this.BaloncukluUyari.IsBalloon = true;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.ContextMenuStrip = this.Menu_İkon;
            this.notifyIcon1.Text = "Mup Ekinlik Takip";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // Menu_İkon
            // 
            this.Menu_İkon.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu_İkon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_İkon_Çıkış});
            this.Menu_İkon.Name = "Menu_Grid_Etkinlikler";
            this.Menu_İkon.ShowImageMargin = false;
            this.Menu_İkon.Size = new System.Drawing.Size(84, 28);
            // 
            // Menu_İkon_Çıkış
            // 
            this.Menu_İkon_Çıkış.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Menu_İkon_Çıkış.Name = "Menu_İkon_Çıkış";
            this.Menu_İkon_Çıkış.Size = new System.Drawing.Size(83, 24);
            this.Menu_İkon_Çıkış.Text = "Çıkış";
            this.Menu_İkon_Çıkış.Click += new System.EventHandler(this.Menu_İkon_Çıkış_Click);
            // 
            // Sql_Zamanlayıcı
            // 
            this.Sql_Zamanlayıcı.Interval = 60000;
            this.Sql_Zamanlayıcı.Tick += new System.EventHandler(this.Sql_Zamanlayıcı_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OdaklanmışGörünüm_Genişlet);
            this.groupBox2.Controls.Add(this.OdaklanmışGörünüm_Kızart);
            this.groupBox2.Controls.Add(this.OdaklanmışGörünüm_DiğerleriniDaralt);
            this.groupBox2.Location = new System.Drawing.Point(3, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 48);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Odaklanmış Görünüm";
            // 
            // OdaklanmışGörünüm_Kızart
            // 
            this.OdaklanmışGörünüm_Kızart.AutoSize = true;
            this.OdaklanmışGörünüm_Kızart.Checked = true;
            this.OdaklanmışGörünüm_Kızart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OdaklanmışGörünüm_Kızart.Location = new System.Drawing.Point(8, 22);
            this.OdaklanmışGörünüm_Kızart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OdaklanmışGörünüm_Kızart.Name = "OdaklanmışGörünüm_Kızart";
            this.OdaklanmışGörünüm_Kızart.Size = new System.Drawing.Size(66, 21);
            this.OdaklanmışGörünüm_Kızart.TabIndex = 22;
            this.OdaklanmışGörünüm_Kızart.Text = "Kızart";
            this.BaloncukluUyari.SetToolTip(this.OdaklanmışGörünüm_Kızart, "Seçili dalı kızartarak gezinmeyi kolaylaştırır");
            this.OdaklanmışGörünüm_Kızart.UseVisualStyleBackColor = true;
            // 
            // OdaklanmışGörünüm_Genişlet
            // 
            this.OdaklanmışGörünüm_Genişlet.AutoSize = true;
            this.OdaklanmışGörünüm_Genişlet.Checked = true;
            this.OdaklanmışGörünüm_Genişlet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OdaklanmışGörünüm_Genişlet.Location = new System.Drawing.Point(107, 22);
            this.OdaklanmışGörünüm_Genişlet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OdaklanmışGörünüm_Genişlet.Name = "OdaklanmışGörünüm_Genişlet";
            this.OdaklanmışGörünüm_Genişlet.Size = new System.Drawing.Size(82, 21);
            this.OdaklanmışGörünüm_Genişlet.TabIndex = 23;
            this.OdaklanmışGörünüm_Genişlet.Text = "Genişlet";
            this.BaloncukluUyari.SetToolTip(this.OdaklanmışGörünüm_Genişlet, "Seçili dalı genişleterek gezinmeyi kolaylaştırır");
            this.OdaklanmışGörünüm_Genişlet.UseVisualStyleBackColor = true;
            // 
            // AnaEkran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 458);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AnaEkran";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Bekleyiniz";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnaEkran_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AnaEkran_FormClosed);
            this.Load += new System.EventHandler(this.AnaEkran_Load);
            this.Shown += new System.EventHandler(this.AnaEkran_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnaEkran_KeyDown);
            this.Resize += new System.EventHandler(this.AnaEkran_Resize);
            this.Menu_Ağaç.ResumeLayout(false);
            this.Menu_Ağaç.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel_Görev.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel_Görev__.ResumeLayout(false);
            this.panel_Görev__.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Etkinlikler)).EndInit();
            this.Menu_Grid_Etkinlikler.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel_Arama.ResumeLayout(false);
            this.panel_Arama.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Arama)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.panel_Ayarlar.ResumeLayout(false);
            this.panel_Ayarlar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.panel_Hatırlatıcı.ResumeLayout(false);
            this.panel_Hatırlatıcı.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Yarın_dd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Bugün_dd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Yarın_cc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Bugün_cc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Yarın_bb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Bugün_bb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Yarın_aa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Hatırlat_Bugün_aa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hatırlatıcı_Tekrarla_adet)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.Menu_İkon.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private AnaEkran.ÇiftTamponluTreeView Ağaç;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripEkle;
        private System.Windows.Forms.ToolStripDropDownButton toolStripArama;
        private System.Windows.Forms.ToolStripDropDownButton toolStripAyarlar;
        private System.Windows.Forms.ToolTip BaloncukluUyari;
        private System.Windows.Forms.ToolStripStatusLabel toolStripEtiket;
        private System.Windows.Forms.ImageList ımageList1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel_Görev__;
        private System.Windows.Forms.TextBox textBox_Görev_Açıklama;
        private System.Windows.Forms.TextBox textBox_Görev_Tanım;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView Grid_Etkinlikler;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel_Arama;
        private System.Windows.Forms.DataGridView Arama;
        private System.Windows.Forms.TextBox textBox_Arama;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel_Ayarlar;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel_Görev;
        private System.Windows.Forms.ToolStripProgressBar toolStripİlerleme;
        private System.Windows.Forms.ContextMenuStrip Menu_Grid_Etkinlikler;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_Sütünlar;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_AçıklamaTekSatırda;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_Sütünlar_Durum;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_Sütünlar_İçerik;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_Sütünlar_Tarih;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_Sütünlar_Açıklama;
        private System.Windows.Forms.ContextMenuStrip Menu_Ağaç;
        private System.Windows.Forms.ToolStripTextBox MenuItem_Ağaç_Tanım;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_ÜzerindeÇalışılıyor;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_Beklemede;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_BittiGeriBildirimBekleniyor;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_Tamamlandı;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_İptalEdildi;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_Diğer;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_Yeni;
        private System.Windows.Forms.LinkLabel label_Görev_Tarih;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_Sıralama;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_Sıralama_YeniUstte;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_Sıralama_EskiUstte;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı;
        private System.Windows.Forms.ToolStripTextBox MenuItem_Grid_Etk_GörüntülenecekEtkinlikSayısı_Adet;
        private System.Windows.Forms.ToolStripTextBox MenuItem_Ağaç_Açıklama;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_TümünüDaralt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox Filtreleme_D1;
        private System.Windows.Forms.CheckBox Filtreleme_D2;
        private System.Windows.Forms.CheckBox Filtreleme_D3;
        private System.Windows.Forms.CheckBox Filtreleme_D4;
        private System.Windows.Forms.CheckBox Filtreleme_D5;
        private System.Windows.Forms.CheckBox Filtreleme_D6;
        private System.Windows.Forms.CheckBox Filtreleme_D7;
        private System.Windows.Forms.CheckBox Filtreleme_D8;
        private System.Windows.Forms.DataGridViewLinkColumn Arama_Bulunan_adı;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_Sil;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_GeriAl;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_Sütünlar_An;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_TümünüAç;
        private System.Windows.Forms.CheckBox Üç_Değiştir;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_Bugün;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_BuHafta;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_Son15Gün;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_BuAy;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_Son3Ay;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_Son6Ay;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_BuYıl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripTextBox MenuItem_Grid_Etk_ZaAr_Baslangıç;
        private System.Windows.Forms.ToolStripTextBox MenuItem_Grid_Etk_ZaAr_Bitiş;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_Filtrele;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_Dün;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_TümEtkinliklerinListele;
        private System.Windows.Forms.TextBox KullanıcıAdı;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sutun_Gorev;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sutun_An;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sutun_Tarih;
        private System.Windows.Forms.DataGridViewImageColumn Sutun_Durum_G;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sutun_Durum_M;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sutun_Açıklama;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_AyarlarSayfaınakiFiltrelemeyiKullan;
        private System.Windows.Forms.CheckBox FiltrelemeTumu;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_GeçenYıl;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Grid_Etk_ZaAr_TümZamanlar;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_DüşükÖncelikli;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_Görev;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_Etkinlik;
        private System.Windows.Forms.CheckBox Grid_Listele_Tarihçe;
        private System.Windows.Forms.Timer Sql_Zamanlayıcı;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_PanodanAl;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_PanoyaKopyala;
        private System.Windows.Forms.ContextMenuStrip Menu_İkon;
        private System.Windows.Forms.ToolStripMenuItem Menu_İkon_Çıkış;
        private System.Windows.Forms.CheckBox OdaklanmışGörünüm_DiğerleriniDaralt;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Panel panel_Hatırlatıcı;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Yarın_c;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Yarın_b;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Yarın_a;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Haftaya_Pa;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Haftaya_Cts;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Haftaya_Cu;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Haftaya_Per;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Haftaya_Çrş;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Haftaya_Sa;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Haftaya_Pzt;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Bugün_c;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Bugün_b;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Bugün_a;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Diğer_Onay;
        private System.Windows.Forms.TextBox Hatırlatıcı_Hatırlat_Diğer_Yazı;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_DahaSonra_1yıl;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_DahaSonra_6ay;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_DahaSonra_3ay;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_DahaSonra_2ay;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_DahaSonra_1ay;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_DahaSonra_3hafta;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_DahaSonra_2hafta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Hatırlatıcı_Tekrarla_dönem;
        private System.Windows.Forms.NumericUpDown Hatırlatıcı_Tekrarla_adet;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_Hatırlatıcı;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_Hatırlatıcı_Kur;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Ağaç_Hatırlatıcı_İptalEt;
        private System.Windows.Forms.CheckBox Hatırlatıcı_Tekrarla_Onay;
        private System.Windows.Forms.Label Hatırlatıcı_Hatırlat_Diğer;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown Hatırlatıcı_Hatırlat_Bugün_aa;
        private System.Windows.Forms.NumericUpDown Hatırlatıcı_Hatırlat_Bugün_cc;
        private System.Windows.Forms.NumericUpDown Hatırlatıcı_Hatırlat_Bugün_bb;
        private System.Windows.Forms.NumericUpDown Hatırlatıcı_Hatırlat_Bugün_dd;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Bugün_d;
        private System.Windows.Forms.NumericUpDown Hatırlatıcı_Hatırlat_Yarın_dd;
        private System.Windows.Forms.NumericUpDown Hatırlatıcı_Hatırlat_Yarın_cc;
        private System.Windows.Forms.NumericUpDown Hatırlatıcı_Hatırlat_Yarın_bb;
        private System.Windows.Forms.NumericUpDown Hatırlatıcı_Hatırlat_Yarın_aa;
        private System.Windows.Forms.RadioButton Hatırlatıcı_Hatırlat_Yarın_d;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox OdaklanmışGörünüm_Kızart;
        private System.Windows.Forms.CheckBox OdaklanmışGörünüm_Genişlet;
    }
}

