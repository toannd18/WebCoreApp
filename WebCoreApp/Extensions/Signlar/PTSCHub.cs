using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Constants;

namespace WebCoreApp.Extensions.Signlar
{
    [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
    public class PTSCHub : Hub
    {
        private readonly IHttpContextAccessor _httpContext;

        public PTSCHub(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public async Task SendMesSsage(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendNotification(List<string> users)
        {
            await Clients.Users(users).SendAsync("SendNotification");
        }

        public override async Task OnConnectedAsync()
        {
            string display = "SignalR Users Group " + _httpContext.HttpContext.User?.FindFirst("Display")?.Value;
            await Groups.AddToGroupAsync(Context.ConnectionId, display);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string display = "SignalR Users Group " + _httpContext.HttpContext.User?.FindFirst("Display")?.Value;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, display);
            await base.OnDisconnectedAsync(exception);
        }
    }
}