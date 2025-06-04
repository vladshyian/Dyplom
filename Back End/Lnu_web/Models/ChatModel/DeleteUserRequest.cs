namespace Lnu_web.Models.ChatModel
{
    public class DeleteUserRequest
    {
        public Guid GroupId { get; set; }
        public List<Guid> UsersId { get; set; }
    }

}
