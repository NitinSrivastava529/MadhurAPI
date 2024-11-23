using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]
    [Keyless]
    public class TotalDistributorWiseDTO
    {
        public string DistributorId { get; set; }
        public int total { get; set; }
    }
}
