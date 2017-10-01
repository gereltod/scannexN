namespace Scannex
{
    partial class frmScanner
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPageUp = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblPage = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDoctype = new System.Windows.Forms.ComboBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button8 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pImageUp = new System.Windows.Forms.PictureBox();
            this.pImage = new System.Windows.Forms.PictureBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pImageUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pImage)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.lblPageUp);
            this.groupBox1.Controls.Add(this.pImageUp);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.lblFile);
            this.groupBox1.Controls.Add(this.lblPage);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbDoctype);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.cmbEmployee);
            this.groupBox1.Controls.Add(this.pImage);
            this.groupBox1.Controls.Add(this.btnBack);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(961, 423);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblPageUp
            // 
            this.lblPageUp.AutoSize = true;
            this.lblPageUp.Location = new System.Drawing.Point(497, 34);
            this.lblPageUp.Name = "lblPageUp";
            this.lblPageUp.Size = new System.Drawing.Size(82, 15);
            this.lblPageUp.TabIndex = 12;
            this.lblPageUp.Text = "Page 0 of 0";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(266, 232);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(53, 44);
            this.button5.TabIndex = 9;
            this.button5.Text = "[";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(266, 182);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(53, 44);
            this.button4.TabIndex = 8;
            this.button4.Text = "]";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(6, 395);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(71, 15);
            this.lblFile.TabIndex = 7;
            this.lblFile.Text = "File name:";
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(178, 34);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(82, 15);
            this.lblPage.TabIndex = 6;
            this.lblPage.Text = "Page 0 of 0";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.Font = new System.Drawing.Font("MS UI Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button3.Location = new System.Drawing.Point(218, 196);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 30);
            this.button3.TabIndex = 5;
            this.button3.Text = ">";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(687, 114);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(249, 77);
            this.textBox1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(606, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Comments:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(606, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Doc Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(606, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Location:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(606, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Employee:";
            // 
            // cmbDoctype
            // 
            this.cmbDoctype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoctype.FormattingEnabled = true;
            this.cmbDoctype.Location = new System.Drawing.Point(687, 85);
            this.cmbDoctype.Name = "cmbDoctype";
            this.cmbDoctype.Size = new System.Drawing.Size(249, 23);
            this.cmbDoctype.TabIndex = 2;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(687, 56);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(249, 23);
            this.cmbLocation.TabIndex = 2;
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(687, 26);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(249, 23);
            this.cmbEmployee.TabIndex = 2;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(278, 441);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(124, 33);
            this.button6.TabIndex = 1;
            this.button6.Text = "Open Scan";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(278, 480);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(124, 33);
            this.button7.TabIndex = 2;
            this.button7.Text = "Import";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Image = global::Scannex.Properties.Resources.file__2_;
            this.button8.Location = new System.Drawing.Point(266, 332);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(53, 44);
            this.button8.TabIndex = 13;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button2
            // 
            this.button2.Image = global::Scannex.Properties.Resources.file__1_;
            this.button2.Location = new System.Drawing.Point(266, 282);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 44);
            this.button2.TabIndex = 13;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pImageUp
            // 
            this.pImageUp.BackColor = System.Drawing.Color.White;
            this.pImageUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pImageUp.Location = new System.Drawing.Point(325, 51);
            this.pImageUp.Name = "pImageUp";
            this.pImageUp.Size = new System.Drawing.Size(254, 330);
            this.pImageUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pImageUp.TabIndex = 10;
            this.pImageUp.TabStop = false;
            // 
            // pImage
            // 
            this.pImage.BackColor = System.Drawing.Color.White;
            this.pImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pImage.Location = new System.Drawing.Point(6, 52);
            this.pImage.Name = "pImage";
            this.pImage.Size = new System.Drawing.Size(254, 330);
            this.pImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pImage.TabIndex = 1;
            this.pImage.TabStop = false;
            this.pImage.DoubleClick += new System.EventHandler(this.pImage_DoubleClick);
            // 
            // btnBack
            // 
            this.btnBack.Image = global::Scannex.Properties.Resources.back;
            this.btnBack.Location = new System.Drawing.Point(266, 102);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(53, 44);
            this.btnBack.TabIndex = 0;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // button1
            // 
            this.button1.Image = global::Scannex.Properties.Resources.next__2_;
            this.button1.Location = new System.Drawing.Point(266, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 44);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(597, 332);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(87, 36);
            this.button9.TabIndex = 14;
            this.button9.Text = "Upload";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // frmScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1001, 562);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmScanner";
            this.Text = "Create Documents";
            this.Load += new System.EventHandler(this.frmScanner_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pImageUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDoctype;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.PictureBox pImage;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.PictureBox pImageUp;
        private System.Windows.Forms.Label lblPageUp;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button9;
    }
}