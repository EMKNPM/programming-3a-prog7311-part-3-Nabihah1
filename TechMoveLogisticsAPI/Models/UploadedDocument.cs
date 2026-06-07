using System.ComponentModel.DataAnnotations;

namespace TechMoveLogisticsAPI.Models
{
    public class UploadedDocument
    {

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedDate { get; set; }
        public bool IsEncrypted { get; set; } = true;

        public string FileUrl { get; set; } = string.Empty;

        //FOREIGN KEY 
        public int ContractId { get; set; }

        //nav property 
        public Contract? Contract { get; set; }

    }
}
