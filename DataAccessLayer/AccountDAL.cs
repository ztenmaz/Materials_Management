using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccessLayer
{
    public class AccountDAL
    {
        public static bool CheckLogin(Account account)
        {
            bool isValidUser = false;
            using (SqlConnection sqlConnection = DatabaseConnect.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("proc_logic", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@user", account.username);
                    sqlCommand.Parameters.AddWithValue("@pass", account.password);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            isValidUser = true;
                        }
                    }
                }
            }
            return isValidUser;
        }

        public static bool CheckUserAndEmailExists(string username, string email)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("CheckUsernameAndEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);

                    SqlParameter existsParam = new SqlParameter("@Exists", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(existsParam);

                    cmd.ExecuteNonQuery();

                    return existsParam.Value != DBNull.Value && (bool)existsParam.Value;
                }
            }
        }
        public static bool ChangeUserPassword(string username, string newPassword)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ChangeUserPassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_username", username);
                    cmd.Parameters.AddWithValue("@p_new_password", newPassword);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
