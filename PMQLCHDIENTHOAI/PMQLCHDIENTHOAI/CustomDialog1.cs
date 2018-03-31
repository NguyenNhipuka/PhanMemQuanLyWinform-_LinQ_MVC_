using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Controls;
using Bunifu.Framework.UI;

namespace PMQLCHDIENTHOAI
{
    public partial class CustomDialog1 : Form
    {
        public CustomDialog1()
        {
            InitializeComponent();
           
        }
        static CustomDialog1 MsgBox;


        static DialogResult result = DialogResult.No;
        public  static DialogResult show(string title,string content,string nameofyes,string nameofno)
        {
            MsgBox = new CustomDialog1();
            //MsgBox.btnNo.
            MsgBox.btnYes.Text = nameofyes;
            MsgBox.btnNo.Text = nameofno;
            MsgBox.txtTieuDe.Text = title;
            MsgBox.txtNoiDung.Text = content;
            result = DialogResult.No;
            MsgBox.ShowDialog();
            return result;
        }
        public static DialogResult show(string title, string content)
        {

            MsgBox = new CustomDialog1();
            MsgBox.btnYes.Text = "Yes";
            MsgBox.btnNo.Text = "No";
            MsgBox.txtTieuDe.Text = title;
            MsgBox.txtNoiDung.Text = content;
            MsgBox.ShowDialog();
            return result;
        }
        private void btnYes_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
            MsgBox.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {

        }
    }
}
