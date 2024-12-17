using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint", Order = 0)]
        public int AutoId { get; set; }

        [Column(TypeName = "varchar(10)", Order = 1)]
        public string? RegPin { get; set; }

        [Column(TypeName = "varchar(20)", Order = 2)]
        public string? RefId { get; set; }

        [Column(TypeName = "varchar(20)", Order = 3)]
        public string? MemberId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? MemberName { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? Password { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? MobileNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dob { get; set; }

        [Column(TypeName = "varchar(14)")]
        public string? AadharNo { get; set; }

        [Column(TypeName = "varchar(400)")]
        public string? Address { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? State { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? City { get; set; }

        [Column(TypeName = "varchar(6)")]
        public string? PinCode { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? Nominee { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? RelationWithNominee { get; set; }

        [Column(TypeName = "char(1)")]
        public char? IsActive { get; set; } = 'Y';
        [Column(TypeName = "varchar(20)")]
        public string? MemberType { get; set; } = "Member";

        [Column(TypeName = "datetime")]
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;
    }
}
