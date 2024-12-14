using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MadhurAPI.Models.DTO
{
    public class MemberDTO
    {  
        public string? RegPin { get; set; }
        public string? RefId { get; set; }
        public string? RefName { get; set; }
        public string? MemberId { get; set; }
        public string? MemberName { get; set; }       
        public string? MobileNo { get; set; }     
        public string? IsActive { get; set; }     
        public DateTime? CreationDate { get; set; }
    }
}
