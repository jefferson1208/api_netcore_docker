using FluentValidation.Results;
using MediatR;
using System;


namespace App.Docker.Domain.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
