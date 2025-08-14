using ColombianCoffee.Src.Shared.Helpers;
using ColombianCoffee.Src.Shared.Contexts;
using ColombianCoffee.Src.Modules.Auth.Application.Services;
using ColombianCoffee.Src.Modules.Auth.Application.Interfaces;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using ColombianCoffee.Src.Modules.Auth.Infraestructure.Repositories;
using ColombianCoffee.Src.Modules.Auth.Application.UI;
using ColombianCoffee.Src.Modules.MainMenu;

namespace ColombianCoffee;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            // ---------------- CREAR DbContext DESDE FACTORY ----------------
            var dbContext = DbContextFactory.Create();
            if (dbContext == null)
            {
                Console.WriteLine("❌ Error: No se pudo crear el contexto de base de datos.");
                return;
            }

            // ---------------- INYECCIÓN MANUAL DE DEPENDENCIAS ----------------
            IUserRepository userRepo = new UserRepository(dbContext);
            var authService = new AuthService(userRepo);
            var authMenu = new AuthMenu(authService);
            var mainMenu = new MainMenu(dbContext);

            // ---------------- MENÚ PRINCIPAL ----------------
            while (true)
            {
                Console.WriteLine("\n====== MENU PRINCIPAL ======");
                Console.WriteLine("1. Módulo Auth (Registrar/Login)");
                Console.WriteLine("2. Acceso a módulo solo para ADMIN");
                Console.WriteLine("3. Acceso a módulo solo para USUARIO");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione opción: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await authMenu.ShowMenuAsync();
                        break;

                    case "2":
                        try
                        {
                            SessionManager.validateRole(UserRole.Admin);
                            Console.WriteLine("✅ Acceso concedido: módulo ADMIN.");
                            mainMenu.Show();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ {ex.Message}");
                        }
                        break;

                    case "3":
                        try
                        {
                            SessionManager.validateRole(UserRole.Usuario);
                            Console.WriteLine("✅ Acceso concedido: módulo USUARIO.");
                            mainMenu.Show();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ {ex.Message}");
                        }
                        break;

                    case "0":
                        Console.WriteLine("Saliendo del sistema...");
                        return;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error inesperado: {ex.Message}");
        }
    }
}
