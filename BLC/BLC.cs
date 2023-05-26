using Entities;
using DALC;
using Microsoft.AspNetCore.Http;

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

        public string addNewsToCategory(NewsRequest news, IFormFile file_s)
        {
            var file = file_s;
            var firebaseUploadImage = new FirebaseOperations(file);
            firebaseUploadImage.UpdownImage();
            string download_url = firebaseUploadImage.downloadUrl;
            news.img_url = download_url;
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