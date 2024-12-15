using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]
    [Keyless]
    public class RewardStoreTotalDTO
    {
        public string? storeId { get; set; }
        public int? total { get; set; }
    }
}
