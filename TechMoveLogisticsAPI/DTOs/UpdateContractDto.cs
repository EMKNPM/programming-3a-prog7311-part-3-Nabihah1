//using TechMoveLogisticsAPI.Models;
using TechMoveLogisticsAPI.Services;

namespace TechMoveLogisticsAPI.DTOs
{
    public class UpdateContractDto
    {


        public int ContractId { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }

        public ContractStatusDto ContractStatus { get; set; }

        public string ContractServiceLevel { get; set; } = string.Empty;

        public int ClientId { get; set; }

    }
}
