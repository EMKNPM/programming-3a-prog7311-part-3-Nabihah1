using PROG7311TechMoveLogistics.Models;
using TechMoveLogisticsAPI.DTOs;

namespace PROG7311TechMoveLogistics.APIServices
{
    public class ContractApiService : IContractApiService
    {
        private readonly HttpClient _httpClient;

        public ContractApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ContractDto>> GetAllContractsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ContractDto>>("api/contracts") ?? new List<ContractDto>();
        }

        public async Task<List<ContractDto>> GetAllContractsAsync(string? status, DateTime? startDate, DateTime? endDate)
        {
            var query = new List<string>();

            if (!string.IsNullOrWhiteSpace(status))
                query.Add($"status={status}");

            if (startDate.HasValue)
                query.Add($"startDate={startDate:yyyy-MM-dd}");

            if (endDate.HasValue)
                query.Add($"endDate={endDate:yyyy-MM-dd}");

            var url = "api/contracts";

            if (query.Count > 0)
                url += "?" + string.Join("&", query);

            Console.WriteLine("API URL: " + url);

            return await _httpClient.GetFromJsonAsync<List<ContractDto>>(url)
                   ?? new List<ContractDto>();
        }


        public async Task<ContractDto?> GetContractByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ContractDto>($"api/contracts/{id}");
        }

        public async Task<string?> CreateContractAsync(CreateContractDto contract)
        {
            var form = new MultipartFormDataContent();

            form.Add(
                new StringContent(contract.ClientId.ToString()),
                nameof(contract.ClientId));

            form.Add(
                new StringContent(contract.ContractStartDate.ToString("yyyy-MM-dd")),
                nameof(contract.ContractStartDate));

            form.Add(
                new StringContent(contract.ContractEndDate.ToString("yyyy-MM-dd")),
                nameof(contract.ContractEndDate));

            form.Add(
                new StringContent(((int)contract.ContractStatus).ToString()),
                nameof(contract.ContractStatus));

            form.Add(
                new StringContent(contract.ContractServiceLevel),
                nameof(contract.ContractServiceLevel));

            if (contract.SignedDocument != null)
            {
                var streamContent =
                    new StreamContent(contract.SignedDocument.OpenReadStream());

                form.Add(
                    streamContent,
                    nameof(contract.SignedDocument),
                    contract.SignedDocument.FileName);
            }

            var response = await _httpClient.PostAsync("api/contracts", form);

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine("STATUS: " + response.StatusCode);
            Console.WriteLine("BODY: " + content);

            if (!response.IsSuccessStatusCode)
                return content;

            return null;
        }

        public async Task UpdateContractAsync(ContractDto contract)
        {
            await _httpClient.PutAsJsonAsync($"api/contracts/{contract.ContractId}", contract);
        }

        public async Task DeleteContractAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/contracts/{id}");
        }
    }
}