using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Docker.Domain.Entities;

namespace App.Docker.Domain.Interfaces.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetById(Guid id);
        Task<List<Product>> GetAll(Expression<Func<Product, bool>> filters);
    }
}
