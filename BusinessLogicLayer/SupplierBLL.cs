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
        public List<Supplier> GetAllSuppliers()
        {
            return supplierDAL.GetSuppliers();
        }

        public bool AddSupplier(Supplier supplier)
        {
            bool isSuccess = false;

            if (string.IsNullOrEmpty(supplier.supplierName))
            {
                throw new Exception("Tên nhà cung cấp không được để trống!");
            }

            if (!IsValidPhoneNumber(supplier.phone))
            {
                throw new Exception("Số điện thoại không hợp lệ!");
            }

            if (!IsValidEmail(supplier.email))
            {
                throw new Exception("Email không hợp lệ!");
            }

            isSuccess = supplierDAL.AddSupplier(supplier);

            return isSuccess;
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
