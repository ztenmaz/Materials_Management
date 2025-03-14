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
        public List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("suppiler_GetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    suppliers.Add(new Supplier
                    {
                        displayIdSupplier = reader.GetString(0),
                        supplierName = reader.GetString(1),
                        address = reader.GetString(2),
                        phone = reader.GetString(3),
                        email = reader.GetString(4),
                    });
                }
            }

            return suppliers;
        }

        public bool AddSupplier(Supplier supplier)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_AddSupplier", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@supplierName", supplier.supplierName);
                cmd.Parameters.AddWithValue("@address", supplier.address);
                cmd.Parameters.AddWithValue("@phoneNumber", supplier.phone);
                cmd.Parameters.AddWithValue("@email", supplier.email);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

}
