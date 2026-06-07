using System.Net;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using TechMoveLogisticsAPI;
using TechMoveLogisticsAPI.DTOs;
using TechMoveLogisticsAPI.Models;
using Xunit;

namespace PROG7311TechMoveLogistics.Test
{
    public class IntegrationTest :
        IClassFixture<CustomWebApplicationFactory<TechMoveLogisticsAPI.Program>>
    {
        private readonly HttpClient _client;

        public IntegrationTest( CustomWebApplicationFactory<TechMoveLogisticsAPI.Program> factory)
        {
            _client = factory.CreateClient();
        }

        // TEST 1: GET /api/contracts
        // Verifies endpoint responds successfully
        [Fact]
        public async Task GetContracts_Returns200AndData()
        {
            // Act
            var response =  await _client.GetAsync("/api/contracts");

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var contracts = await response.Content.ReadFromJsonAsync<List<ContractDto>>();

            Assert.NotNull(contracts);
        }

        // TEST 2: CREATE THEN READ
        // Verifies data integrity
        [Fact]
        public async Task CreateContract_ThenRetrieve_ReturnsData()
        {
            // Arrange
            var newContract = new CreateContractDto
            {
                ClientId = 1,
                ContractStartDate = DateTime.UtcNow,
                ContractEndDate = DateTime.UtcNow.AddDays(10),
                ContractStatus = ContractStatus.Active,
                ContractServiceLevel = "Premium"
            };

            // Act - Create           
            var createResponse =
                await _client.PostAsJsonAsync("/api/contracts", newContract);

            // Show the actual API response
            var error =
                await createResponse.Content.ReadAsStringAsync();

            Console.WriteLine(error);

            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode); ;

            // Act - Retrieve
            var getResponse = await _client.GetAsync("/api/contracts");

            // Assert GET
            Assert.Equal(HttpStatusCode.OK,getResponse.StatusCode);

            var contracts = await getResponse.Content.ReadFromJsonAsync<List<ContractDto>>();

            Assert.NotNull(contracts);

            Assert.Contains(contracts!, c => c.ContractServiceLevel == "Premium");
        }
    }
}