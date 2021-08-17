using Bogus;
using System;
using System.Collections.Generic;
using App.Docker.Domain.Commands.Products;
using App.Docker.Domain.Entities;
using Xunit;

namespace App.Docker.Tests.Fixtures
{
    [CollectionDefinition(nameof(ProductCollection))]
    public class ProductCollection: ICollectionFixture<ProductTestsFixture> { }
    public class ProductTestsFixture
    {
        public List<Product> GenerateListOfValidProducts(int quantidade = 1)
        {
            var products = new Faker<Product>("pt_BR")
                .CustomInstantiator(p => new Product(p.Random.String(10, 15), p.Random.String(10, 100),p.Random.Decimal(10, 10000)));

            return products.Generate(quantidade);
        }

        public List<Product> GenerateListOfInvalidProducts(int quantidade = 1)
        {
            var products = new Faker<Product>("pt_BR")
                .CustomInstantiator(p => new Product(p.Random.String(1, 4), p.Random.String(1, 9), p.Random.Decimal(0, 10)));

            return products.Generate(quantidade);
        }

        public List<CreateProductCommand> GenerateListOfCommandsCreateValid(int quantidade = 1)
        {
            var commands = new Faker<CreateProductCommand>("pt_BR")
                .CustomInstantiator(p => new CreateProductCommand(p.Random.String(10, 15), p.Random.String(10, 100), p.Random.Decimal(10, 10000)));

            return commands.Generate(quantidade);
        }

        public List<CreateProductCommand> GenerateListOfCommandsCreateInvalid(int quantidade = 1)
        {

            var commands = new Faker<CreateProductCommand>("pt_BR")
                .CustomInstantiator(p => new CreateProductCommand(p.Random.String(1, 4), p.Random.String(1, 9), p.Random.Decimal(0, 10)));

            return commands.Generate(quantidade);
        }

        public List<UpdateProductCommand> GenerateListOfCommandsUpdateValid(int quantidade = 1)
        {
            var commands = new Faker<UpdateProductCommand>("pt_BR")
                .CustomInstantiator(p => new UpdateProductCommand(Guid.NewGuid(), p.Random.String(10, 100), p.Random.Decimal(10, 10000)));

            return commands.Generate(quantidade);
        }

        public List<UpdateProductCommand> GenerateListOfCommandsUpdateInvalid(int quantidade = 1)
        {
            var commands = new Faker<UpdateProductCommand>("pt_BR")
                .CustomInstantiator(p => new UpdateProductCommand(Guid.NewGuid(), p.Random.String(1, 9), p.Random.Decimal(0, 10)));
            return commands.Generate(quantidade);
        }
    }
}
