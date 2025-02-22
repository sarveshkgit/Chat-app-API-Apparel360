using Apperel360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Domain.Interfaces
{
    public interface IChatRepository
    {
        public List<ChatViewModels> GetChats(Guid SenderUserID, Guid ReceiverUserID);
        public ChatViewModels SendMessage(ChatModels model);
    }
}
