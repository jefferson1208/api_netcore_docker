using App.Docker.Domain.Messages;
using App.Docker.Domain.Validations.Products;
using System;

namespace App.Docker.Domain.Commands.Products
{
    public class UpdateProductCommand : Command
    {
        public UpdateProductCommand(Guid id, string description, decimal price)
        {
            Id = id;
            Description = description;
            Price = price;

            Validate();
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public override void Validate()
        {
            ValidationResult = new UpdateProductCommandValidations().Validate(this);
        }
    }
}
