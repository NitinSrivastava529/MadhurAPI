using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]
    [Keyless]
    public class RewardDistributorInfoDTO
    {
        public string MemberId { get; set; }
        public string DistributorId { get; set; }
        public string Level { get; set; }
    }
}
