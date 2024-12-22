using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]
    [Keyless]
    public class AllMemberDTO
    {
        public string? MemberId { get; set; }
        public string? MemberName { get; set; }
        public string? RefId { get; set; }        
        public string? MobileNo { get; set; }
        public string? City { get; set; }    
    }
}
