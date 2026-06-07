using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClientsAsync();

        Task<Client?> GetClientByIdAsync(int id);

        Task CreateClientAsync(Client client);

        Task UpdateClientAsync(Client client);

        Task DeleteClientAsync(int id);

    }
}
