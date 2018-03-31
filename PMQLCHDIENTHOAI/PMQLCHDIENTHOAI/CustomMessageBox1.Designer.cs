namespace PMQLCHDIENTHOAI
{
    partial class CustomMessageBox1
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
            this.btnYes = new System.Windows.Forms.Button();
            this.txtNoiDung = new MetroFramework.Controls.MetroTile();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.btnYes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            this.btnYes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.Image = global::PMQLCHDIENTHOAI.Properties.Resources.icon;
            this.btnYes.Location = new System.Drawing.Point(125, 80);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(174, 47);
            this.btnYes.TabIndex = 1;
            this.btnYes.Text = "Thoát";
            this.btnYes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.ActiveControl = null;
            this.txtNoiDung.DisplayFocusBorder = false;
            this.txtNoiDung.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtNoiDung.Location = new System.Drawing.Point(0, 0);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(409, 74);
            this.txtNoiDung.TabIndex = 4;
            this.txtNoiDung.Text = "Nội dung";
            this.txtNoiDung.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtNoiDung.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtNoiDung.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.txtNoiDung.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.txtNoiDung.UseCustomBackColor = true;
            this.txtNoiDung.UseCustomForeColor = true;
            this.txtNoiDung.UseSelectable = true;
            this.txtNoiDung.UseStyleColors = true;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // CustomMessageBox1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(409, 128);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.btnYes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomMessageBox1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomMessageBox1";
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Button btnYes;
        private MetroFramework.Controls.MetroTile txtNoiDung;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
    }
}