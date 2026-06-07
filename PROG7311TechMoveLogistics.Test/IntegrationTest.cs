using System.Net;
using System.Net.Http.Json;
using TechMoveLogisticsAPI.DTOs;
using TechMoveLogisticsAPI.Models;
using Xunit;

namespace PROG7311TechMoveLogistics.Test
{
    public class IntegrationTest
    {
        private readonly HttpClient _client;

        public IntegrationTest()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:7001/")
            };
        }

       
        // 1. BASIC API HEALTH CHECK (200 OK)
       [Fact]
        public async Task GetClients_Returns200AndData()
        {
            var response = await _client.GetAsync("api/clients");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var clients =
                await response.Content.ReadFromJsonAsync<List<ClientDTO>>();

            Assert.NotNull(clients);
            Assert.NotEmpty(clients);
        }

      
        // 2. CREATE → READ → VERIFY (CLIENT)
       [Fact]
        public async Task Client_Create_Read_Update_Delete_FullLifecycle()
        {
            // create 
            var createDto = new CreateClientDTO
            {
                ClientName = $"Lifecycle-{Guid.NewGuid()}",
                ContactDetails = "life@test.com",
                Region = "Gauteng"
            };

            var createResponse =
                await _client.PostAsJsonAsync("api/clients", createDto);

            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

            var created =
                await createResponse.Content.ReadFromJsonAsync<Client>();

            Assert.NotNull(created);
            Assert.True(created!.ClientId > 0);

            // read
            var getResponse =
                await _client.GetAsync($"api/clients/{created.ClientId}");

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var retrieved =
                await getResponse.Content.ReadFromJsonAsync<ClientDTO>();

            Assert.NotNull(retrieved);
            Assert.Equal(createDto.ClientName, retrieved!.ClientName);
            Assert.Equal(createDto.ContactDetails, retrieved.ContactDetails);
            Assert.Equal(createDto.Region, retrieved.Region);

            // update 
            var updateDto = new UpdateClientDto
            {
                ClientId = created.ClientId,
                ClientName = createDto.ClientName + "_Updated",
                ContactDetails = "updated@test.com",
                Region = "Western Cape"
            };

            var updateResponse =
                await _client.PutAsJsonAsync($"api/clients/{created.ClientId}", updateDto);

            Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

            var updatedCheck =
                await _client.GetAsync($"api/clients/{created.ClientId}");

            var updated =
                await updatedCheck.Content.ReadFromJsonAsync<ClientDTO>();

            Assert.Equal("Western Cape", updated!.Region);

            // delete
            var deleteResponse =
                await _client.DeleteAsync($"api/clients/{created.ClientId}");

            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            var afterDelete =
                await _client.GetAsync($"api/clients/{created.ClientId}");

            Assert.Equal(HttpStatusCode.NotFound, afterDelete.StatusCode);
        }

       
        // 3. CREATE CONTRACT → VERIFY RELATIONSHIP     
        [Fact]
        public async Task Contract_Create_VerifiesClientRelationship()
        {
            var clients =
                await _client.GetFromJsonAsync<List<ClientDTO>>("api/clients");

            Assert.NotNull(clients);
            Assert.NotEmpty(clients);

            var clientId = clients!.First().ClientId;

            var contractDto = new CreateContractDto
            {
                ClientId = clientId,
                ContractStartDate = DateTime.Today,
                ContractEndDate = DateTime.Today.AddMonths(1),
                ContractStatus = ContractStatusDto.Active,
                ContractServiceLevel = "Premium"
            };

            var createResponse =
                await _client.PostAsJsonAsync("api/contracts", contractDto);

            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

            var contracts =
                await _client.GetFromJsonAsync<List<ContractDto>>("api/contracts");

            Assert.NotNull(contracts);

            var createdContract = contracts!
                .FirstOrDefault(c =>
                    c.ClientId == clientId &&
                    c.ContractServiceLevel == "Premium");

            Assert.NotNull(createdContract);
        }

      
        // 4. NEGATIVE TEST (BONUS MARKS)
       [Fact]
        public async Task GetInvalidClient_Returns404()
        {
            var response = await _client.GetAsync("api/clients/999999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}