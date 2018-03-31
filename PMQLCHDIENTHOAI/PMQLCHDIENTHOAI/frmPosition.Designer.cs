namespace PMQLCHDIENTHOAI
{
    partial class frmPosition
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
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btnSaveCV_Add = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnExit = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnUpdateCV_Add = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnDeleteCV_Add = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnAddCV_Add = new Bunifu.Framework.UI.BunifuImageButton();
            this.txtNameCV_Add = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txtIdCV_Add = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel7 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel12 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.cHUCVUDataGridView = new System.Windows.Forms.DataGridView();
            this.cHUCVUBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new PMQLCHDIENTHOAI.DataSet();
            this.cHUCVUTableAdapter = new PMQLCHDIENTHOAI.DataSetTableAdapters.CHUCVUTableAdapter();
            this.tableAdapterManager = new PMQLCHDIENTHOAI.DataSetTableAdapters.TableAdapterManager();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveCV_Add)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdateCV_Add)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteCV_Add)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddCV_Add)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cHUCVUDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cHUCVUBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PMQLCHDIENTHOAI.Properties.Resources.team_leader;
            this.pictureBox1.Location = new System.Drawing.Point(15, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 73);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.DarkGray;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(114, 41);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(162, 24);
            this.bunifuCustomLabel1.TabIndex = 28;
            this.bunifuCustomLabel1.Text = "Quản lý chức vụ";
            // 
            // btnSaveCV_Add
            // 
            this.btnSaveCV_Add.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveCV_Add.Enabled = false;
            this.btnSaveCV_Add.Image = global::PMQLCHDIENTHOAI.Properties.Resources.disquette;
            this.btnSaveCV_Add.ImageActive = null;
            this.btnSaveCV_Add.Location = new System.Drawing.Point(249, 366);
            this.btnSaveCV_Add.Name = "btnSaveCV_Add";
            this.btnSaveCV_Add.Size = new System.Drawing.Size(55, 55);
            this.btnSaveCV_Add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSaveCV_Add.TabIndex = 68;
            this.btnSaveCV_Add.TabStop = false;
            this.btnSaveCV_Add.Zoom = 10;
            this.btnSaveCV_Add.Click += new System.EventHandler(this.btnSaveCV_Add_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Image = global::PMQLCHDIENTHOAI.Properties.Resources.x_button;
            this.btnExit.ImageActive = null;
            this.btnExit.Location = new System.Drawing.Point(898, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 40);
            this.btnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnExit.TabIndex = 69;
            this.btnExit.TabStop = false;
            this.btnExit.Zoom = 10;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpdateCV_Add
            // 
            this.btnUpdateCV_Add.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdateCV_Add.Image = global::PMQLCHDIENTHOAI.Properties.Resources.rotation;
            this.btnUpdateCV_Add.ImageActive = null;
            this.btnUpdateCV_Add.Location = new System.Drawing.Point(188, 366);
            this.btnUpdateCV_Add.Name = "btnUpdateCV_Add";
            this.btnUpdateCV_Add.Size = new System.Drawing.Size(55, 55);
            this.btnUpdateCV_Add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnUpdateCV_Add.TabIndex = 70;
            this.btnUpdateCV_Add.TabStop = false;
            this.btnUpdateCV_Add.Zoom = 10;
            this.btnUpdateCV_Add.Click += new System.EventHandler(this.btnUpdateCV_Add_Click);
            // 
            // btnDeleteCV_Add
            // 
            this.btnDeleteCV_Add.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteCV_Add.Image = global::PMQLCHDIENTHOAI.Properties.Resources.paper_bin;
            this.btnDeleteCV_Add.ImageActive = null;
            this.btnDeleteCV_Add.Location = new System.Drawing.Point(117, 366);
            this.btnDeleteCV_Add.Name = "btnDeleteCV_Add";
            this.btnDeleteCV_Add.Size = new System.Drawing.Size(55, 55);
            this.btnDeleteCV_Add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnDeleteCV_Add.TabIndex = 71;
            this.btnDeleteCV_Add.TabStop = false;
            this.btnDeleteCV_Add.Zoom = 10;
            this.btnDeleteCV_Add.Click += new System.EventHandler(this.btnDeleteCV_Add_Click);
            // 
            // btnAddCV_Add
            // 
            this.btnAddCV_Add.BackColor = System.Drawing.Color.Transparent;
            this.btnAddCV_Add.Image = global::PMQLCHDIENTHOAI.Properties.Resources.add_phone;
            this.btnAddCV_Add.ImageActive = null;
            this.btnAddCV_Add.Location = new System.Drawing.Point(56, 366);
            this.btnAddCV_Add.Name = "btnAddCV_Add";
            this.btnAddCV_Add.Size = new System.Drawing.Size(55, 55);
            this.btnAddCV_Add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAddCV_Add.TabIndex = 67;
            this.btnAddCV_Add.TabStop = false;
            this.btnAddCV_Add.Zoom = 10;
            this.btnAddCV_Add.Click += new System.EventHandler(this.btnAddCV_Add_Click);
            // 
            // txtNameCV_Add
            // 
            this.txtNameCV_Add.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNameCV_Add.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameCV_Add.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNameCV_Add.HintForeColor = System.Drawing.Color.Empty;
            this.txtNameCV_Add.HintText = "";
            this.txtNameCV_Add.isPassword = false;
            this.txtNameCV_Add.LineFocusedColor = System.Drawing.Color.DeepSkyBlue;
            this.txtNameCV_Add.LineIdleColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(179)))), ((int)(((byte)(158)))));
            this.txtNameCV_Add.LineMouseHoverColor = System.Drawing.Color.LightSkyBlue;
            this.txtNameCV_Add.LineThickness = 1;
            this.txtNameCV_Add.Location = new System.Drawing.Point(61, 270);
            this.txtNameCV_Add.Margin = new System.Windows.Forms.Padding(4);
            this.txtNameCV_Add.Name = "txtNameCV_Add";
            this.txtNameCV_Add.Size = new System.Drawing.Size(229, 38);
            this.txtNameCV_Add.TabIndex = 64;
            this.txtNameCV_Add.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtIdCV_Add
            // 
            this.txtIdCV_Add.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIdCV_Add.Enabled = false;
            this.txtIdCV_Add.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdCV_Add.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtIdCV_Add.HintForeColor = System.Drawing.Color.Empty;
            this.txtIdCV_Add.HintText = "";
            this.txtIdCV_Add.isPassword = false;
            this.txtIdCV_Add.LineFocusedColor = System.Drawing.Color.DeepSkyBlue;
            this.txtIdCV_Add.LineIdleColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(179)))), ((int)(((byte)(158)))));
            this.txtIdCV_Add.LineMouseHoverColor = System.Drawing.Color.LightSkyBlue;
            this.txtIdCV_Add.LineThickness = 1;
            this.txtIdCV_Add.Location = new System.Drawing.Point(61, 170);
            this.txtIdCV_Add.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdCV_Add.Name = "txtIdCV_Add";
            this.txtIdCV_Add.Size = new System.Drawing.Size(229, 38);
            this.txtIdCV_Add.TabIndex = 65;
            this.txtIdCV_Add.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(603, 45);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(161, 20);
            this.bunifuCustomLabel2.TabIndex = 60;
            this.bunifuCustomLabel2.Text = "Danh sách chức vụ";
            // 
            // bunifuCustomLabel4
            // 
            this.bunifuCustomLabel4.AutoSize = true;
            this.bunifuCustomLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(93, 88);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(183, 20);
            this.bunifuCustomLabel4.TabIndex = 61;
            this.bunifuCustomLabel4.Text = "Thông tin loại chức vụ";
            // 
            // bunifuCustomLabel7
            // 
            this.bunifuCustomLabel7.AutoSize = true;
            this.bunifuCustomLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel7.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.bunifuCustomLabel7.Location = new System.Drawing.Point(57, 136);
            this.bunifuCustomLabel7.Name = "bunifuCustomLabel7";
            this.bunifuCustomLabel7.Size = new System.Drawing.Size(99, 20);
            this.bunifuCustomLabel7.TabIndex = 62;
            this.bunifuCustomLabel7.Text = "Mã chức vụ";
            // 
            // bunifuCustomLabel12
            // 
            this.bunifuCustomLabel12.AutoSize = true;
            this.bunifuCustomLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel12.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.bunifuCustomLabel12.Location = new System.Drawing.Point(51, 242);
            this.bunifuCustomLabel12.Name = "bunifuCustomLabel12";
            this.bunifuCustomLabel12.Size = new System.Drawing.Size(105, 20);
            this.bunifuCustomLabel12.TabIndex = 63;
            this.bunifuCustomLabel12.Text = "Tên chức vụ";
            // 
            // cHUCVUDataGridView
            // 
            this.cHUCVUDataGridView.AllowUserToAddRows = false;
            this.cHUCVUDataGridView.AllowUserToDeleteRows = false;
            this.cHUCVUDataGridView.AllowUserToOrderColumns = true;
            this.cHUCVUDataGridView.AutoGenerateColumns = false;
            this.cHUCVUDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cHUCVUDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cHUCVUDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.cHUCVUDataGridView.DataSource = this.cHUCVUBindingSource;
            this.cHUCVUDataGridView.Location = new System.Drawing.Point(403, 88);
            this.cHUCVUDataGridView.MultiSelect = false;
            this.cHUCVUDataGridView.Name = "cHUCVUDataGridView";
            this.cHUCVUDataGridView.ReadOnly = true;
            this.cHUCVUDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cHUCVUDataGridView.ShowCellToolTips = false;
            this.cHUCVUDataGridView.ShowEditingIcon = false;
            this.cHUCVUDataGridView.ShowRowErrors = false;
            this.cHUCVUDataGridView.Size = new System.Drawing.Size(514, 235);
            this.cHUCVUDataGridView.TabIndex = 71;
            // 
            // cHUCVUBindingSource
            // 
            this.cHUCVUBindingSource.DataMember = "CHUCVU";
            this.cHUCVUBindingSource.DataSource = this.dataSet;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cHUCVUTableAdapter
            // 
            this.cHUCVUTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CHUCVUTableAdapter = this.cHUCVUTableAdapter;
            this.tableAdapterManager.NHANVIENTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = PMQLCHDIENTHOAI.DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "MACV";
            this.dataGridViewTextBoxColumn1.FillWeight = 50.76142F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã chức vụ";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TENCV";
            this.dataGridViewTextBoxColumn2.FillWeight = 149.2386F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Tên chức vụ";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // frmPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(958, 502);
            this.Controls.Add(this.cHUCVUDataGridView);
            this.Controls.Add(this.btnSaveCV_Add);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdateCV_Add);
            this.Controls.Add(this.btnDeleteCV_Add);
            this.Controls.Add(this.btnAddCV_Add);
            this.Controls.Add(this.txtNameCV_Add);
            this.Controls.Add(this.txtIdCV_Add);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.bunifuCustomLabel7);
            this.Controls.Add(this.bunifuCustomLabel12);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPosition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmAddStaff";
            this.Load += new System.EventHandler(this.frmPosition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveCV_Add)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdateCV_Add)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteCV_Add)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddCV_Add)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cHUCVUDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cHUCVUBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuImageButton btnSaveCV_Add;
        private Bunifu.Framework.UI.BunifuImageButton btnExit;
        private Bunifu.Framework.UI.BunifuImageButton btnUpdateCV_Add;
        private Bunifu.Framework.UI.BunifuImageButton btnDeleteCV_Add;
        private Bunifu.Framework.UI.BunifuImageButton btnAddCV_Add;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtNameCV_Add;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtIdCV_Add;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel7;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel12;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource cHUCVUBindingSource;
        private DataSetTableAdapters.CHUCVUTableAdapter cHUCVUTableAdapter;
        private DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView cHUCVUDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
    }
}