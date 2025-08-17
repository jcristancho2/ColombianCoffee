using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using ColombianCoffee.Src.Modules.Auth.Application.Interfaces;
using ColombianCoffee.Src.Shared.Contexts;


namespace ColombianCoffee.Src.Modules.Auth.Application.Services
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; private set; }

        public static void Login(User user) => CurrentUser = user;

        public static void Logout() => CurrentUser = null;

        public static void validateRole(UserRole role)
        {
            if (CurrentUser == null)
                throw new Exception("No hay usuario logueado");

            if (CurrentUser.Role != role)
                throw new Exception("Permiso denegado");
        }
    }
}