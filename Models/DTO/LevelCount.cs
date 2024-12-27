using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]
    [Keyless]
    public class LevelCount
    {
        public int level { get; set; }
        public int member { get; set; }
        public int purchase { get; set; }
        public int total { get; set; }
    }
}
