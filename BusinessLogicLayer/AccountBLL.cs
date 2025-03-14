using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer
{
    public class AccountBLL
    {
        public static bool Login(Account account, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(account.username))
            {
                errorMessage = "Tên đăng nhập không được để trống";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(account.password))
            {
                errorMessage = "Mật khẩu không được để trống !";
                return false;
            }
            bool isSuccess = AccountDAL.CheckLogin(account);
            if (!isSuccess)
            {
                errorMessage = "Tài khoản hoặc mật khẩu không chính xác!";
            }
            else
            {
                errorMessage = null;
            }

            return isSuccess;
        }

        public static bool checkUserAndEmail(Account account, Officer officer, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(account.username))
            {
                errorMessage = "Tên đăng nhập không được để trống !";
                return false;
            }
            if (string.IsNullOrWhiteSpace(officer.email))
            {
                errorMessage = "Email không được để trống !";
                return false;
            }
            bool checkOutput = AccountDAL.CheckUserAndEmailExists(account.username, officer.email);
            if (!checkOutput)
            {
                errorMessage = "Tên đăng nhập hoặc email không đúng !";
            }
            else
            {
                errorMessage = null;
            }
            return checkOutput;
        }
        public static bool UpdateNewPass(string username, string newPass, string reNewPass, out string errorMessage)
        {
            errorMessage = "";

            // Kiểm tra mật khẩu có rỗng không
            if (string.IsNullOrWhiteSpace(newPass))
            {
                errorMessage = "Vui lòng nhập mật khẩu mới!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(reNewPass))
            {
                errorMessage = "Vui lòng nhập lại mật khẩu mới!";
                return false;
            }
            if (!newPass.Equals(reNewPass)) // Fix: Kiểm tra nếu KHÔNG khớp mới báo lỗi
            {
                errorMessage = "Mật khẩu mới và mật khẩu nhập lại không trùng khớp!";
                return false;
            }

            // Gọi hàm cập nhật mật khẩu
            bool isUpdated = AccountDAL.ChangeUserPassword(username, newPass);

            return true;
        }

    }
}
