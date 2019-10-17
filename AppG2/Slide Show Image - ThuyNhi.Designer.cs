namespace AppG2
{
    partial class Slide_Show_Image___ThuyNhi
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
            this.lnkPic = new System.Windows.Forms.LinkLabel();
            this.picAnh = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkPic
            // 
            this.lnkPic.AutoSize = true;
            this.lnkPic.Location = new System.Drawing.Point(288, 296);
            this.lnkPic.Name = "lnkPic";
            this.lnkPic.Size = new System.Drawing.Size(82, 13);
            this.lnkPic.TabIndex = 1;
            this.lnkPic.TabStop = true;
            this.lnkPic.Text = "Chọn folder ảnh";
            this.lnkPic.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkPic_LinkClicked);
            // 
            // picAnh
            // 
            this.picAnh.Location = new System.Drawing.Point(167, 52);
            this.picAnh.Name = "picAnh";
            this.picAnh.Size = new System.Drawing.Size(327, 217);
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAnh.TabIndex = 0;
            this.picAnh.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Slide_Show_Image___ThuyNhi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 370);
            this.Controls.Add(this.lnkPic);
            this.Controls.Add(this.picAnh);
            this.Name = "Slide_Show_Image___ThuyNhi";
            this.Text = "Slide_Show_Image___ThuyNhi";
            this.Load += new System.EventHandler(this.Slide_Show_Image___ThuyNhi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picAnh;
        private System.Windows.Forms.LinkLabel lnkPic;
        private System.Windows.Forms.Timer timer1;
    }
}