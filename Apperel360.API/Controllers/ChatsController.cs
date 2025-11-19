using Apperel360.API.Hubs;
using Apperel360.Application.Interfaces;
using Apperel360.Application.Logic.Interfaces;
using Apperel360.Application.Services;
using Apperel360.Domain.Interfaces;
using Apperel360.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net;

namespace Apperel360.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class ChatsController : ControllerBase
    {
        private IChatService _chatService;
        IHubContext<ChatHub> _hubContext;
        private IJwtToken _jwtToken;
        private IAppUtility _appUtility;
        public ChatsController(IChatService chatService, IHubContext<ChatHub> hubContext, IJwtToken jwtToken, IAppUtility appUtility)
        {
            _chatService = chatService;
            _hubContext = hubContext;
            _jwtToken = jwtToken;
            _appUtility = appUtility;
        }


        [HttpGet]
        [ActionName("GetChats")]
        public IActionResult GetChats(Guid SenderUserID, Guid ReceiverUserID)
        {
            try
            {
                if (SenderUserID == Guid.Empty && ReceiverUserID == Guid.Empty)
                {
                    return BadRequest();
                }
                var messageDetails = _chatService.GetChats(SenderUserID, ReceiverUserID);
                if (messageDetails != null)
                {
                    return Ok(new { Type = "success", Code = "001", Message = "Message Send Successfully", Data = messageDetails });
                }
                else
                {
                    return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = MessageStream.SomethingWentWrong });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = ex.Message });
            }

        }

        [HttpPost]
        [ActionName("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ChatModels model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var messageData = _chatService.SendMessage(model);
                if (messageData != null)
                {
                    if (messageData.IsSucess == 1)
                    {
                        var connectionId = ChatHub.Users.FirstOrDefault(p => p.Value == model.ReceiverUserID).Key;
                        if (connectionId != null)
                        {
                            await _hubContext.Clients.Client(connectionId.ToString()).SendAsync("Messages", model);
                        }

                        return Ok(new { Type = "success", Code = HttpStatusCode.OK.ToString(), Message = MessageStream.MessageSentSuccessfully, Data = model });
                    }
                    else
                        return Ok(new { Type = "fail", Code = HttpStatusCode.OK.ToString(), Message = MessageStream.SomethingWentWrong });

                }
                else
                {
                    return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = MessageStream.SomethingWentWrong });
                }
            }
            catch (Exception ex) { return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = ex.Message }); }

        }

    }
}
