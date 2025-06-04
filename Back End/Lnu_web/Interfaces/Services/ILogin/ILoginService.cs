using Lnu_web.Dbo.User;
using Microsoft.AspNetCore.Identity;

namespace Lnu_web.Interfaces.Services.ILogin
{
    public interface ILoginService
    {
        Task<(SignInResult Result, ApplicationUser User)> LoginAsync(string username, string password);
    }
}
