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
    public partial class UC_addMaterial : UserControl
    {
        private SupplierBLL supplierBLL = new SupplierBLL();
        private MaterialBLL materialBLL = new MaterialBLL();
        public event Action OnMaterialAdded; // Sự kiện để cập nhật danh sách
        public event Action OnCloseForm;     // Sự kiện để đóng form cha
        public UC_addMaterial()
        {
            InitializeComponent();
            LoadSuppliers();
        }
        private void LoadSuppliers()
        {
            List<Supplier> suppliers = supplierBLL.GetAllSuppliers();
            suppliers.Insert(0, new Supplier { supplierId = 0, supplierName = "---Chọn nhà cung cấp---" });

            cbbSupplier.DataSource = suppliers;
            cbbSupplier.DisplayMember = "supplierName"; // Hiển thị tên nhà cung cấp
            cbbSupplier.ValueMember = "supplierId"; // Giá trị là ID nhà cung cấp
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int parsedQuantity;
            Material material = new Material
            {
                nameMaterial = txbNameMaterial.Text.Trim(),
                supplierName = cbbSupplier.SelectedValue.ToString().Trim(),
                quantity = int.TryParse(txbQuantity.Text.Trim(), out parsedQuantity) ? parsedQuantity : (int?)null, // Gán null nếu không nhập
                unit = txbUnit.Text.Trim()
            };

            if (materialBLL.AddMaterial(material, out string errorMessage))
            {
                MessageBox.Show("Thêm vật tư thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnMaterialAdded?.Invoke(); // Gửi sự kiện cập nhật danh sách
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
