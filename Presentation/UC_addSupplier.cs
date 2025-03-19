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
using System.Xml.Linq;

namespace Presentation
{
    public partial class UC_addSupplier : UserControl
    {
        private SupplierBLL supplierBLL = new SupplierBLL();
        public event Action OnSupplierAdded; // Sự kiện để cập nhật danh sách
        public event Action OnCloseForm;     // Sự kiện để đóng form cha
        public UC_addSupplier()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier
            {
                supplierName = txbSupplier_Name.Text.Trim(),
                address = txbAddress.Text.Trim(),
                phone = txbPhone.Text.Trim(),
                email = txbEmail.Text.Trim()
            };
            if (supplierBLL.AddSupplier(supplier, out string errorMessage))
            {
                MessageBox.Show("Thêm nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnSupplierAdded?.Invoke(); // Gửi sự kiện cập nhật danh sách
                OnCloseForm?.Invoke();      // Đóng form cha
            }
            else
            {
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCloseForm?.Invoke();
        }
    }
}
