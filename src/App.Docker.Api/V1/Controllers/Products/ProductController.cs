using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using App.Docker.Api.Controllers;
using App.Docker.Domain.Commands.Products;
using App.Docker.Domain.Communication;
using App.Docker.Domain.Interfaces.Products;
using App.Docker.Domain.Messages;
using App.Docker.Api.ViewModels.Products;

namespace App.Docker.Api.V1.Controllers.Products
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [Authorize]
    public class ProductController: MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProductQuery _productQuery;
        public ProductController(INotificationHandler<DomainNotification> notifications, IProductQuery productQuery,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _productQuery = productQuery;
        }

        [HttpPost("create-product")]
        public async Task<ActionResult> CreateProduct(ProductViewModel product)
        {
            if (!ModelState.IsValid) return GenerateCustomResponse(product);

            var command = new CreateProductCommand(product.Name, product.Description, product.Price);

            var result = await _mediatorHandler.SendCommand(command);

            if (result) return GenerateCustomResponse(product);

            return GenerateCustomResponse();

        }

        [HttpPut("update-product")]
        public async Task<ActionResult> UpdateProduct(UpdateProductViewModel product)
        {
            if (!ModelState.IsValid) return GenerateCustomResponse(product);

            var command = new UpdateProductCommand(product.Id, product.Description, product.Price);

            var result = await _mediatorHandler.SendCommand(command);

            if (result) return GenerateCustomResponse(product);

            return GenerateCustomResponse();

        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var products = await _productQuery.GetAll();

            return GenerateCustomResponse(products);

        }
    }
}
