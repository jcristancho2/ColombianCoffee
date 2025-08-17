using ColombianCoffee.src.Modules.Varieties.Application.Interfaces;
using ColombianCoffee.src.Modules.Varieties.Application.DTOs;
using ColombianCoffee.src.Modules.Varieties.Domain.Entities;
using Spectre.Console;

namespace ColombianCoffee.src.Modules.Varieties.Application.UI;

public sealed class VarietyUI : IVarietyUI
{
    private readonly IVarietyService _varietyService;
    private readonly Dictionary<string, Action<VarietyFilterDto>> _filterActions;

    public VarietyUI(IVarietyService varietyService)
    {
        _varietyService = varietyService;

        _filterActions = new Dictionary<string, Action<VarietyFilterDto>>
        {
            ["Nombre contiene"] = f =>
            {
                var term = AnsiConsole.Ask<string>("Ingrese parte del nombre:");
                f.NameContains = term;
            },
            ["Porte (altura)"] = f =>
            {
                var porte = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Seleccione el porte de la planta:")
                        .AddChoices("Alto", "Medio", "Bajo")
                );
                f.PlantHeight = porte;
            },
            ["Tamaño de grano"] = f =>
            {
                var size = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Seleccione el tamaño del grano:")
                        .AddChoices("Pequeño", "Medio", "Grande")
                );
                f.BeanSize = size;
            },
            ["Potencial de rendimiento"] = f =>
            {
                var rendimiento = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Seleccione el rendimiento:")
                        .AddChoices("Muy bajo", "Bajo", "Medio", "Alto", "Excepcional")
                );
                f.YieldPotential = rendimiento;
            },
            ["Resistencia a roya"] = f =>
            {
                var resistencia = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Seleccione la resistencia a la roya:")
                        .AddChoices("Susceptible", "Tolerante", "Resistente", "Altamente resistente")
                );
                f.RustResistance = resistencia;
            },
            ["Altitud mínima"] = f =>
            {
                var min = AnsiConsole.Ask<int>("Ingrese altitud mínima (msnm):");
                f.MinAltitude = min;
            },
            ["Altitud máxima"] = f =>
            {
                var max = AnsiConsole.Ask<int>("Ingrese altitud máxima (msnm):");
                f.MaxAltitude = max;
            }
        };
    }

    public async Task Show()
    {
        var filter = new VarietyFilterDto();

        while(true)
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Selecciona un filtro para aplicar:")
                .AddChoices(_filterActions.Keys.Append("Aplicar filtros"))
            );

            if (selection == "Aplicar filtros")
                break;

            _filterActions[selection](filter);

            var addFilter = AnsiConsole.Confirm("¿Desea agregar otro filtro?");
            if (!addFilter)
                break;
        }

        var varieties = await _varietyService.GetFilteredVarietiesAsync(filter);

        if (!varieties.Any())
        {
            AnsiConsole.WriteLine("No se encontraron variedades que coincidan con los filtros aplicados.");
            AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            return;
        }

        // Mostrar lista filtrada
        var selectedId = AnsiConsole.Prompt(
        new SelectionPrompt<uint>()
            .Title("Seleccione una variedad para ver detalles:")
            .AddChoices(varieties.Select(v => v.Id))
            .UseConverter(id =>
            {
                var v = varieties.First(x => x.Id == id);
                return $"{id} - {v.Name}";
            })
        );

        var detail = await _varietyService.GetVarietyDetailAsync(selectedId);

        if (detail != null)
        {
            var verPdf = AnsiConsole.Confirm("¿Desea ver el PDF de la variedad?");
            if (verPdf)
            {
                AnsiConsole.WriteLine($"Generando PDF para la variedad {detail.Name}...");
                //TODO: Generar PDF
                AnsiConsole.WriteLine("PDF generado correctamente.");
                AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                AnsiConsole.Write(new Table()
                .RoundedBorder()
                .AddColumn("Propiedad")
                .AddColumn("Valor")
                .AddRow("Nombre", detail.Name)
                .AddRow("Nombre científico", detail.ScientificName)
                .AddRow("Historia", detail.History ?? "-")
                .AddRow("Especie", detail.SpeciesName)
                .AddRow("Grupo genético", detail.GeneticGroupName)
                .AddRow("Linaje", detail.LineageName)
                .AddRow("Porte", detail.PlantHeight)
                .AddRow("Tamaño de grano", detail.BeanSize)
                .AddRow("Potencial de rendimiento", detail.YieldPotential)
                .AddRow("Tiempo de cosecha", detail.HarvestTime ?? "-")
                .AddRow("Tiempo de maduración", detail.MaturationTime ?? "-")
                .AddRow("Requerimiento nutricional", detail.NutritionalRequirement ?? "-")
                .AddRow("Resistencia a roya", detail.RustResistance)
                .AddRow("Resistencia a antracnosis", detail.AnthracnoseResistance)
                .AddRow("Resistencia a nematodos", detail.NematodesResistance)
                .AddRow("Altitud mínima", $"{detail.MinAltitude} msnm")
                .AddRow("Altitud máxima", $"{detail.MaxAltitude} msnm")
                .AddRow("Calidad de altitud", detail.AltitudeQualityLabel)
                .AddRow("Densidad de siembra", 
                    detail.PlantingDensityValue.HasValue && !string.IsNullOrEmpty(detail.PlantingDensityUnit)
                        ? $"{detail.PlantingDensityValue} {detail.PlantingDensityUnit}"
                        : "-")
                .AddRow("Unidad de altitud", detail.AltitudeUnit ?? "-")
                .AddRow("Imagen", detail.ImageUrl ?? "-"));
                AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    

        
    }

}