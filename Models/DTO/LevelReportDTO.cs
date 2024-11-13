using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]
    [Keyless]
    public class LevelReportDTO
    {
        public int level { get; set; }
        public int totalCount { get; set; }
    }
}
