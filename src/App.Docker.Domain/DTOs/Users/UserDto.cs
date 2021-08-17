using System;

namespace App.Docker.Domain.DTOs.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }

        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

    }
}
