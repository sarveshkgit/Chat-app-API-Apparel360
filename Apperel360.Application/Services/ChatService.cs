using Apperel360.Application.Interfaces;
using Apperel360.Domain.Interfaces;
using Apperel360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Services
{
    public class ChatService: IChatService
    {
        private readonly IChatRepository _chatRepository;
        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public List<ChatViewModels> GetChats(Guid SenderUserID, Guid ReceiverUserID)
        {
            return _chatRepository.GetChats(SenderUserID, ReceiverUserID);
        }

        public ChatViewModels SendMessage(ChatModels model)
        {
            return _chatRepository.SendMessage(model);
        }
    }
}
