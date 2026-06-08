using Microsoft.AspNetCore.Mvc;
using TechMoveLogisticsAPI.Models;
using TechMoveLogisticsAPI.Services;
using TechMoveLogisticsAPI.DTOs;

namespace TechMoveLogisticsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientsController(IClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDTO>>> GetClients()
        {
            var clients = await _service.GetAllClientsAsync();

            var result = clients.Select(c => new ClientDTO
            {
                ClientId = c.ClientId,
                ClientName = c.ClientName,
                ContactDetails = c.ContactDetails,
                Region = c.Region,

                Contracts = c.Contracts.Select(ct => new ContractSummaryDto
                {
                    ContractId = ct.ContractId,
                    ContractStartDate = ct.ContractStartDate,
                    ContractEndDate = ct.ContractEndDate,
                    ContractStatus = ct.ContractStatus,
                    ContractServiceLevel = ct.ContractServiceLevel
                }).ToList()
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetClient(int id)
        {
            var client = await _service.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            var dto = new ClientDTO
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                ContactDetails = client.ContactDetails,
                Region = client.Region
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateClient(CreateClientDTO dto)
        {
            var client = new Client
            {
                ClientName = dto.ClientName,
                ContactDetails = dto.ContactDetails,
                Region = dto.Region
            };

            await _service.CreateClientAsync(client);

            return CreatedAtAction(
                nameof(GetClient),
                new { id = client.ClientId },
                client);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(int id,  UpdateClientDto dto)
        {
            if (id != dto.ClientId)
            {
                return BadRequest();
            }

            var client = new Client
            {
                ClientId = dto.ClientId,
                ClientName = dto.ClientName,
                ContactDetails = dto.ContactDetails,
                Region = dto.Region
            };

            await _service.UpdateClientAsync(client);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            await _service.DeleteClientAsync(id);

            return NoContent();
        }
    }
}