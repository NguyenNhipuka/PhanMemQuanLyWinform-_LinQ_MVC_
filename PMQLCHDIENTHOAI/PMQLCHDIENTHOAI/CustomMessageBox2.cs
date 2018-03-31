using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Controls;
 namespace PMQLCHDIENTHOAI
{
    public partial class CustomMessageBox2 : Form
    {
        public CustomMessageBox2()
        {
            InitializeComponent();          
        }
        static CustomMessageBox2 MsgBox;
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static void show(string title, string content, Boolean success)
        {
            
            MsgBox = new CustomMessageBox2();
            //MsgBox.btnNo.
            MsgBox.lblTitle.Text = title;
            MsgBox.lblContent.Text = content;
            if(success)
            {
                MsgBox.pic.Image = Image.FromFile(Application.StartupPath + "\\Resources\\" + "checked.png");
            }
            else
            {
                MsgBox.pic.Image = Image.FromFile(Application.StartupPath + "\\Resources\\" + "close.png");

            }
           
            MsgBox.ShowDialog();
        }

        private void CustomMessageBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                this.Close();
            }
        }
    }
}
