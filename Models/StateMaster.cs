using System.ComponentModel.DataAnnotations;

namespace MadhurAPI.Models
{
    public class StateMaster
    {
        [Key]
        public int RecId { get; set; }
        public int country_id { get; set; }
        public string state_code { get; set; }
        public string state_name { get; set; }
    }
}
