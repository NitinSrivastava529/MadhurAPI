using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [Keyless]
    public class LevelWiseMemberDTO
    {
        public string? MemberId { get; set; }
        public string? MemberName { get; set; }
        public string? MobileNo { get; set; }
        public string? City { get; set; }
        public string? Reward { get; set; }
    }
}
