using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Docker.Domain.Entities;

namespace App.Docker.Domain.Interfaces.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetByFilters(Expression<Func<User, bool>> filters);

    }
}
