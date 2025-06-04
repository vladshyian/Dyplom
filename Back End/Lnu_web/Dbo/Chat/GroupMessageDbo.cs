using System.ComponentModel.DataAnnotations.Schema;

namespace Lnu_web.Dbo.Chat
{
    public class GroupMessageDbo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public Guid GroupId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime SendAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}
