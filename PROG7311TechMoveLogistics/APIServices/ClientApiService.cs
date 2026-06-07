using PROG7311TechMoveLogistics.Models;
using System.Net.Http.Json;

namespace PROG7311TechMoveLogistics.APIServices
{

    public class ClientApiService : IClientAPIService
    {
        private readonly HttpClient _httpClient;

        public ClientApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Client>>
                ("api/clients")
                   ?? new List<Client>();
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Client>
                ($"api/clients/{id}");
        }

        public async Task CreateClientAsync(Client client)
        {
            await _httpClient.PostAsJsonAsync(
                "api/clients",
                client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            await _httpClient.PutAsJsonAsync(
                $"api/clients/{client.ClientId}",
                client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _httpClient.DeleteAsync(
                $"api/clients/{id}");
        }
    }
}