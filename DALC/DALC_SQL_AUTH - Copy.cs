using Entities;
using Microsoft.Data.SqlClient;

namespace DALC
{
    public class DALC_SQL_AUTH : IDALC_SQL_AUTH
    {

        public string _CONN_STR = "data Source=127.0.0.1,1433;database=e-news;user id=sa;password=AA123456@@;TrustServerCertificate=True";

        public List<Role> get_roles()
        {
            List<Role> roles = new List<Role>();
            using (SqlConnection connection = new SqlConnection(_CONN_STR))
            {
                connection.Open();
                String sql = "EXECUTE [dbo].[GET_ROLES];";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Role p = new()
                            {
                                id = (int)reader.GetInt64(0),
                                role_name = reader.GetString(1),
                            };
                            roles.Add(p);
                        }
                    }
                }
            }
            return roles;
        }

        public void user_register(RegistrationBodyRequest user_info, string encrypted_pass, DateOnly date)
        {
            using (SqlConnection connection = new SqlConnection(_CONN_STR))
            {
                connection.Open();
                var sql = $"EXECUTE [dbo].[ADD_USER]  '{user_info.fullname}', " +
                    $" '{date}' , " +
                    $"'{user_info.email}', " +
                    $"{user_info.role.id}, " +
                    $"'{encrypted_pass}';";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

        }
    }
}
