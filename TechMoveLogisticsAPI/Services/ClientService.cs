using TechMoveLogisticsAPI.Models;
using TechMoveLogisticsAPI.Repositories;

namespace TechMoveLogisticsAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepo _repository;

        public ClientService(IClientRepo repository)
        {
            _repository = repository;
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateClientAsync(Client client)
        {
            await _repository.AddAsync(client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            await _repository.UpdateAsync(client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}