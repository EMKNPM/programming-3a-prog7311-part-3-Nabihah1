using System.ComponentModel.DataAnnotations;

namespace PROG7311TechMoveLogistics.Models
{
    public class ContractFormViewModel
    {
        // info not stored in db 
        //it just receives input and handles file upload 

        public int ClientId { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }

        public ContractStatus ContractStatus { get; set; }

       // public TechMoveLogisticsAPI.Models.ContractStatus ContractStatus { get; set; }

        [Required]
        public string ContractServiceLevel { get; set; } = string.Empty;
        public IFormFile SignedDocument { get; set; }


    }
}
