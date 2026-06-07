using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Repositories
{
    public interface IServiceRequestRepo
    {

        Task<List<ServiceRequest>> GetAllAsync();

        Task<ServiceRequest?> GetByIdAsync(int id);

        Task AddAsync(ServiceRequest request);

        Task UpdateAsync(ServiceRequest request);

        Task DeleteAsync(int id);
    }
}
