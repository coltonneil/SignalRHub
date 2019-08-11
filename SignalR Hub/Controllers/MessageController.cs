using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRHub;
using System;

namespace SignalR_Hub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IHubContext<MessageHub> _hubContext;

        public MessageController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public string Post([FromBody]Message msg)
        {
            string retMessage;
            //Console.WriteLine(msg.Type);
            if(msg.Type == "privateMessage")
            {
                _hubContext.Clients.Group(msg.GroupName).SendAsync("ReceiveMessage", msg.Payload);
            }
            else
            {
                _hubContext.Clients.All.SendAsync("ReceiveMessage", msg.Payload);
            }
            retMessage = "Success";


            return retMessage;
        }
    }
}