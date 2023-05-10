using Entities;
using DALC;

namespace BLC
{
    public class BLC
    {
        private DALC_SQL DALC_SQL;

        public BLC()
        {
            DALC_SQL = new DALC_SQL();       
        }

        public List<Category> GetCategories()
        {
            return DALC_SQL.getAllCategory();
        }

        public string addNewsToCategory(NewsRequest news)
        {
            return DALC_SQL.addNewsToSpecificCategory(news);
        }

        public List<News> GetNews(int category_id)
        {
            return DALC_SQL.getAllNewsForASpecificCategory(category_id);
        }

        public List<News> SLIDER_NEWS()
        {
            return DALC_SQL.slider_news();
        }
    }
}