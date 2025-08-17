using Spectre.Console;

namespace ColombianCoffee.Src.Shared.Utils;

public class ScreenController
{
    public static void PauseScreen()
    {
        AnsiConsole.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }
}