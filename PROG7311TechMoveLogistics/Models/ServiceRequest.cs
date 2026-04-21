using System.ComponentModel.DataAnnotations;

namespace PROG7311TechMoveLogistics.Models
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }

        [Required]
        [StringLength(200)]
        public string SrDescription { get; set; } = string.Empty;

        // foreign rate entered by user 
        [Range(0.01, 10000000)]
        public double CostForeign { get; set; }

        //converted Rand amount that we get after using the API
        public double CostZAR { get; set; }

        [Required]
        public ServiceRequestStatus SrStatus { get; set; }


        // FOREIGN KEY 
        public int ContractId { get; set; }



        //navigation properties 
        // 1 request belongs to 1 contract 
        public Contract? Contract { get; set; }


    }
}
