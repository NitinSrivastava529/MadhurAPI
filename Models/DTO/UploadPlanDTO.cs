using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [Keyless]
    [NotMapped]
    public class UploadPlanDTO
    {
        public IFormFile? file { get; set; }
        public string type { get; set; }
    }
}
