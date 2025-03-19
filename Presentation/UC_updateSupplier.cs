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
    public partial class UC_updateSupplier : UserControl
    {
        private SupplierBLL supplierBLL = new SupplierBLL();
        private Supplier currentSupplier;
        public event Action OnSupplierUpdated; // Sự kiện để cập nhật danh sách
        public event Action OnCloseForm;     // Sự kiện để đóng form cha
        public UC_updateSupplier(Supplier supplier)
        {
            InitializeComponent();
            currentSupplier = supplier;
            LoadSupplierData();
        }

        private void LoadSupplierData()
        {
            txbIdSupplier.Text = currentSupplier.displayIdSupplier.ToString();
            txbSupplier_Name.Text = currentSupplier.supplierName;
            txbAddress.Text = currentSupplier.address;
            txbPhone.Text = currentSupplier.phone;
            txbEmail.Text = currentSupplier.email;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            currentSupplier.supplierName = txbSupplier_Name.Text;
            currentSupplier.address = txbAddress.Text;
            currentSupplier.phone = txbPhone.Text;
            currentSupplier.email = txbEmail.Text;

            if (supplierBLL.UpdateSupplier(currentSupplier, out string errorMessage))
            {
                MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnSupplierUpdated?.Invoke(); // Gửi sự kiện cập nhật danh sách
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
