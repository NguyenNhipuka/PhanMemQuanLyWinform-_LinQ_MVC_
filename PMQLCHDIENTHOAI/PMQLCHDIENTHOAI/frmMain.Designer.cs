namespace PMQLCHDIENTHOAI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblMANV = new MetroFramework.Controls.MetroLabel();
            this.txtMANV = new System.Windows.Forms.Label();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.pnlMenuLeft = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.pnlContainmenu = new System.Windows.Forms.Panel();
            this.btnSold = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnBuy = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnProFix = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnReport = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnStaff = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnSupplier = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnCustomer = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnDevice = new Bunifu.Framework.UI.BunifuFlatButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnHome = new Bunifu.Framework.UI.BunifuImageButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMenuTongle = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnHide = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnMin = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnExit = new Bunifu.Framework.UI.BunifuImageButton();
            this.pnlHeader.SuspendLayout();
            this.pnlMenuLeft.SuspendLayout();
            this.pnlContainmenu.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenuTongle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(89)))), ((int)(((byte)(174)))));
            this.pnlHeader.Controls.Add(this.btnHide);
            this.pnlHeader.Controls.Add(this.btnMin);
            this.pnlHeader.Controls.Add(this.lblMANV);
            this.pnlHeader.Controls.Add(this.txtMANV);
            this.pnlHeader.Controls.Add(this.btnExit);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1340, 38);
            this.pnlHeader.TabIndex = 1;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            this.pnlHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseMove);
            this.pnlHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseUp);
            // 
            // lblMANV
            // 
            this.lblMANV.AutoSize = true;
            this.lblMANV.Location = new System.Drawing.Point(12, 9);
            this.lblMANV.Name = "lblMANV";
            this.lblMANV.Size = new System.Drawing.Size(0, 0);
            this.lblMANV.TabIndex = 2;
            this.lblMANV.UseCustomBackColor = true;
            this.lblMANV.UseCustomForeColor = true;
            this.lblMANV.UseStyleColors = true;
            // 
            // txtMANV
            // 
            this.txtMANV.AutoSize = true;
            this.txtMANV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtMANV.ForeColor = System.Drawing.Color.Aqua;
            this.txtMANV.Location = new System.Drawing.Point(12, 9);
            this.txtMANV.Name = "txtMANV";
            this.txtMANV.Size = new System.Drawing.Size(0, 20);
            this.txtMANV.TabIndex = 2;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.pnlHeader;
            this.bunifuDragControl1.Vertical = true;
            // 
            // pnlMenuLeft
            // 
            this.pnlMenuLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(112)))), ((int)(((byte)(232)))));
            this.pnlMenuLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlMenuLeft.BackgroundImage")));
            this.pnlMenuLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMenuLeft.Controls.Add(this.pnlContainmenu);
            this.pnlMenuLeft.Controls.Add(this.panel3);
            this.pnlMenuLeft.Controls.Add(this.panel1);
            this.pnlMenuLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenuLeft.GradientBottomLeft = System.Drawing.Color.SeaGreen;
            this.pnlMenuLeft.GradientBottomRight = System.Drawing.Color.SeaGreen;
            this.pnlMenuLeft.GradientTopLeft = System.Drawing.Color.SeaGreen;
            this.pnlMenuLeft.GradientTopRight = System.Drawing.Color.SeaGreen;
            this.pnlMenuLeft.Location = new System.Drawing.Point(0, 38);
            this.pnlMenuLeft.Name = "pnlMenuLeft";
            this.pnlMenuLeft.Quality = 10;
            this.pnlMenuLeft.Size = new System.Drawing.Size(206, 662);
            this.pnlMenuLeft.TabIndex = 0;
            // 
            // pnlContainmenu
            // 
            this.pnlContainmenu.AutoScroll = true;
            this.pnlContainmenu.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainmenu.Controls.Add(this.btnSold);
            this.pnlContainmenu.Controls.Add(this.btnBuy);
            this.pnlContainmenu.Controls.Add(this.btnProFix);
            this.pnlContainmenu.Controls.Add(this.btnReport);
            this.pnlContainmenu.Controls.Add(this.btnStaff);
            this.pnlContainmenu.Controls.Add(this.btnSupplier);
            this.pnlContainmenu.Controls.Add(this.btnCustomer);
            this.pnlContainmenu.Controls.Add(this.btnDevice);
            this.pnlContainmenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainmenu.ForeColor = System.Drawing.Color.OrangeRed;
            this.pnlContainmenu.Location = new System.Drawing.Point(0, 90);
            this.pnlContainmenu.Name = "pnlContainmenu";
            this.pnlContainmenu.Size = new System.Drawing.Size(206, 572);
            this.pnlContainmenu.TabIndex = 4;
            // 
            // btnSold
            // 
            this.btnSold.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnSold.BackColor = System.Drawing.Color.Transparent;
            this.btnSold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSold.BorderRadius = 0;
            this.btnSold.ButtonText = "Quản Lý Bán Hàng";
            this.btnSold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSold.DisabledColor = System.Drawing.Color.Gray;
            this.btnSold.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSold.Iconcolor = System.Drawing.Color.Transparent;
            this.btnSold.Iconimage = global::PMQLCHDIENTHOAI.Properties.Resources.shopping_cart;
            this.btnSold.Iconimage_right = null;
            this.btnSold.Iconimage_right_Selected = null;
            this.btnSold.Iconimage_Selected = null;
            this.btnSold.IconMarginLeft = 6;
            this.btnSold.IconMarginRight = 0;
            this.btnSold.IconRightVisible = true;
            this.btnSold.IconRightZoom = 0D;
            this.btnSold.IconVisible = true;
            this.btnSold.IconZoom = 50D;
            this.btnSold.IsTab = true;
            this.btnSold.Location = new System.Drawing.Point(0, 30);
            this.btnSold.Margin = new System.Windows.Forms.Padding(5);
            this.btnSold.Name = "btnSold";
            this.btnSold.Normalcolor = System.Drawing.Color.Transparent;
            this.btnSold.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnSold.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSold.selected = false;
            this.btnSold.Size = new System.Drawing.Size(214, 50);
            this.btnSold.TabIndex = 2;
            this.btnSold.Tag = "1";
            this.btnSold.Text = "Quản Lý Bán Hàng";
            this.btnSold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSold.Textcolor = System.Drawing.Color.White;
            this.btnSold.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSold.Click += new System.EventHandler(this.btnSold_Click);
            // 
            // btnBuy
            // 
            this.btnBuy.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnBuy.BackColor = System.Drawing.Color.Transparent;
            this.btnBuy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuy.BorderRadius = 0;
            this.btnBuy.ButtonText = "Quản Lý Nhập Hàng";
            this.btnBuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuy.DisabledColor = System.Drawing.Color.Gray;
            this.btnBuy.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuy.Iconcolor = System.Drawing.Color.Transparent;
            this.btnBuy.Iconimage = global::PMQLCHDIENTHOAI.Properties.Resources.strolley_with_packages;
            this.btnBuy.Iconimage_right = null;
            this.btnBuy.Iconimage_right_Selected = null;
            this.btnBuy.Iconimage_Selected = null;
            this.btnBuy.IconMarginLeft = 6;
            this.btnBuy.IconMarginRight = 0;
            this.btnBuy.IconRightVisible = true;
            this.btnBuy.IconRightZoom = 0D;
            this.btnBuy.IconVisible = true;
            this.btnBuy.IconZoom = 50D;
            this.btnBuy.IsTab = true;
            this.btnBuy.Location = new System.Drawing.Point(0, 90);
            this.btnBuy.Margin = new System.Windows.Forms.Padding(5);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Normalcolor = System.Drawing.Color.Transparent;
            this.btnBuy.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnBuy.OnHoverTextColor = System.Drawing.Color.White;
            this.btnBuy.selected = false;
            this.btnBuy.Size = new System.Drawing.Size(214, 50);
            this.btnBuy.TabIndex = 2;
            this.btnBuy.Tag = "1";
            this.btnBuy.Text = "Quản Lý Nhập Hàng";
            this.btnBuy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuy.Textcolor = System.Drawing.Color.White;
            this.btnBuy.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // btnProFix
            // 
            this.btnProFix.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnProFix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProFix.BackColor = System.Drawing.Color.Transparent;
            this.btnProFix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnProFix.BorderRadius = 0;
            this.btnProFix.ButtonText = "Lập phiếu BH-SC";
            this.btnProFix.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProFix.DisabledColor = System.Drawing.Color.Gray;
            this.btnProFix.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProFix.Iconcolor = System.Drawing.Color.Transparent;
            this.btnProFix.Iconimage = global::PMQLCHDIENTHOAI.Properties.Resources.pro2;
            this.btnProFix.Iconimage_right = null;
            this.btnProFix.Iconimage_right_Selected = null;
            this.btnProFix.Iconimage_Selected = null;
            this.btnProFix.IconMarginLeft = 6;
            this.btnProFix.IconMarginRight = 0;
            this.btnProFix.IconRightVisible = true;
            this.btnProFix.IconRightZoom = 0D;
            this.btnProFix.IconVisible = true;
            this.btnProFix.IconZoom = 50D;
            this.btnProFix.IsTab = true;
            this.btnProFix.Location = new System.Drawing.Point(0, 366);
            this.btnProFix.Margin = new System.Windows.Forms.Padding(5);
            this.btnProFix.Name = "btnProFix";
            this.btnProFix.Normalcolor = System.Drawing.Color.Transparent;
            this.btnProFix.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnProFix.OnHoverTextColor = System.Drawing.Color.White;
            this.btnProFix.selected = false;
            this.btnProFix.Size = new System.Drawing.Size(225, 42);
            this.btnProFix.TabIndex = 2;
            this.btnProFix.Tag = "1";
            this.btnProFix.Text = "Lập phiếu BH-SC";
            this.btnProFix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProFix.Textcolor = System.Drawing.Color.White;
            this.btnProFix.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProFix.Click += new System.EventHandler(this.btnProFix_Click);
            // 
            // btnReport
            // 
            this.btnReport.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReport.BorderRadius = 0;
            this.btnReport.ButtonText = "Hướng dẫn";
            this.btnReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReport.DisabledColor = System.Drawing.Color.Gray;
            this.btnReport.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Iconcolor = System.Drawing.Color.Transparent;
            this.btnReport.Iconimage = global::PMQLCHDIENTHOAI.Properties.Resources.newspaper;
            this.btnReport.Iconimage_right = null;
            this.btnReport.Iconimage_right_Selected = null;
            this.btnReport.Iconimage_Selected = null;
            this.btnReport.IconMarginLeft = 6;
            this.btnReport.IconMarginRight = 0;
            this.btnReport.IconRightVisible = true;
            this.btnReport.IconRightZoom = 0D;
            this.btnReport.IconVisible = true;
            this.btnReport.IconZoom = 50D;
            this.btnReport.IsTab = true;
            this.btnReport.Location = new System.Drawing.Point(0, 418);
            this.btnReport.Margin = new System.Windows.Forms.Padding(5);
            this.btnReport.Name = "btnReport";
            this.btnReport.Normalcolor = System.Drawing.Color.Transparent;
            this.btnReport.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnReport.OnHoverTextColor = System.Drawing.Color.White;
            this.btnReport.selected = false;
            this.btnReport.Size = new System.Drawing.Size(201, 42);
            this.btnReport.TabIndex = 2;
            this.btnReport.Tag = "1";
            this.btnReport.Text = "Hướng dẫn";
            this.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.Textcolor = System.Drawing.Color.White;
            this.btnReport.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnStaff
            // 
            this.btnStaff.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnStaff.BackColor = System.Drawing.Color.Transparent;
            this.btnStaff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStaff.BorderRadius = 0;
            this.btnStaff.ButtonText = "Quản Lý Nhân Viên";
            this.btnStaff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStaff.DisabledColor = System.Drawing.Color.Gray;
            this.btnStaff.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStaff.Iconcolor = System.Drawing.Color.Transparent;
            this.btnStaff.Iconimage = global::PMQLCHDIENTHOAI.Properties.Resources.collaboration;
            this.btnStaff.Iconimage_right = null;
            this.btnStaff.Iconimage_right_Selected = null;
            this.btnStaff.Iconimage_Selected = null;
            this.btnStaff.IconMarginLeft = 6;
            this.btnStaff.IconMarginRight = 0;
            this.btnStaff.IconRightVisible = true;
            this.btnStaff.IconRightZoom = 0D;
            this.btnStaff.IconVisible = true;
            this.btnStaff.IconZoom = 50D;
            this.btnStaff.IsTab = true;
            this.btnStaff.Location = new System.Drawing.Point(0, 202);
            this.btnStaff.Margin = new System.Windows.Forms.Padding(5);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Normalcolor = System.Drawing.Color.Transparent;
            this.btnStaff.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnStaff.OnHoverTextColor = System.Drawing.Color.White;
            this.btnStaff.selected = false;
            this.btnStaff.Size = new System.Drawing.Size(206, 50);
            this.btnStaff.TabIndex = 2;
            this.btnStaff.Tag = "1";
            this.btnStaff.Text = "Quản Lý Nhân Viên";
            this.btnStaff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStaff.Textcolor = System.Drawing.Color.White;
            this.btnStaff.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // btnSupplier
            // 
            this.btnSupplier.Activecolor = System.Drawing.Color.CornflowerBlue;
            this.btnSupplier.BackColor = System.Drawing.Color.Transparent;
            this.btnSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSupplier.BorderRadius = 0;
            this.btnSupplier.ButtonText = "Quản Lý Nhà Cung Cấp";
            this.btnSupplier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSupplier.DisabledColor = System.Drawing.Color.Gray;
            this.btnSupplier.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplier.Iconcolor = System.Drawing.Color.Transparent;
            this.btnSupplier.Iconimage = global::PMQLCHDIENTHOAI.Properties.Resources.meeting;
            this.btnSupplier.Iconimage_right = null;
            this.btnSupplier.Iconimage_right_Selected = null;
            this.btnSupplier.Iconimage_Selected = null;
            this.btnSupplier.IconMarginLeft = 6;
            this.btnSupplier.IconMarginRight = 0;
            this.btnSupplier.IconRightVisible = true;
            this.btnSupplier.IconRightZoom = 0D;
            this.btnSupplier.IconVisible = true;
            this.btnSupplier.IconZoom = 50D;
            this.btnSupplier.IsTab = true;
            this.btnSupplier.Location = new System.Drawing.Point(0, 145);
            this.btnSupplier.Margin = new System.Windows.Forms.Padding(5);
            this.btnSupplier.Name = "btnSupplier";
            this.btnSupplier.Normalcolor = System.Drawing.Color.Transparent;
            this.btnSupplier.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnSupplier.OnHoverTextColor = System.Drawing.Color.White;
            this.btnSupplier.selected = false;
            this.btnSupplier.Size = new System.Drawing.Size(206, 50);
            this.btnSupplier.TabIndex = 2;
            this.btnSupplier.Tag = "1";
            this.btnSupplier.Text = "Quản Lý Nhà Cung Cấp";
            this.btnSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSupplier.Textcolor = System.Drawing.Color.White;
            this.btnSupplier.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplier.Click += new System.EventHandler(this.btnSupplier_Click);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCustomer.BackColor = System.Drawing.Color.Transparent;
            this.btnCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCustomer.BorderRadius = 0;
            this.btnCustomer.ButtonText = "Quản Lý Khách Hàng";
            this.btnCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomer.DisabledColor = System.Drawing.Color.Gray;
            this.btnCustomer.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomer.Iconcolor = System.Drawing.Color.Transparent;
            this.btnCustomer.Iconimage = global::PMQLCHDIENTHOAI.Properties.Resources.group;
            this.btnCustomer.Iconimage_right = null;
            this.btnCustomer.Iconimage_right_Selected = null;
            this.btnCustomer.Iconimage_Selected = null;
            this.btnCustomer.IconMarginLeft = 6;
            this.btnCustomer.IconMarginRight = 0;
            this.btnCustomer.IconRightVisible = true;
            this.btnCustomer.IconRightZoom = 0D;
            this.btnCustomer.IconVisible = true;
            this.btnCustomer.IconZoom = 50D;
            this.btnCustomer.IsTab = true;
            this.btnCustomer.Location = new System.Drawing.Point(0, 256);
            this.btnCustomer.Margin = new System.Windows.Forms.Padding(5);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Normalcolor = System.Drawing.Color.Transparent;
            this.btnCustomer.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnCustomer.OnHoverTextColor = System.Drawing.Color.White;
            this.btnCustomer.selected = false;
            this.btnCustomer.Size = new System.Drawing.Size(230, 52);
            this.btnCustomer.TabIndex = 2;
            this.btnCustomer.Tag = "1";
            this.btnCustomer.Text = "Quản Lý Khách Hàng";
            this.btnCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomer.Textcolor = System.Drawing.Color.White;
            this.btnCustomer.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // btnDevice
            // 
            this.btnDevice.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDevice.BackColor = System.Drawing.Color.Transparent;
            this.btnDevice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDevice.BorderRadius = 0;
            this.btnDevice.ButtonText = "Quản Lý Thiết Bị";
            this.btnDevice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDevice.DisabledColor = System.Drawing.Color.Gray;
            this.btnDevice.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDevice.Iconcolor = System.Drawing.Color.Transparent;
            this.btnDevice.Iconimage = global::PMQLCHDIENTHOAI.Properties.Resources.smartphone;
            this.btnDevice.Iconimage_right = null;
            this.btnDevice.Iconimage_right_Selected = null;
            this.btnDevice.Iconimage_Selected = null;
            this.btnDevice.IconMarginLeft = 6;
            this.btnDevice.IconMarginRight = 0;
            this.btnDevice.IconRightVisible = true;
            this.btnDevice.IconRightZoom = 0D;
            this.btnDevice.IconVisible = true;
            this.btnDevice.IconZoom = 50D;
            this.btnDevice.IsTab = true;
            this.btnDevice.Location = new System.Drawing.Point(0, 314);
            this.btnDevice.Margin = new System.Windows.Forms.Padding(5);
            this.btnDevice.Name = "btnDevice";
            this.btnDevice.Normalcolor = System.Drawing.Color.Transparent;
            this.btnDevice.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(109)))), ((int)(((byte)(255)))));
            this.btnDevice.OnHoverTextColor = System.Drawing.Color.White;
            this.btnDevice.selected = false;
            this.btnDevice.Size = new System.Drawing.Size(230, 42);
            this.btnDevice.TabIndex = 2;
            this.btnDevice.Tag = "1";
            this.btnDevice.Text = "Quản Lý Thiết Bị";
            this.btnDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDevice.Textcolor = System.Drawing.Color.White;
            this.btnDevice.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDevice.Click += new System.EventHandler(this.btnDevice_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.btnHome);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(206, 56);
            this.panel3.TabIndex = 5;
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.Transparent;
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHome.Image = global::PMQLCHDIENTHOAI.Properties.Resources.home;
            this.btnHome.ImageActive = null;
            this.btnHome.Location = new System.Drawing.Point(0, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(206, 56);
            this.btnHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHome.TabIndex = 2;
            this.btnHome.TabStop = false;
            this.btnHome.WaitOnLoad = true;
            this.btnHome.Zoom = 10;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnMenuTongle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 34);
            this.panel1.TabIndex = 3;
            // 
            // btnMenuTongle
            // 
            this.btnMenuTongle.BackColor = System.Drawing.Color.Transparent;
            this.btnMenuTongle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMenuTongle.Image = global::PMQLCHDIENTHOAI.Properties.Resources.menu;
            this.btnMenuTongle.ImageActive = null;
            this.btnMenuTongle.Location = new System.Drawing.Point(157, 0);
            this.btnMenuTongle.Name = "btnMenuTongle";
            this.btnMenuTongle.Size = new System.Drawing.Size(49, 34);
            this.btnMenuTongle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMenuTongle.TabIndex = 1;
            this.btnMenuTongle.TabStop = false;
            this.btnMenuTongle.Zoom = 10;
            this.btnMenuTongle.Click += new System.EventHandler(this.btnMenuTongle_Click);
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHide.BackColor = System.Drawing.Color.Transparent;
            this.btnHide.Image = global::PMQLCHDIENTHOAI.Properties.Resources.horizontal_line_remove_button;
            this.btnHide.ImageActive = null;
            this.btnHide.Location = new System.Drawing.Point(1218, 9);
            this.btnHide.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnHide.Name = "btnHide";
            this.btnHide.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnHide.Size = new System.Drawing.Size(31, 26);
            this.btnHide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHide.TabIndex = 4;
            this.btnHide.TabStop = false;
            this.btnHide.Zoom = 10;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.Image = global::PMQLCHDIENTHOAI.Properties.Resources.multi_tab;
            this.btnMin.ImageActive = null;
            this.btnMin.Location = new System.Drawing.Point(1255, 8);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(31, 26);
            this.btnMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMin.TabIndex = 4;
            this.btnMin.TabStop = false;
            this.btnMin.Zoom = 10;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Image = global::PMQLCHDIENTHOAI.Properties.Resources.cancel;
            this.btnExit.ImageActive = null;
            this.btnExit.Location = new System.Drawing.Point(1292, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(31, 26);
            this.btnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnExit.TabIndex = 4;
            this.btnExit.TabStop = false;
            this.btnExit.Zoom = 10;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1340, 700);
            this.Controls.Add(this.pnlMenuLeft);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(1340, 700);
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.LocationChanged += new System.EventHandler(this.frmMain_LocationChanged);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMenuLeft.ResumeLayout(false);
            this.pnlContainmenu.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnMenuTongle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuGradientPanel pnlMenuLeft;
        private Bunifu.Framework.UI.BunifuImageButton btnExit;
        private System.Windows.Forms.Panel pnlHeader;
        private Bunifu.Framework.UI.BunifuImageButton btnMenuTongle;
        private Bunifu.Framework.UI.BunifuFlatButton btnSold;
        private Bunifu.Framework.UI.BunifuFlatButton btnSupplier;
        private Bunifu.Framework.UI.BunifuFlatButton btnBuy;
        private Bunifu.Framework.UI.BunifuFlatButton btnDevice;
        private Bunifu.Framework.UI.BunifuFlatButton btnStaff;
        private Bunifu.Framework.UI.BunifuFlatButton btnReport;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuFlatButton btnCustomer;
        private Bunifu.Framework.UI.BunifuImageButton btnHome;
        private System.Windows.Forms.Panel pnlContainmenu;
        private System.Windows.Forms.Panel panel3;
        private Bunifu.Framework.UI.BunifuFlatButton btnProFix;
        private System.Windows.Forms.Label txtMANV;
        private MetroFramework.Controls.MetroLabel lblMANV;
        private Bunifu.Framework.UI.BunifuImageButton btnMin;
        private Bunifu.Framework.UI.BunifuImageButton btnHide;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
    }
}