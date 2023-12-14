using Microsoft.AspNetCore.SignalR;
using SignalRExample.Interfaces;

namespace SignalRExample.Hubs
{
    public class ClientData 
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
    }
    public class MyHub : Hub<IMessageClient>
    {
        static List<ClientData> clients = new List<ClientData>();

        //My business'a taşıdık
        ////Server'a gelen mesajı (message) diğer client'lara göndereceğiz.
        public async Task SendMessageAsync(string message, string userName)
        {
            //await Clients.All.SendAsync("ReceiveMessage", message, userName, Context.ConnectionId);
            await Clients.All.ReceiveMessage(message, userName, Context.ConnectionId);
        }

        //Bir kullanıcı bağlantı sağladığından çalışacak method
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var userName = httpContext.Request.Query["UserName"];
            var data = new ClientData { ConnectionId = Context.ConnectionId, UserName = userName };
            clients.Add(data);
            //await Clients.All.SendAsync("clients", clients);
            //await Clients.All.SendAsync("userJoined", Context.ConnectionId);
            await Clients.All.Clients(clients);
            await Clients.All.UserJoined(data);
        }

        //Bir kullanıcı bağlantısını kopardığında çalışacak method
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var httpContext = Context.GetHttpContext();
            var userName = httpContext.Request.Query["UserName"];
            var data = new ClientData { ConnectionId = Context.ConnectionId, UserName = userName };

            var deletedClient = clients.FirstOrDefault(x => x.ConnectionId == data.ConnectionId);
            clients.Remove(deletedClient);
            //await Clients.All.SendAsync("clients", clients);
            //await Clients.All.SendAsync("userLeaved", Context.ConnectionId);
            await Clients.All.Clients(clients);
            await Clients.All.UserLeaved(data);
        }

    }
}
