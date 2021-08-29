using App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    
    public class SignalController : ControllerBase
    {
        private readonly IHubContext<StockExchangeHub> _hubContext;
        public SignalController(IHubContext<StockExchangeHub> hubcontext)
        {
            _hubContext = hubcontext;
        }
        [HttpPost]
        [Route("tin-hieu-mua-ban")]
        public IActionResult SendSignalBuySell(List<TradingModel> data)
        {
            this._hubContext.Clients.All.SendAsync("SendSignalBuySell", data);
            return Ok();
        }        
    }
}
