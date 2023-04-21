using Entities;
using Microsoft.Data.SqlClient;
using System;

namespace DALC
{
    public class DALC_SQL : IDALC
    {
        public string _CONN_STR = "data Source=127.0.0.1,1433;database=e-news;user id=sa;password=AA123456@@;TrustServerCertificate=True";

        public List<Category> getAllCategory()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(_CONN_STR))
            {
                connection.Open();
                String sql = "EXEC [dbo].[GET_ALL_CATEGORIES];";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category p = new()
                            {
                                id = (int)reader.GetInt64(0),
                                name = reader.GetString(1),
                                img_url = reader.GetString(2)
                            };
                            categories.Add(p);
                        }
                    }
                }
            }
            return categories;
        }
    }
}