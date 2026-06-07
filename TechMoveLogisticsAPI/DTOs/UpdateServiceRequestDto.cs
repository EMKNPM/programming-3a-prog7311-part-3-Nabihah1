using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.DTOs
{
    public class UpdateServiceRequestDto
    {

        public int ServiceRequestId { get; set; }

        public string SrDescription { get; set; }  = string.Empty;

        public double CostForeign { get; set; }

        public ServiceRequestStatus SrStatus { get; set; }

        public int ContractId { get; set; }

    }
}
