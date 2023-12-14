using Microsoft.AspNetCore.SignalR;
using SignalRExample.Business.Interface;
using SignalRExample.Hubs;
namespace SignalRExample.Business.Concrete
{
    public class MyBusiness : IMyBusiness
    {
        private readonly IHubContext<MyHub> _hubContext;

        public MyBusiness(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendMessageAsync(string message, string userName)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message, userName);
        }

    }
}
