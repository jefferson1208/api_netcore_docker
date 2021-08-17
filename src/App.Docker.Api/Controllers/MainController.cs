using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using App.Docker.Domain.Communication;
using App.Docker.Domain.Handlers;
using App.Docker.Domain.Messages;

namespace App.Docker.Api.Controllers
{
    [ApiController]
    public abstract class MainController: ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;
        protected MainController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected bool CheckOperationIsValid()
        {
            return !_notifications.VerifyNotifications();
        }
        protected ActionResult GenerateCustomResponse(object result = null)
        {
            if (CheckOperationIsValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }
        protected ActionResult GenerateCustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyErrorModelInvalid(modelState);
            return GenerateCustomResponse();
        }

        protected void NotifyErrorModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError("ModelState", errorMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(code, message));
        }

    }
}
