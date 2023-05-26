using BLC;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace e_newsapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private BLC_AUTH _BLC_AUTH;
        private IConfiguration _config;

        public AuthController(IConfiguration configuration) 
        {
            this._config = configuration;
            this._BLC_AUTH = new(this._config);
        }

        [HttpGet("/roles")]
        public List<Role> GET_ROLES()
        {
            return _BLC_AUTH.GET_ROLES();
        }

        [HttpPost("/signup")]
        public IActionResult signup([FromBody] RegistrationBodyRequest user_info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                this._BLC_AUTH.user_registration(user_info);
                return Ok("Registration Successfully!");
            }
            catch (UserEmailExistException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/signin")]
        public IActionResult signinp([FromBody] LoginRequestBody user_info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string msg = this._BLC_AUTH.user_login(user_info);
            return Ok(new { Token = msg });

        }
    }
}
