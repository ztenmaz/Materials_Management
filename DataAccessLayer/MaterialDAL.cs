using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MaterialDAL
    {
        public List<Material> GetAllMaterials(string keyword = null)
        {
            List<Material> materials = new List<Material>();

            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("material_GetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@keyword", string.IsNullOrEmpty(keyword) ? (object)DBNull.Value : keyword);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    materials.Add(new Material
                    {
                        idMaterial = reader.GetString(0),
                        nameMaterial = reader.GetString(1),
                        supplierName = reader.GetString(2),
                        quantity = reader.GetInt32(3),
                        unit = reader.GetString(4),
                        status = reader.GetString(5)
                    });
                }
            }

            return materials;
        }
        public bool AddMaterial(Material material)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("material_Add", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@nameMaterial", material.nameMaterial);
                        cmd.Parameters.AddWithValue("@idSupplier", material.supplierName);
                        cmd.Parameters.AddWithValue("@quantity", material.quantity);
                        cmd.Parameters.AddWithValue("@unit", material.unit);

                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0; // Trả về true nếu thêm thành công
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                    return false;
                }
            }
        }
        public bool UpdateMaterial(Material material)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("material_Update", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idMaterial", int.Parse(material.idMaterial));
                        cmd.Parameters.AddWithValue("@nameMaterial", material.nameMaterial);
                        cmd.Parameters.AddWithValue("@idSupplier", material.supplierName);
                        cmd.Parameters.AddWithValue("@quantity", material.quantity);
                        cmd.Parameters.AddWithValue("@unit", material.unit);

                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0; // Trả về true nếu thêm thành công
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
