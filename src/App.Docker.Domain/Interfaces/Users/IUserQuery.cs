using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Docker.Domain.DTOs.Users;

namespace App.Docker.Domain.Interfaces.Users
{
    public interface IUserQuery : IDisposable
    {
        Task<List<UserDto>> GetByFilters(Dictionary<string, string> filters);
    }
}
