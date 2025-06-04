using Lnu_web.Data;
using Lnu_web.Dbo.Chat;
using Lnu_web.Interfaces.Services.IChatRepository;
using Lnu_web.Models.ChatModel;
using Lnu_web.Models.User;
using Lnu_web.Reposotories.ChatRepository;

namespace Lnu_web.Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly ChatRepository _chatRepository;

        public ChatService(ChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<List<ChatModel>> GetChatHistory(string senderId, string receiverId)
        {
            return await _chatRepository.GetChatHistory(senderId, receiverId);
        }
        public async Task<string> SaveMessage(ChatModel chatModel)
        {
            return await _chatRepository.SaveMessage(chatModel);
        }

        public async Task<List<ChatsListModel>> GetUserChats(string userId)
        {
            return await _chatRepository.GetUserChats(userId);
        }

        public async Task<bool> MakeMessageAsRead(string messageId)
        {
            return await _chatRepository.MakeMessageAsRead(messageId);
        }

        public async Task<string> CreateGroup(CreateGroupRequest groupRequest)
        {
            if (groupRequest != null)
            {
                return await _chatRepository.CreateGroup(groupRequest);
            }

            return null;
        }

        public async Task<string> SentGroupMessage(ChatModel model)
        {
            return await _chatRepository.SentGroupMessage(model);
        }

        public async Task<List<GroupMessageDbo>> GetGroupChatHistoryAsync(Guid groupId)
        {
            var messages = await _chatRepository.GetGroupChatHistoryAsync(groupId);

            if (messages != null)
            {
                return messages;
            };

            return null;
        }

        public async Task<bool> MakeGroupMessageAsRead(Guid messageId)
        {
            return await _chatRepository.MakeGroupMessageAsRead(messageId);
        }

        public async Task<List<UserModel>> GetUsersByGroup(Guid groupId)
        {
            if (groupId == Guid.Empty)
            {
                return null;
            }

            var usersList = await _chatRepository.GetUsersByGroup(groupId);

            if (usersList != null)
            {
                return usersList;
            }

            return null;
        }

        public async Task<bool> DeleteUserFromGroup(List<Guid> userIds, Guid groupId)
        {
            if (userIds == null || !userIds.Any())
                return false;

            return await _chatRepository.DeleteUserFromGroup(userIds, groupId);
        }

        public async Task<bool> AddUsersToGroup(AddUserRequest request)
        {
            return await _chatRepository.AddUsersToGroup(request);
        }
    }
}
