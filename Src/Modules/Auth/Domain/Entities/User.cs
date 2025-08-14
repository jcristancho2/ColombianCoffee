using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColombianCoffee.Src.Modules.Auth.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiration { get; set; }
    }
}