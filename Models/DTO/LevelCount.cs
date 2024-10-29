using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]
    [Keyless]
    public class LevelCount
    {
        public int? Level { get; set; }
        public int? Total { get; set; }
    }
}
