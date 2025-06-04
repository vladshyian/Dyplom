using AutoMapper;
using Lnu_web.Data;
using Lnu_web.Dbo.Chat;
using Lnu_web.Interfaces.Reposotories.IChatRepository;
using Lnu_web.Models.ChatModel;
using Lnu_web.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lnu_web.Reposotories.ChatRepository
{
    public class ChatRepository : IChatRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ChatRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<ChatModel>> GetChatHistory(string senderId, string receiverId)
        {
            var messages = await _dataContext.PrivateChats
                                               .Where(chat =>
                                                   (chat.SenderId == senderId && chat.ReceiverId == receiverId) ||
                                                   (chat.SenderId == receiverId && chat.ReceiverId == senderId))
                                               .OrderBy(chat => chat.SendAt) 
                                               .ToListAsync();


            var chatHistory = messages.Select(chat => new ChatModel
            {
                MessageId = chat.MessageId.ToString(),
                SenderId = chat.SenderId,
                ReceiverId = chat.ReceiverId,
                Text = chat.Content,
                SendAt = chat.SendAt,
                isRead = chat.isRead
            }).ToList();

            return chatHistory;
        }
        public async Task<string> SaveMessage(ChatModel model)
        {
            var message = new PrivateChatsDbo
            {
                MessageId = Guid.NewGuid(),
                SenderId = model.SenderId,
                ReceiverId = model.ReceiverId,
                Content = model.Text,
                SendAt = DateTime.Now,
                isRead = false
            };

            await _dataContext.PrivateChats.AddAsync(message);

            await _dataContext.SaveChangesAsync();

            return message.MessageId.ToString();
        }
        public async Task<List<ChatsListModel>> GetUserChats(string userId)
        {
            var userGuid = Guid.Parse(userId);

            var privateChats = await _dataContext.PrivateChats
                .Where(chat => chat.SenderId == userId || chat.ReceiverId == userId)
                .ToListAsync();

            var groupedPrivateChats = privateChats
                .GroupBy(chat => chat.SenderId == userId ? chat.ReceiverId : chat.SenderId)
                .Select(group => new
                {
                    ReceiverId = group.Key,
                    LatestMessage = group.OrderByDescending(chat => chat.SendAt).FirstOrDefault()
                });

            var privateChatModels = new List<ChatsListModel>();

            foreach (var chat in groupedPrivateChats)
            {
                var teacher = await _dataContext.Teachers
                    .Include(t => t.CoreInfo)
                    .FirstOrDefaultAsync(t => t.Id.ToString() == chat.ReceiverId);

                var student = await _dataContext.Students
                    .Include(s => s.CoreStudent)
                    .FirstOrDefaultAsync(s => s.Id.ToString() == chat.ReceiverId);

                privateChatModels.Add(new ChatsListModel
                {
                    RecieverId = chat.ReceiverId.ToString(),
                    RecieverName = teacher?.CoreInfo?.Name ?? student?.CoreStudent?.Name ?? "Unknown",
                    UserPhoto = teacher?.CoreInfo?.PhotoPath,
                    Text = chat.LatestMessage?.Content,
                    Time = chat.LatestMessage?.SendAt,
                    ChatType = "Private"
                });
            }

            var groupUsers = await _dataContext.GroupUsers
                .Where(c=>c.UserId == userGuid).ToListAsync();

            var groupChatModels = new List<ChatsListModel>();

            if (groupUsers != null)
            {
                var groupChats = new List<GroupDbo>();

                foreach (var user in groupUsers)
                {
                    var groupChat = await _dataContext.ChatGroups
                    .Where(gc => gc.Id == user.GroupId)
                    .FirstOrDefaultAsync();

                    if (groupChat != null)
                    {
                        groupChats.Add(groupChat);
                    }
                }

                foreach (var groupChat in groupChats)
                {
                    var latestMessage = await _dataContext.Messages
                    .Where(m => m.GroupId == groupChat.Id)
                    .OrderByDescending(m => m.SendAt)
                    .FirstOrDefaultAsync();

                    groupChatModels.Add(new ChatsListModel
                    {
                        RecieverId = groupChat.Id.ToString(),
                        RecieverName = groupChat.GroupName,
                        UserPhoto = null,
                        Text = latestMessage?.Text,
                        Time = latestMessage?.SendAt,
                        ChatType = "Group"
                    });
                }
            }
            
            var result = privateChatModels
                .Concat(groupChatModels)
                .OrderByDescending(c => c.Time)
                .ToList();

            return result;
        }
        public async Task<bool> MakeMessageAsRead(string messageId)
        {
            Guid guid = Guid.Parse(messageId);
            var message = await _dataContext.PrivateChats.FirstOrDefaultAsync(c=> c.MessageId == guid);

            if(message == null)
            {
                return false;
            }

            message.isRead = true;

            await _dataContext.SaveChangesAsync();

            return true;
        }
        public async Task<string> CreateGroup(CreateGroupRequest groupRequest)
        {

            var newGroup = new GroupDbo
            {
                Id = Guid.NewGuid(),
                GroupName = groupRequest.GroupName,
                CreatedBy = groupRequest.AdminId,
                CreatedAt = DateTime.UtcNow,
            };

            for (int i = 0; i < groupRequest.UserIds.Count; i++)
            {
                newGroup.GroupUsers.Add(new GroupUsersDbo
                {
                    Id = Guid.NewGuid(),
                    GroupId = newGroup.Id,
                    UserId = groupRequest.UserIds[i],
                    User = groupRequest.UserName[i]
                });
            }


            await _dataContext.ChatGroups.AddAsync(newGroup);
            await _dataContext.SaveChangesAsync();

            return newGroup.Id.ToString();
        }
        public async Task<string> SentGroupMessage(ChatModel model)
        {
            var message = new GroupMessageDbo
            {
                MessageId = Guid.NewGuid(),
                SenderId = Guid.Parse(model.SenderId),
                SenderName = model.SenderName,
                GroupId = Guid.Parse(model.ReceiverId),
                Text = model.Text,
                SendAt = DateTime.Now,
                IsRead = false,
            };

            await _dataContext.Messages.AddAsync(message);

            await _dataContext.SaveChangesAsync();

            return message.MessageId.ToString();
        }
        public async Task<List<GroupMessageDbo>> GetGroupChatHistoryAsync(Guid groupId)
        {
            var messages = await _dataContext.Messages
                                              .Where(m => m.GroupId == groupId)
                                              .OrderBy(m => m.SendAt)        
                                              .ToListAsync();           

            return messages;
        }
        public async Task<bool> MakeGroupMessageAsRead(Guid messageId)
        {
            var message = await _dataContext.Messages.FirstOrDefaultAsync(m => m.MessageId == messageId);

            if(message == null)
            {
                return false;
            }

            message.IsRead = true;

            await _dataContext.SaveChangesAsync();

            return true;
        }
        public async Task<List<UserModel>> GetUsersByGroup(Guid groupId)
        {
            if (groupId == Guid.Empty)
            {
                return null;
            }

            var users = await _dataContext.GroupUsers.Where(s => s.GroupId == groupId).ToListAsync();

            if(users.Count == 0)
            {
                return null;
            }

            var usersList = _mapper.Map<List<UserModel>>(users);

            return usersList;
        }
        public async Task<bool> DeleteUserFromGroup(List<Guid> userIds, Guid groupId)
        {
            if (userIds == null || !userIds.Any())
                return false;

            try
            {
                var group = await _dataContext.ChatGroups
                    .Include(g => g.GroupUsers) // Завантажуємо зв'язки
                    .FirstOrDefaultAsync(g => g.Id == groupId);

                if (group == null)
                    return false;

                var usersToRemove = group.GroupUsers.Where(u => userIds.Contains(u.UserId)).ToList();

                if (!usersToRemove.Any())
                {
                    Console.WriteLine("Користувачів для видалення не знайдено.");
                    return false;
                }

                foreach (var user in usersToRemove)
                {
                    group.GroupUsers.Remove(user); // Видаляємо користувачів
                }

                await _dataContext.SaveChangesAsync(); // Зберігаємо зміни

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка видалення користувачів: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddUsersToGroup(AddUserRequest request)
        {
            if (request == null || request.Users == null || !request.Users.Any())
            {
                return false;
            }

            var group = await _dataContext.ChatGroups.FirstOrDefaultAsync(c => c.Id == request.GroupId);
            if (group == null)
            {
                return false;
            }

            foreach (var user in request.Users)
            {
                var groupUser = new GroupUsersDbo
                {
                    Id = Guid.NewGuid(),
                    GroupId = request.GroupId,
                    UserId = Guid.Parse(user.Id),
                    User = user.RecieverName
                };

                var isUserInGroup = await _dataContext.GroupUsers
                    .AnyAsync(gu => gu.GroupId == request.GroupId && gu.UserId == Guid.Parse(user.Id));

                if (!isUserInGroup)
                {
                    _dataContext.GroupUsers.Add(groupUser); 
                }
            }

            var result = await _dataContext.SaveChangesAsync();
            return true;
        }


    }
}

