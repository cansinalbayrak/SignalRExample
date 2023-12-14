using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRExample.Business.Interface;
using SignalRExample.Hubs;

namespace SignalRExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly IMyBusiness _myBusiness;
        private readonly IHubContext<MyHub> _hubContext;

        //Burada ister bir ara katman oluşturup IMyBusiness nesnesini kullanırız, ya da doğrudan IHubContext interface'ini implemente edip onu kullanırız.
        public SenderController(IMyBusiness myBusiness, IHubContext<MyHub> hubContext)
        {
            _myBusiness = myBusiness;
            _hubContext = hubContext;
        }

        [HttpGet("{message}/{userName}")]
        public async Task<IActionResult> SendMyMessage(string message, string userName)
        {
            await _myBusiness.SendMessageAsync(message, userName);
            return Ok();
        }
    }
}
