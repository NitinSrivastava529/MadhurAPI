using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models.DTO
{
    [NotMapped]
    [Keyless]
    public class TotalRepurchaseDTO
    {
        public int Total { get; set; }
        public int Today { get; set; }
        public List<RepurchaseList> list { get; set; }        
    }
    public class RepurchaseList()
    {
        public string RepurchaseId { get; set; }
        public string RepurchaseName { get; set; }
        public string RefId { get; set; }
    }
}
