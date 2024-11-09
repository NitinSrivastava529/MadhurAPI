using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]
    [Keyless]
    public class LevelWiseMemberDTO
    {
        public string? MemberId { get; set; }
        public string? MemberName { get; set; }
        public int? levelCount { get; set; }
    }
}
