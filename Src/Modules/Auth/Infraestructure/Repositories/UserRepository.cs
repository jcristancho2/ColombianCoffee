using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using ColombianCoffee.Src.Modules.Auth.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using ColombianCoffee.Src.Shared.Contexts;

namespace ColombianCoffee.Src.Modules.Auth.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUsernameAsync(string username) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        public async Task<User?> GetEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}