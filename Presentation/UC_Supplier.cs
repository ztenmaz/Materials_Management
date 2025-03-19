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
using System.Text.RegularExpressions;

namespace Presentation
{
    public partial class UC_Supplier : UserControl
    {
        private SupplierBLL supplierBLL = new SupplierBLL();
        public UC_Supplier()
        {
            InitializeComponent();
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            dgvTable.Rows.Clear();
            
            List<Supplier> suppliers = supplierBLL.GetAllSuppliers();
            foreach (var supplier in suppliers)
            {
                dgvTable.Rows.Add(supplier.displayIdSupplier, supplier.supplierName, supplier.phone, supplier.email, supplier.address);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form tempForm = new Form();
            tempForm.Text = "Thêm Nhà Cung Cấp";
            tempForm.Size = new Size(491, 350);
            tempForm.FormBorderStyle = FormBorderStyle.None;

            UC_addSupplier uc = new UC_addSupplier();
            uc.Dock = DockStyle.Fill;

            uc.OnSupplierAdded += LoadSuppliers;

            // Khi nhấn "Hủy" hoặc thêm xong, đóng form
            uc.OnCloseForm += () => tempForm.Close();

            tempForm.Controls.Add(uc);

            tempForm.StartPosition = FormStartPosition.CenterScreen;
            tempForm.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã nhà cung cấp từ DataGridView (hiển thị dạng NCC01, NCC10,...)
            string supplierCode = dgvTable.SelectedRows[0].Cells["idSupplier"].Value.ToString();
            int idSupplier;

            // Tách ID từ "NCC01" => 1, "NCC10" => 10
            if (!int.TryParse(supplierCode.Replace("NCC", ""), out idSupplier))
            {
                MessageBox.Show("Lỗi định dạng mã nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lấy dữ liệu từ DataGridView (tránh truy vấn DB nếu không cần thiết)
            string supplierName = dgvTable.SelectedRows[0].Cells["supplierName"].Value.ToString();
            string address = dgvTable.SelectedRows[0].Cells["address"].Value.ToString();
            string phoneNumber = dgvTable.SelectedRows[0].Cells["phone"].Value.ToString();
            string email = dgvTable.SelectedRows[0].Cells["email"].Value.ToString();

            // Tạo đối tượng Supplier
            Supplier selectedSupplier = new Supplier
            {
                displayIdSupplier = supplierCode,
                supplierId = idSupplier,
                supplierName = supplierName,
                address = address,
                phone = phoneNumber,
                email = email
            };

            Form tempForm = new Form();
            tempForm.Text = "Sửa Nhà Cung Cấp";
            tempForm.Size = new Size(491, 350);
            tempForm.FormBorderStyle = FormBorderStyle.None;

            UC_updateSupplier uc = new UC_updateSupplier(selectedSupplier);
            uc.Dock = DockStyle.Fill;

            uc.OnSupplierUpdated += LoadSuppliers;

            // Khi nhấn "Hủy" hoặc thêm xong, đóng form
            uc.OnCloseForm += () => tempForm.Close();

            tempForm.Controls.Add(uc);

            tempForm.StartPosition = FormStartPosition.CenterScreen;
            tempForm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string supplierCode = dgvTable.SelectedRows[0].Cells["idSupplier"].Value.ToString();
            int idSupplier;

            // Tách ID từ "NCC01" => 1, "NCC10" => 10
            if (!int.TryParse(supplierCode.Replace("NCC", ""), out idSupplier))
            {
                MessageBox.Show("Lỗi định dạng mã nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!supplierBLL.DeleteSupplier(idSupplier, out string errorMessage))
                {
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSuppliers(); // Cập nhật lại danh sách
                }
            }
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            dgvTable.Rows.Clear();

            List<Supplier> suppliers = supplierBLL.GetAllSuppliers(txbSearch.Text.Trim());
            foreach (var supplier in suppliers)
            {
                dgvTable.Rows.Add(supplier.displayIdSupplier, supplier.supplierName, supplier.phone, supplier.email, supplier.address);
            }
        }
    }
}
