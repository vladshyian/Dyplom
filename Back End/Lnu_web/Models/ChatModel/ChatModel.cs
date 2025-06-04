namespace Lnu_web.Models.ChatModel
{
    public class ChatModel
    {
        public string MessageId { get; set; }
        public string Text { get; set; }
        public DateTime SendAt { get; set; }
        public string SenderId { get; set; }
        public string? SenderName {  get; set; }
        public string ReceiverId { get; set; }
        public bool isRead { get; set; } = false;
    }
}
