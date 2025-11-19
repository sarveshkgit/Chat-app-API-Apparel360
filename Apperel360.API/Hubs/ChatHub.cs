using Apperel360.Domain.Interfaces;
using Apperel360.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace Apperel360.API.Hubs
{
    public sealed class ChatHub:Hub
    {
        private readonly IAccountRepository _accountRepository;
        public ChatHub(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        
        public static Dictionary<string, Guid> Users = new ();

        public async Task Connect(Guid UserId)
        {
            // Remove any existing entry for the same connectionId to avoid conflicts
            if (!Users.ContainsKey(Context.ConnectionId))
            {
                Users[Context.ConnectionId] = UserId;
            }

            //Users.Add(Context.ConnectionId, UserId);

            UserViewModel? userData = _accountRepository.GetUserDetail(UserId);
            if (userData != null)
            {
                userData.Status = "Online";
                int updated = _accountRepository.UpdateUserStatus(UserId, "Online");

                // Notify all clients about the updated user status
                await Clients.All.SendAsync("Users",userData);
            }
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //Guid userid;
            //Users.TryGetValue(Context.ConnectionId, out userid);
            //UserViewModel? userData = _accountRepository.GetUserDetail(userid);
            //if (userData != null)
            //{
            //    userData.Status = "Offline";
            //    int c = _accountRepository.UpdateUserStatus(userid, "Offline");
            //    await Clients.All.SendAsync("Users", userData);
            //}

            if (Users.TryGetValue(Context.ConnectionId, out Guid userId))
            {
                // Remove the disconnected user from the dictionary
                Users.Remove(Context.ConnectionId);

                UserViewModel? userData = _accountRepository.GetUserDetail(userId);
                if (userData != null)
                {
                    userData.Status = "Offline";
                    int updated = _accountRepository.UpdateUserStatus(userId, "Offline");

                    // Notify all clients about the updated user status
                    await Clients.All.SendAsync("Users", userData);
                }

                
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
