namespace TechMoveLogisticsAPI.DTOs
{
    public class ClientDTO
    {
                public int ClientId { get; set; }

        public string ClientName { get; set; } = string.Empty;

        public string ContactDetails { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public List<ContractSummaryDto> Contracts { get; set; } = new();

    }
}
