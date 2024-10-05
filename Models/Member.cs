namespace MadhurAPI.Models
{
    public class Member
    {
        public int AutoId { get; set; }
        public string PinNo { get; set; }
        public string RefId { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public DateOnly? dob { get; set; }     
        public string AadharNo { get; set; } 
        public string Address { get; set; } 
        public string State { get; set; } 
        public string City { get; set; } 
        public string PinCode { get; set; } 
        public string NomineeName { get; set; } 
        public string RelationWithNominee { get; set; } 
        public string CreationDate { get; set; } 
    }
}
