using BusinessLogicLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class formVerifyAccount : Form
    {
        private string currentOTP = "";
        public formVerifyAccount()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            formLogin formLogin = new formLogin();
            formLogin.Show();
            Close();
        }

        private void txbClose_Click(object sender, EventArgs e)
        {
            formLogin formLogin = new formLogin();
            formLogin.Show();
            Close();
        }

        private void txbForgetPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string username = txbUserConfirm.Text.Trim();
            string email = txbEmailComfirm.Text.Trim();

            Account account = new Account { username = username };
            Officer officer = new Officer { email = email};

            bool checkInput = AccountBLL.checkUserAndEmail(account, officer, out string errorMessage);
            if(!checkInput)
            {
                MessageBox.Show(errorMessage, "Lỗi xác minh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                currentOTP = SendCodeToEmail.SendVerificationEmail(officer);
                MessageBox.Show("Mã xác minh đã được gửi về email của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }    
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string recipientEmail = txbOtpCode.Text.Trim();
            if (string.IsNullOrWhiteSpace(txbUserConfirm.Text.Trim()))
            {
                MessageBox.Show("Tên đăng nhập không được để trống !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(txbEmailComfirm.Text.Trim()))
            {
                MessageBox.Show("Email không được để trống !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(recipientEmail))
            {
                MessageBox.Show("Vui lòng nhập OTP để xác thực !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (recipientEmail.Equals(currentOTP))
                {
                    string data = txbUserConfirm.Text.Trim();
                    formChangePass formChange = new formChangePass(data);
                    formChange.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Mã OTP không chính xác !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }    
            }    
        }
    }
}
