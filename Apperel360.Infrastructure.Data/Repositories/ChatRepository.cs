using Apperel360.Domain.Interfaces;
using Apperel360.Domain.Models;
using Apperel360.Infrastructure.Data.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Apperel360.Infrastructure.Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        IDapperDbContext _dapper;
        public ChatRepository(IDapperDbContext dapper)
        {
            _dapper = dapper;
        }
        public List<ChatViewModels> GetChats(Guid SenderUserID, Guid ReceiverUserID)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("SenderUserID", SenderUserID, System.Data.DbType.Guid);
            dynamicParameters.Add("ReceiverUserID", ReceiverUserID, System.Data.DbType.Guid);
            return _dapper.ExecuteGetAll<ChatViewModels>("proc_GetChats", dynamicParameters);
        }

        public ChatViewModels SendMessage(ChatModels model)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("SenderUserID", model.SenderUserID, System.Data.DbType.Guid);
            dynamicParameters.Add("ReceiverUserID", model.ReceiverUserID, System.Data.DbType.Guid);
            dynamicParameters.Add("ChatMessage", model.ChatMessage, System.Data.DbType.String);
            return _dapper.ExecuteGet<ChatViewModels>("proc_SendMessage", dynamicParameters);
        }
    }
}
