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
    }
}