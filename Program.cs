using ColombianCoffee.Src.Shared.Helpers;
using ColombianCoffee.Src.Shared.Contexts;
using ColombianCoffee.Src.Modules.Auth.Application.Services;
using ColombianCoffee.Src.Modules.Auth.Application.Interfaces;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using ColombianCoffee.Src.Modules.Auth.Infraestructure.Repositories;
using ColombianCoffee.Src.Modules.Auth.Application.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ColombianCoffee;

public class Program
{
    public static async Task Main(string[] args)
    {
        // ---------------- LEER CONFIGURACIÓN DESDE appsettings.json ----------------
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("❌ Error: No se encontró la cadena de conexión 'DefaultConnection' en appsettings.json.");
            return;
        }


        // ---------------- CONFIGURAR EF CORE CON MYSQL ----------------
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;

        var dbContext = new AppDbContext(options);

        // ---------------- INYECCIÓN MANUAL DE DEPENDENCIAS ----------------
        IUserRepository userRepo = new UserRepository(dbContext);
        AuthService authService = new AuthService(userRepo);
        AuthMenu authMenu = new AuthMenu(authService);

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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ {ex.Message}");
                    }
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}
