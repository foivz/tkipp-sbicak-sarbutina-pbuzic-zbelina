﻿namespace ZMGDesktop
{
    partial class FrmPregledKlijenata
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
            this.dgvKlijenti = new System.Windows.Forms.DataGridView();
            this.labelKlijenti = new System.Windows.Forms.Label();
            this.btnDodajKlijenta = new System.Windows.Forms.Button();
            this.btnIzbrisiKlijenta = new System.Windows.Forms.Button();
            this.btnUrediKlijenta = new System.Windows.Forms.Button();
            this.btnDetaljiKlijenta = new System.Windows.Forms.Button();
            this.btnUveziKlijenta = new System.Windows.Forms.Button();
            this.btnNatrag = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKlijenti)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvKlijenti
            // 
            this.dgvKlijenti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKlijenti.Location = new System.Drawing.Point(12, 67);
            this.dgvKlijenti.Name = "dgvKlijenti";
            this.dgvKlijenti.RowHeadersWidth = 51;
            this.dgvKlijenti.RowTemplate.Height = 24;
            this.dgvKlijenti.Size = new System.Drawing.Size(849, 439);
            this.dgvKlijenti.TabIndex = 0;
            // 
            // labelKlijenti
            // 
            this.labelKlijenti.AutoSize = true;
            this.labelKlijenti.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelKlijenti.Location = new System.Drawing.Point(12, 33);
            this.labelKlijenti.Name = "labelKlijenti";
            this.labelKlijenti.Size = new System.Drawing.Size(137, 20);
            this.labelKlijenti.TabIndex = 1;
            this.labelKlijenti.Text = "Pregled klijenata:";
            // 
            // btnDodajKlijenta
            // 
            this.btnDodajKlijenta.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDodajKlijenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDodajKlijenta.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDodajKlijenta.Location = new System.Drawing.Point(884, 33);
            this.btnDodajKlijenta.Name = "btnDodajKlijenta";
            this.btnDodajKlijenta.Size = new System.Drawing.Size(147, 51);
            this.btnDodajKlijenta.TabIndex = 2;
            this.btnDodajKlijenta.Text = "Dodaj klijenta";
            this.btnDodajKlijenta.UseVisualStyleBackColor = false;
            this.btnDodajKlijenta.Click += new System.EventHandler(this.btnDodajKlijenta_Click);
            // 
            // btnIzbrisiKlijenta
            // 
            this.btnIzbrisiKlijenta.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnIzbrisiKlijenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzbrisiKlijenta.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnIzbrisiKlijenta.Location = new System.Drawing.Point(884, 90);
            this.btnIzbrisiKlijenta.Name = "btnIzbrisiKlijenta";
            this.btnIzbrisiKlijenta.Size = new System.Drawing.Size(147, 53);
            this.btnIzbrisiKlijenta.TabIndex = 3;
            this.btnIzbrisiKlijenta.Text = "Izbriši klijenta";
            this.btnIzbrisiKlijenta.UseVisualStyleBackColor = false;
            this.btnIzbrisiKlijenta.Click += new System.EventHandler(this.btnIzbrisiKlijenta_Click);
            // 
            // btnUrediKlijenta
            // 
            this.btnUrediKlijenta.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnUrediKlijenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnUrediKlijenta.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUrediKlijenta.Location = new System.Drawing.Point(884, 149);
            this.btnUrediKlijenta.Name = "btnUrediKlijenta";
            this.btnUrediKlijenta.Size = new System.Drawing.Size(147, 54);
            this.btnUrediKlijenta.TabIndex = 4;
            this.btnUrediKlijenta.Text = "Uredi klijenta";
            this.btnUrediKlijenta.UseVisualStyleBackColor = false;
            this.btnUrediKlijenta.Click += new System.EventHandler(this.btnUrediKlijenta_Click);
            // 
            // btnDetaljiKlijenta
            // 
            this.btnDetaljiKlijenta.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDetaljiKlijenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDetaljiKlijenta.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDetaljiKlijenta.Location = new System.Drawing.Point(884, 209);
            this.btnDetaljiKlijenta.Name = "btnDetaljiKlijenta";
            this.btnDetaljiKlijenta.Size = new System.Drawing.Size(147, 52);
            this.btnDetaljiKlijenta.TabIndex = 5;
            this.btnDetaljiKlijenta.Text = "Detalji klijenta";
            this.btnDetaljiKlijenta.UseVisualStyleBackColor = false;
            this.btnDetaljiKlijenta.Click += new System.EventHandler(this.btnDetaljiKlijenta_Click);
            // 
            // btnUveziKlijenta
            // 
            this.btnUveziKlijenta.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnUveziKlijenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnUveziKlijenta.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUveziKlijenta.Location = new System.Drawing.Point(884, 267);
            this.btnUveziKlijenta.Name = "btnUveziKlijenta";
            this.btnUveziKlijenta.Size = new System.Drawing.Size(147, 61);
            this.btnUveziKlijenta.TabIndex = 6;
            this.btnUveziKlijenta.Text = "Uvezi klijenta (XML)";
            this.btnUveziKlijenta.UseVisualStyleBackColor = false;
            this.btnUveziKlijenta.Click += new System.EventHandler(this.btnUveziKlijenta_Click);
            // 
            // btnNatrag
            // 
            this.btnNatrag.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnNatrag.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnNatrag.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnNatrag.Location = new System.Drawing.Point(908, 466);
            this.btnNatrag.Name = "btnNatrag";
            this.btnNatrag.Size = new System.Drawing.Size(108, 40);
            this.btnNatrag.TabIndex = 7;
            this.btnNatrag.Text = "Natrag";
            this.btnNatrag.UseVisualStyleBackColor = false;
            this.btnNatrag.Click += new System.EventHandler(this.btnNatrag_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(731, 39);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(130, 22);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(728, 19);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(88, 16);
            this.lblSearch.TabIndex = 9;
            this.lblSearch.Text = "Pretraživanje:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(634, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 35);
            this.button1.TabIndex = 10;
            this.button1.Text = "Sortiraj";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmPregledKlijenata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 518);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnNatrag);
            this.Controls.Add(this.btnUveziKlijenta);
            this.Controls.Add(this.btnDetaljiKlijenta);
            this.Controls.Add(this.btnUrediKlijenta);
            this.Controls.Add(this.btnIzbrisiKlijenta);
            this.Controls.Add(this.btnDodajKlijenta);
            this.Controls.Add(this.labelKlijenti);
            this.Controls.Add(this.dgvKlijenti);
            this.Name = "FrmPregledKlijenata";
            this.Text = "Pregled klijenata";
            this.Load += new System.EventHandler(this.FrmPregledKlijenata_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKlijenti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvKlijenti;
        private System.Windows.Forms.Label labelKlijenti;
        private System.Windows.Forms.Button btnDodajKlijenta;
        private System.Windows.Forms.Button btnIzbrisiKlijenta;
        private System.Windows.Forms.Button btnUrediKlijenta;
        private System.Windows.Forms.Button btnDetaljiKlijenta;
        private System.Windows.Forms.Button btnUveziKlijenta;
        private System.Windows.Forms.Button btnNatrag;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button button1;
    }
}

