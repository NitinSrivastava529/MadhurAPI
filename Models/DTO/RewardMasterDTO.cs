using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]   
    public class RewardMasterDTO
    {                   
        public int AutoId { get; set; }      
        public string? MemberId { get; set; }      
        public string? level { get; set; }    
        public string? Remark { get; set; }
        [NotMapped]
        public IFormFile?  file_path { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;
    }
}
