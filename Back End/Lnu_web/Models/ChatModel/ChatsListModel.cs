namespace Lnu_web.Models.ChatModel
{
    public class ChatsListModel
    {
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
        public string SenderName { get; set; }
        public string RecieverName { get; set; }
        public string UserPhoto { get; set; }
        public string Text {  get; set; }
        public DateTime? Time { get; set; }
        public string ChatType { get; set; }

    }
}
