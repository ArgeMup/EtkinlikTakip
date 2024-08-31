namespace Etkinlik_Takip
{
    partial class NotlarEkranı
    {
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.NotlarınKendisi = new System.Windows.Forms.RichTextBox();
            this.Belirginleştir = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Belirginleştir_Kızart = new System.Windows.Forms.ToolStripMenuItem();
            this.Belirginleştir_Sarart = new System.Windows.Forms.ToolStripMenuItem();
            this.Belirginleştir_Beyazlat = new System.Windows.Forms.ToolStripMenuItem();
            this.Ayraç = new System.Windows.Forms.SplitContainer();
            this.Belirginleştir.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ayraç)).BeginInit();
            this.Ayraç.Panel2.SuspendLayout();
            this.Ayraç.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotlarınKendisi
            // 
            this.NotlarınKendisi.AcceptsTab = true;
            this.NotlarınKendisi.AutoWordSelection = true;
            this.NotlarınKendisi.ContextMenuStrip = this.Belirginleştir;
            this.NotlarınKendisi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotlarınKendisi.EnableAutoDragDrop = true;
            this.NotlarınKendisi.HideSelection = false;
            this.NotlarınKendisi.Location = new System.Drawing.Point(0, 0);
            this.NotlarınKendisi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NotlarınKendisi.Name = "NotlarınKendisi";
            this.NotlarınKendisi.Size = new System.Drawing.Size(637, 380);
            this.NotlarınKendisi.TabIndex = 1;
            this.NotlarınKendisi.Text = "";
            this.toolTip1.SetToolTip(this.NotlarınKendisi, "Kaydedip çıkmak için - esc tuşu kullanılabilir\r\nYazılar seçilip\r\n\tağacın bir dalı" +
        " üzerine bırakılarak yeni görev oluşturulabilir\r\n\tsağ tık menüsü ile renklendiri" +
        "lebilir");
            this.NotlarınKendisi.WordWrap = false;
            this.NotlarınKendisi.TextChanged += new System.EventHandler(this.NotlarınKendisi_TextChanged);
            // 
            // Belirginleştir
            // 
            this.Belirginleştir.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Belirginleştir.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Belirginleştir_Kızart,
            this.Belirginleştir_Sarart,
            this.Belirginleştir_Beyazlat});
            this.Belirginleştir.Name = "Belirginleştir";
            this.Belirginleştir.ShowImageMargin = false;
            this.Belirginleştir.ShowItemToolTips = false;
            this.Belirginleştir.Size = new System.Drawing.Size(110, 76);
            // 
            // Belirginleştir_Kızart
            // 
            this.Belirginleştir_Kızart.Name = "Belirginleştir_Kızart";
            this.Belirginleştir_Kızart.Size = new System.Drawing.Size(109, 24);
            this.Belirginleştir_Kızart.Tag = "";
            this.Belirginleştir_Kızart.Text = "Kızart";
            this.Belirginleştir_Kızart.Click += new System.EventHandler(this.Belirginleştir_X_Click);
            // 
            // Belirginleştir_Sarart
            // 
            this.Belirginleştir_Sarart.Name = "Belirginleştir_Sarart";
            this.Belirginleştir_Sarart.Size = new System.Drawing.Size(109, 24);
            this.Belirginleştir_Sarart.Text = "Sarart";
            this.Belirginleştir_Sarart.Click += new System.EventHandler(this.Belirginleştir_X_Click);
            // 
            // Belirginleştir_Beyazlat
            // 
            this.Belirginleştir_Beyazlat.Name = "Belirginleştir_Beyazlat";
            this.Belirginleştir_Beyazlat.Size = new System.Drawing.Size(109, 24);
            this.Belirginleştir_Beyazlat.Text = "Beyazlat";
            this.Belirginleştir_Beyazlat.Click += new System.EventHandler(this.Belirginleştir_X_Click);
            // 
            // Ayraç
            // 
            this.Ayraç.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Ayraç.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ayraç.Location = new System.Drawing.Point(0, 0);
            this.Ayraç.Name = "Ayraç";
            // 
            // Ayraç.Panel2
            // 
            this.Ayraç.Panel2.Controls.Add(this.NotlarınKendisi);
            this.Ayraç.Size = new System.Drawing.Size(845, 384);
            this.Ayraç.SplitterDistance = 200;
            this.Ayraç.TabIndex = 4;
            // 
            // NotlarEkranı
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 384);
            this.Controls.Add(this.Ayraç);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimizeBox = false;
            this.Name = "NotlarEkranı";
            this.ShowInTaskbar = false;
            this.Text = "Notlar";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.NotlarEkranı_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NotlarEkranı_FormClosing);
            this.Load += new System.EventHandler(this.Notlar_Load);
            this.Shown += new System.EventHandler(this.NotlarEkranı_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Notlar_KeyDown);
            this.Belirginleştir.ResumeLayout(false);
            this.Ayraç.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Ayraç)).EndInit();
            this.Ayraç.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RichTextBox NotlarınKendisi;
        private System.Windows.Forms.ContextMenuStrip Belirginleştir;
        private System.Windows.Forms.ToolStripMenuItem Belirginleştir_Kızart;
        private System.Windows.Forms.ToolStripMenuItem Belirginleştir_Sarart;
        private System.Windows.Forms.ToolStripMenuItem Belirginleştir_Beyazlat;
        private System.Windows.Forms.SplitContainer Ayraç;
    }
}