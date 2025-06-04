using Lnu_web.Models.LoginModel;
using Lnu_web.Services.LoginService;
using Lnu_web.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Lnu_web.Controllers.LoginController
{
    [ApiController]
    [Route("Login")]
    public class LoginController : Controller
    {

        private readonly LoginService _accountService;

        public LoginController(LoginService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("LoginStudent")]
        public async Task<IActionResult> LoginStudent([FromBody] Models.LoginModel.LoginRequest model)
        {
            var (result, user) = await _accountService.LoginAsync(model.Email, model.Password);
            if (result.Succeeded && user != null)
            { 
                return Ok(new
                {
                    userId = user.StudentId,
                    message = "Login successful",
                });
            }

            return Unauthorized("Invalid username or password");
        }

        [HttpPost("LoginTeacher")]
        public async Task<IActionResult> LoginTeacher([FromBody] Models.LoginModel.LoginRequest model)
        {
            var (result, user) = await _accountService.LoginAsync(model.Email, model.Password);
            if (result.Succeeded && user != null)
            {

                return Ok (new
                {
                    userId = user.TeacherId,
                    message = "Login successful",
                });
            }

            return Unauthorized("Invalid username or password");
        }


    }
}
