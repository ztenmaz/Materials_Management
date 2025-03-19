using BusinessLogicLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Presentation
{
    public partial class UC_Material : UserControl
    {
        private MaterialBLL materialsBLL = new MaterialBLL();
        public UC_Material()
        {
            InitializeComponent();
            LoadMaterials();
        }
        private void LoadMaterials()
        {
            dgvTable.Rows.Clear();

            List<Material> materials = materialsBLL.GetAllMaterials();
            foreach (var material in materials)
            {
                dgvTable.Rows.Add(material.idMaterial, material.nameMaterial, material.supplierName, material.quantity, material.unit, material.status);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form tempForm = new Form();
            tempForm.Text = "Thêm vật tư";
            tempForm.Size = new Size(491, 350);
            tempForm.FormBorderStyle = FormBorderStyle.None;

            UC_addMaterial uc = new UC_addMaterial();
            uc.Dock = DockStyle.Fill;

            uc.OnMaterialAdded += LoadMaterials;

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
                MessageBox.Show("Vui lòng chọn một vật tư để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvTable.CurrentRow;
            string materialCode = selectedRow.Cells["idMaterial"].Value.ToString();

            // Tách idMaterial từ mã VTxx
            int idMaterial;
            if (!int.TryParse(materialCode.Replace("VT", ""), out idMaterial))
            {
                MessageBox.Show("Lỗi định dạng mã vật tư!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nameMaterial = selectedRow.Cells["nameMaterial"].Value.ToString();
            string supplierName = selectedRow.Cells["idSupplier"].Value.ToString();
            int quantity = Convert.ToInt32(selectedRow.Cells["quantity"].Value);
            string unit = selectedRow.Cells["unit"].Value.ToString();

            // Tạo đối tượng Material
            Material selectedMaterial = new Material
            {
                idMaterial = idMaterial.ToString(),
                nameMaterial = nameMaterial,
                supplierName = supplierName,
                quantity = quantity,
                unit = unit
            };

            Form tempForm = new Form();
            tempForm.Text = "Sửa vật tư";
            tempForm.Size = new Size(491, 350);
            tempForm.FormBorderStyle = FormBorderStyle.None;

            UC_updateMaterial uc = new UC_updateMaterial(selectedMaterial);
            uc.Dock = DockStyle.Fill;

            uc.OnMaterialUpdated += LoadMaterials;

            // Khi nhấn "Hủy" hoặc thêm xong, đóng form
            uc.OnCloseForm += () => tempForm.Close();

            tempForm.Controls.Add(uc);

            tempForm.StartPosition = FormStartPosition.CenterScreen;
            tempForm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            dgvTable.Rows.Clear();

            List<Material> materials = materialsBLL.GetAllMaterials(txbSearch.Text.Trim());
            foreach (var material in materials)
            {
                dgvTable.Rows.Add(material.idMaterial, material.nameMaterial, material.supplierName, material.quantity, material.unit, material.status);
            }
        }
    }
}
