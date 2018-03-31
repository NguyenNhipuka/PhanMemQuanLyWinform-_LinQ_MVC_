using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BUS;
namespace PMQLCHDIENTHOAI
{
    public partial class frmLogin : Form
    {
        public delegate void sendMessage(string message);
        public event sendMessage sendMessageEvent;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUser;
            txtUser.Focus();
           
            //txtPass.
            //txtPass.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            goLogin();


        }

        private void goLogin()
        {
            string name = txtUser.Text.ToString();
            string pass = txtPass.Text.ToString();
            if (name.Length > 6)
            {
                CustomMessageBox1.show("Tài khoản không tồn tại"); return;
            }
            if (name == string.Empty || pass == string.Empty)
            {
                CustomMessageBox1.show("Mời bạn điền đầy đủ thông tin");
                return;
            }
            string ErrMess = "";
            int ErrC = 0;
            Boolean result = TaiKhoanBUS.Instance.check_user(name, pass, out ErrC, out ErrMess);
            if (result && ErrC == 0)//thành công, tk đang hoạt động
            {
                ///// đăng nhập thành công
                if (sendMessageEvent != null)
                {
                    sendMessageEvent(name);
                    this.Close();
                }
            }
        }
        private void openMainForm(object obj)
        {
           // Application.Run(new frmMain());
        }

        private void timeGO_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity + .05;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult ok;
            ok = MessageBox.Show("Bạn có muốn thoát chương trình không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok == DialogResult.Yes)
                Application.Exit();
        }

        private void btnMinize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtPass_KeyPress(object sender, EventArgs e)
        {
           
        }

        private void btnLogin_Enter(object sender, EventArgs e)
        {
            //btnLogin_Click(sender, e);
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                // Enter key pressed
                txtPass.Focus();
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                // Enter key pressed
                goLogin();
            }
        }
    }
}
