using Lnu_web.Dbo.User;
using Lnu_web.Interfaces.Reposotories.ILogin;
using Microsoft.AspNetCore.Identity;

namespace Lnu_web.Reposotories.LoginRepository
{
    public class Login : ILogin
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Login(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<SignInResult> SignInUserAsync(ApplicationUser user, string password)
        {

            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;

        }
    }
}
