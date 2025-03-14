using BusinessLogicLayer;
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
    public partial class formChangePass : Form
    {
        private string receivedData;
        public formChangePass(string data)
        {
            InitializeComponent();
            receivedData = data;
        }

        private void txbClose_Click(object sender, EventArgs e)
        {
            formLogin formLogin = new formLogin();
            formLogin.Show();
            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            formLogin formLogin = new formLogin();
            formLogin.Show();
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string username = receivedData;
            string newPass = txbNewPassword.Text.Trim();
            string reNewPass = txbReNewPassword.Text.Trim();
            string errorMessage;
            bool isSuccess = AccountBLL.UpdateNewPass(username, newPass, reNewPass, out errorMessage);

            if (isSuccess)
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                formLogin login = new formLogin();
                login.Show();
            }
            else
            {
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
