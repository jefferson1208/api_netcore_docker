using System.Collections.Generic;
using System.Threading.Tasks;
using App.Docker.Domain.DTOs.Products;
using App.Docker.Domain.Entities;
using App.Docker.Domain.Interfaces.Products;

namespace App.Docker.Domain.Queries.Products
{
    public class ProductQuery : IProductQuery
    {
        private readonly IProductRepository _productRepository;

        public ProductQuery(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAll();

            return GenerateListProductsDto(products);
        }

        private List<ProductDto> GenerateListProductsDto(List<Product> products)
        {
            var productsDto = new List<ProductDto>();

            products.ForEach((p) =>
            {
                productsDto.Add(new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    RegistrationDate = p.ConvertDateUtcToDateLocal(p.RegistrationDate),
                    Price = p.Price,
                });
            });

            return productsDto;
        }
    }
}
