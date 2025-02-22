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

        public async void Connect(Guid UserId)
        {
            Users.Add(Context.ConnectionId, UserId);
            UserViewModel? userData = _accountRepository.GetUserDetail(UserId);
            if (userData != null)
            {
                userData.Status = "Online";
                await Clients.All.SendAsync("Users",userData);
            }
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Guid userid;
            Users.TryGetValue(Context.ConnectionId, out userid);
            UserViewModel? userData = _accountRepository.GetUserDetail(userid);
            if (userData != null)
            {
                userData.Status = "Offline";
                await Clients.All.SendAsync("Users", userData);
            }
        }
    }
}
