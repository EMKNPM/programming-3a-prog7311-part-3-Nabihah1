using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace PROG7311TechMoveLogistics.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required]
        [StringLength(100)]
        public string ClientName { get; set; } = string.Empty;


        [Required]
        [StringLength(150)]
        public string ContactDetails { get; set; } = string.Empty;


        [Required]
        [StringLength(100)]
        public string Region { get; set; } = string.Empty;


        //navigation property 
        // 1 clinet can have many contracts 
        public List<Contract> Contracts { get; set; } = new List<Contract>();
  
    
    }
}
