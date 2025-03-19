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
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Presentation
{
    public partial class UC_updateMaterial : UserControl
    {
        private SupplierBLL supplierBLL = new SupplierBLL();
        private MaterialBLL materialBLL = new MaterialBLL();
        private Material currentMaterial;
        public event Action OnMaterialUpdated; // Sự kiện để cập nhật danh sách
        public event Action OnCloseForm;     // Sự kiện để đóng form cha
        public UC_updateMaterial(Material material)
        {
            InitializeComponent();
            currentMaterial = material;
            LoadSupplierData();
            LoadSuppliers();
            txbIdMaterial.Enabled = false;
        }

        private void LoadSuppliers()
        {
            List<Supplier> suppliers = supplierBLL.GetAllSuppliers();
            suppliers.Insert(0, new Supplier { supplierId = 0, supplierName = "---Chọn nhà cung cấp---" });

            cbbSupplier.DataSource = suppliers;
            cbbSupplier.DisplayMember = "supplierName"; // Hiển thị tên nhà cung cấp
            cbbSupplier.ValueMember = "supplierId"; // Giá trị là ID nhà cung cấp

            cbbSupplier.Text = currentMaterial.supplierName;
        }

        private void LoadSupplierData()
        {
            txbIdMaterial.Text = currentMaterial.idMaterial;
            txbNameMaterial.Text = currentMaterial.nameMaterial;
            cbbSupplier.Text = currentMaterial.supplierName;
            txbQuantity.Text = currentMaterial.quantity.ToString();
            txbUnit.Text = currentMaterial.unit;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int parsedQuantity;
            currentMaterial.nameMaterial = txbNameMaterial.Text.Trim();
            currentMaterial.supplierName = cbbSupplier.SelectedValue.ToString().Trim();
            currentMaterial.quantity = int.TryParse(txbQuantity.Text.Trim(), out parsedQuantity) ? parsedQuantity : (int?)null; // Gán null nếu không nhập
            currentMaterial.unit = txbUnit.Text.Trim();

            if (materialBLL.UpdateMaterial(currentMaterial, out string errorMessage))
            {
                MessageBox.Show("Cập nhật vật tư thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnMaterialUpdated?.Invoke(); // Gửi sự kiện cập nhật danh sách
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
