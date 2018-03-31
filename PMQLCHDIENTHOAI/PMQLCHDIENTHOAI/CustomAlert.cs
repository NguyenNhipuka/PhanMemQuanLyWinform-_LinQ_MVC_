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
    public partial class CustomAlert : Form
    {
        public CustomAlert(string message)
        {
            InitializeComponent();
            lblmessage.Text = message;
        }
        
        public static void Show(string message)
        {
            new CustomAlert(message).Show();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            close.Start();
        }

        private void timeout_Tick(object sender, EventArgs e)
        {
            close.Start();
        }

        private void CustomAlert_Load(object sender, EventArgs e)
        {
            this.Top = -1*this.Height;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 60;
            show.Start();
        }
        void annimateForm()
        {

        }
        int interval = 0;
        private void show_Tick(object sender, EventArgs e)
        {
            if (this.Top < 60)
            {
                this.Top += interval;
                interval += 2;
            }
            else
            {
                show.Stop();
            }
        }

        private void close_Tick(object sender, EventArgs e)
        {
            if(this.Opacity<0)
            {
                this.Opacity -= 0.1;
            }
            else
            {
                this.Close();
            }
        }
    }
}
