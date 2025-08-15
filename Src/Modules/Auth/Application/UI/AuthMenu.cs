using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using ColombianCoffee.Src.Modules.Auth.Application.Interfaces;
using ColombianCoffee.Src.Shared.Contexts;
using ColombianCoffee.Src.Modules.Auth.Application.Services;

namespace ColombianCoffee.Src.Modules.Auth.Application.UI
{
    public class AuthMenu
    {
        private readonly AuthService _authService;

        public AuthMenu(AuthService authService)
        {
            _authService = authService;
        }

        public async Task ShowMenuAsync()
        {
            while (true)
            {
                Console.WriteLine("1. Registrar Usuario");
                Console.WriteLine("2. Login");
                Console.WriteLine("0. Salir");

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        await Register();
                        break;
                    case "2":
                        await Login();
                        break;
                    case "0":
                        return;
                }
            }
        }

        private async Task Register()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine()!;
            Console.Write("Email: ");
            string email = Console.ReadLine()!;
            Console.Write("Password: ");
            string password = Console.ReadLine()!;
            Console.Write("Role (Admin/Usuario): ");
            string roleInput = Console.ReadLine()!;
            UserRole role = Enum.Parse<UserRole>(roleInput, true);

            await _authService.RegisterAsync(username, email, password, role);
            Console.WriteLine("Usuario registrado correctamente!");
        }

        private async Task Login()
        {
            Console.Write("Username o Email: ");
            string usernameOrEmail = Console.ReadLine()!;
            Console.Write("Password: ");
            string password = Console.ReadLine()!;

            try
            {
                var user = await _authService.LoginAsync(usernameOrEmail, password);
                SessionManager.Login(user);
                Console.WriteLine($"Bienvenido {user.Username}! Rol: {user.Role}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
    }
}