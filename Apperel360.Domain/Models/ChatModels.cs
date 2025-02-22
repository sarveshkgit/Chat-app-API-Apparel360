using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Domain.Models
{
    public class ChatModels
    {
        public Guid SenderUserID { get; set; }
        public Guid ReceiverUserID { get; set; }
        public string ChatMessage { get; set; } = string.Empty;
    }

    public class ChatViewModels
    {
        public Guid SenderUserID { get; set; }
        public Guid ReceiverUserID { get; set; }
        public string ChatMessage { get; set; } = string.Empty;
        public string Dated { get; set; } = string.Empty;
        public int IsSucess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
