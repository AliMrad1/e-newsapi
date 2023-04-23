namespace Entities
{
    public class Category
    {
        public int id { set; get; }
        public string name { set; get; }
        public string img_url { set; get; }
    }

    public class News
    { 
        public int id { set; get; }
        public string title { set; get; }
        public string img_url { set; get;}
        public string short_desc { set; get; }
        public string long_desc { set; get;}
        public Category category { set; get; }
        public string created_at { set; get; }
        public string author { set; get;}
    }

    public record NewsRequest(string title, string short_desc, string long_desc, string? img_url, CategoryRequest category, string author) { }

    public record CategoryRequest(int id) { }
}