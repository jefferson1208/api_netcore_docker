using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<List<ProductDto>> GetAll(Dictionary<string, string> filters)
        {
            var products = await _productRepository.GetAll(CreateFilter(filters));

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

        private Expression<Func<Product, bool>> CreateFilter(Dictionary<string, string> filtros)
        {
            Expression<Func<Product, bool>> query = h => true;

            foreach (var filtro in filtros)
            {
                switch (filtro.Key)
                {
                    case "Name":
                        query = query.And(q => q.Name.Contains(filtro.Value.Trim()));
                        break;
                    case "Description":
                        query = query.And(q => q.Description.Contains(filtro.Value.Trim()));
                        break;
                    case "Price":
                        query = query.And(q => q.Price >= Convert.ToDecimal(filtro.Value));
                        break;
                    default:
                        break;
                }
            }

            return query;
        }
    }
}
