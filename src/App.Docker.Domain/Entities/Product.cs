using System;
using App.Docker.Domain.Interfaces;
using App.Docker.Domain.Validations.Products;

namespace App.Docker.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public Product(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
            RegistrationDate = DateTime.UtcNow;

            Validate();
        }

        protected Product() { }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public DateTime RegistrationDate { get; private set; }


        public void ChangePrice(decimal price)
        {
            Price = price;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }

        public void Validate()
        {
            var validator = new ProductValidations().Validate(this);

            ValidationResult = validator;
        }
    }
}
