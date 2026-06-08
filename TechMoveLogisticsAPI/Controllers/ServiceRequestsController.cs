using Microsoft.AspNetCore.Mvc;
using TechMoveLogisticsAPI.Models;
using TechMoveLogisticsAPI.Repositories;
using TechMoveLogisticsAPI.Services;
using TechMoveLogisticsAPI.DTOs;

namespace TechMoveLogisticsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestService _service;
        private readonly IServiceRequestRepo _repository;

        public ServiceRequestsController(
            IServiceRequestService service,
            IServiceRequestRepo repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceRequestDto>>> GetRequests()
        {
            var requests =  await _repository.GetAllAsync();

            var result = requests.Select(r => new ServiceRequestDto
            {
                ServiceRequestId = r.ServiceRequestId,
                SrDescription = r.SrDescription,
                CostForeign = r.CostForeign,
                CostZAR = r.CostZAR,
                SrStatus = r.SrStatus,
                ContractId = r.ContractId,

                Contract = r.Contract == null ? null : new ContractSummaryDto
                {
                    ContractId = r.Contract.ContractId,
                    ContractStartDate = r.Contract.ContractStartDate,
                    ContractEndDate = r.Contract.ContractEndDate,
                    ContractStatus = r.Contract.ContractStatus,
                    ContractServiceLevel = r.Contract.ContractServiceLevel
                }
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequestDto>> GetRequest(
            int id)
        {
            var request =
                await _repository.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            var dto = new ServiceRequestDto
            {
                ServiceRequestId = request.ServiceRequestId,
                SrDescription = request.SrDescription,
                CostForeign = request.CostForeign,
                CostZAR = request.CostZAR,
                SrStatus = request.SrStatus,
                ContractId = request.ContractId
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRequest( CreateServiceRequestDto dto)
        {
            var request = new ServiceRequest
            {
                SrDescription = dto.SrDescription,
                CostForeign = dto.CostForeign,
                SrStatus = dto.SrStatus,
                ContractId = dto.ContractId
            };

            await _service.CreateServiceRequestAsync(
                request);

            return CreatedAtAction(
                nameof(GetRequest),
                new { id = request.ServiceRequestId },
                request);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRequest(int id, UpdateServiceRequestDto dto)
        {
            if (id != dto.ServiceRequestId)
            {
                return BadRequest();
            }

            var request = new ServiceRequest
            {
                ServiceRequestId = dto.ServiceRequestId,
                SrDescription = dto.SrDescription,
                CostForeign = dto.CostForeign,
                SrStatus = dto.SrStatus,
                ContractId = dto.ContractId
            };

            await _service.EditServiceRequestAsync(
                request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRequest(
            int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}