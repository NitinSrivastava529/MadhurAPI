using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MadhurAPI.Models.DTO
{
    public class MemberDTO
    {
        public int AutoId { get; set; } 
        public string RegPin { get; set; }
        public string RefId { get; set; }
        public string RefName { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public DateTime? dob { get; set; }
        public string AadharNo { get; set; }
        public string Address { get; set; }
        public string State { get; set; }       
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Nominee { get; set; }
        public string RelationWithNominee { get; set; }
        public char? IsActive { get; set; }    
        public DateTime? CreationDate { get; set; }
    }
}
