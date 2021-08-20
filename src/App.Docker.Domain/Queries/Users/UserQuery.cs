using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Docker.Domain.Communication;
using App.Docker.Domain.DTOs.Users;
using App.Docker.Domain.Entities;
using App.Docker.Domain.Interfaces.Users;
using App.Docker.Domain.Messages;

namespace App.Docker.Domain.Queries.Users
{
    public class UserQuery : IUserQuery
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _mediatorHandler;
        public UserQuery(IUserRepository userRepository, IMediatorHandler mediatorHandler)
        {
            _userRepository = userRepository;
            _mediatorHandler = mediatorHandler;
        }

        private UserDto GenerateUsersDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                RegistrationDate = user.ConvertDateUtcToDateLocal(user.RegistrationDate),
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber
            };
        }
        private List<UserDto> GenerateListUsersDto(List<User> users)
        {
            var usersDto = new List<UserDto>();

            users.ForEach((u) =>
            {
                usersDto.Add(new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    UserName = u.UserName,
                    RegistrationDate = u.ConvertDateUtcToDateLocal(u.RegistrationDate),
                    Email = u.Email,
                    EmailConfirmed = u.EmailConfirmed,
                    PhoneNumber = u.PhoneNumber
                });
            });

            return usersDto;
        }
            
        public async Task<List<UserDto>> GetByFilters(Dictionary<string, string> filters)
        {
            if (!filters.Any())
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Users", "Nenhum filtro informado para pesquisa!"));
                return null;
            }

            var users = await _userRepository.GetByFilters(CreateFilter(filters));

            if (users is null) return null;

            return GenerateListUsersDto(users);
        }


        private Expression<Func<User, bool>> CreateFilter(Dictionary<string, string> filtros)
        {
            Expression<Func<User, bool>> query = h => true;
            var dates = new List<DateTime>();

            foreach (var filtro in filtros)
            {
                switch (filtro.Key)
                {
                    case "FullName":
                        query = query.And(q => q.FullName.Contains(filtro.Value));
                        break;
                    case "UserName":
                        query = query.And(q => q.UserName.Contains(filtro.Value));
                        break;
                    case "Email":
                        query = query.And(q => q.Email.Contains(filtro.Value));
                        break;
                    default:
                        dates.Add(ConvertStringToDateTime(filtro.Value));
                        break;
                }
            }

            if(dates.Count > 1)
            {
                query = query.And(q => q.RegistrationDate >= dates[0] && q.RegistrationDate <= dates[1]);
            }

            return query;
        }

        private DateTime ConvertStringToDateTime(string date)
        {
            return Convert.ToDateTime(date,
                        CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
        }
        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }

    public static class PredicateBuilder
    {
        public static Expression<Func<User, bool>> And(this Expression<Func<User, bool>> expr1,
                                                                    Expression<Func<User, bool>> expr2)
        {
            var invoke = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<User, bool>>
                (Expression.AndAlso(expr1.Body, invoke), expr1.Parameters);
        }
    }
}
