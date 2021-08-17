using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Docker.Domain.Entities;
using App.Docker.Domain.Interfaces;
using App.Docker.Domain.Interfaces.Products;
using App.Docker.Infra.Data.Context;

namespace App.Docker.Infra.Data.Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Product entity)
        {
            _context.Products.Add(entity);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void Remove(Product entity)
        {
            _context.Products.Remove(entity);
        }

        public void Update(Product entity)
        {
            _context.Products.Update(entity);
        }
    }
}
