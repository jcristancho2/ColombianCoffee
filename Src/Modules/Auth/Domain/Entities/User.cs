using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColombianCoffee.Src.Modules.Auth.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}

public enum UserRole
{
    Admin,
    Usuario
}

