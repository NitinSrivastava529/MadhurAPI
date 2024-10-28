using Microsoft.EntityFrameworkCore;

namespace MadhurAPI.Models.DTO
{
    [Keyless]
    public class LevelCount
    {
        public int? Level { get; set; }
        public int? Total { get; set; }
    }
}
