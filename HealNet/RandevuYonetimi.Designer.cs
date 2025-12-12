namespace HealNet
{
    partial class RandevuYonetimi
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
            System.Windows.Forms.Button btnGeri;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHastaTelefon = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAra = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHastaTC = new System.Windows.Forms.TextBox();
            this.txtHastaAdSoyad = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpTarih = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.comboDoktorlar = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBrans = new System.Windows.Forms.ComboBox();
            this.comboSaat = new System.Windows.Forms.ComboBox();
            this.dtgRandevular = new System.Windows.Forms.DataGridView();
            this.btnTemizle = new System.Windows.Forms.Button();
            this.btnYenile = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnKaydet = new System.Windows.Forms.Button();
            btnGeri = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRandevular)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtHastaTelefon);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnAra);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtHastaTC);
            this.groupBox1.Controls.Add(this.txtHastaAdSoyad);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(27, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 277);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hasta Seçimi";
            // 
            // txtHastaTelefon
            // 
            this.txtHastaTelefon.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtHastaTelefon.Location = new System.Drawing.Point(152, 159);
            this.txtHastaTelefon.Multiline = true;
            this.txtHastaTelefon.Name = "txtHastaTelefon";
            this.txtHastaTelefon.ReadOnly = true;
            this.txtHastaTelefon.Size = new System.Drawing.Size(210, 34);
            this.txtHastaTelefon.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(27, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 20);
            this.label3.TabIndex = 36;
            this.label3.Text = "Hasta Telefon";
            // 
            // btnAra
            // 
            this.btnAra.BackColor = System.Drawing.Color.White;
            this.btnAra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAra.FlatAppearance.BorderSize = 0;
            this.btnAra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAra.Font = new System.Drawing.Font("Segoe UI Variable Text", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(50)))));
            this.btnAra.Location = new System.Drawing.Point(377, 45);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(76, 28);
            this.btnAra.TabIndex = 35;
            this.btnAra.Text = "ARA";
            this.btnAra.UseVisualStyleBackColor = false;
            this.btnAra.Click += new System.EventHandler(this.btnAra_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hasta TC";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(27, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hasta Ad Soyad";
            // 
            // txtHastaTC
            // 
            this.txtHastaTC.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtHastaTC.Location = new System.Drawing.Point(152, 45);
            this.txtHastaTC.Multiline = true;
            this.txtHastaTC.Name = "txtHastaTC";
            this.txtHastaTC.Size = new System.Drawing.Size(210, 34);
            this.txtHastaTC.TabIndex = 1;
            // 
            // txtHastaAdSoyad
            // 
            this.txtHastaAdSoyad.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtHastaAdSoyad.Location = new System.Drawing.Point(152, 100);
            this.txtHastaAdSoyad.Multiline = true;
            this.txtHastaAdSoyad.Name = "txtHastaAdSoyad";
            this.txtHastaAdSoyad.ReadOnly = true;
            this.txtHastaAdSoyad.Size = new System.Drawing.Size(210, 34);
            this.txtHastaAdSoyad.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpTarih);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.comboDoktorlar);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.comboBrans);
            this.groupBox2.Controls.Add(this.comboSaat);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(27, 382);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 275);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Randevu Detayları";
            // 
            // dtpTarih
            // 
            this.dtpTarih.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTarih.Location = new System.Drawing.Point(194, 147);
            this.dtpTarih.Name = "dtpTarih";
            this.dtpTarih.Size = new System.Drawing.Size(200, 27);
            this.dtpTarih.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(36, 152);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "Tarih";
            // 
            // comboDoktorlar
            // 
            this.comboDoktorlar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboDoktorlar.FormattingEnabled = true;
            this.comboDoktorlar.Location = new System.Drawing.Point(194, 89);
            this.comboDoktorlar.Name = "comboDoktorlar";
            this.comboDoktorlar.Size = new System.Drawing.Size(210, 36);
            this.comboDoktorlar.TabIndex = 8;
            this.comboDoktorlar.SelectedIndexChanged += new System.EventHandler(this.comboDoktorlar_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(36, 210);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 20);
            this.label16.TabIndex = 5;
            this.label16.Text = "Saat";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(36, 44);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 20);
            this.label17.TabIndex = 6;
            this.label17.Text = "Poliklinik ";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(36, 99);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 20);
            this.label18.TabIndex = 7;
            this.label18.Text = "Doktor";
            // 
            // comboBrans
            // 
            this.comboBrans.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBrans.FormattingEnabled = true;
            this.comboBrans.Items.AddRange(new object[] {
            "Dahiliye",
            "Kardiyoloji",
            "Göğüs Hastalıkları",
            "Kulak, Burun, Boğaz (KBB)",
            "Nöroloji",
            "Ortopedi",
            "Dermatoloji",
            "Üroloji",
            "Genel Cerrahi",
            "Aile Hekimliği",
            "Psikiyatri",
            "Fizik Tedavi ve Rehabilitasyon",
            "Gastroenteroloji",
            "Endokrinoloji",
            "Hematoloji"});
            this.comboBrans.Location = new System.Drawing.Point(194, 34);
            this.comboBrans.Name = "comboBrans";
            this.comboBrans.Size = new System.Drawing.Size(210, 36);
            this.comboBrans.TabIndex = 7;
            this.comboBrans.SelectedIndexChanged += new System.EventHandler(this.comboBrans_SelectedIndexChanged);
            // 
            // comboSaat
            // 
            this.comboSaat.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboSaat.FormattingEnabled = true;
            this.comboSaat.Items.AddRange(new object[] {
            ""});
            this.comboSaat.Location = new System.Drawing.Point(194, 200);
            this.comboSaat.Name = "comboSaat";
            this.comboSaat.Size = new System.Drawing.Size(210, 36);
            this.comboSaat.TabIndex = 6;
            // 
            // dtgRandevular
            // 
            this.dtgRandevular.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(50)))));
            this.dtgRandevular.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgRandevular.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRandevular.Location = new System.Drawing.Point(503, 52);
            this.dtgRandevular.Name = "dtgRandevular";
            this.dtgRandevular.RowHeadersWidth = 51;
            this.dtgRandevular.RowTemplate.Height = 24;
            this.dtgRandevular.Size = new System.Drawing.Size(806, 684);
            this.dtgRandevular.TabIndex = 36;
            // 
            // btnTemizle
            // 
            this.btnTemizle.BackColor = System.Drawing.Color.White;
            this.btnTemizle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTemizle.FlatAppearance.BorderSize = 0;
            this.btnTemizle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTemizle.Font = new System.Drawing.Font("Segoe UI Variable Text", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTemizle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(50)))));
            this.btnTemizle.Location = new System.Drawing.Point(179, 677);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(144, 34);
            this.btnTemizle.TabIndex = 40;
            this.btnTemizle.Text = "TEMİZLE";
            this.btnTemizle.UseVisualStyleBackColor = false;
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);
            // 
            // btnYenile
            // 
            this.btnYenile.BackColor = System.Drawing.Color.White;
            this.btnYenile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYenile.FlatAppearance.BorderSize = 0;
            this.btnYenile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYenile.Font = new System.Drawing.Font("Segoe UI Variable Text", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYenile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(50)))));
            this.btnYenile.Location = new System.Drawing.Point(1080, 12);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(106, 34);
            this.btnYenile.TabIndex = 39;
            this.btnYenile.Text = "YENİLE";
            this.btnYenile.UseVisualStyleBackColor = false;
            // 
            // btnSil
            // 
            this.btnSil.BackColor = System.Drawing.Color.White;
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.FlatAppearance.BorderSize = 0;
            this.btnSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSil.Font = new System.Drawing.Font("Segoe UI Variable Text", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSil.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(50)))));
            this.btnSil.Location = new System.Drawing.Point(1208, 12);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(101, 34);
            this.btnSil.TabIndex = 38;
            this.btnSil.Text = "SİL";
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.BackColor = System.Drawing.Color.White;
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.FlatAppearance.BorderSize = 0;
            this.btnKaydet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI Variable Text", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKaydet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(50)))));
            this.btnKaydet.Location = new System.Drawing.Point(336, 677);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(144, 34);
            this.btnKaydet.TabIndex = 37;
            this.btnKaydet.Text = "KAYDET";
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnGeri
            // 
            btnGeri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(50)))));
            btnGeri.Cursor = System.Windows.Forms.Cursors.Hand;
            btnGeri.FlatAppearance.BorderSize = 0;
            btnGeri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGeri.Font = new System.Drawing.Font("Segoe UI Variable Text", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            btnGeri.ForeColor = System.Drawing.Color.White;
            btnGeri.Image = global::HealNet.Properties.Resources.icons8_back_501;
            btnGeri.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnGeri.Location = new System.Drawing.Point(12, 12);
            btnGeri.Name = "btnGeri";
            btnGeri.Size = new System.Drawing.Size(80, 59);
            btnGeri.TabIndex = 41;
            btnGeri.UseVisualStyleBackColor = false;
            btnGeri.Click += new System.EventHandler(this.btnGeri_Click);
            // 
            // RandevuYonetimi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1321, 748);
            this.Controls.Add(btnGeri);
            this.Controls.Add(this.btnTemizle);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.dtgRandevular);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RandevuYonetimi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "v";
            this.Load += new System.EventHandler(this.RandevuYonetimi_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRandevular)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHastaTC;
        private System.Windows.Forms.TextBox txtHastaAdSoyad;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboDoktorlar;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBrans;
        private System.Windows.Forms.ComboBox comboSaat;
        private System.Windows.Forms.Button btnAra;
        private System.Windows.Forms.DateTimePicker dtpTarih;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dtgRandevular;
        private System.Windows.Forms.Button btnTemizle;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.TextBox txtHastaTelefon;
        private System.Windows.Forms.Label label3;
    }
}