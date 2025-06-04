using Lnu_web.Models.ChatModel;

namespace Lnu_web.Interfaces.Reposotories.IChatRepository
{
    public interface IChatRepository
    {
        public Task<string> SaveMessage(ChatModel chatModel);
        public Task<List<ChatModel>> GetChatHistory(string senderId, string receiverId);
        public Task<bool> MakeMessageAsRead(string messageId);
        public Task<string> CreateGroup(CreateGroupRequest groupRequest);
    }
}
