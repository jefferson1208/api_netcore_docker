using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Docker.Domain.Entities;
using App.Docker.Domain.Interfaces;
using App.Docker.Domain.Interfaces.Users;
using App.Docker.Infra.Data.Context;

namespace App.Docker.Infra.Data.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(User entity)
        {
            _dbContext.Users.Add(entity);
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<List<User>> GetByFullName(string fullName)
        {
            return await _dbContext.Users.AsNoTracking().Where(u => u.FullName.StartsWith(fullName)).ToListAsync();
        }

        public void Remove(User entity)
        {
            _dbContext.Users.Remove(entity);
        }

        public void Update(User entity)
        {
            _dbContext.Users.Update(entity);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public async Task<List<User>> GetByFilters(Expression<Func<User, bool>> filters)
        {
            return await _dbContext.Users.Where(filters).ToListAsync();
        }
        
    }
}
