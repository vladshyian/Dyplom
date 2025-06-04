using Lnu_web.Dbo.Chat;
using Lnu_web.Models.User;

namespace Lnu_web.Models.ChatModel
{
    public class AddUserRequest
    {
        public Guid GroupId { get; set; }
        public List<UserModel> Users
        {
            get; set;
        }
    }
}
