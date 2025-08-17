using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;

namespace ColombianCoffee.Src.Modules.Auth.Application.Interfaces
{
    public interface IUserRepository
    {

        Task<User?> GetUsernameAsync(string username);
        Task<User?> GetEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}
