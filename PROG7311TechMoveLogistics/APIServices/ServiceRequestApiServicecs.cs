using PROG7311TechMoveLogistics.Models;

namespace PROG7311TechMoveLogistics.APIServices
{
    public class ServiceRequestApiService  : IServiceRequestApiService
    {
        private readonly HttpClient _httpClient;

        public ServiceRequestApiService( HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ServiceRequest>> GetAllAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<List<ServiceRequest>>
                    ("api/servicerequests")
                ?? new List<ServiceRequest>();
        }

        public async Task<ServiceRequest?> GetByIdAsync(int id)
        {
            return await _httpClient
                .GetFromJsonAsync<ServiceRequest>
                    ($"api/servicerequests/{id}");
        }

        public async Task CreateAsync( ServiceRequest serviceRequest)
        {
            await _httpClient.PostAsJsonAsync(
                "api/servicerequests",
                serviceRequest);
        }

        public async Task UpdateAsync( ServiceRequest serviceRequest)
        {
            await _httpClient.PutAsJsonAsync(
                $"api/servicerequests/{serviceRequest.ServiceRequestId}",
                serviceRequest);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync(
                $"api/servicerequests/{id}");
        }
    }
}