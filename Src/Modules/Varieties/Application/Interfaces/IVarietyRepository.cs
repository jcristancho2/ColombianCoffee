using ColombianCoffee.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.src.Modules.Varieties.Application.Interfaces
{
    public interface IVarietyRepository
    {
        IQueryable<Variety> Query();

        Task<Variety?> GetByIdAsync(uint id);
    }
}