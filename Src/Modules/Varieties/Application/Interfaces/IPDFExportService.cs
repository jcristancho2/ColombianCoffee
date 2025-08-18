using ColombianCoffee.Src.Modules.Varieties.Domain.Entities;
using FluentResults;

namespace ColombianCoffee.Src.Modules.Varieties.Application.Interfaces
{
    public interface IPDFExportService
    {
        Task<Result<string>> ExportVariedadAsync(Variety variedad, string directoryPath);
    }
}