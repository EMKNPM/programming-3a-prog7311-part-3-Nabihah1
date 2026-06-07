namespace TechMoveLogisticsAPI.DTOs
{
    public class DocumentDto
    {
        public int Id { get; set; }

        public string FileName { get; set; }  = string.Empty;

        public string FilePath { get; set; }= string.Empty;
        public string FileUrl { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public DateTime UploadedDate { get; set; }

        public bool IsEncrypted { get; set; }

    }
}
