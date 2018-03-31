using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bunifu;
using Word = Microsoft.Office.Interop.Word;

namespace PMQLCHDIENTHOAI
{
  
    public partial class frmMain : Form
    {
        ////kich thuoc menu lon 214;menu nho  =50, 1286
        public string currentform = "formHome";
        /// <summary>
        /// /form đang thực thi
        /// </summary>
        Size sizelarge = new Size(1286,650);
        Point pointlarge = new Point(60,55);
        Size sizesmall = new Size(1122,650);
        Point pointsmall = new Point(227, 55);
        public  frmBuy frmbuy = new frmBuy();//1
        public  frmCustomers frmcustomer = new frmCustomers();//2
        public  frmDevice frmdivice = new frmDevice();//3
        public  frmHome frmhome= new frmHome();//4
        public  frmReport frmreport = new frmReport();//5
        public  frmSold frmsold = new frmSold();//6
        public  frmStaff frmstaff = new frmStaff();//7
        public  frmSupplier frmsupplier = new frmSupplier();//8
        public  frmProFix frmprofix = new frmProFix();//9
        public static int flat = 0;
        public string id = "";
        public frmMain()
        {
            InitializeComponent();
            pnlMenuLeft.AutoScroll = false;
            pnlContainmenu.AutoScroll = panel1.AutoScroll= false;
        }
        public void fr_sendMessageEvent(string messaege)
        {
            lblMANV.Text = messaege;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMenuTongle_Click(object sender, EventArgs e)      
        {
           
            if (pnlMenuLeft.Width < 206)
            {
                pnlMenuLeft.Width = 206;
                autosizeForm(sizesmall,pointsmall);
            }
            else
            {
                pnlMenuLeft.Width = 50;
                autosizeForm(sizelarge,pointlarge);
            }
            
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            try
            {
                frmhome.MdiParent = this;

                frmhome.IdNV = lblMANV.Text.ToString();
                frmhome.sendMessageEvent += fr_sendMessageEvent;
                if (flat == 4)
                {
                    frmhome.BringToFront();
                    return;
                }
                frmhome.tabindex = 1;
                frmhome.openTabPage(1);
                frmhome.Show();

                frmhome.block_Menu();
                closeForm();
                flat = 4;
            }
            catch
            {
                Application.Exit();
            }
              
        }

        //region My Funcion
        public void closeForm()
        {
            switch (flat)
            {
                case 1:frmbuy.Hide();
                    btnBuy.selected = false;
                    break;
                case 2:frmcustomer.Hide();
                    btnCustomer.selected = false;
                    //frmcustomer.Exitfrm();
                    break;
                case 3:frmdivice.Hide();
                    btnDevice.selected = false;
                    break;
                case 4:frmhome.Hide();
                    break;
                case 5:frmreport.Hide();
                    btnReport.selected = false;  break;
                case 6:frmsold.Hide();
                    btnSold.selected = false; break;
                case 7:frmstaff.Hide();
                    btnStaff.selected = false;
                   // frmstaff.Exitfrm();
                    break;
                case 8:frmsupplier.Hide();
                    btnSupplier.selected = false; break;
                case 9: frmprofix.Hide();
                    btnProFix.selected = false;
                    break;
                default:break;
            }
           
            
        }
        public void autosizeForm(Size size,Point point)
        {
            switch (flat)
            {
                case 1:
                    frmbuy.BringToFront();
                    //frmbuy.Size = size;
                    //frmbuy.Location = point;
                    break;
                case 2:
                    frmcustomer.BringToFront();
                    //frmcustomer.Size = size;
                    //frmcustomer.Location = point;
                    break;
                case 3:
                    frmdivice.BringToFront();
                    //frmdivice.Size = size;
                    //frmdivice.Location = point;
                    break;
                case 4:
                    frmhome.BringToFront();
                    //frmhome.Size = size;
                    //frmhome.Location = point;
                    break;
                case 5:
                    frmreport.BringToFront();
                    //frmreport.Size = size;
                    //frmreport.Location = point;
                    break;
                case 6:
                    frmsold.BringToFront();
                    //frmsold.Size = size;
                    //frmsold.Location = point;
                    break;
                case 7:
                    frmstaff.BringToFront();
                    //frmstaff.Size = size;
                    //frmstaff.Location = point;
                    break;
                case 8:
                    frmsupplier.BringToFront();
                    //frmsupplier.Size = size;
                    //frmsupplier.Location = point;
                    break;
                case 9:
                    frmprofix.BringToFront();
                    //frmprofix.Size = size;
                    //frmprofix.Location = point;
                    break;
                default: break;
            }

        }

        //endregion My Funcion

        private void btnSold_Click(object sender, EventArgs e)
        {
            frmsold.MdiParent = this;
            frmsold.IDTK = lblMANV.Text.ToString();
            if (flat == 6)
            {
                frmsold.BringToFront();
                return;
            }
            frmsold.tabindex = 1;
            frmsold.openTabPage(frmsold.tabindex);
            
            frmsold.Show();         
            closeForm();
            flat = 6;
            btnSold.selected = true;
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            frmbuy.IDTK= lblMANV.Text.ToString();
            frmbuy.blockmenu();
            frmbuy.MdiParent = this;
            if (flat == 1) {
                frmbuy.BringToFront();
                return; }
            
            frmbuy.Show();
            frmbuy.tabindex = 1;
            frmbuy.openTabPage(frmbuy.tabindex);
            closeForm();
            flat = 1;
            btnBuy.selected = true;
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            frmsupplier.IDTK = lblMANV.Text.ToString();
            frmsupplier.MdiParent = this;
            if (flat == 8) {
                frmsupplier.BringToFront();
                return;
                    }
            frmsupplier.tabindex = 1;
            frmsupplier.openTabPage(frmsupplier.tabindex);
            closeForm();
            btnSupplier.selected = true;
            frmsupplier.Show();           
           
            flat = 8;
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            frmstaff.IDTK = lblMANV.Text.ToString();
            frmstaff.MdiParent = this;
            if (flat == 7) {
                this.SendToBack();
                //frmstaff.BringToFront();
                return;
            }
            //frmstaff = new frmStaff();
            frmstaff.tabindex = 1;
            frmstaff.openTabPage(frmstaff.tabindex);
            closeForm();
            btnStaff.selected = true;
            frmstaff.Show();           
            flat = 7;
           
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmcustomer.IDTK = lblMANV.Text.ToString();
            frmcustomer.MdiParent = this; 
            if (flat == 2) {
                frmcustomer.BringToFront();
                return; }
           // frmcustomer = new frmCustomers();
            frmcustomer.tabindex = 1;
            frmcustomer.openTabPage(frmcustomer.tabindex);
            closeForm();
            btnCustomer.selected = true;
            frmcustomer.Show();        
            flat = 2;
        }

        private void btnDevice_Click(object sender, EventArgs e)
        {
            frmdivice.IDTK = lblMANV.Text.ToString();
            frmdivice.MdiParent = this;
            if (flat == 3) { frmdivice.BringToFront(); return; }
            frmdivice.tabindex = 1;
            frmdivice.openTabPage(frmdivice.tabindex);
            frmdivice.Show();
            closeForm();
            flat = 3;
            btnDevice.selected = true;
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            //frmreport.MdiParent = this;
            //if (flat == 5) { frmreport.BringToFront(); return; }
            //frmreport.Show();
            //closeForm();
            //flat = 5;
            //btnReport.selected = true;
            string my ="D:\\MonHoc\\managesmartphoneshop\\manageshop\\PMQLCHDIENTHOAI\\PMQLCHDIENTHOAI\\bin\\Debug\\PMDocument.docx";
            //ExportWord(my, true);
           // string x = Application.StartupPath + "\\bin\\Debug\\" + "checked.png";
           // ExportWord(my, true);
            ExportWord(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "PMDocument.docx", true);
        }

        public Boolean ExportWord(string vPath, bool vCreateApp)
        {
             Word.Application _app;
             Word.Document _doc;
                 object _pathFile;
             _pathFile = vPath;
            
            _app = new Word.Application();
            _app.Visible = vCreateApp;
            object ob = System.Reflection.Missing.Value;
            _doc = _app.Documents.Add(ref _pathFile, ref ob, ref ob, ref ob);
            return true;
        }
        private void btnProFix_Click(object sender, EventArgs e)
        {
            frmprofix.IDTK = lblMANV.Text.ToString();
            frmprofix.MdiParent = this;
            if (flat == 9) { frmprofix.BringToFront(); return; }
            frmprofix.tabindex = 1;
            frmprofix.openTabPage(frmdivice.tabindex);
            frmprofix.Show();
            closeForm();
            flat = 9;
            btnProFix.selected = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                // closeForm();
                flat = 4;
                frmLogin frmlogin = new frmLogin();
                frmlogin.sendMessageEvent += fr_sendMessageEvent;
                frmlogin.ShowDialog();
                // frmhome.sendMessageEvent += fr_sendMessageEvent;
                frmhome.IdNV = lblMANV.Text.ToString();
                frmhome.sendMessageEvent += fr_sendMessageEvent;
                frmhome.block_Menu();
                frmhome.MdiParent = this;
                frmhome.Show();
                frmhome.openTabPage(1);
            }
            catch
            {
                Application.Exit();
            }
           
        }

        private void frmMain_LocationChanged(object sender, EventArgs e)
        {

        }

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            //mouseDown = true;
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            //if (mouseDown)
            //{
            //    mouseX = MousePosition.X -200 ;
            //    mouseY = MousePosition.Y-40 ;
            //    this.SetDesktopLocation(mouseX, mouseY);
            //}
            
        }

        private void pnlHeader_MouseUp(object sender, MouseEventArgs e)
        {
            //mouseDown = false;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
                this.WindowState = FormWindowState.Maximized;


        }
    }
}
