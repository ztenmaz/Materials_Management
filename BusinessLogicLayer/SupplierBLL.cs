using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class SupplierBLL
    {
        private SupplierDAL supplierDAL = new SupplierDAL();
        public List<Supplier> GetAllSuppliers(string keyword = null)
        {
            return supplierDAL.GetSuppliers(keyword);
        }

        public bool AddSupplier(Supplier supplier, out string errorMessage)
        {
            if (string.IsNullOrEmpty(supplier.supplierName))
            {
                errorMessage = "Tên nhà cung cấp không được để trống!";
                return false;
            }

            if (!IsValidPhoneNumber(supplier.phone))
            {
                errorMessage = "Số điện thoại không hợp lệ!";
                return false;
            }

            if (!IsValidEmail(supplier.email))
            {
                errorMessage = "Email không hợp lệ!";
                return false;
            }

            bool isAdded = supplierDAL.AddSupplier(supplier);

            if (!isAdded)
            {
                errorMessage = "Thêm nhà cung cấp thất bại!";
                return false;
            }

            errorMessage = null; // Không có lỗi
            return true;
        }

        public bool UpdateSupplier(Supplier supplier, out string errorMessage)
        {
            if (string.IsNullOrEmpty(supplier.supplierName))
            {
                errorMessage = "Tên nhà cung cấp không được để trống!";
                return false;
            }

            if (!IsValidPhoneNumber(supplier.phone))
            {
                errorMessage = "Số điện thoại không hợp lệ!";
                return false;
            }

            if (!IsValidEmail(supplier.email))
            {
                errorMessage = "Email không hợp lệ!";
                return false;
            }

            bool isUpdated = supplierDAL.UpdateSupplier(supplier);

            if (!isUpdated)
            {
                errorMessage = "Cập nhật nhà cung cấp thất bại!";
                return false;
            }

            errorMessage = null; // Không có lỗi
            return true;
        }

        public bool DeleteSupplier(int idSupplier, out string errorMessage)
        {
            // Kiểm tra xem nhà cung cấp có đang được sử dụng không
            bool isUsed = supplierDAL.CheckSupplierInUse(idSupplier);

            if (isUsed)
            {
                errorMessage = "Nhà cung cấp đang được sử dụng, không thể xóa!";
                return false;
            }

            // Gọi DAL để xóa
            bool isDeleted = supplierDAL.DeleteSupplier(idSupplier);

            if (!isDeleted)
            {
                errorMessage = "Xóa nhà cung cấp thất bại!";
                return false;
            }

            errorMessage = null;
            return true;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private static bool IsValidPhoneNumber(string phone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^0\d{9}$");
        }
    }
}
