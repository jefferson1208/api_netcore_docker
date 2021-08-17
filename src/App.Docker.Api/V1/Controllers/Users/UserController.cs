using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using App.Docker.Api.Controllers;
using App.Docker.Domain.Communication;
using App.Docker.Domain.Interfaces.Users;
using App.Docker.Domain.Messages;
using App.Docker.Infra.CrossCutting.Identity.Entities;
using App.Docker.Infra.CrossCutting.Identity.Model;
using App.Docker.Infra.CrossCutting.Ioc.Extensions;
using App.Docker.Api.ViewModels.Users;

namespace App.Docker.Api.V1.Controllers.Users
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [Authorize]
    public class UserController : MainController
    {

        private readonly SignInManager<UserRegister> _sigInManager;
        private readonly UserManager<UserRegister> _userManager;
        private readonly IUserQuery _userQuery;
        private readonly AppSettings _appSettings;
        public UserController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler,
                                SignInManager<UserRegister> sigInManager, UserManager<UserRegister> userManager,
                                IUserQuery userQuery, IOptions<AppSettings> appSettings) : base(notifications, mediatorHandler)
        {
            _sigInManager = sigInManager;
            _userManager = userManager;
            _userQuery = userQuery;
            _appSettings = appSettings.Value;
        }

        [HttpPost, Route("signIn")]
        [AllowAnonymous]
        public async Task<ActionResult> SignIn(SignInViewModel signIn)
        {
            if (!ModelState.IsValid) return GenerateCustomResponse(signIn);

            var result = await _sigInManager.PasswordSignInAsync(signIn.UserName, signIn.Password, false, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signIn.UserName);

                if (!user.EmailConfirmed)
                {
                    return Unauthorized("E-mail do usuário ainda não confirmado!");
                }

                //gerar jwt
                return GenerateCustomResponse(GenerateJwt());
            }
            else if (result.IsLockedOut)
            {
                return Unauthorized("Usuário temporáriamente bloqueado. Tente novamente mais tarde!");
            }
            
            return Unauthorized("Usuário ou Senha Incorretos!");
        }

        [HttpPost, Route("signUp")]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(SignUpViewModel signUp)
        {
            if (!ModelState.IsValid) return GenerateCustomResponse(signUp);

            var user = new UserRegister
            {
                UserName = signUp.UserName,
                FullName = signUp.FullName,
                Email = signUp.Email,
                EmailConfirmed = true,
                RegistrationDate = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, signUp.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    NotifyError(error.Code, error.Description);
                }

                return GenerateCustomResponse();
            }


            return GenerateCustomResponse(GenerateJwt());
        }

        [HttpGet]
        //aqui tem que ser autorizado
        public async Task<ActionResult> GetUsers([FromQuery]UserViewModel user)
        {
            var users = await _userQuery.GetByFilters(user.CreateFilters());

            return GenerateCustomResponse(users);
        }

        private string GenerateJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }

    }
}
