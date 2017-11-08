namespace Scannex
{
    partial class frmScanPages
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbScanner = new System.Windows.Forms.ComboBox();
            this.rd100 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rd300 = new System.Windows.Forms.RadioButton();
            this.rd200 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSource = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPage = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdGray = new System.Windows.Forms.RadioButton();
            this.rdColor = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.rdBlack = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scanner Devices";
            // 
            // cmbScanner
            // 
            this.cmbScanner.FormattingEnabled = true;
            this.cmbScanner.Location = new System.Drawing.Point(135, 9);
            this.cmbScanner.Margin = new System.Windows.Forms.Padding(2);
            this.cmbScanner.Name = "cmbScanner";
            this.cmbScanner.Size = new System.Drawing.Size(203, 21);
            this.cmbScanner.TabIndex = 0;
            // 
            // rd100
            // 
            this.rd100.AutoSize = true;
            this.rd100.Location = new System.Drawing.Point(85, 15);
            this.rd100.Margin = new System.Windows.Forms.Padding(2);
            this.rd100.Name = "rd100";
            this.rd100.Size = new System.Drawing.Size(64, 17);
            this.rd100.TabIndex = 3;
            this.rd100.TabStop = true;
            this.rd100.Text = "100 DPI";
            this.rd100.UseVisualStyleBackColor = true;
            this.rd100.CheckedChanged += new System.EventHandler(this.rd100_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rd300);
            this.groupBox1.Controls.Add(this.rd200);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rd100);
            this.groupBox1.Location = new System.Drawing.Point(22, 94);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(157, 83);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // rd300
            // 
            this.rd300.AutoSize = true;
            this.rd300.Location = new System.Drawing.Point(85, 55);
            this.rd300.Margin = new System.Windows.Forms.Padding(2);
            this.rd300.Name = "rd300";
            this.rd300.Size = new System.Drawing.Size(64, 17);
            this.rd300.TabIndex = 5;
            this.rd300.TabStop = true;
            this.rd300.Text = "300 DPI";
            this.rd300.UseVisualStyleBackColor = true;
            this.rd300.CheckedChanged += new System.EventHandler(this.rd300_CheckedChanged);
            // 
            // rd200
            // 
            this.rd200.AutoSize = true;
            this.rd200.Checked = true;
            this.rd200.Location = new System.Drawing.Point(85, 35);
            this.rd200.Margin = new System.Windows.Forms.Padding(2);
            this.rd200.Name = "rd200";
            this.rd200.Size = new System.Drawing.Size(64, 17);
            this.rd200.TabIndex = 4;
            this.rd200.TabStop = true;
            this.rd200.Text = "200 DPI";
            this.rd200.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Resolution:";
            // 
            // cmbSource
            // 
            this.cmbSource.FormattingEnabled = true;
            this.cmbSource.Items.AddRange(new object[] {
            "Document feeder",
            "Flatbed"});
            this.cmbSource.Location = new System.Drawing.Point(135, 38);
            this.cmbSource.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Size = new System.Drawing.Size(203, 21);
            this.cmbSource.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Source";
            // 
            // cmbPage
            // 
            this.cmbPage.FormattingEnabled = true;
            this.cmbPage.Location = new System.Drawing.Point(135, 67);
            this.cmbPage.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPage.Name = "cmbPage";
            this.cmbPage.Size = new System.Drawing.Size(203, 21);
            this.cmbPage.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Page Size";
            // 
            // rdGray
            // 
            this.rdGray.AutoSize = true;
            this.rdGray.Location = new System.Drawing.Point(74, 55);
            this.rdGray.Margin = new System.Windows.Forms.Padding(2);
            this.rdGray.Name = "rdGray";
            this.rdGray.Size = new System.Drawing.Size(72, 17);
            this.rdGray.TabIndex = 8;
            this.rdGray.TabStop = true;
            this.rdGray.Text = "Grayscale";
            this.rdGray.UseVisualStyleBackColor = true;
            this.rdGray.CheckedChanged += new System.EventHandler(this.rdGray_CheckedChanged);
            // 
            // rdColor
            // 
            this.rdColor.AutoSize = true;
            this.rdColor.Checked = true;
            this.rdColor.Location = new System.Drawing.Point(74, 35);
            this.rdColor.Margin = new System.Windows.Forms.Padding(2);
            this.rdColor.Name = "rdColor";
            this.rdColor.Size = new System.Drawing.Size(49, 17);
            this.rdColor.TabIndex = 7;
            this.rdColor.TabStop = true;
            this.rdColor.Text = "Color";
            this.rdColor.UseVisualStyleBackColor = true;
            this.rdColor.CheckedChanged += new System.EventHandler(this.rdColor_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Picture:";
            // 
            // rdBlack
            // 
            this.rdBlack.AutoSize = true;
            this.rdBlack.Location = new System.Drawing.Point(74, 15);
            this.rdBlack.Margin = new System.Windows.Forms.Padding(2);
            this.rdBlack.Name = "rdBlack";
            this.rdBlack.Size = new System.Drawing.Size(85, 17);
            this.rdBlack.TabIndex = 6;
            this.rdBlack.TabStop = true;
            this.rdBlack.Text = "Black/White";
            this.rdBlack.UseVisualStyleBackColor = true;
            this.rdBlack.CheckedChanged += new System.EventHandler(this.rdBlack_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(160, 185);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 26);
            this.button1.TabIndex = 9;
            this.button1.Text = "Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(255, 185);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 26);
            this.button2.TabIndex = 10;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdColor);
            this.groupBox2.Controls.Add(this.rdGray);
            this.groupBox2.Controls.Add(this.rdBlack);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(187, 94);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(173, 83);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmbSource);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.cmbScanner);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbPage);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(380, 222);
            this.panel2.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 33);
            this.panel1.TabIndex = 11;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::Scannex.Properties.Resources.error;
            this.pictureBox2.Location = new System.Drawing.Point(353, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(14, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Scan pages";
            // 
            // frmScanPages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(380, 255);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScanPages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan Pages";
            this.Load += new System.EventHandler(this.frmScanPages_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbScanner;
        private System.Windows.Forms.RadioButton rd100;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdGray;
        private System.Windows.Forms.RadioButton rdColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdBlack;
        private System.Windows.Forms.RadioButton rd300;
        private System.Windows.Forms.RadioButton rd200;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label6;
    }
}