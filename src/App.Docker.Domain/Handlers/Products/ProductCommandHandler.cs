using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using App.Docker.Domain.Commands.Products;
using App.Docker.Domain.Communication;
using App.Docker.Domain.Entities;
using App.Docker.Domain.Interfaces.Products;
using App.Docker.Domain.Messages;

namespace App.Docker.Domain.Handlers.Products
{
    public class ProductCommandHandler : IRequestHandler<CreateProductCommand, bool>,
        IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _produtoRepository;
        private readonly IMediatorHandler _mediatorHandler;
        public ProductCommandHandler(IProductRepository productRepository, IMediatorHandler mediatorHandler)
        {

            _produtoRepository = productRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (!command.ValidationResult.IsValid)
            {
                PublishNotifications(command.ValidationResult, command.MessageType);
                return false;
            }

            var product = new Product(command.Name, command.Description, command.Price);

            if (product.ValidationResult.IsValid)
            {
                _produtoRepository.Add(product);

                return await _produtoRepository.UnitOfWork.Commit();
            }
            else
            {
                PublishNotifications(product.ValidationResult, "Product");
            }

            return false;
        }

        public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (!command.ValidationResult.IsValid)
            {
                PublishNotifications(command.ValidationResult, command.MessageType);
                return false;
            }

            var product = await _produtoRepository.GetById(command.Id);

            if(product is null)
            {
                PublishNotification("Produto não encontrado", "Product");
                return false;
            }

            if(!string.IsNullOrEmpty(command.Description))
                product.ChangeDescription(command.Description);

            product.ChangePrice(command.Price);

            product.Validate();

            if (!product.ValidationResult.IsValid)
            {
                PublishNotifications(product.ValidationResult, "Product");
                return false;
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private void PublishNotifications(ValidationResult validationResult, string messageType)
        {
            foreach (var error in validationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(messageType, error.ErrorMessage));
            }

        }

        private void PublishNotification(string message, string messageType)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(messageType, message));
        }
    }
}
