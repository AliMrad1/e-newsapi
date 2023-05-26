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

        [HttpPost("addnews"), DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public IActionResult addNewsToCategory(IFormFile file)
        {
            if (file.Length <= 0)
            {
                return BadRequest("No file is selected");
            }

            //string response = blc.addNewsToCategory(News,file);

           // AddNewsResponse p = new(response);
            return Ok("wooww");
        }

        [HttpGet("news")]
        public List<News> GetNews()
        {
            int id = Convert.ToInt32(Request.Query["id"]);
            return blc.GetNews(id);
        }

        [HttpGet("slider/news")]
        public List<News> SLIDER_NEWS()
        {
            return blc.SLIDER_NEWS();
        }
    }
}