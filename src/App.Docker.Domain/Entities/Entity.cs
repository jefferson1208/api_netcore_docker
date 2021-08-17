using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Docker.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public virtual DateTime ConvertDateUtcToDateLocal(DateTime date)
        {
            return date.ToLocalTime();
        }
    }
}
