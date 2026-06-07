using TechMoveLogisticsAPI.DTOs;

namespace PROG7311TechMoveLogistics.APIServices
{
    public interface IContractApiService
    {
        Task<List<ContractDto>> GetAllContractsAsync();

        Task<List<ContractDto>> GetAllContractsAsync(string? status,DateTime? startDate,DateTime? endDate
        );

        Task<ContractDto?> GetContractByIdAsync(int id);

        Task<string?> CreateContractAsync(CreateContractDto contract);

        Task UpdateContractAsync(ContractDto contract);

        Task DeleteContractAsync(int id);
    }
}