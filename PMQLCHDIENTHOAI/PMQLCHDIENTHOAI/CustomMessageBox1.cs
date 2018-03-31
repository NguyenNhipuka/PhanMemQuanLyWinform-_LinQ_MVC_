using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMQLCHDIENTHOAI
{
    public partial class CustomMessageBox1 : Form
    {
        public CustomMessageBox1()
        {
            InitializeComponent();
        }
        static CustomMessageBox1 MsgBox;
        private void btnYes_Click(object sender, EventArgs e)
        {
            MsgBox.Close();
        }
        public static void show(string content)
        {
            MsgBox = new CustomMessageBox1();
            MsgBox.txtNoiDung.Text = content;
            MsgBox.ShowDialog();
            return ;
        }
    }
}
