using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDomain.Contracts;
using CoreDomain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiUser.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        [HttpGet("Message")]
        [Route("send/{mensaje}")]
        public bool SendMessage(string mensaje)
        {
            IEmitter<string> emisor = new EmiterService<string>();
            emisor.Emit(mensaje);
            return true;
        }
    }
}
