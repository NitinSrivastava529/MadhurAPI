using System.ComponentModel.DataAnnotations;

namespace MadhurAPI.Models
{
    public class DistrictMaster
    {
        [Key]
        public int recid { get; set; }
        public int state_code { get; set; }
        public int dist_code { get; set; }
        public string distt_name { get; set; }      
    }
}
