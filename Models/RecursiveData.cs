using Microsoft.EntityFrameworkCore;

namespace MadhurAPI.Models
{
    [Keyless]
    public class RecursiveData
    {
        public string? MemberId { get; set; }
        public string? MemberName { get; set; }
        public string? RefId { get; set; }
        public string? ReferralName { get; set; }
        public string? state { get; set; }
        public string? city { get; set; }   
    }
}
