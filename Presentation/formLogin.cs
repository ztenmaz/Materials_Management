using BusinessLogicLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }

        private void txbForgetPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formVerifyAccount formVerify = new formVerifyAccount();
            formVerify.Show();
            Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text.Trim();
            string password = txbPassword.Text.Trim();
            Account account = new Account { username = username, password = password };
            bool isSuccess = AccountBLL.Login(account, out string errorMessage);

            if (!isSuccess)
            {
                MessageBox.Show(errorMessage, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                formMain fM = new formMain();
                fM.Show();
                this.Hide();
            }    
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
