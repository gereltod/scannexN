namespace Scannex
{
    partial class frmScannerNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScannerNew));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnImport = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbDoctype = new System.Windows.Forms.ComboBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSelected = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlPictures = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlUnselected = new System.Windows.Forms.Panel();
            this.lblUnselected = new System.Windows.Forms.Label();
            this.pnlOnly = new System.Windows.Forms.Panel();
            this.lblOnly = new System.Windows.Forms.Label();
            this.pnlAll = new System.Windows.Forms.Panel();
            this.lblAll = new System.Windows.Forms.Label();
            this.lblZoomOut = new System.Windows.Forms.Label();
            this.lblZoomIn = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox5);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.btnImport);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(999, 60);
            this.panel1.TabIndex = 0;
            this.panel1.DoubleClick += new System.EventHandler(this.panel1_DoubleClick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::Scannex.Properties.Resources.error;
            this.pictureBox3.Location = new System.Drawing.Point(964, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 36);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(787, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "Sage Corp, LLC.";
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox5.Image = global::Scannex.Properties.Resources.help;
            this.pictureBox5.Location = new System.Drawing.Point(500, 13);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(40, 36);
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox4.Image = global::Scannex.Properties.Resources.settings;
            this.pictureBox4.Location = new System.Drawing.Point(400, 12);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(85, 36);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // btnImport
            // 
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.Image = global::Scannex.Properties.Resources.import1;
            this.btnImport.Location = new System.Drawing.Point(301, 12);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(85, 36);
            this.btnImport.TabIndex = 2;
            this.btnImport.TabStop = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::Scannex.Properties.Resources.scan;
            this.pictureBox2.Location = new System.Drawing.Point(93, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(170, 38);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Scannex.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(22, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.pnlAdd);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtComment);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.cmbDoctype);
            this.panel2.Controls.Add(this.cmbLocation);
            this.panel2.Controls.Add(this.cmbEmployee);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(749, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 417);
            this.panel2.TabIndex = 1;
            // 
            // pnlAdd
            // 
            this.pnlAdd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlAdd.Location = new System.Drawing.Point(5, 146);
            this.pnlAdd.Margin = new System.Windows.Forms.Padding(2);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(242, 191);
            this.pnlAdd.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(18, 339);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "Custom longtext field label:";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(21, 356);
            this.txtComment.Margin = new System.Windows.Forms.Padding(2);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(204, 54);
            this.txtComment.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "Document Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Owner";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.button1.Location = new System.Drawing.Point(21, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(204, 33);
            this.button1.TabIndex = 3;
            this.button1.Text = "Upload document";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbDoctype
            // 
            this.cmbDoctype.FormattingEnabled = true;
            this.cmbDoctype.Location = new System.Drawing.Point(21, 118);
            this.cmbDoctype.Name = "cmbDoctype";
            this.cmbDoctype.Size = new System.Drawing.Size(204, 23);
            this.cmbDoctype.TabIndex = 2;
            this.cmbDoctype.SelectedIndexChanged += new System.EventHandler(this.cmbDoctype_SelectedIndexChanged);
            // 
            // cmbLocation
            // 
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(21, 64);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(204, 23);
            this.cmbLocation.TabIndex = 1;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(21, 35);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(204, 23);
            this.cmbEmployee.TabIndex = 0;
            this.cmbEmployee.SelectedIndexChanged += new System.EventHandler(this.cmbEmployee_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.lblSelected);
            this.panel3.Controls.Add(this.lblStatus);
            this.panel3.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 477);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(999, 33);
            this.panel3.TabIndex = 2;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseDown);
            this.panel3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseUp);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(891, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "Scannex v2.0.1";
            // 
            // lblSelected
            // 
            this.lblSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelected.AutoSize = true;
            this.lblSelected.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelected.ForeColor = System.Drawing.Color.DimGray;
            this.lblSelected.Location = new System.Drawing.Point(608, 8);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(95, 14);
            this.lblSelected.TabIndex = 1;
            this.lblSelected.Text = "Selected of pages";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.Location = new System.Drawing.Point(13, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(69, 14);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Logged in as";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.panel4.Controls.Add(this.pnlPictures);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(749, 417);
            this.panel4.TabIndex = 3;
            // 
            // pnlPictures
            // 
            this.pnlPictures.AutoScroll = true;
            this.pnlPictures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPictures.Location = new System.Drawing.Point(0, 50);
            this.pnlPictures.Name = "pnlPictures";
            this.pnlPictures.Size = new System.Drawing.Size(749, 367);
            this.pnlPictures.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel5.Controls.Add(this.pnlUnselected);
            this.panel5.Controls.Add(this.lblUnselected);
            this.panel5.Controls.Add(this.pnlOnly);
            this.panel5.Controls.Add(this.lblOnly);
            this.panel5.Controls.Add(this.pnlAll);
            this.panel5.Controls.Add(this.lblAll);
            this.panel5.Controls.Add(this.lblZoomOut);
            this.panel5.Controls.Add(this.lblZoomIn);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.ForeColor = System.Drawing.Color.DimGray;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(749, 50);
            this.panel5.TabIndex = 0;
            // 
            // pnlUnselected
            // 
            this.pnlUnselected.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlUnselected.ForeColor = System.Drawing.Color.DarkGray;
            this.pnlUnselected.Location = new System.Drawing.Point(208, 35);
            this.pnlUnselected.Name = "pnlUnselected";
            this.pnlUnselected.Size = new System.Drawing.Size(115, 2);
            this.pnlUnselected.TabIndex = 11;
            // 
            // lblUnselected
            // 
            this.lblUnselected.AutoSize = true;
            this.lblUnselected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUnselected.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnselected.ForeColor = System.Drawing.Color.DarkGray;
            this.lblUnselected.Location = new System.Drawing.Point(213, 17);
            this.lblUnselected.Name = "lblUnselected";
            this.lblUnselected.Size = new System.Drawing.Size(108, 16);
            this.lblUnselected.TabIndex = 10;
            this.lblUnselected.Text = "Unselected only";
            this.lblUnselected.Click += new System.EventHandler(this.lblUnselected_Click);
            // 
            // pnlOnly
            // 
            this.pnlOnly.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlOnly.ForeColor = System.Drawing.Color.DarkGray;
            this.pnlOnly.Location = new System.Drawing.Point(110, 35);
            this.pnlOnly.Name = "pnlOnly";
            this.pnlOnly.Size = new System.Drawing.Size(98, 2);
            this.pnlOnly.TabIndex = 9;
            // 
            // lblOnly
            // 
            this.lblOnly.AutoSize = true;
            this.lblOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblOnly.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnly.ForeColor = System.Drawing.Color.DarkGray;
            this.lblOnly.Location = new System.Drawing.Point(114, 17);
            this.lblOnly.Name = "lblOnly";
            this.lblOnly.Size = new System.Drawing.Size(93, 16);
            this.lblOnly.TabIndex = 8;
            this.lblOnly.Text = "Selected only";
            this.lblOnly.Click += new System.EventHandler(this.lblOnly_Click);
            // 
            // pnlAll
            // 
            this.pnlAll.BackColor = System.Drawing.Color.Red;
            this.pnlAll.Location = new System.Drawing.Point(13, 35);
            this.pnlAll.Name = "pnlAll";
            this.pnlAll.Size = new System.Drawing.Size(98, 2);
            this.pnlAll.TabIndex = 7;
            // 
            // lblAll
            // 
            this.lblAll.AutoSize = true;
            this.lblAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAll.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAll.ForeColor = System.Drawing.Color.Black;
            this.lblAll.Location = new System.Drawing.Point(13, 17);
            this.lblAll.Name = "lblAll";
            this.lblAll.Size = new System.Drawing.Size(98, 16);
            this.lblAll.TabIndex = 6;
            this.lblAll.Text = "View all pages";
            this.lblAll.Click += new System.EventHandler(this.lblAll_Click);
            // 
            // lblZoomOut
            // 
            this.lblZoomOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblZoomOut.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoomOut.Location = new System.Drawing.Point(527, 17);
            this.lblZoomOut.Name = "lblZoomOut";
            this.lblZoomOut.Size = new System.Drawing.Size(67, 16);
            this.lblZoomOut.TabIndex = 5;
            this.lblZoomOut.Text = "Zoom out";
            this.lblZoomOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblZoomOut.Click += new System.EventHandler(this.lblZoomOut_Click);
            this.lblZoomOut.MouseLeave += new System.EventHandler(this.lblZoomIn_MouseLeave);
            this.lblZoomOut.MouseHover += new System.EventHandler(this.lblZoomIn_MouseHover);
            // 
            // lblZoomIn
            // 
            this.lblZoomIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblZoomIn.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoomIn.Location = new System.Drawing.Point(463, 17);
            this.lblZoomIn.Name = "lblZoomIn";
            this.lblZoomIn.Size = new System.Drawing.Size(58, 16);
            this.lblZoomIn.TabIndex = 4;
            this.lblZoomIn.Text = "Zoom in";
            this.lblZoomIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblZoomIn.Click += new System.EventHandler(this.lblZoomIn_Click);
            this.lblZoomIn.MouseLeave += new System.EventHandler(this.lblZoomIn_MouseLeave);
            this.lblZoomIn.MouseHover += new System.EventHandler(this.lblZoomIn_MouseHover);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(702, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "90°L";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            this.label4.MouseLeave += new System.EventHandler(this.lblZoomIn_MouseLeave);
            this.label4.MouseHover += new System.EventHandler(this.lblZoomIn_MouseHover);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(653, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "90°R";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            this.label3.MouseLeave += new System.EventHandler(this.lblZoomIn_MouseLeave);
            this.label3.MouseHover += new System.EventHandler(this.lblZoomIn_MouseHover);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(530, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rotate selected:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmScannerNew
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(999, 510);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmScannerNew";
            this.Text = "frmScannerNew";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmScannerNew_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmScannerNew_KeyDown_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox btnImport;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlPictures;
        private System.Windows.Forms.Label lblZoomOut;
        private System.Windows.Forms.Label lblZoomIn;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox cmbDoctype;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Label lblAll;
        private System.Windows.Forms.Panel pnlAll;
        private System.Windows.Forms.Panel pnlUnselected;
        private System.Windows.Forms.Label lblUnselected;
        private System.Windows.Forms.Panel pnlOnly;
        private System.Windows.Forms.Label lblOnly;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}