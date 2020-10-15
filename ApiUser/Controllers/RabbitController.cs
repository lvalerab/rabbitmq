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
        /// <summary>
        /// Envia un mensaje a la cola definida en rabbitm1
        /// </summary>
        /// <param name="mensaje">Mensaje a enviar</param>
        /// <returns>Siempre true</returns>
        [HttpGet]
        [Route("send/{mensaje}")]
        public ActionResult<bool> SendMessage(string mensaje)
        {
            try { 
                IEmitter<string> emisor = new EmiterService<string>();
                emisor.Emit(mensaje);
                return Ok(true);
            } catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
