using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Docker.Domain.DTOs.Products;

namespace App.Docker.Domain.Interfaces.Products
{
    public interface IProductQuery : IDisposable
    {
        Task<List<ProductDto>> GetAll();
    }
}
