using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Repositories
{
    public interface IContractRepo
    {
        Task<List<Contract>> GetAllAsync(
        string? status,
        DateTime? startDate,
        DateTime? endDate);

        Task<Contract?> GetByIdAsync(int id);

        Task AddAsync(Contract contract);

        Task UpdateAsync(Contract contract);

        Task DeleteAsync(int id);

    }
}
