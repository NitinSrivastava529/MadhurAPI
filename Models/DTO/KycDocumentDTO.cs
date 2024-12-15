namespace MadhurAPI.Models.DTO
{
    public class KycDocumentDTO
    {
        public string memberId { get; set; }
        public IFormFile file { get; set; }
        public string type { get; set; }
    }
}
