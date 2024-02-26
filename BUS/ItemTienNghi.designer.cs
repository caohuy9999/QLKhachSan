namespace QLKhachSan.fHelper.Phong
{
    partial class ItemTienNghi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemTienNghi));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.lblNameTienNghi = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.pnlMain.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.pictureBox);
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(181, 193);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.Controls.Add(this.lblNameTienNghi);
            this.pnlTop.Controls.Add(this.lblTonKho);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(179, 20);
            this.pnlTop.TabIndex = 89;
            // 
            // lblTonKho
            // 
            this.lblTonKho.BackColor = System.Drawing.Color.Gray;
            this.lblTonKho.Cursor = System.Windows.Forms.Cursors.Cross;
            this.lblTonKho.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTonKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTonKho.ForeColor = System.Drawing.Color.White;
            this.lblTonKho.Location = new System.Drawing.Point(154, 0);
            this.lblTonKho.Name = "lblTonKho";
            this.lblTonKho.Size = new System.Drawing.Size(25, 20);
            this.lblTonKho.TabIndex = 89;
            this.lblTonKho.Text = "0";
            this.lblTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameTienNghi
            // 
            this.lblNameTienNghi.BackColor = System.Drawing.Color.Transparent;
            this.lblNameTienNghi.Cursor = System.Windows.Forms.Cursors.No;
            this.lblNameTienNghi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNameTienNghi.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameTienNghi.ForeColor = System.Drawing.Color.Black;
            this.lblNameTienNghi.Location = new System.Drawing.Point(0, 0);
            this.lblNameTienNghi.Name = "lblNameTienNghi";
            this.lblNameTienNghi.Size = new System.Drawing.Size(154, 20);
            this.lblNameTienNghi.TabIndex = 90;
            this.lblNameTienNghi.Text = "Tủ Lạnh";
            this.lblNameTienNghi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox
            // 
            this.pictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(0, 20);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(179, 171);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 90;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // ItemTienNghi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "ItemTienNghi";
            this.Size = new System.Drawing.Size(181, 193);
            this.pnlMain.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTop;
        internal Bunifu.Framework.UI.BunifuCustomLabel lblNameTienNghi;
        private System.Windows.Forms.Label lblTonKho;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}
