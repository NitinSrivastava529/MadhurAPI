using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]
    [Keyless]
    public class TotalKeyDTO
    {
        public int member { get; set; }
        public int purchase { get; set; }
        public int reward { get; set; }
        public int total { get; set; }
        public TotalKeyDTO()
        {
            total = member + purchase + reward;
        }
    }
}
