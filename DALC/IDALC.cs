using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALC
{
    internal interface IDALC
    {
        List<Category> getAllCategory();
        string addNewsToSpecificCategory(NewsRequest news);
        List<News> getAllNewsForASpecificCategory(int category_id);
        List<News> slider_news();
    }
}
