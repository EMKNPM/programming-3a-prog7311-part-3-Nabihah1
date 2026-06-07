using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.DTOs
{
    public class ContractSummaryDto
    {

        public int ContractId { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }

        public ContractStatus ContractStatus { get; set; }

        public string ContractServiceLevel { get; set; }    = string.Empty;
    }
}
