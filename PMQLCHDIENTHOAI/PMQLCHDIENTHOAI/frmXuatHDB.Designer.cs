namespace PMQLCHDIENTHOAI
{
    partial class frmXuatHDB
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
            this.CryViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // CryViewer
            // 
            this.CryViewer.ActiveViewIndex = -1;
            this.CryViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CryViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.CryViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CryViewer.Location = new System.Drawing.Point(0, 0);
            this.CryViewer.Name = "CryViewer";
            this.CryViewer.ShowLogo = false;
            this.CryViewer.ShowTextSearchButton = false;
            this.CryViewer.Size = new System.Drawing.Size(833, 387);
            this.CryViewer.TabIndex = 0;
            this.CryViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmXuatHDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 387);
            this.Controls.Add(this.CryViewer);
            this.Name = "frmXuatHDB";
            this.Text = "frmXuatHDB";
            this.Load += new System.EventHandler(this.frmXuatHDB_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer CryViewer;
    }
}