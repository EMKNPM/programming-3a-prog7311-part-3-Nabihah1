using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Repositories
{
    public interface IClientRepo
    {
        Task<List<Client>> GetAllAsync();

        Task<Client?> GetByIdAsync(int id);

        Task AddAsync(Client client);

        Task UpdateAsync(Client client);

        Task DeleteAsync(int id);

    }
}
