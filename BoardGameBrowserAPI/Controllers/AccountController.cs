using BoardGameBrowserAPI.Contracts;
using BoardGameBrowserAPI.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameBrowserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
        {
            this._authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] APIUserDTO userDTO)
        {
            var errors = await _authManager.Register(userDTO);

            if(errors.Any())
            {
                foreach(var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] APIUserDTO userDTO)
        {
            var authResponse = await _authManager.Login(userDTO);

            if(authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
    }
}
