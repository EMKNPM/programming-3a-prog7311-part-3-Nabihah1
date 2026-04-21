using PROG7311TechMoveLogistics.Models; 

namespace PROG7311TechMoveLogistics.Services
{
    public interface IContractService
    {

        Task<List<Contract>> GetAllContractsAsync(string? status, DateTime? startDate, DateTime? endDate);
        Task<Contract?> GetContractDetailsAsync(int id);
        Task CreateContractAsync(ContractFormViewModel viewModel);
        Task<Contract?> GetContractByIdAsync(int id);
        Task UpdateContractAsync(Contract contract);
        Task DeleteContractAsync(int id);


    }
}
