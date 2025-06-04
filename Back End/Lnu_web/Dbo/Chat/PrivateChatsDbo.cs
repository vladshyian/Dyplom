namespace Lnu_web.Dbo.Chat
{
    public class PrivateChatsDbo
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public bool isRead { get; set; }
    }
}
