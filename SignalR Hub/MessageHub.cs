using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRHub
{
    public class MessageHub : Hub
    {

        public Task SendCardData(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("RecieveCardData", message);
        }

        public Task PromptForPayment(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("RecievePaymentRequest", message);
        }

        public Task NotifyPaymentStatus(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("RecievePaymentStatus", message);
        }

        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("UserConnected", Context.ConnectionId);
            await Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}