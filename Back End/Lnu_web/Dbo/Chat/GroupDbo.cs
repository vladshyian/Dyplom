using System.ComponentModel.DataAnnotations;

namespace Lnu_web.Dbo.Chat
{
    public class GroupDbo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string GroupName { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<GroupUsersDbo> GroupUsers { get; set; } = new List<GroupUsersDbo>();
        public ICollection<GroupMessageDbo> Messages { get; set; } = new List<GroupMessageDbo>();
    }
}
