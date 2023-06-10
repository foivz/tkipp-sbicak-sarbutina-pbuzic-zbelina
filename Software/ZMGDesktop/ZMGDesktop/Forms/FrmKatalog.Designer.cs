namespace ZMGDesktop
{
    partial class FrmKatalog
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
            this.dgvMaterijali = new System.Windows.Forms.DataGridView();
            this.dgvUsluge = new System.Windows.Forms.DataGridView();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnZaprimi = new System.Windows.Forms.Button();
            this.btnNatrag = new System.Windows.Forms.Button();
            this.labelMaterijali = new System.Windows.Forms.Label();
            this.labelUsluge = new System.Windows.Forms.Label();
            this.lblRoba = new System.Windows.Forms.Label();
            this.dgvRoba = new System.Windows.Forms.DataGridView();
            this.btnIzvoz = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterijali)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsluge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoba)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMaterijali
            // 
            this.dgvMaterijali.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterijali.Location = new System.Drawing.Point(35, 25);
            this.dgvMaterijali.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvMaterijali.Name = "dgvMaterijali";
            this.dgvMaterijali.RowHeadersWidth = 51;
            this.dgvMaterijali.RowTemplate.Height = 24;
            this.dgvMaterijali.Size = new System.Drawing.Size(559, 231);
            this.dgvMaterijali.TabIndex = 0;
            // 
            // dgvUsluge
            // 
            this.dgvUsluge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsluge.Location = new System.Drawing.Point(35, 287);
            this.dgvUsluge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvUsluge.Name = "dgvUsluge";
            this.dgvUsluge.RowHeadersWidth = 51;
            this.dgvUsluge.RowTemplate.Height = 24;
            this.dgvUsluge.Size = new System.Drawing.Size(559, 150);
            this.dgvUsluge.TabIndex = 1;
            // 
            // btnDodaj
            // 
            this.btnDodaj.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDodaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDodaj.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDodaj.Location = new System.Drawing.Point(629, 34);
            this.btnDodaj.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(145, 66);
            this.btnDodaj.TabIndex = 2;
            this.btnDodaj.Text = "Dodaj materijal";
            this.btnDodaj.UseVisualStyleBackColor = false;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnObrisi.ForeColor = System.Drawing.SystemColors.Control;
            this.btnObrisi.Location = new System.Drawing.Point(629, 119);
            this.btnObrisi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(145, 66);
            this.btnObrisi.TabIndex = 3;
            this.btnObrisi.Text = "Obriši materijal";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnZaprimi
            // 
            this.btnZaprimi.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnZaprimi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnZaprimi.ForeColor = System.Drawing.SystemColors.Control;
            this.btnZaprimi.Location = new System.Drawing.Point(629, 208);
            this.btnZaprimi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnZaprimi.Name = "btnZaprimi";
            this.btnZaprimi.Size = new System.Drawing.Size(145, 66);
            this.btnZaprimi.TabIndex = 4;
            this.btnZaprimi.Text = "Zaprimi materijal";
            this.btnZaprimi.UseVisualStyleBackColor = false;
            this.btnZaprimi.Click += new System.EventHandler(this.btnZaprimi_Click);
            // 
            // btnNatrag
            // 
            this.btnNatrag.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnNatrag.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnNatrag.ForeColor = System.Drawing.SystemColors.Control;
            this.btnNatrag.Location = new System.Drawing.Point(657, 661);
            this.btnNatrag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNatrag.Name = "btnNatrag";
            this.btnNatrag.Size = new System.Drawing.Size(117, 46);
            this.btnNatrag.TabIndex = 5;
            this.btnNatrag.Text = "Natrag";
            this.btnNatrag.UseVisualStyleBackColor = false;
            this.btnNatrag.Click += new System.EventHandler(this.btnNatrag_Click);
            // 
            // labelMaterijali
            // 
            this.labelMaterijali.AutoSize = true;
            this.labelMaterijali.Location = new System.Drawing.Point(35, 2);
            this.labelMaterijali.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMaterijali.Name = "labelMaterijali";
            this.labelMaterijali.Size = new System.Drawing.Size(64, 16);
            this.labelMaterijali.TabIndex = 6;
            this.labelMaterijali.Text = "Materijali:";
            // 
            // labelUsluge
            // 
            this.labelUsluge.AutoSize = true;
            this.labelUsluge.Location = new System.Drawing.Point(39, 263);
            this.labelUsluge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUsluge.Name = "labelUsluge";
            this.labelUsluge.Size = new System.Drawing.Size(53, 16);
            this.labelUsluge.TabIndex = 7;
            this.labelUsluge.Text = "Usluge:";
            // 
            // lblRoba
            // 
            this.lblRoba.AutoSize = true;
            this.lblRoba.Location = new System.Drawing.Point(35, 453);
            this.lblRoba.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRoba.Name = "lblRoba";
            this.lblRoba.Size = new System.Drawing.Size(44, 16);
            this.lblRoba.TabIndex = 9;
            this.lblRoba.Text = "Roba:";
            // 
            // dgvRoba
            // 
            this.dgvRoba.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoba.Location = new System.Drawing.Point(35, 475);
            this.dgvRoba.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvRoba.Name = "dgvRoba";
            this.dgvRoba.RowHeadersWidth = 51;
            this.dgvRoba.RowTemplate.Height = 24;
            this.dgvRoba.Size = new System.Drawing.Size(559, 231);
            this.dgvRoba.TabIndex = 8;
            // 
            // btnIzvoz
            // 
            this.btnIzvoz.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnIzvoz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzvoz.ForeColor = System.Drawing.SystemColors.Control;
            this.btnIzvoz.Location = new System.Drawing.Point(629, 300);
            this.btnIzvoz.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIzvoz.Name = "btnIzvoz";
            this.btnIzvoz.Size = new System.Drawing.Size(145, 75);
            this.btnIzvoz.TabIndex = 10;
            this.btnIzvoz.Text = "Izvoz materijala (CSV)";
            this.btnIzvoz.UseVisualStyleBackColor = false;
            this.btnIzvoz.Click += new System.EventHandler(this.btnIzvoz_Click);
            // 
            // FrmKatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 714);
            this.Controls.Add(this.btnIzvoz);
            this.Controls.Add(this.lblRoba);
            this.Controls.Add(this.dgvRoba);
            this.Controls.Add(this.labelUsluge);
            this.Controls.Add(this.labelMaterijali);
            this.Controls.Add(this.btnNatrag);
            this.Controls.Add(this.btnZaprimi);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.dgvUsluge);
            this.Controls.Add(this.dgvMaterijali);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmKatalog";
            this.Text = "FrmKatalog";
            this.Load += new System.EventHandler(this.FrmKatalog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterijali)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsluge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoba)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMaterijali;
        private System.Windows.Forms.DataGridView dgvUsluge;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnZaprimi;
        private System.Windows.Forms.Button btnNatrag;
        private System.Windows.Forms.Label labelMaterijali;
        private System.Windows.Forms.Label labelUsluge;
        private System.Windows.Forms.Label lblRoba;
        private System.Windows.Forms.DataGridView dgvRoba;
        private System.Windows.Forms.Button btnIzvoz;
    }
}