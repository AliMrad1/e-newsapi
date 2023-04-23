using BLC;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace e_newsapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private BLC.BLC blc;

        public CategoryController() 
        {
            this.blc = new BLC.BLC();
        }

        [HttpGet("all")]
        public List<Category> GetCategories()
        {
           return blc.GetCategories();
        }

        [HttpPost("addnews")]
        public string addNewsToCategory(NewsRequest news)
        {
            return blc.addNewsToCategory(news);
        }

        [HttpGet("news")]
        public List<News> GetNews()
        {
            int id = Convert.ToInt32(Request.Query["id"]);
            return blc.GetNews(id);
        }
    }
}