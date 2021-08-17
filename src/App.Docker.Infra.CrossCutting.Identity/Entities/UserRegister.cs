using Microsoft.AspNetCore.Identity;
using System;

namespace App.Docker.Infra.CrossCutting.Identity.Entities
{
    public class UserRegister : IdentityUser<Guid>
    {
        public UserRegister()
        {
            Id = Guid.NewGuid();
        }
        public string FullName { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
