using Lnu_web.Dbo.Chat;
using Lnu_web.Models.ChatModel;
using Lnu_web.Models.User;
using Lnu_web.Services.ChatService;
using Microsoft.AspNetCore.Mvc;


namespace Lnu_web.Controllers.ChatController
{
    [ApiController]
    [Route("Chat")]
    public class ChatController : Controller
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("sentToPrivateChat")]
        public async Task<ActionResult<string>> SendMessage([FromBody] ChatModel model)
        {
            if (model == null)
            {
                return BadRequest(model);
            }

            var messageId = await _chatService.SaveMessage(model);

            if (messageId != null)
            {
                return Ok(new { id = messageId });
            }

            return BadRequest(model);
        }

        [HttpPost("getHistory")]
        public async Task<ActionResult<List<ChatModel>>> GetHistory([FromBody] ChatHistoryRequest historyRequest)
        {
            var chatHistory = await _chatService.GetChatHistory(historyRequest.SenderId, historyRequest.ReceiverId);

            if (chatHistory == null)
            {
                return NotFound();
            }

            return Ok(chatHistory);
        }

        [HttpGet("getUserChats{id}")]
        public async Task<ActionResult<List<ChatsListModel>>> GetUserChats(string id)
        {
            var userChats = await _chatService.GetUserChats(id);

            if (userChats == null)
            {
                return NotFound();
            }

            return Ok(userChats);
        }

        [HttpPatch("updateMessageReadStatus/{messageId}")]
        public async Task<ActionResult> MakeMessageAsRead(string messageId)
        {
            if (messageId == null)
            {
                return BadRequest("MessageId is null");
            }

            if (await _chatService.MakeMessageAsRead(messageId))
            {
                return Ok();
            }

            return NotFound("Message was not found!");
        }

        [HttpPost("createGroup")]
        public async Task<ActionResult<string>> CreateGroup([FromBody] CreateGroupRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request is bad");
            }

            var groupId = await _chatService.CreateGroup(request);

            if (groupId != null)
            {
                return Ok(new { id = groupId });
            }

            return BadRequest();
        }

        [HttpPost("sentGroupMessage")]
        public async Task<ActionResult> SentGroupMessage([FromBody] ChatModel model)
        {
            if (model == null)
            {
                return BadRequest("Message is empty");
            }

            var messageId = await _chatService.SentGroupMessage(model);

            if (messageId != null)
            {
                return Ok(new { id = messageId });
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet("getGroupChatHistory{id}")]
        public async Task<ActionResult<List<GroupMessageDbo>>> GetGroupChatHistory(Guid id)
        {
            var messages = await _chatService.GetGroupChatHistoryAsync(id);

            if (messages != null)
            {
                return Ok(messages);
            }

            return NotFound();
        }

        [HttpPatch("updateGroupMessageReadStatus/{messageId}")]
        public async Task<ActionResult> MakeGroupMessageAsRead(Guid messageId)
        {
            if (await _chatService.MakeGroupMessageAsRead(messageId))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getUsersByGroup/{groupId}")]
        public async Task<ActionResult<List<UserModel>>> GetUsersByGroup(Guid groupId)
        {
            var usersList = await _chatService.GetUsersByGroup(groupId);

            if (usersList != null)
            {
                return Ok(usersList);
            }

            return BadRequest();
        }

        [HttpPost("deleteFromGroup")]
        public async Task<ActionResult<bool>> DeleteUsersFromGroup([FromBody] DeleteUserRequest request)
        {
            if (request == null || request.UsersId == null || !request.UsersId.Any() || request.GroupId == Guid.Empty)
            {
                return BadRequest("Невірні дані в запиті.");
            }

            var result = await _chatService.DeleteUserFromGroup(request.UsersId, request.GroupId);

            if (result)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest("Не вдалося видалити користувачів з групи.");
            }
        }

        [HttpPost("addUserToGroup")]
        public async Task<ActionResult<bool>> AddUsersToGroup([FromBody] AddUserRequest request)
        {
            if(await _chatService.AddUsersToGroup(request))
            {
                return Ok(true);
            }
            return BadRequest();
        }

    }
}
