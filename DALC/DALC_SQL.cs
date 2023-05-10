using Entities;
using Microsoft.Data.SqlClient;
using System;

namespace DALC
{
    public class DALC_SQL : IDALC
    {
        public string _CONN_STR = "data Source=127.0.0.1,1433;database=e-news;user id=sa;password=AA123456@@;TrustServerCertificate=True";

        public string addNewsToSpecificCategory(NewsRequest news)
        {
            using (SqlConnection connection = new SqlConnection(_CONN_STR))
            {
                connection.Open();
                var sql = $"EXECUTE [dbo].[ADD_NEWS]  '{news.title}', " +
                    $" '{news.short_desc}' , " +
                    $"'{news.long_desc}', " +
                    $"{news.category.id}, " +
                    $"'{news.img_url}', " +
                    $"'{news.author}';";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return "news has been added!";
        }

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

        public List<News> getAllNewsForASpecificCategory(int category_id)
        {
            List<News> news = new List<News>();
            using (SqlConnection connection = new SqlConnection(_CONN_STR))
            {
                connection.Open();
                String sql = $"EXECUTE  [dbo].[GET_NEWS_FOR_EACH_CATEGORY] {category_id};";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            News p = new()
                            {
                                id = (int) reader.GetInt64(0),
                                author = reader.GetString(1),
                                title = reader.GetString(2),
                                short_desc = reader.GetString(3),
                                long_desc = reader.GetString(4),
                                img_url = reader.GetString(5),
                                created_at = (string) reader.GetDateTime(6).ToString("yyyy-MM-dd HH:mm:ss"),
                                category = new() { name = reader.GetString(7) }
                            };
                            news.Add(p);
                        }
                    }
                }
            }
            return news;
        }

        public List<News> slider_news()
        {
            List<News> SLIDER_NEWS = new List<News>();
            using (SqlConnection connection = new SqlConnection(_CONN_STR))
            {
                connection.Open();
                String sql = $"EXECUTE  [dbo].[SLIDER_NEWS];";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            News p = new()
                            {
                                id = (int)reader.GetInt64(0),
                                title = reader.GetString(1),
                                author = reader.GetString(2),
                                short_desc = reader.GetString(3),
                                img_url = reader.GetString(4),
                                created_at = (string)reader.GetDateTime(5).ToString("yyyy-MM-dd HH:mm:ss"),
                                category = new() { name = reader.GetString(6) }
                            };
                            SLIDER_NEWS.Add(p);
                        }
                    }
                }
            }
            return SLIDER_NEWS;
        }
    }
}