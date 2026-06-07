using TechMoveLogisticsAPI.DTOs;
using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Services
{
    public interface IContractService
    {
        Task<List<Contract>> GetAllContractsAsync( string? status, DateTime? startDate, DateTime? endDate);

        Task<Contract?> GetContractByIdAsync(int id);

        Task<int> CreateContractAsync(CreateContractDto dto);

        Task UpdateContractAsync(Contract contract);

        Task DeleteContractAsync(int id);

    }
}
