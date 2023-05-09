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

        public AuthController() 
        {
            this._BLC_AUTH = new();
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

            this._BLC_AUTH.user_registration(user_info);
            return Ok();
        }
    }
}
