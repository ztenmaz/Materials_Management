using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class MaterialBLL
    {
        private MaterialDAL materialDAL = new MaterialDAL();

        public List<Material> GetAllMaterials(string keyword = null)
        {
            return materialDAL.GetAllMaterials(keyword);
        }
        public bool AddMaterial(Material material,out string errorMessage)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(material.nameMaterial))
            {
                errorMessage = "Tên vật tư không được để trống!";
                return false;
            }

            if (material.supplierName == "0") // Giả sử "0" là giá trị mặc định khi chưa chọn
            {
                errorMessage = "Vui lòng chọn nhà cung cấp!";
                return false;
            }

            // Kiểm tra số lượng (quantity)
            if (!material.quantity.HasValue) // Kiểm tra null
            {
                errorMessage = "Vui lòng nhập số lượng!";
                return false;
            }

            if (material.quantity <= 0) // Kiểm tra số lượng phải lớn hơn 0
            {
                errorMessage = "Số lượng phải lớn hơn 0!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(material.unit))
            {
                errorMessage = "Đơn vị không được để trống!";
                return false;
            }

            // Nếu hợp lệ, gọi DAL để thêm vật tư
            bool isAdded = materialDAL.AddMaterial(material);

            if (!isAdded)
            {
                errorMessage = "Thêm vật tư thất bại!";
                return false;
            }

            errorMessage = null; // Không có lỗi
            return true;
        }
        public bool UpdateMaterial(Material material, out string errorMessage)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(material.nameMaterial))
            {
                errorMessage = "Tên vật tư không được để trống!";
                return false;
            }

            if (material.supplierName == "0") // Giả sử "0" là giá trị mặc định khi chưa chọn
            {
                errorMessage = "Vui lòng chọn nhà cung cấp!";
                return false;
            }

            // Kiểm tra số lượng (quantity)
            if (!material.quantity.HasValue) // Kiểm tra null
            {
                errorMessage = "Vui lòng nhập số lượng!";
                return false;
            }

            /*if (material.quantity <= 0) // Kiểm tra số lượng phải lớn hơn 0
            {
                errorMessage = "Số lượng phải lớn hơn 0!";
                return false;
            }*/

            if (string.IsNullOrWhiteSpace(material.unit))
            {
                errorMessage = "Đơn vị không được để trống!";
                return false;
            }

            // Nếu hợp lệ, gọi DAL để thêm vật tư
            bool isUpdated = materialDAL.UpdateMaterial(material);

            if (!isUpdated)
            {
                errorMessage = "Cập nhật vật tư thất bại!";
                return false;
            }

            errorMessage = null; // Không có lỗi
            return true;
        }
    }
}
