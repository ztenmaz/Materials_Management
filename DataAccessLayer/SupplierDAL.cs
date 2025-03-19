using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Runtime.Remoting.Messaging;

namespace DataAccessLayer
{
    public class SupplierDAL
    {
        public List<Supplier> GetSuppliers(string keyword = null)
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("suppiler_GetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@keyword", string.IsNullOrEmpty(keyword) ? (object)DBNull.Value : keyword);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    suppliers.Add(new Supplier
                    {
                        supplierId = reader.GetInt32(0),
                        displayIdSupplier = reader.GetString(1),
                        supplierName = reader.GetString(2),
                        address = reader.GetString(3),
                        phone = reader.GetString(4),
                        email = reader.GetString(5),
                    });
                }
            }

            return suppliers;
        }

        public bool AddSupplier(Supplier supplier)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("supplier_Add", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@supplierName", supplier.supplierName);
                cmd.Parameters.AddWithValue("@address", supplier.address);
                cmd.Parameters.AddWithValue("@phoneNumber", supplier.phone);
                cmd.Parameters.AddWithValue("@email", supplier.email);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateSupplier(Supplier supplier)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("supplier_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idSupplier", supplier.supplierId);
                    cmd.Parameters.AddWithValue("@supplierName", supplier.supplierName);
                    cmd.Parameters.AddWithValue("@address", supplier.address);
                    cmd.Parameters.AddWithValue("@phoneNumber", supplier.phone);
                    cmd.Parameters.AddWithValue("@email", supplier.email);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool CheckSupplierInUse(int idSupplier)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("supplier_CheckBeforeDelete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idSupplier", idSupplier);

                    conn.Open();
                    int result = (int)cmd.ExecuteScalar(); // Nhận kết quả từ SP
                    return result == 1; // Nếu trả về 1, nhà cung cấp đang được sử dụng
                }
            }
        }
        public bool DeleteSupplier(int idSupplier)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("supplier_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idSupplier", idSupplier);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }

}
