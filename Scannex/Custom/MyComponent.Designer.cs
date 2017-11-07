namespace Scannex
{
    partial class MyComponent
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pImage = new System.Windows.Forms.PictureBox();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pImage)).BeginInit();
            // 
            // pImage
            // 
            this.pImage.Location = new System.Drawing.Point(0, 0);
            this.pImage.Name = "pImage";
            this.pImage.Size = new System.Drawing.Size(267, 354);
            this.pImage.TabIndex = 0;
            this.pImage.TabStop = false;
            // 
            // lblCount
            // 
            this.lblCount.Location = new System.Drawing.Point(0, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(100, 23);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "Page ";
            ((System.ComponentModel.ISupportInitialize)(this.pImage)).EndInit();

        }

        #endregion

        private System.Windows.Forms.PictureBox pImage;
        private System.Windows.Forms.Label lblCount;
    }
}
