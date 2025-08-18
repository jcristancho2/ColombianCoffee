using ColombianCoffee.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.Src.Modules.Varieties.Application.Interfaces
{
    public interface IPDFExportService
    {
        void ExportVariedad(Variety variedad, string filePath);
    }
}