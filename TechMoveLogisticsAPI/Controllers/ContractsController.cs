using Microsoft.AspNetCore.Mvc;
using TechMoveLogisticsAPI.DTOs;
using TechMoveLogisticsAPI.Services;
using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _service;

        public ContractsController(IContractService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContractDto>>> GetContracts(string? status, DateTime? startDate,  DateTime? endDate)
        {
            var contracts = await _service.GetAllContractsAsync(status,  startDate,  endDate);

            var result = contracts.Select(c => new ContractDto
            {
                ContractId = c.ContractId,
                ContractStartDate = c.ContractStartDate,
                ContractEndDate = c.ContractEndDate,
                ContractStatus = (ContractStatusDto)c.ContractStatus,
                ContractServiceLevel = c.ContractServiceLevel,
                ClientId = c.ClientId,
                ClientName = c.Client?.ClientName ?? ""
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContractDto>> GetContract(int id)
        {
            var contract = await _service.GetContractByIdAsync(id);

            if (contract == null)
            {
                return NotFound();
            }

            var dto = new ContractDto
            {
                ContractId = contract.ContractId,
                ContractStartDate = contract.ContractStartDate,
                ContractEndDate = contract.ContractEndDate,
                ContractStatus = (ContractStatusDto)contract.ContractStatus,
                ContractServiceLevel = contract.ContractServiceLevel,
                ClientId = contract.ClientId,
                ClientName = contract.Client?.ClientName ?? ""
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateContract(CreateContractDto dto)
        {
            Console.WriteLine($"START: {dto.ContractStartDate}");
            Console.WriteLine($"END: {dto.ContractEndDate}");

            //if (dto.ContractEndDate.Date >= dto.ContractStartDate.Date)
            //{
            //    return BadRequest("End date must be after start date.");
            //}


            //if (string.IsNullOrWhiteSpace(dto.ContractServiceLevel))
            //{
            //    return BadRequest("Service level is required.");
            //}

            var id = await _service.CreateContractAsync(dto);

            return CreatedAtAction(nameof(GetContract), new { id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContractDto>> UpdateContract(int id, UpdateContractDto dto)
        {
            if (id != dto.ContractId)
            {
                return BadRequest("ID mismatch.");
            }

            // if (dto.ContractEndDate.Date >= dto.ContractStartDate.Date)
            //{
            //    return BadRequest("End date must be after start date.");
            //}

            var contract = new Contract
            {
                ContractId = dto.ContractId,
                ContractStartDate = dto.ContractStartDate,
                ContractEndDate = dto.ContractEndDate,
                ContractStatus = (TechMoveLogisticsAPI.Models.ContractStatus) dto.ContractStatus,
                ContractServiceLevel = dto.ContractServiceLevel,
                ClientId = dto.ClientId
            };

            await _service.UpdateContractAsync(contract);

            // return updated result (clean DTO response)
            var updatedDto = new ContractDto
            {
                ContractId = contract.ContractId,
                ContractStartDate = contract.ContractStartDate,
                ContractEndDate = contract.ContractEndDate,
                ContractStatus = (ContractStatusDto)contract.ContractStatus,
                ContractServiceLevel = contract.ContractServiceLevel,
                ClientId = contract.ClientId,
                ClientName = "" 
            };

            return Ok(updatedDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContract(int id)
        {
            await _service.DeleteContractAsync(id);

            return NoContent();
        }


        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStatus(int id,[FromBody] ContractStatus status)
        {
            var contract = await _service.GetContractByIdAsync(id);

            if (contract == null)
            {
                return NotFound();
            }

            contract.ContractStatus = status;

            await _service.UpdateContractAsync(contract);

            return NoContent();
        }


    }
}