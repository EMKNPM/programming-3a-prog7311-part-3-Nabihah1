using TechMoveLogisticsAPI.Models;
using TechMoveLogisticsAPI.DTOs;

namespace TechMoveLogisticsAPI.DTOs
{
    public class CreateContractDto
    {
        public int ClientId { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }

        public ContractStatusDto ContractStatus { get; set; }

        public string ContractServiceLevel { get; set; } = string.Empty;

        public IFormFile? SignedDocument { get; set; }

    }
}
