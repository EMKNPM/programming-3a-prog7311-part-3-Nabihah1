using PROG7311TechMoveLogistics.Models;

namespace PROG7311TechMoveLogistics.APIServices
{
    public interface IClientAPIService
    {

            Task<List<Client>> GetAllClientsAsync();

            Task<Client?> GetClientByIdAsync(int id);

            Task CreateClientAsync(Client client);

            Task UpdateClientAsync(Client client);

            Task DeleteClientAsync(int id);
        }
}
