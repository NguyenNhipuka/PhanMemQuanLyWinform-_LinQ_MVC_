namespace PMQLCHDIENTHOAI
{
    partial class CustomDialog1
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
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTieuDe = new MetroFramework.Controls.MetroTile();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtNoiDung = new MetroFramework.Controls.MetroTile();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.pnlButton.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnNo);
            this.pnlButton.Controls.Add(this.btnYes);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 126);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(425, 47);
            this.pnlButton.TabIndex = 6;
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.btnNo.FlatAppearance.BorderSize = 0;
            this.btnNo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            this.btnNo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.Image = global::PMQLCHDIENTHOAI.Properties.Resources.cancel__1_1;
            this.btnNo.Location = new System.Drawing.Point(244, 0);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(181, 47);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "No";
            this.btnNo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnYes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.btnYes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            this.btnYes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.Image = global::PMQLCHDIENTHOAI.Properties.Resources.icon;
            this.btnYes.Location = new System.Drawing.Point(0, 0);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(174, 47);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "Yes";
            this.btnYes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTieuDe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 42);
            this.panel1.TabIndex = 7;
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.ActiveControl = null;
            this.txtTieuDe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.txtTieuDe.DisplayFocusBorder = false;
            this.txtTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTieuDe.Location = new System.Drawing.Point(0, 0);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(425, 42);
            this.txtTieuDe.TabIndex = 46;
            this.txtTieuDe.Text = "Tiêu đề";
            this.txtTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtTieuDe.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtTieuDe.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.txtTieuDe.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.txtTieuDe.UseCustomBackColor = true;
            this.txtTieuDe.UseCustomForeColor = true;
            this.txtTieuDe.UseMnemonic = false;
            this.txtTieuDe.UseSelectable = true;
            this.txtTieuDe.UseStyleColors = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtNoiDung);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(425, 84);
            this.panel2.TabIndex = 8;
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.ActiveControl = null;
            this.txtNoiDung.DisplayFocusBorder = false;
            this.txtNoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNoiDung.Location = new System.Drawing.Point(0, 0);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(425, 84);
            this.txtNoiDung.TabIndex = 3;
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
            this.bunifuDragControl1.TargetControl = this.txtTieuDe;
            this.bunifuDragControl1.Vertical = true;
            // 
            // CustomDialog1
            // 
            this.AcceptButton = this.btnYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnNo;
            this.ClientSize = new System.Drawing.Size(425, 173);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomDialog1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomDialog1";
            this.pnlButton.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroTile txtNoiDung;
        private MetroFramework.Controls.MetroTile txtTieuDe;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
    }
}