using Lnu_web.Dbo.User;
using Lnu_web.Interfaces.Services.ILogin;
using Lnu_web.Reposotories.LoginRepository;
using Microsoft.AspNetCore.Identity;

namespace Lnu_web.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly Login _loginRepository;

        public LoginService(Login loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<(SignInResult Result, ApplicationUser User)> LoginAsync(string username, string password)
        {
            var user = await _loginRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return (SignInResult.Failed, null);
            }

            var result = await _loginRepository.SignInUserAsync(user, password);
            return (result, user);
        }

    }
}
