namespace HealNet
{
    partial class AnaEkran
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
            System.Windows.Forms.Button btnCikis;
            System.Windows.Forms.Button btnDoktor;
            System.Windows.Forms.Button btnHastaKayit;
            System.Windows.Forms.Button btnRandevu;
            this.label1 = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRandevuSayisi = new System.Windows.Forms.Label();
            this.lblDoktorSayisi = new System.Windows.Forms.Label();
            this.lblHastaSayisi = new System.Windows.Forms.Label();
            btnCikis = new System.Windows.Forms.Button();
            btnDoktor = new System.Windows.Forms.Button();
            btnHastaKayit = new System.Windows.Forms.Button();
            btnRandevu = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCikis
            // 
            btnCikis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            btnCikis.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCikis.FlatAppearance.BorderSize = 0;
            btnCikis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCikis.Font = new System.Drawing.Font("Segoe UI Variable Text", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            btnCikis.ForeColor = System.Drawing.Color.White;
            btnCikis.Image = global::HealNet.Properties.Resources.icons8_exit_50;
            btnCikis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnCikis.Location = new System.Drawing.Point(486, 520);
            btnCikis.Name = "btnCikis";
            btnCikis.Size = new System.Drawing.Size(474, 78);
            btnCikis.TabIndex = 6;
            btnCikis.Text = "Çıkış";
            btnCikis.UseVisualStyleBackColor = false;
            // 
            // btnDoktor
            // 
            btnDoktor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            btnDoktor.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDoktor.FlatAppearance.BorderSize = 0;
            btnDoktor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDoktor.Font = new System.Drawing.Font("Segoe UI Variable Text", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            btnDoktor.ForeColor = System.Drawing.Color.White;
            btnDoktor.Image = global::HealNet.Properties.Resources.icons8_doctor_64;
            btnDoktor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnDoktor.Location = new System.Drawing.Point(26, 280);
            btnDoktor.Name = "btnDoktor";
            btnDoktor.Size = new System.Drawing.Size(333, 78);
            btnDoktor.TabIndex = 4;
            btnDoktor.Text = "Doktor Yönetimi";
            btnDoktor.UseVisualStyleBackColor = false;
            btnDoktor.Click += new System.EventHandler(this.btnDoktor_Click);
            // 
            // btnHastaKayit
            // 
            btnHastaKayit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            btnHastaKayit.Cursor = System.Windows.Forms.Cursors.Hand;
            btnHastaKayit.FlatAppearance.BorderSize = 0;
            btnHastaKayit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnHastaKayit.Font = new System.Drawing.Font("Segoe UI Variable Text", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            btnHastaKayit.ForeColor = System.Drawing.Color.White;
            btnHastaKayit.Image = global::HealNet.Properties.Resources.icons8_user_64;
            btnHastaKayit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnHastaKayit.Location = new System.Drawing.Point(26, 143);
            btnHastaKayit.Name = "btnHastaKayit";
            btnHastaKayit.Size = new System.Drawing.Size(333, 78);
            btnHastaKayit.TabIndex = 1;
            btnHastaKayit.Text = "Hasta Yönetimi";
            btnHastaKayit.UseVisualStyleBackColor = false;
            btnHastaKayit.Click += new System.EventHandler(this.btnHastaKayit_Click);
            // 
            // btnRandevu
            // 
            btnRandevu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            btnRandevu.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRandevu.FlatAppearance.BorderSize = 0;
            btnRandevu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRandevu.Font = new System.Drawing.Font("Segoe UI Variable Text", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            btnRandevu.ForeColor = System.Drawing.Color.White;
            btnRandevu.Image = global::HealNet.Properties.Resources.icons8_survey_64;
            btnRandevu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnRandevu.Location = new System.Drawing.Point(26, 429);
            btnRandevu.Name = "btnRandevu";
            btnRandevu.Size = new System.Drawing.Size(333, 78);
            btnRandevu.TabIndex = 3;
            btnRandevu.Text = "Randevu Yönetimi";
            btnRandevu.UseVisualStyleBackColor = false;
            btnRandevu.Click += new System.EventHandler(this.btnRandevu_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Variable Text", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(33, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 61);
            this.label1.TabIndex = 5;
            this.label1.Text = "HealNet";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(50)))));
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(btnCikis);
            this.panelMain.Controls.Add(btnDoktor);
            this.panelMain.Controls.Add(btnHastaKayit);
            this.panelMain.Controls.Add(btnRandevu);
            this.panelMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panelMain.Location = new System.Drawing.Point(78, 90);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(975, 614);
            this.panelMain.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Variable Text", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(435, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(537, 61);
            this.label2.TabIndex = 8;
            this.label2.Text = "Genel Bakış";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.panel1.Controls.Add(this.lblRandevuSayisi);
            this.panel1.Controls.Add(this.lblDoktorSayisi);
            this.panel1.Controls.Add(this.lblHastaSayisi);
            this.panel1.Location = new System.Drawing.Point(445, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 185);
            this.panel1.TabIndex = 7;
            // 
            // lblRandevuSayisi
            // 
            this.lblRandevuSayisi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.lblRandevuSayisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRandevuSayisi.Font = new System.Drawing.Font("Segoe UI Variable Text", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblRandevuSayisi.ForeColor = System.Drawing.Color.White;
            this.lblRandevuSayisi.Location = new System.Drawing.Point(31, 128);
            this.lblRandevuSayisi.Name = "lblRandevuSayisi";
            this.lblRandevuSayisi.Size = new System.Drawing.Size(458, 43);
            this.lblRandevuSayisi.TabIndex = 11;
            // 
            // lblDoktorSayisi
            // 
            this.lblDoktorSayisi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.lblDoktorSayisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDoktorSayisi.Font = new System.Drawing.Font("Segoe UI Variable Text", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDoktorSayisi.ForeColor = System.Drawing.Color.White;
            this.lblDoktorSayisi.Location = new System.Drawing.Point(31, 73);
            this.lblDoktorSayisi.Name = "lblDoktorSayisi";
            this.lblDoktorSayisi.Size = new System.Drawing.Size(458, 38);
            this.lblDoktorSayisi.TabIndex = 10;
            // 
            // lblHastaSayisi
            // 
            this.lblHastaSayisi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.lblHastaSayisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHastaSayisi.Font = new System.Drawing.Font("Segoe UI Variable Text", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblHastaSayisi.ForeColor = System.Drawing.Color.White;
            this.lblHastaSayisi.Location = new System.Drawing.Point(31, 15);
            this.lblHastaSayisi.Name = "lblHastaSayisi";
            this.lblHastaSayisi.Size = new System.Drawing.Size(458, 37);
            this.lblHastaSayisi.TabIndex = 9;
            // 
            // AnaEkran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(1150, 700);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AnaEkran";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ana Menü";
            this.Load += new System.EventHandler(this.AnaEkran_Load);
            this.panelMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRandevuSayisi;
        private System.Windows.Forms.Label lblDoktorSayisi;
        private System.Windows.Forms.Label lblHastaSayisi;
    }
}