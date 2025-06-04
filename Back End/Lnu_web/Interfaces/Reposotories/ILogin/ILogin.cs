using Lnu_web.Dbo.User;
using Microsoft.AspNetCore.Identity;

namespace Lnu_web.Interfaces.Reposotories.ILogin
{
    public interface ILogin
    {
        Task<SignInResult> SignInUserAsync(ApplicationUser user, string password);
    }
}
