namespace PMQLCHDIENTHOAI
{
    partial class frmXuatSuaChua
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
            this.CryViewer.ShowGroupTreeButton = false;
            this.CryViewer.Size = new System.Drawing.Size(771, 441);
            this.CryViewer.TabIndex = 0;
            this.CryViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.CryViewer.Load += new System.EventHandler(this.CryViewer_Load);
            // 
            // frmXuatSuaChua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 441);
            this.Controls.Add(this.CryViewer);
            this.Name = "frmXuatSuaChua";
            this.Text = "frmXuatSuaChua";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer CryViewer;
    }
}