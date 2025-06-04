namespace Lnu_web.Models.ChatModel
{
    public class CreateGroupRequest
    {
        public string GroupName { get; set; } = string.Empty;
        public Guid AdminId { get; set; }
        public List<Guid> UserIds { get; set; } = new List<Guid>();
        public List<string> UserName { get; set;} = new List<string>();
    }
}
