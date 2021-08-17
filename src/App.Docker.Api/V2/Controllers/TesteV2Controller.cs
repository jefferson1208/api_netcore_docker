using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using App.Docker.Api.Controllers;
using App.Docker.Domain.Communication;
using App.Docker.Domain.Messages;

namespace App.Docker.Api.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/teste")]
    public class TesteV2Controller : MainController
    {
        public TesteV2Controller(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler) :base(notifications, mediatorHandler)
        {

        }

        [HttpGet, Route("nova-versao")]
        public async Task<ActionResult> Valor()
        {
            return await Task.FromResult(Ok("Versão 2"));
        }
    }
}
