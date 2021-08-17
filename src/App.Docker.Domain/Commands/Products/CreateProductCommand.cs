using App.Docker.Domain.Messages;
using App.Docker.Domain.Validations.Products;

namespace App.Docker.Domain.Commands.Products
{
    public class CreateProductCommand : Command
    {
        public CreateProductCommand(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;

            Validate();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public override void Validate()
        {
            ValidationResult = new CreateProductCommandValidations().Validate(this);
        }
    }
}
