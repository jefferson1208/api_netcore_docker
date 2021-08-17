﻿using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Docker.Domain.Messages;

namespace App.Docker.Domain.Handlers
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;
        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }
        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);

            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual bool VerifyNotifications()
        {
            return GetNotifications().Any();
        }
        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}