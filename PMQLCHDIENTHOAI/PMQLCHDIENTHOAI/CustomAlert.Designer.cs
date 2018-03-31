namespace PMQLCHDIENTHOAI
{
    partial class CustomAlert
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
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblmessage = new MetroFramework.Controls.MetroTile();
            this.lblExit = new System.Windows.Forms.Label();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.timeout = new System.Windows.Forms.Timer(this.components);
            this.show = new System.Windows.Forms.Timer(this.components);
            this.close = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.lblmessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::PMQLCHDIENTHOAI.Properties.Resources.customer_service;
            this.pictureBox1.Location = new System.Drawing.Point(1, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblmessage
            // 
            this.lblmessage.ActiveControl = null;
            this.lblmessage.BackColor = System.Drawing.Color.Transparent;
            this.lblmessage.Controls.Add(this.lblExit);
            this.lblmessage.DisplayFocusBorder = false;
            this.lblmessage.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblmessage.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblmessage.Location = new System.Drawing.Point(102, 0);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(309, 92);
            this.lblmessage.TabIndex = 1;
            this.lblmessage.Text = "metroTile1";
            this.lblmessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblmessage.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.lblmessage.UseCustomBackColor = true;
            this.lblmessage.UseCustomForeColor = true;
            this.lblmessage.UseMnemonic = false;
            this.lblmessage.UseSelectable = true;
            this.lblmessage.UseStyleColors = true;
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExit.ForeColor = System.Drawing.Color.White;
            this.lblExit.Location = new System.Drawing.Point(284, 0);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(25, 24);
            this.lblExit.TabIndex = 0;
            this.lblExit.Text = "X";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // timeout
            // 
            this.timeout.Enabled = true;
            this.timeout.Interval = 5000;
            this.timeout.Tick += new System.EventHandler(this.timeout_Tick);
            // 
            // show
            // 
            this.show.Interval = 50;
            this.show.Tick += new System.EventHandler(this.show_Tick);
            // 
            // close
            // 
            this.close.Tick += new System.EventHandler(this.close_Tick);
            // 
            // CustomAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(137)))), ((int)(((byte)(88)))));
            this.ClientSize = new System.Drawing.Size(411, 92);
            this.Controls.Add(this.lblmessage);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomAlert";
            this.Opacity = 0.95D;
            this.Text = "CustomAlert";
            this.Load += new System.EventHandler(this.CustomAlert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.lblmessage.ResumeLayout(false);
            this.lblmessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroTile lblmessage;
        private System.Windows.Forms.Label lblExit;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Timer timeout;
        private System.Windows.Forms.Timer show;
        private System.Windows.Forms.Timer close;
    }
}