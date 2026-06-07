using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.DTOs
{
    public class CreateServiceRequestDto
    {
        public string SrDescription { get; set; }  = string.Empty;

        public double CostForeign { get; set; }

        public ServiceRequestStatus SrStatus { get; set; }

        public int ContractId { get; set; }

    }
}
