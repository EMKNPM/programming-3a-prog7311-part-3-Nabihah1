using System.ComponentModel.DataAnnotations;

namespace TechMoveLogisticsAPI.DTOs
{
    public class CreateClientDTO
    {

        [Required]
        [StringLength(100)]
        public string ClientName { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string ContactDetails { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Region { get; set; } = string.Empty;

    }
}
