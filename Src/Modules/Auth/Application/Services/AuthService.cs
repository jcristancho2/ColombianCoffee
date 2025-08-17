using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using ColombianCoffee.Src.Modules.Auth.Application.Interfaces;
using BCrypt.Net;
using ColombianCoffee.Src.Shared.Contexts;

namespace ColombianCoffee.Src.Modules.Auth.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _repo;

        public AuthService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> RegisterAsync(string username, string email, string password, UserRole role)
        {
            if (await _repo.GetUsernameAsync(username) != null)
                throw new Exception("Usuario ya existe");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                Role = role
            };

            await _repo.AddUserAsync(user);
            return true;
        }

        public async Task<User> LoginAsync(string usernameOrEmail, string password)
        {
            var user = await _repo.GetUsernameAsync(usernameOrEmail)
                       ?? await _repo.GetEmailAsync(usernameOrEmail);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new Exception("Credenciales incorrectas");

            return user;
        }
    }
}