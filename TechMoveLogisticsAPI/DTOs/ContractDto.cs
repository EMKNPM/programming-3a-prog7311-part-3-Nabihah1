using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.DTOs
{
    public class ContractDto
    {
        public int ContractId { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }

        public ContractStatusDto ContractStatus { get; set; }

        public string ContractServiceLevel { get; set; }   = string.Empty;

        public int ClientId { get; set; }

        public string ClientName { get; set; }  = string.Empty;

        public List<ServiceRequestDto> ServiceRequests { get; set; } =  new();

        public List<DocumentDto> Documents { get; set; } =new();

    }
}
