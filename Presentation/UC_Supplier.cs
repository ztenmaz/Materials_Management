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

            tempForm.Controls.Add(uc);

            tempForm.StartPosition = FormStartPosition.CenterScreen;
            tempForm.ShowDialog();
        }
    }
}
